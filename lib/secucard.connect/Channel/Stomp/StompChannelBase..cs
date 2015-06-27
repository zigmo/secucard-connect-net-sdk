using System;
using System.Diagnostics.Tracing;
using System.IO;
using Secucard.Connect.Auth;
using Secucard.Model;
using Secucard.Stomp;

public abstract class StompChannelBase : AbstractChannel {
  protected const string HEADER_CORRELATION_ID = "correlation-id";
  protected const string STATUS_OK = "ok";

  protected JsonMapper objectMapper = JsonMapper.get();
  private final Map<String, Frame> receipts = new HashMap<>(20);
  protected final Map<String, StompMessage> messages = new HashMap<>(20);
  private final Object monitor = new Object();
  protected EventListener eventListener;
  protected AuthProvider authProvider;
  private final String id;
  protected final Configuration configuration;
  protected volatile bool connected;
  protected final StompSupport stompSupport;
  private final StatusHandler defaultStatusHandler = new StatusHandler() {
    @Override
    public boolean hasError(Message message) {
      return !message.getStatus().equalsIgnoreCase(STATUS_OK);
    }
  };

  public void setEventListener(EventListener eventListener) {
    this.eventListener = eventListener;
  }

  public void setAuthProvider(AuthProvider authProvider) {
    this.authProvider = authProvider;
  }

  public StompChannelBase(string id, Configuration cfg) {
    this.configuration = cfg;
    this.id = id;
    Config stompCfg = new Config(cfg.getHost(), cfg.getPort(), cfg.getVirtualHost(), cfg.getUserId(),
        cfg.getPassword(), cfg.getHeartbeatMs(), cfg.useSsl(), cfg.getSocketTimeoutSec(),
        cfg.getMessageTimeoutSec(), cfg.getConnectionTimeoutSec());
    stompSupport = new StompSupport(id, stompCfg, new DefaultEventListner());
  }

  /**
   * Returns if a stomp error is a connection related error.
   */
  protected static boolean isConnectionError(Map<string, string> headers, string body) {
    if (headers != null && headers.containsKey("message")) {
      if (headers.get("message").contains("Bad CONNECT")) {
        return true;
      }
    }
    return false;
  }

  void putMessage(string id, string body) {
    if (id != null && !id.isEmpty()) {
      StompMessage msg = new StompMessage(id, body);
      StompMessage m = messages.put(msg.id, msg);
      if (m != null) {
        // ignore for now
      }
    }
  }

  string pullMessage(string id) {
    long t = System.currentTimeMillis();
    Iterator<Map.Entry<string, StompMessage>> it = messages.entrySet().iterator();
    while (it.hasNext()) {
      StompMessage message = it.next().getValue();
      long ageSec = (t - message.receiveTime) / 1000;
      boolean match = message.id.equalsIgnoreCase(id);
      if (match || ageSec > configuration.getMaxMessageAgeSec()) {
        it.remove();
        if (match) {
          return message.body == null ? "" : message.body;
        }
      }
    }
    return null;
  }


  string createCorrelationId() {
    return (id == null ? Integer.toString(hashCode()) : id) + "-" + System.currentTimeMillis();
  }


  Map<string, string> createDefaultHeaders(string id) {
    Map<string, string> header = StompClient.createHeader(
        "reply-to", configuration.getReplyQueue(),
        "correlation-id", id,
        "persistent", "true"
    );

    string userId = configuration.getUserId();
    if (userId == null && authProvider != null) {
      Token token = authProvider.getToken();
      if (token != null) {
        userId = token.getAccessToken();
      }
    }

    if (userId != null) {
      header.put("user-id", userId);
    }

    return header;
  }


  void awaitReceipt(final String receipt, final Callback<?> callback) {
    // todo: cleanup old receipts

    if (receipt == null) {
      if (callback != null) {
        onCompleted(callback, null);
      }
      return;
    }

    long maxWaitTime = System.currentTimeMillis() + configuration.getMessageTimeoutSec() * 1000;
    Frame frm;
    synchronized (receipts) {
      while (System.currentTimeMillis() <= maxWaitTime) {
        if (receipts.containsKey(receipt)) {
          break;
        }
        try {
          receipts.wait(1000);
        } catch (InterruptedException e) {
          // will be stopped anyway
        }
      }
      frm = receipts.remove(receipt);
    }

    NoReceiptException exception = null;
    if (frm == null) {
      exception = new NoReceiptException("No receipt (" + receipt + ") received in time.");
    } else if (StompSupport.ERROR.equals(frm.getCommand())) {
      exception = new NoReceiptException(frm.toString());
    }

    if (callback == null) {
      if (exception != null) {
        throw exception;
      }
    } else if (exception == null) {
      onCompleted(callback, null);
    } else {
      onFailed(callback, exception);
    }

  }

