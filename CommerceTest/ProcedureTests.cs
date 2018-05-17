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
            var procedure = new Procedure();
            var getBuyerOrder = procedure.GetBuyerOrder("1");
            Assert.IsTrue(getBuyerOrder.Count() != 0);
        }

        [TestMethod()]
        public void FindOrderdetaiByOrderIDTest()
        {
            var procedure = new Procedure();
            var findOrderdetaiByOrderID = procedure.FindOrderdetaiByOrderID(1);
            Assert.IsTrue(findOrderdetaiByOrderID.Count() != 0);
        }

        [TestMethod()]
        public void GetHowLongHireDateTest()
        {
            var procedure = new Procedure();
            var getHowLongHireDate = procedure.GetHowLongHireDate();
            Assert.IsTrue(getHowLongHireDate.Count() != 0);
        }

        [TestMethod()]
        public void FindProductsByCategoryTest()
        {
            var procedure = new Procedure();
            var findProductsByCategory = procedure.FindProductsByCategory();
            Assert.IsTrue(findProductsByCategory.Count() != 0);
        }

        [TestMethod()]
        public void GetProductOrderTest()
        {
            var procedure = new Procedure();
            var getProductOrder = procedure.GetProductOrder();
            Assert.IsTrue(getProductOrder.Count() != 0);
        }
    }
}