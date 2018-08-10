using Zigmo.Secucard.Connect.NetCore.Client;
using Zigmo.Secucard.Connect.NetCore.Product.Loyalty.Model;

namespace Zigmo.Secucard.Connect.NetCore.Product.Loyalty
{
    public class CardGroupsService : ProductService<CardGroup>
    {
        public static readonly ServiceMetaData<CardGroup> MetaData = new ServiceMetaData<CardGroup>(
            "loyalty", "cardgroups");

        /// <summary>
        /// Check if a passcode is enabled for this card
        /// </summary>
        /// <param name="cardGroupId">CRG_XYZ</param>
        /// <param name="transactionType">Type of transaction</param>
        /// <param name="cardNumber">Number of the card</param>
        /// <returns></returns>
        public bool CheckPasscodeEnabled(string cardGroupId, string transactionType, string cardNumber)
        {
            var data = new { action = transactionType, cardnumber = cardNumber };
            return this.ExecuteToBool(cardGroupId, "checkPasscodeEnabled", null, data, null);
        }

        protected override ServiceMetaData<CardGroup> GetMetaData()
        {
            return MetaData;
        }
    }
}