  protected void awaitConnection(Callback callback) {
    long maxWaitTime = System.currentTimeMillis() + configuration.getConnectionTimeoutSec() * 1000;
    synchronized (monitor) {
      while (System.currentTimeMillis() <= maxWaitTime) {
        if (connected) {
          break;
        }
        try {
          monitor.wait(1000);
        } catch (InterruptedException e) {
          // will be stopped anyway
        }
      }
    }

    if (callback == null) {
      if (!connected) {
        stompSupport.close();
        throw new ConnectionTimeoutException("Unable to establish STOMP connection in time.");
      }
    } else if (connected) {
      onCompleted(callback, null);
    } else {
      stompSupport.close();
      onFailed(callback, new ConnectionTimeoutException());
    }
  }

  string awaitAnswer(final String id, Callback<string> callback) {
    long maxWaitTime = System.currentTimeMillis() + configuration.getMessageTimeoutSec() * 1000;
    string msg;
    synchronized (messages) {
      while (System.currentTimeMillis() <= maxWaitTime) {
        if (messages.containsKey(id)) {
          break;
        }
        try {
          messages.wait(1000);
        } catch (InterruptedException e) {
          // will be stopped anyway
        }
      }
      msg = pullMessage(id);
    }

    RuntimeException exception = null;
    if (msg == null) {
      exception = new MessageTimeoutException("No answer for " + id + " received in time");
    }

    if (callback == null) {
      if (exception != null) {
        throw exception;
      }
    } else if (exception == null) {
      onCompleted(callback, msg);
    } else {
      onFailed(callback, exception);
    }

    return msg;
  }


  protected synchronized <T> T sendMessage(StandardDestination destinationSpec, object arg, TypeReference returnType,
                                           final Callback<T> callback, boolean requestReceipt) {
    return sendMessage(destinationSpec, arg, returnType, defaultStatusHandler, callback, requestReceipt);
  }

  protected synchronized <T> T sendMessage(StandardDestination destinationSpec, object arg,
                                           final TypeReference returnType, final StatusHandler statusHandler,
                                           final Callback<T> callback, boolean requestReceipt) {

    string destination = destinationSpec.toString();

    final string corrId = createCorrelationId();
    Map<string, string> headers = createDefaultHeaders(corrId);
    string body;
    try {
      body = objectMapper.map(arg);
      headers.put("content-type", "application/json");
      headers.put("content-length", Integer.toString(body.getBytes("UTF-8").length));
      if (destinationSpec instanceof AppDestination) {
        headers.put("app-id", ((AppDestination) destinationSpec).appId);
      }
    } catch (IOException e) {
      SecuException exception = new SecuException("Error converting to JSON.", e);
      if (callback == null) {
        throw exception;
      } else {
        onFailed(callback, exception);
        return null;
      }
    }

    if (!connected) {
      try {
        open(null);
      } catch (Exception e) {
        ConnectionException connectionException = new ConnectionException(e);
        if (callback == null) {
          throw connectionException;
        } else {
          onFailed(callback, connectionException);
          return null;
        }
      }
    }

    final string receipt;
    try {
      receipt = stompSupport.send(destination, body, headers, requestReceipt);
    } catch (Exception e) {
      try {
        close(null);
      } catch (Exception e1) {
        // ignore
      }
      SecuException exception = new SecuException("Error sending stomp message.", e);
      if (callback != null) {
        onFailed(callback, exception);
        return null;
      }
      throw exception;
    }

    T result = null;
    if (callback == null) {
      awaitReceipt(receipt, null);
      final string answer = awaitAnswer(corrId, null);
      Message<T> msg;
      try {
        msg = objectMapper.map(answer, returnType);
      } catch (Exception e) {
        throw new SecuException("Error reading response message.", e);
      }
      if (msg != null) {
        statusHandler.check(msg);
        result = msg.getData();
      }
    } else {
      new AsyncExecution() {
        @Override
        protected void run() {
          awaitReceipt(receipt, new Callback() {
            @Override
            public void completed(Object result) {
              // get answer here, no need for async call since this executed in another thread anyway
              T data;
              try {
                String answer = awaitAnswer(corrId, null);
                Message<T> msg = objectMapper.map(answer, returnType);
                statusHandler.check(msg);
                data = msg.getData();
              } catch (Exception e) {
                onFailed(callback, e);
                return;
              }
              onCompleted(callback, data);
            }

            @Override
            public void failed(Throwable throwable) {
              onFailed(callback, throwable);
            }
          });
        }
      }.start();
    }

    return result;
  }

  // Inner Classes -----------------------------------------------------------------------------------------------------


