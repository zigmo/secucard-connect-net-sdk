 using System;

namespace Zigmo.Secucard.Connect.NetCore.Product.Common.Model
{
    public class ProductException : Exception
    {
        public Status Status { get; set; }

        public ProductException(string message) : base(message)
        {
            
        }
    }
}
