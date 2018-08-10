namespace Zigmo.Secucard.Connect.NetCore.Client
{
    public interface IService
    {
        ClientContext Context { get; set; }
        void RegisterEvents();
    }
}