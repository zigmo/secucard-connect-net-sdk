namespace Zigmo.Secucard.Connect.NetCore.Product.Payment
{
    /// <summary>
    /// Holds service references and service type constants for "payment" product.
    /// </summary>
    public class Payment
    {
        public ContainersService Containers { get; set; }

        public ContractService Contracts { get; set; }

        public CustomerPaymentService Customers { get; set; }

        public SecupayDebitsService Secupaydebits { get; set; }

        public SecupayPrepaysService Secupayprepays { get; set; }

        public SecupayCreditcardsService Secupaycreditcards { get; set; }

        public SecupayInvoicesService Secupayinvoices { get; set; }

        public PaymentTransactionsService PaymentTransactions { get; set; }
    }
}