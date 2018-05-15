using System;
using System.Linq;
using BuildSchool.MvcSolution.OnlineStore.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommerceTest
{
    [TestClass]
    public class TestGetFunction
    {
        [TestMethod]
        public void Test_FindByProductName()
        {
            var repository = new ProductRepository();
            var product = repository.FindByProductName("abc");
            Assert.IsTrue(product.Count() == 0);
        }
        [TestMethod]
        public void Test_FindByName()
        {
            var repository = new EmployeesRepository();
            var product = repository.FindByName("abc");
            Assert.IsNull(product);
        }
        [TestMethod]
        public void Test_GetStatus()
        {
            var repository = new OrdersRepository();
            var orders = repository.GetStatus("出貨中");
            Assert.IsTrue(orders.Count() == 0);
        }
        [TestMethod]
        public void Test_GetOrderDate()
        {
            var repository = new OrdersRepository();
            var orders = repository.GetOrderDate("2018%");
            Assert.IsTrue(orders.Count() == 0);
        }
        [TestMethod]
        public void Test_FindByHireYear()
        {
            var repository = new EmployeesRepository();
            var employee = repository.FindByHireYear(1900, 2000);
            Assert.IsTrue(employee.Count() == 0);
        }
    }
}
