﻿using BuildSchool.MvcSolution.OnlineStore.Models;
using Dapper;
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

    public class ColorFilter
    {
        public int CategroyID { get; set; }
        public string CategoryName { get; set; }
        public int ProductFormatID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string image { get; set; }
        public string Color { get; set; }
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
        public string Image { get; set; }
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

    
    public class FindFormatIDByProductIDCS
    {
        public int ProductFormatID { get; set; }
        public string Image { get; set; }
        public int StockQuantity { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class SearchProductName
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Color { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public string Image { get; set; }
        public string CategoryName { get; set; }
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

        public IEnumerable<ColorFilter> ColorFilters(string Color)
        {
            
            var command = Command("dbo.ColorFilter");
            command.Parameters.Add(new SqlParameter("@Color", Color));
            command.Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            var colorfilter = new List<ColorFilter>();
            while (reader.Read())
            {
                var colorfilters = new ColorFilter();
                colorfilters = DbReaderModelBinder<ColorFilter>.Bind(reader);
                colorfilter.Add(colorfilters);
            }
            command.Connection.Close();
            return colorfilter;
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

        public IEnumerable<FindProductFormatByProductID> GetFormatByProductID(int productid)
        {
            var command = Command("dbo.FindProductFormatByProductID");
            command.Parameters.Add(new SqlParameter("@productid", productid));
            command.Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            var GetFormatByProductID = new List<FindProductFormatByProductID>();
            while (reader.Read())
            {
                var GetProductFormat = new FindProductFormatByProductID();
                GetProductFormat = DbReaderModelBinder<FindProductFormatByProductID>.Bind(reader);
                GetFormatByProductID.Add(GetProductFormat);
            }
            command.Connection.Close();
            return GetFormatByProductID;
        }

        public IEnumerable<FindFormatIDByProductIDCS> GetFormatIDByProductIDCS(int productid, string size, string color)
        {
            var command = Command("dbo.FindFormatIDByProductIDCS");
            command.Parameters.Add(new SqlParameter("@productid", productid));
            command.Parameters.Add(new SqlParameter("@color", color));
            command.Parameters.Add(new SqlParameter("@size", size));
            command.Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            var GetFormatIDByProductIDCS = new List<FindFormatIDByProductIDCS>();
            while (reader.Read())
            {
                var GetProductFormatID = new FindFormatIDByProductIDCS();
                GetProductFormatID = DbReaderModelBinder<FindFormatIDByProductIDCS>.Bind(reader);
                GetFormatIDByProductIDCS.Add(GetProductFormatID);
            }
            command.Connection.Close();
            return GetFormatIDByProductIDCS;
        }


        public Orders FindOrderID(string MemberID)
        {
            var command = Command("dbo.FindOrderID");
            command.Parameters.Add(new SqlParameter("@MemberID",MemberID));
            command.Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                return DbReaderModelBinder<Orders>.Bind(reader);
            }
            return null;
        }

        public ShoppingCart SearchRepeatCart(string memberID,int formatID)
        {
            var command = Command("dbo.SearchRepeatInCart");
            command.Parameters.Add(new SqlParameter("@memberID", memberID));
            command.Parameters.Add(new SqlParameter("@formatID", formatID));
            command.Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                return DbReaderModelBinder<ShoppingCart>.Bind(reader);
            }
            return null;
        }

        public SqlCommand Command(string CommandText)
        {
            SqlConnection connection = new SqlConnection(SqlConnectionString.ConnectionString());
            SqlCommand command = new SqlCommand();

            command.CommandText = CommandText;
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = connection;

            return command;
        }

        public IEnumerable<FindIndexProducts> FindMoneyBetween(decimal lower,decimal higher)
        {
            var command = Command("dbo.FindMoneyBetween");
            command.Parameters.Add(new SqlParameter("@lower", lower));
            command.Parameters.Add(new SqlParameter("@higher", higher));
            command.Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            var FindMoneyBetween = new List<FindIndexProducts>();
            while (reader.Read())
            {
                var GetBetween = new FindIndexProducts();
                GetBetween = DbReaderModelBinder<FindIndexProducts>.Bind(reader);
                FindMoneyBetween.Add(GetBetween);
            }
            command.Connection.Close();
            return FindMoneyBetween;
        }

        public IEnumerable<Products> FindProductsByCategoryID(int CategoryID)
        {
            IDbConnection connection = new SqlConnection(SqlConnectionString.ConnectionString());
            return connection.Query<Products>("dbo.FindProductsByCategoryID",new { @CategoryID = CategoryID}, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<SearchProductName> Search(string productname)
        {
            var command = Command("dbo.Search");
            command.Parameters.Add(new SqlParameter("@productname", productname));
            command.Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            var SearchProductName = new List<SearchProductName>();
            while (reader.Read())
            {
                var Search = new SearchProductName();
                Search = DbReaderModelBinder<SearchProductName>.Bind(reader);
                SearchProductName.Add(Search);
            }
            command.Connection.Close();
            return SearchProductName;
        }
    }
}
