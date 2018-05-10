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
        public void Create(ProductFormat model) //新增
        {
            SqlConnection connection = new SqlConnection(
                "data source=SZUYUANHUANG-PC; database=Commerce; integrated security=true");
            var sql = "INSERT INTO ProductFormat VALUES (@ProductFormatID, @ProductID, @Size, @Color,@StockQuantity)";

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

        public void Update(ProductFormat model) //修改
        {
            SqlConnection connection = new SqlConnection(
                "data source=SZUYUANHUANG-PC; database=Commerce; integrated security=true");
            var sql = "UPDATE ProductFormat SET ProductID=@ProductID, Size=@Size, Color=@Color,StockQuantity =@StockQuantity WHERE ProductFormatID = @ProductFormatID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductID", model.ProductID);
            command.Parameters.AddWithValue("@Size", model.Size);
            command.Parameters.AddWithValue("@Color", model.Color);
            command.Parameters.AddWithValue("@StockQuantity", model.StockQuantity);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(ProductFormat model) //刪除
        {
            SqlConnection connection = new SqlConnection(
                "data source=SZUYUANHUANG-PC; database=Commerce; integrated security=true");
            var sql = "DELETE FROM ProductFormat WHERE ProductFormatID = @ProductFormatID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductFormatID", model.ProductFormatID);


            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public ProductFormat FindById(int ProductFormatID)
        {
            SqlConnection connection = new SqlConnection(
                "data source=SZUYUANHUANG-PC; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM fotmats WHERE ProductFormatID = @ProductFormatID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductFormatID", ProductFormatID);

            connection.Open();

            var properties = typeof(ProductFormat).GetProperties();
            ProductFormat fotmats = null;
            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                fotmats = new ProductFormat();
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var fieldName = reader.GetName(i);
                    var property = properties.FirstOrDefault((o) => o.Name == fieldName);

                    if (property == null)
                        continue;

                    if (!reader.IsDBNull(i))
                        property.SetValue(fotmats, reader.GetValue(i));
                }
            }

            reader.Close();

            return fotmats;
        }

        public IEnumerable<ProductFormat> GetAll()
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM Members";

            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            var properties = typeof(ProductFormat).GetProperties();
            var fotmats = new List<ProductFormat>();

            while (reader.Read())
            {
                var member = new ProductFormat();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var fieldName = reader.GetName(i);
                    var property = properties.FirstOrDefault(p => p.Name == fieldName);

                    if (property == null)
                        continue;
                    if (!reader.IsDBNull(i))
                        property.SetValue(fotmats, reader.GetValue(i));

                }

            }

            reader.Close();

            return fotmats;

        }
    }
}
