using System;

namespace Zigmo.Secucard.Connect.NetCore.Storage
{
    [Serializable]
    public class StorageItem
    {
        public long? Ticks { get; set; }
        public string Type { get; set; }
        public object Value { get; set; }
    }
}