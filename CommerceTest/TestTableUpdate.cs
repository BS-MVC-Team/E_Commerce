using System;
using System.Linq;
using BuildSchool.MvcSolution.OnlineStore.Models;
using BuildSchool.MvcSolution.OnlineStore.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommerceTest
{
    [TestClass]
    public class TestTableUpdate
    {
        
            [TestMethod]
            public void Categorys_Update()
            {
                var repository = new CategoryRepository();
                Category category = new Category
                {
                    CategoryID = 1,
                    CategoryName = "上衣1"
                };
                repository.Update(category);
                var categorys = repository.FindById(1);
                Assert.IsTrue(categorys.CategoryName == "上衣1");
            }
            [TestMethod]
            public void Products_Update()
            {
                var repository = new ProductRepository();
                Products product = new Products
                {
                    ProductID = 1,
                    ProductName = "短T1",
                    UnitPrice = 470,
                    Description = "如對商品尺寸有任何疑問，請先私訊我詢問，以免造成彼此困擾~感謝~",
                    CategoryID = 1,
                };
                repository.Update(product);
                var products = repository.FindById(1);
                Assert.IsTrue(products.ProductName == "短T1");
            }
            [TestMethod]
            public void ProductFormat_Update()
            {
                var repository = new ProductFormatRepository();
                ProductFormat productFormat = new ProductFormat()
                {
                    ProductFormatID = 1,
                    ProductID = 1,
                    Color = "紅色",
                    Size = "2L",
                    StockQuantity = 40
                };
                repository.Update(productFormat);
                var productFormats = repository.FindById(1);
                Assert.IsTrue(productFormats.Size == "2L");
            }
            [TestMethod]
            public void Members_Update()
            {
                var repository = new MemberRepository();
                Members member = new Members()
                {
                    MemberID = "456",
                    Password = "456",
                    Name = "測試會員2號",
                    Phone = "0123456789",
                    Email = "456@yahoo.com.tw",
                    Address = "300新竹市香山區五福路二段707號"
                };
                repository.Update(member);
                var members = repository.FindById("456");
                Assert.IsTrue(members.Email == "456@yahoo.com.tw");
            }
            [TestMethod]
            public void Employees_Update()
            {
                var repository = new EmployeesRepository();
                Employees employee = new Employees()
                {
                    EmployeeID = 1,
                    Name = "洪識超1",
                    Phone = "0123456789",
                    HireDate = new DateTime(1996, 06, 01),
                };
                repository.Update(employee);
                var employees = repository.FindById(1);
                Assert.IsTrue(employees.Name == "洪識超1");
            }
            [TestMethod]
            public void Orders_Update()
            {
                var repository = new OrdersRepository();
                Nullable<DateTime> n = null;
                Orders order = new Orders()
                {
                    OrderID = 7,
                    EmployeeID = 1,
                    MemberID = "123",
                    ShipName = "黃宗畦1",
                    ShipAddress = "300新竹市香山區五福路二段707號",
                    ShipPhone = "0123456789",
                    ShippedDate = new DateTime(2018, 05, 13),
                    OrderDate = new DateTime(2018, 05, 12),
                    ReceiptedDate = n,
                    Discount = 0,
                    Status = "派送中"
                };
                repository.Update(order);
                var orders = repository.FindById(7);
                Assert.IsTrue(orders.ShipName == "黃宗畦1");
            }
            [TestMethod]
            public void OrderDetails_Update()
            {
                var repository = new OrderDetailsRepository();
                OrderDetails orderdetail = new OrderDetails()
                {
                    OrderID = 7,
                    ProductFormatID = 1,
                    Quantity = 5,
                    UnitPrice = 470
                };
                repository.Update(orderdetail);
                var orders = repository.GetAll();
                Assert.IsTrue(orders.Count() > 0);
            }

    }
}
