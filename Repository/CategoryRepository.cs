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
    public class CategoryRepository
    {
        public void Create(Category model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "INSERT INTO Category VALUES (@CategoryID, @CategoryName)";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@CategoryID", model.CategoryID);
            command.Parameters.AddWithValue("@CategoryName", model.CategoryName);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }


        public void Update(Category model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "UPDATE Category SET CategoryName = @CategoryName WHERE CategoryID = @CategoryID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@CategoryID", model.CategoryID);
            command.Parameters.AddWithValue("@CategoryName", model.CategoryName);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(Category model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "DELETE FROM Category WHERE CategoryID = @CategoryID ";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@CategoryID", model.CategoryID);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public Category FindById(int CategoryID)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM Category WHERE CategoryID = @CategoryID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@CategoryID", CategoryID);

            connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            var category = new Category();

            while (reader.Read())
            {
                category.CategoryID = (int)reader.GetValue(reader.GetOrdinal("CategoryID"));
                category.CategoryName = reader.GetValue(reader.GetOrdinal("CategoryName")).ToString();
            }

            reader.Close();

            return category;
        }

        public IEnumerable<Category> GetAll()
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM Category";

            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            var categories = new List<Category>();

            while (reader.Read())
            {
                var category = new Category();
                category.CategoryID = (int)reader.GetValue(reader.GetOrdinal("CategoryID"));
                category.CategoryName = reader.GetValue(reader.GetOrdinal("CategoryName")).ToString();

                categories.Add(category);
            }

            reader.Close();

            return categories;
        }
    }
}
