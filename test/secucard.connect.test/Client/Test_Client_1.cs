﻿namespace secucard.connect.test.Rest
{
    using System;
    using System.Linq;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Secucard.Connect;
    using Secucard.Connect.Product.General;
    using Secucard.Connect.Rest;
    using Secucard.Connect.Test;

    [TestClass]
    [DeploymentItem("Data", "Data")]
    public class Test_Client_1 : Test_Client_Base
    {
        [TestMethod, TestCategory("Rest")]
        public void Test_Payment_Customers_1_GET()
        {
            SecucardConnect client = SecucardConnect.Create("id", ClientConfiguration);
            client.Connect();

            var service = client.Getservice<GeneralSkeltonsService>();

        }


    }
}