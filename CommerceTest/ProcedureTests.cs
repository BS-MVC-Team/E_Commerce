using Microsoft.VisualStudio.TestTools.UnitTesting;
using Procedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procedure.Tests
{
    [TestClass()]
    public class ProcedureTests
    {
        [TestMethod()]
        public void GetBuyerOrderTest()
        {
            var procrdure = new Procedure();
            var getBuyerOrder = procrdure.GetBuyerOrder("1");
            Assert.IsTrue(getBuyerOrder.Count() != 0);
        }
    }
}