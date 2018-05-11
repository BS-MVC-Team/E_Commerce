using BuildSchool.MvcSolution.OnlineStore.Models;
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
                "data source=.; database=Commerce; integrated security=true");
            var sql = "INSERT INTO Products VALUES (@ProductID, @ProductName, @UnitPrice, @Description, @CategoryID, @ProductImage)";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductID", model.ProductID);
            command.Parameters.AddWithValue("@ProductName", model.ProductName);
            command.Parameters.AddWithValue("@UnitPrice", model.UnitPrice);
            command.Parameters.AddWithValue("@Description", model.Description);
            command.Parameters.AddWithValue("@CategoryID", model.CategoryID);
            command.Parameters.AddWithValue("@ProductImage", model.ProductImage);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Products model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "UPDATE Products SET ProductName = @ProductName, UnitPrice = @UnitPrice, Description = @Description, CategoryID = @CategoryID, ProductImage = @ProductImage WHERE ProductID = @ProductID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductID", model.ProductID);
            command.Parameters.AddWithValue("@ProductName", model.ProductName);
            command.Parameters.AddWithValue("@UnitPrice", model.UnitPrice);
            command.Parameters.AddWithValue("@Description", model.Description);
            command.Parameters.AddWithValue("@CategoryID", model.CategoryID);
            command.Parameters.AddWithValue("@ProductImage", model.ProductImage);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(Products model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "DELETE FROM Products WHERE ProductID = @ProductID ";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductID", model.ProductID);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public Products FindById(int ProductID)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM Products WHERE ProductID = @ProductID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductID", ProductID);

            connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            Products product = null;
            var properties = typeof(Products).GetProperties();
            while (reader.Read())
            {
                product = new Products();
                product = DbReaderModelBinder<Products>.Bind(reader);

            }
            reader.Close();

            return product;
        }

        public IEnumerable<Products> GetAll()
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM Products";

            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            var products = new List<Products>();
            var properties = typeof(Products).GetProperties();

            while (reader.Read())
            {
                var product = new Products();
                product = DbReaderModelBinder<Products>.Bind(reader);

                products.Add(product);
            }

            reader.Close();

            return products;

        }


        public Products FindByProductName(string ProductName)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM Products WHERE ProductName = @ProductName";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductName", ProductName);

            connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            Products product = null;
            var properties = typeof(Products).GetProperties();

            while (reader.Read())
            {
                product = new Products();
                product = DbReaderModelBinder<Products>.Bind(reader);
            }

            reader.Close();

            return product;
        }
    }
}