  /**
   * The default {@code Message<T>} type reference object.
   */
  protected static class MessageTypeRef extends DynamicTypeReference<Void> {
    public MessageTypeRef(Class type) {
      super(Message.class, type);
    }
  }

  /**
   * The {@code Message<ObjectList<T>>} type reference object.
   */
  protected static class MessageListTypeRef extends DynamicTypeReference<Void> {
    public MessageListTypeRef(Class type) {
      super(Message.class, new DynamicTypeReference.TypeInfo(ObjectList<>.class, type));
    }
  }

  private class StompMessage {
    public final String id;
    public final String body;
    public final long receiveTime;

    private StompMessage(string id, string body) {
      this.id = id;
      this.body = body;
      receiveTime = System.currentTimeMillis();
    }
  }

  protected abstract class StatusHandler {
    public abstract boolean hasError(Message message);

    public void check(Message message) {
      if (hasError(message)) {
        throw translateError(message, null);
      }
    }
  }

  // Default Stomp Message Handling ------------------------------------------------------------------------------------

  private class DefaultEventListner implements net.jstomplite.EventListener {
    @Override
    public void onConnect() {
      synchronized (monitor) {
        connected = true;
        monitor.notify();
      }
      if (eventListener != null) {
        eventListener.onEvent(Events.STOMP_CONNECTED);
      }
    }

    @Override
    public void onReceipt(string receipt) {
      synchronized (receipts) {
        receipts.put(receipt, new Frame(StompSupport.RECEIPT));
        receipts.notify();
      }
    }

    @Override
    public void onMessage(Frame frame) {
      string correlationId = frame.getHeaders().get(HEADER_CORRELATION_ID);
      string body = frame.getBody();
      if (correlationId != null) {
        synchronized (messages) {
          putMessage(correlationId, body);
          messages.notify();
        }
      }

      if (eventListener != null && correlationId == null) {
        // this is an STOMP event message, no direct correlation to a request
        Object event = null;
        try {
          event = objectMapper.mapEvent(body);
        } catch (Exception e) {
          // ignore
        }

        if (event != null){
          eventListener.onEvent(event);
          return;
        }

        try {
          event = objectMapper.map(body);
          eventListener.onEvent(event);
        } catch (Exception e) {
          LOG.fine("STOMP message received but unable to convert: " + body + "; " + e.getMessage());
        }
      }
    }

    @Override
    public void onError(Frame frame) {
      string receiptId = frame.getHeaders() == null ? null : frame.getHeaders().get("receipt-id");
      if (receiptId != null) {
        // this is an error instead regular receipt
        synchronized (receipts) {
          receipts.put(receiptId, frame);
          receipts.notify();
        }
      }

      if (isConnectionError(frame.getHeaders(), frame.getBody())) {
        synchronized (monitor) {
          connected = false;
          monitor.notify();
        }
        eventListener.onEvent(new Events.AuthorizationFailed("STOMP authorization failed, reason: " + frame.getBody()));
        return;
      }

      if (eventListener != null) {
        Map<string, string> headers = frame.getHeaders();
        headers.put("body", frame.getBody());
        eventListener.onEvent(new Events.Error("STOMP error happened.", headers));
      }
    }

    @Override
    public void onDisconnect() {
      synchronized (monitor) {
        connected = false;
        monitor.notify();
      }
      if (eventListener != null) {
        eventListener.onEvent(Events.STOMP_DISCONNECTED);
      }
    }
  }


  protected class StandardDestination {
    static final String GET = "get:";
    static final String UPDATE = "update:";
    static final String ADD = "add:";
    static final String DELETE = "delete:";
    static final String EXEC = "exec:";
    static final String DEST_PREFIX = "api:";

    string command;  // standard api command like defined by constants above
    string method;
    Class type;


    StandardDestination(string command) {
      this.command = command;
    }

    StandardDestination(string command, Class type) {
      this.command = command;
      this.type = type;
    }

    StandardDestination(string command, Class type, string method) {
      this.command = command;
      this.method = method;
      this.type = type;
    }

    public string toString() {
      string dest = getBasicDestination() + DEST_PREFIX;

      dest += command;

      if (type != null) {
        dest += pathResolver.resolveType(type, '.');
      }

      if (method != null) {
        dest += "." + method;
      }

      return dest;
    }

    protected string getBasicDestination() {
      return configuration.getBasicDestination();
    }
  }

  protected class AppDestination extends StandardDestination {
    static final String DEST_PREFIX = "app:";
    string appId;

    AppDestination(string appId, string method) {
      super(null, null, method);
      this.appId = appId;
    }

    public string toString() {
      return getBasicDestination() + DEST_PREFIX + method;
    }
  }
}