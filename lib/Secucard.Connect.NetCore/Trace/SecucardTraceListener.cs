using System;
using System.Diagnostics;

namespace Zigmo.Secucard.Connect.NetCore.Trace
{
    public class SecucardTraceListener : TextWriterTraceListener
    {
        public SecucardTraceListener(string file)
            : base(file)
        {
        }

        public override void WriteLine(string message)
        {
            Write(string.Format("{0:yyyyMMdd-HHmmss.fff}", DateTime.Now));
            Write(": ");
            base.WriteLine(message);
            Flush();
        }
    }
}