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
    public class ProductFormatRepository
    {
        public void Create(ProductFormat model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "INSERT INTO Members VALUES (@ProductFormatID, @ProductID, @Size, @Color, @StockQuantity)";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductFormatID", model.ProductFormatID);
            command.Parameters.AddWithValue("@ProductID", model.ProductID);
            command.Parameters.AddWithValue("@Size", model.Size);
            command.Parameters.AddWithValue("@Color", model.Color);
            command.Parameters.AddWithValue("@StockQuantity", model.StockQuantity);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(ProductFormat model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "UPDATE ProductFormat SET ProductID=@ProductID, Size=@Size, Color=@Color, StockQuantity=@StockQuantity WHERE ProductFormatID = @ProductFormatID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductFormatID", model.ProductFormatID);
            command.Parameters.AddWithValue("@ProductID", model.ProductID);
            command.Parameters.AddWithValue("@Size", model.Size);
            command.Parameters.AddWithValue("@Color", model.Color);
            command.Parameters.AddWithValue("@StockQuantity", model.StockQuantity);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(ProductFormat model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "DELETE FROM ProductFormat WHERE ProductFormatID = @ProductFormatID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductFormatID", model.ProductFormatID);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public Members FindById(string ProductFormatID)
        {
            SqlConnection connection = new SqlConnection(
                 "data source=.; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM ProductFormat";

            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            var productFormat = new List<ProductFormat>();

            var properties = typeof(Members).GetProperties();
            ProductFormat productformats = null;



            while (reader.Read())
            {
                productFormat = new Members();
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var fieldName = reader.GetName(i);
                    var property = properties.FirstOrDefault(
                        p => p.Name == fieldName);

                    if (property == null)
                        continue;

                    if (!reader.IsDBNull(i))
                        property.SetValue(members,
                            reader.GetValue(i));
                }

            }

            reader.Close();

            return members;
        }

        public IEnumerable<Members> GetAll()
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM Members";

            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            var members = new List<Members>();

            while (reader.Read())
            {
                var member = new Members();
                member.MemberID = reader.GetValue(reader.GetOrdinal("MemberID")).ToString();
                member.Password = reader.GetValue(reader.GetOrdinal("Password")).ToString();
                member.Name = reader.GetValue(reader.GetOrdinal("Name")).ToString();
                member.Phone = reader.GetValue(reader.GetOrdinal("Phone")).ToString();
                member.Address = reader.GetValue(reader.GetOrdinal("Address")).ToString();
                member.Email = reader.GetValue(reader.GetOrdinal("Email")).ToString();
                members.Add(member);
            }

            reader.Close();

            return members;

        }
    }
}
