
using BuildSchool.MvcSolution.OnlineStore.Models;
using Dapper;
using Procedure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace BuildSchool.MvcSolution.OnlineStore.Repository
{
    public class ProductRepository
    {
        public void Create(Products model)
        {
            SqlConnection connection = new SqlConnection(
                SqlConnectionString.ConnectionString());
            var sql = "INSERT INTO Products VALUES (@ProductName, @UnitPrice, @Description, @CategoryID, @ShelfDate)";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductName", model.ProductName);
            command.Parameters.AddWithValue("@UnitPrice", model.UnitPrice);
            command.Parameters.AddWithValue("@Description", model.Description);
            command.Parameters.AddWithValue("@CategoryID", model.CategoryID);
            command.Parameters.AddWithValue("@ShelfDate", model.ShelfDate);


            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Products model)
        {
            SqlConnection connection = new SqlConnection(
                SqlConnectionString.ConnectionString());
            var sql = "UPDATE Products SET ProductName = @ProductName, UnitPrice = @UnitPrice, Description = @Description, CategoryID = @CategoryID, ShelfDate = @ShelfDate WHERE ProductID = @ProductID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductID", model.ProductID);
            command.Parameters.AddWithValue("@ProductName", model.ProductName);
            command.Parameters.AddWithValue("@UnitPrice", model.UnitPrice);
            command.Parameters.AddWithValue("@Description", model.Description);
            command.Parameters.AddWithValue("@CategoryID", model.CategoryID);
            command.Parameters.AddWithValue("@CategoryID", model.ShelfDate);


            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(Products model)
        {
            SqlConnection connection = new SqlConnection(
                SqlConnectionString.ConnectionString());
            var sql = "DELETE FROM Products WHERE ProductID = @ProductID ";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductID", model.ProductID);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public Products FindById(int ProductID)
        {
            IDbConnection connection = new SqlConnection(SqlConnectionString.ConnectionString());
            var result = connection.Query<Products>("SELECT * FROM Products WHERE ProductID = @ProductID", new { ProductID });
            Products product = null;
            foreach (var item in result)
            {
                product = item;
            }
            return product;
            //SqlConnection connection = new SqlConnection(
            //    SqlConnectionString.ConnectionString());
            //var sql = "SELECT * FROM Products WHERE ProductID = @ProductID";

            //SqlCommand command = new SqlCommand(sql, connection);

            //command.Parameters.AddWithValue("@ProductID", ProductID);

            //connection.Open();

            //var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            //Products product = null;
            //var properties = typeof(Products).GetProperties();
            //while (reader.Read())
            //{
            //    product = new Products();
            //    product = DbReaderModelBinder<Products>.Bind(reader);

            //}
            //reader.Close();

            //return product;
        }

        public IEnumerable<Products> GetAll()
        {
            IDbConnection connection = new SqlConnection(SqlConnectionString.ConnectionString());
            return connection.Query<Products>("SELECT * FROM Products ");
            //SqlConnection connection = new SqlConnection(
            //    SqlConnectionString.ConnectionString());
            //var sql = "SELECT * FROM Products";

            //SqlCommand command = new SqlCommand(sql, connection);
            //connection.Open();

            //var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            //var products = new List<Products>();
            //var properties = typeof(Products).GetProperties();

            //while (reader.Read())
            //{
            //    var product = new Products();
            //    product = DbReaderModelBinder<Products>.Bind(reader);

            //    products.Add(product);
            //}

            //reader.Close();

            //return products;

        }

        public IEnumerable<Products> FindProductByUnitPrice(decimal lower, decimal upper)
        {
            SqlConnection connection = new SqlConnection(SqlConnectionString.ConnectionString());
            return connection.Query<Products>("FindProductByUnitPrice", new { lower, upper }, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<FindProductFormatByProductID> FindProductFormatByProductID(int productid)
        {
            SqlConnection connection = new SqlConnection(SqlConnectionString.ConnectionString());
            return connection.Query<FindProductFormatByProductID>("FindProductFormatByProductID", new { productid }, commandType: CommandType.StoredProcedure);
        }


        public IEnumerable<Products> FindByProductName(string ProductName)
        {
            IDbConnection connection = new SqlConnection(SqlConnectionString.ConnectionString());
            return connection.Query<Products>("SELECT * FROM Products WHERE ProductName = @ProductName", new {ProductName });
        }


        public IEnumerable<FindIndexProducts> FindIndexProducts()
        {
            SqlConnection connection = new SqlConnection(SqlConnectionString.ConnectionString());
            return connection.Query<FindIndexProducts>("SELECT * FROM IndexProduct");
        }

        public IEnumerable<NewProduct> NewProduct()
        {
            SqlConnection connection = new SqlConnection(SqlConnectionString.ConnectionString());
            return connection.Query<NewProduct>("SELECT * FROM NewProduct");
        }

        public IEnumerable<HighToLowUnitprice> HighToLowUnitprice()
        {
            SqlConnection connection = new SqlConnection(SqlConnectionString.ConnectionString());
            return connection.Query<HighToLowUnitprice>("SELECT * FROM HighToLowUnitprice ORDER BY UnitPrice DESC");
        }

        public IEnumerable<LowToHighUnitprice> LowToHighUnitprice()
        {
            SqlConnection connection = new SqlConnection(SqlConnectionString.ConnectionString());
            return connection.Query<LowToHighUnitprice>("SELECT * FROM LowToHighUnitprice ORDER BY UnitPrice");
        }

        public Products FindNextProductID()
        {
            SqlConnection connection = new SqlConnection(
                SqlConnectionString.ConnectionString());
            var sql = "SELECT TOP 1 * FROM Products ORDER BY ProductID DESC";
            return connection.Query<Products>(sql).FirstOrDefault();
        }

        public IEnumerable<PopualityProduct> PopularityProduct()
        {
            SqlConnection connection = new SqlConnection(SqlConnectionString.ConnectionString());
            return connection.Query<PopualityProduct>("select p.ProductID,SUM(o.Quantity) AS Quantity, p.UnitPrice, p.ProductName, pf.Image, c.CategoryName from Products p " +
                "inner join ProductFormat pf on pf.ProductID = p.ProductID " +
                "inner join Category c on c.CategoryID = p.CategoryID " + 
                "inner join OrderDetails o on o.ProductFormatID = pf.ProductFormatID " +
                "GROUP BY p.ProductID, p.UnitPrice, p.ProductName, pf.Image, c.CategoryName " +
                "ORDER BY SUM(o.Quantity) DESC");
        }
    }
}
