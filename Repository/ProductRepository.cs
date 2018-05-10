using BuildSchool.MvcSolution.OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSchool.MvcSolution.OnlineStore.Repository
{
    public class ProductRepository
    {
        public void Create(Product model)
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

        public void Update(Product model)
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

        public void Delete(Product model)
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

        public Product FindById(int ProductID)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM Products WHERE ProductID = @ProductID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductID", ProductID);

            connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            var product = new Product();

            while (reader.Read())
            {
                product.ProductID = (int)reader.GetValue(reader.GetOrdinal("ProductID"));
                product.ProductName = reader.GetValue(reader.GetOrdinal("ProductName")).ToString();
                product.UnitPrice = (int)reader.GetValue(reader.GetOrdinal("UnitPrice"));
                product.Description = reader.GetValue(reader.GetOrdinal("Description")).ToString();
                product.CategoryID = (int)reader.GetValue(reader.GetOrdinal("CategoryID"));
                product.ProductImage = reader.GetValue(reader.GetOrdinal("ProductImage")).ToString();
            }

            reader.Close();

            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM Products";

            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            var products = new List<Product>();

            while (reader.Read())
            {
                var product = new Product();
                product.ProductID = (int)reader.GetValue(reader.GetOrdinal("ProductID"));
                product.ProductName = reader.GetValue(reader.GetOrdinal("ProductName")).ToString();
                product.UnitPrice = (int)reader.GetValue(reader.GetOrdinal("UnitPrice"));
                product.Description = reader.GetValue(reader.GetOrdinal("Description")).ToString();
                product.CategoryID = (int)reader.GetValue(reader.GetOrdinal("CategoryID"));
                product.ProductImage = reader.GetValue(reader.GetOrdinal("ProductImage")).ToString();

                products.Add(product);
            }

            reader.Close();

            return products;

        }
    }
}
