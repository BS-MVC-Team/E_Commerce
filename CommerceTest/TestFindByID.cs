using System;
using BuildSchool.MvcSolution.OnlineStore.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommerceTest
{
    [TestClass]
    public class TestFindByID
    {
        [TestMethod]
        public void Test_FindByID_Category()
        {
            var repository = new CategoryRepository();
            var category = repository.FindById(1);
            Assert.IsNull(category);
        }
        [TestMethod]
        public void Test_FindByID_Product()
        {
            var repository = new ProductRepository();
            var product = repository.FindById(1);
            Assert.IsNull(product);
        }
        [TestMethod]
        public void Test_FindByID_Employee()
        {
            var repository = new EmployeesRepository();
            var employee = repository.FindById(1);
            Assert.IsNull(employee);
        }
        [TestMethod]
        public void Test_FindByID_Member()
        {
            var repository = new MemberRepository();
            var member = repository.FindById("abc");
            Assert.IsNull(member);
        }
        [TestMethod]
        public void Test_FindByID_Order()
        {
            var repository = new OrdersRepository();
            var orders = repository.FindById(1);
            Assert.IsNull(orders);
        }
        [TestMethod]
        public void Test_FindByID_ProductFormat()
        {
            var repository = new ProductFormatRepository();
            var product = repository.FindById(1);
            Assert.IsNull(product);
        }
        [TestMethod]
        public void Test_FindByID_OrderDetails()
        {
            var repository = new OrderDetailsRepository();
            var orderdetails = repository.FindById(1);
            Assert.IsNull(orderdetails);
        }
    }
}
