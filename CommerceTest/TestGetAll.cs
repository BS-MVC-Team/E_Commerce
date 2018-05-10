using System;
using System.Linq;
using BuildSchool.MvcSolution.OnlineStore.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommerceTest
{
    [TestClass]
    public class TestGetAll
    {
        [TestMethod]
        public void Test_GetAll_Category()
        {
            var repository = new CategoryRepository();
            var category = repository.GetAll();
            Assert.IsTrue(category.Count() == 0);
        }
        [TestMethod]
        public void Test_GetAll_Product()
        {
            var repository = new ProductRepository();
            var product = repository.GetAll();
            Assert.IsTrue(product.Count() == 0);
        }
        [TestMethod]
        public void Test_GetAll_Employee()
        {
            var repository = new EmployeesRepository();
            var employee = repository.GetAll();
            Assert.IsTrue(employee.Count() == 0);
        }
        [TestMethod]
        public void Test_GetAll_Member()
        {
            var repository = new MemberRepository();
            var member = repository.GetAll();
            Assert.IsTrue(member.Count() == 0);
        }
        [TestMethod]
        public void Test_GetAll_Order()
        {
            var repository = new OrdersRepository();
            var orders = repository.GetAll();
            Assert.IsTrue(orders.Count() == 0);
        }
        [TestMethod]
        public void Test_GetAll_ProductFormat()
        {
            var repository = new ProductFormatRepository();
            var product = repository.GetAll();
            Assert.IsTrue(product.Count() == 0);
        }
        [TestMethod]
        public void Test_GetAll_OrderDetails()
        {
            var repository = new OrderDetailsRepository();
            var orderdetails = repository.GetAll();
            Assert.IsTrue(orderdetails.Count() == 0);
        }
    }
}
