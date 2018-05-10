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
    public class CategoryRepository
    {
        //新增分類
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

        //修改分類
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

        //刪除分類
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

        //查詢分類by ID
        public Category FindById(int CategoryID)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM Category WHERE CategoryID = @CategoryID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@CategoryID", CategoryID);

            connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            var properties = typeof(Category).GetProperties();
            Category category = null;
            

            while (reader.Read())
            {
                //3
                category = DbReaderModelBinder<Category>.Bind(reader);

                //2
                //category = new Category();
                //for (var i = 0; i < reader.FieldCount; i++)
                //{
                //    var fieldName = reader.GetName(i);
                //    var property = properties.FirstOrDefault(
                //        p => p.Name == fieldName);

                //    if (property == null)
                //        continue;

                //    if (!reader.IsDBNull(i))
                //        property.SetValue(category,
                //            reader.GetValue(i));
                //}

                //1
                //category.CategoryID = (int)reader.GetValue(reader.GetOrdinal("CategoryID"));
                //category.CategoryName = reader.GetValue(reader.GetOrdinal("CategoryName")).ToString();
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
