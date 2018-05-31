using BuildSchool.MvcSolution.OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Procedure
{
    public class GetBuyerOrderModel
    {       
        public string MemberID { get; set; }
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
    }

    public class FindOrderdetaiByOrderIDModel
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class GetHowLongHireDateModel
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime HireDate { get; set; }
        public int How_Long { get; set; }
    }

    public class FindProductsByCategoryModel
    {
        public int CategroyID { get; set; }
        public string CategoryName { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductImage { get; set; }
    }

    public class GetProductOrderModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Total { get; set; }
    }

    public class FindProductFormatByProductID
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int StockQuantity { get; set; }
        public int ProductFormatID { get; set; }
    }

    public class ShoppingCartInformation
    {
        public int ShoppingCartID { get; set; }
        public int ProductFormatID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
    }

    public class Procedure
    {
        public IEnumerable<GetBuyerOrderModel> GetBuyerOrder(string memberID)
        {
            var command = Command("dbo.GetBuyerOrder");
            command.Parameters.Add(new SqlParameter("@memberID", memberID));
            command.Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            var GetBuyerOrders = new List<GetBuyerOrderModel>();
            while (reader.Read())
            {
                var GetBuyerOrder = new GetBuyerOrderModel();
                GetBuyerOrder = DbReaderModelBinder<GetBuyerOrderModel>.Bind(reader);
                GetBuyerOrders.Add(GetBuyerOrder);
            }
            command.Connection.Close();
            return GetBuyerOrders;
        }

        public IEnumerable<FindOrderdetaiByOrderIDModel> FindOrderdetaiByOrderID(int orderid)
        {
            var command = Command("dbo.FindOrderdetaiByOrderID");
            command.Parameters.Add(new SqlParameter("@orderid", orderid));
            command.Connection.Open();
            SqlDataReader reader = command.ExecuteReader(); 
            var FindOrderdetaiByOrderIDs = new List<FindOrderdetaiByOrderIDModel>();
            while (reader.Read())
            {
                var FindOrderdetaiByOrderID = new FindOrderdetaiByOrderIDModel();
                FindOrderdetaiByOrderID = DbReaderModelBinder<FindOrderdetaiByOrderIDModel>.Bind(reader);
                FindOrderdetaiByOrderIDs.Add(FindOrderdetaiByOrderID);
            }
            command.Connection.Close();
            return FindOrderdetaiByOrderIDs;
        }

        public IEnumerable<GetHowLongHireDateModel> GetHowLongHireDate()
        {
            var command = Command("dbo.GetHowLongHireDate");
            command.Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            var GetHowLongHireDates = new List<GetHowLongHireDateModel>();
            while (reader.Read())
            {
                var GetHowLongHireDate = new GetHowLongHireDateModel();
                GetHowLongHireDate = DbReaderModelBinder<GetHowLongHireDateModel>.Bind(reader);
                GetHowLongHireDates.Add(GetHowLongHireDate);
            }
            command.Connection.Close();
            return GetHowLongHireDates;
        }

        public IEnumerable<FindProductsByCategoryModel> FindProductsByCategory()
        {
            var command = Command("dbo.FindProductsByCategory");
            command.Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            var FindProductsByCategories = new List<FindProductsByCategoryModel>();
            while (reader.Read())
            {
                var FindProductsByCategory = new FindProductsByCategoryModel();
                FindProductsByCategory = DbReaderModelBinder<FindProductsByCategoryModel>.Bind(reader);
                FindProductsByCategories.Add(FindProductsByCategory);
            }
            command.Connection.Close();
            return FindProductsByCategories;
        }

        public IEnumerable<GetProductOrderModel> GetProductOrder()
        {
            var command = Command("dbo.GetProductOrder");
            command.Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            var GetProductOrders = new List<GetProductOrderModel>();
            while (reader.Read())
            {
                var GetProductOrder = new GetProductOrderModel();
                GetProductOrder = DbReaderModelBinder<GetProductOrderModel>.Bind(reader);
                GetProductOrders.Add(GetProductOrder);
            }
            command.Connection.Close();
            return GetProductOrders;
        }

        public SqlCommand Command(string CommandText)
        {
            SqlConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
            SqlCommand command = new SqlCommand();

            command.CommandText = CommandText;
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = connection;

            return command;
        }
    }
}
