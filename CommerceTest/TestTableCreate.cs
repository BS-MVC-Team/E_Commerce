using System;
using System.Linq;
using BuildSchool.MvcSolution.OnlineStore.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommerceTest
{
    [TestClass]
    public class TestTableCreate
    {
        [TestMethod]
        public void Categorys_Create()
        {
            var repository = new CategoryRepository();
            //Category category = new Category
            //{
            //    CategoryName = "上衣"
            //};
            //repository.Create(category);
            //Category category1 = new Category
            //{
            //    CategoryName = "外套"
            //};
            //repository.Create(category1);
            //Category category2 = new Category
            //{
            //    CategoryName = "褲子"
            //};
            //repository.Create(category2);
            var categorys = repository.GetAll();
            Assert.IsTrue(categorys.Count() == 0);
        }
        [TestMethod]
        public void Products_Create()
        {
            var repository = new ProductRepository();
            //Product product = new Product
            //{
            //    ProductName = "短T",
            //    UnitPrice = 470,
            //    Description = "如對商品尺寸有任何疑問，請先私訊我詢問，以免造成彼此困擾~感謝~",
            //    CategoryID = 1,
            //    ProductImage = "test1"  
            //};
            //repository.Create(product);
            //Product product1 = new Product
            //{
            //    ProductName = "牛仔破褲",
            //    UnitPrice = 368,
            //    Description = "如對商品尺寸有任何疑問，請先私訊我詢問，以免造成彼此困擾~感謝~",
            //    CategoryID = 3,
            //    ProductImage = "test1"
            //};
            //repository.Create(product1);
            //Product product2 = new Product
            //{
            //    ProductName = "防風外套",
            //    UnitPrice = 730,
            //    Description = "如對商品尺寸有任何疑問，請先私訊我詢問，以免造成彼此困擾~感謝~",
            //    CategoryID = 2,
            //    ProductImage = "test1"
            //};
            //repository.Create(product2);
            //Product product3 = new Product
            //{
            //    ProductName = "760C圖案短T",
            //    UnitPrice = 199,
            //    Description = "如對商品尺寸有任何疑問，請先私訊我詢問，以免造成彼此困擾~感謝~",
            //    CategoryID = 1,
            //    ProductImage = "test1"
            //};
            //repository.Create(product3);
            var products = repository.GetAll();
            Assert.IsTrue(products.Count() == 0);
        }
        [TestMethod]
        public void ProductFormat_Create()
        {
            var repository = new ProductFormatRepository();
            //ProductFormat productFormat = new ProductFormat()
            //{
            //    ProductID = 1,
            //    Color = "紅色",
            //    Size = "L",
            //    StockQuantity = 50
            //};
            //repository.Create(productFormat);
            //ProductFormat productFormat1 = new ProductFormat()
            //{
            //    ProductID = 1,
            //    Color = "藍色",
            //    Size = "M",
            //    StockQuantity = 30
            //};
            //repository.Create(productFormat1);
            //ProductFormat productFormat2 = new ProductFormat()
            //{
            //    ProductID = 2,
            //    Color = "黑色",
            //    Size = "S",
            //    StockQuantity = 45
            //};
            //repository.Create(productFormat2);
            var productFormats = repository.GetAll();
            Assert.IsTrue(productFormats.Count() == 0);
        }
        [TestMethod]
        public void Members_Create()
        {
            var repository = new MemberRepository();
            //Members member = new Members()
            //{
            //    MemberID = "123",
            //    Password = "123",
            //    Name = "測試會員1號",
            //    Phone = "0123456789",
            //    Email = "123@gmail.com",
            //    Address = "300新竹市香山區五福路二段707號"
            //};
            //repository.Create(member);
            //Members member1 = new Members()
            //{
            //    MemberID = "456",
            //    Password = "456",
            //    Name = "測試會員2號",
            //    Phone = "0123456789",
            //    Email = "123@yahoo.com.tw",
            //    Address = "300新竹市香山區五福路二段707號"
            //};
            //repository.Create(member1);
            var members = repository.GetAll();
            Assert.IsTrue(members.Count() == 0);
        }
        [TestMethod]
        public void Employees_Create()
        {
            var repository = new EmployeesRepository();
            //Employees employee = new Employees()
            //{
            //    Name = "洪識超" ,
            //    Phone = "0123456789" ,
            //    HireDate = new DateTime(1996, 06, 01),
            //};
            //repository.Create(employee);
            //Employees employee1 = new Employees()
            //{
            //    Name = "簡愷祐",
            //    Phone = "0123456789",
            //    HireDate = new DateTime(1996, 01, 29),
            //};
            //repository.Create(employee1);
            //Employees employee2 = new Employees()
            //{
            //    Name = "黃思源",
            //    Phone = "0123456789",
            //    HireDate = new DateTime(1996, 02, 16),
            //};
            //repository.Create(employee2);
            var employees = repository.GetAll();
            Assert.IsTrue(employees.Count() == 0);
        }
        [TestMethod]
        public void Orders_Create()
        {
            var repository = new OrdersRepository();
            //Nullable<DateTime> n = null;
            //Orders order = new Orders()
            //{
            //    EmployeeID = 1,
            //    MemberID = "123",
            //    ShipName = "黃宗畦",
            //    ShipAddress = "300新竹市香山區五福路二段707號",
            //    ShipPhone = "0123456789",
            //    ShippedDate = new DateTime(2018, 05, 13),
            //    OrderDate = new DateTime(2018, 05, 12),
            //    ReceiptedDate = n,
            //    Discount = 0,
            //    Status = "派送中"
            //};
            //repository.Create(order);
            //Orders order1 = new Orders()
            //{
            //    EmployeeID = 2,
            //    MemberID = "123",
            //    ShipName = "黃宗畦",
            //    ShipAddress = "300新竹市香山區五福路二段707號",
            //    ShipPhone = "0123456789",
            //    ShippedDate = n,
            //    OrderDate = new DateTime(2018, 05, 12),
            //    ReceiptedDate = n,
            //    Discount = 100,
            //    Status = "已下定"
            //};
            //repository.Create(order1);
            var orders = repository.GetAll();
            Assert.IsTrue(orders.Count() == 0);
        }
        [TestMethod]
        public void OrderDetails_Create()
        {
            var repository = new OrderDetailsRepository();
            //OrderDetails orderdetail = new OrderDetails()
            //{
            //    OrderID = 1,
            //    ProductFormatID = 1,
            //    Quantity = 10,
            //    UnitPrice = 470
            //};
            //repository.Create(orderdetail);
            //OrderDetails orderdetail1 = new OrderDetails()
            //{
            //    OrderID = 2,
            //    ProductFormatID = 2,
            //    Quantity = 5,
            //    UnitPrice = 470
            //};
            //repository.Create(orderdetail1);
            //OrderDetails orderdetail2 = new OrderDetails()
            //{
            //    OrderID = 3,
            //    ProductFormatID = 1,
            //    Quantity = 50,
            //    UnitPrice = 470
            //};
            //repository.Create(orderdetail2);
            var orders = repository.GetAll();
            Assert.IsTrue(orders.Count() == 0);
        }
    }
}
