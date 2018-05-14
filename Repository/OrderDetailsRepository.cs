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
    public class OrderDetailsRepository
    {
        public void Create(OrderDetails model) //新增
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "INSERT INTO OrderDetails VALUES (@OrderID, @ProductFormatID, @UnitPrice, @Quantity)";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@OrderID", model.OrderID);
            command.Parameters.AddWithValue("@ProductFormatID", model.ProductFormatID);
            command.Parameters.AddWithValue("@UnitPrice", model.UnitPrice);
            command.Parameters.AddWithValue("@Quantity", model.Quantity);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(OrderDetails model) //修改
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "UPDATE OrderDetails SET UnitPrice=@UnitPrice, Quantity=@Quantity WHERE OrderID = @OrderID AND ProductFormatID=@ProductFormatID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@UnitPrice", model.UnitPrice);
            command.Parameters.AddWithValue("@Quantity", model.Quantity);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(OrderDetails model) //刪除
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "DELETE FROM OrderDetails WHERE OrderID = @OrderID AND ProductFormatID = @ProductFormatID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@OrderID", model.OrderID);
            command.Parameters.AddWithValue("@ProductFormatID", model.ProductFormatID);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }


        public OrderDetails FindById(int orderId) //單查一筆資料
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM OrderDetails WHERE OrderID = @OrderID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@OrderID", orderId);

            connection.Open();

            var properties = typeof(OrderDetails).GetProperties();
            OrderDetails orderDetails = null;
            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                orderDetails = new OrderDetails();
                for(var i=0;i<reader.FieldCount;i++)
                {
                    var fieldName = reader.GetName(i);
                    var property = properties.FirstOrDefault((o) => o.Name == fieldName);

                    if (property == null)
                        continue;

                    if (!reader.IsDBNull(i))
                        property.SetValue(orderDetails, reader.GetValue(i));
                }
            }

            reader.Close();

            return orderDetails;
        }

        public IEnumerable<OrderDetails> GetAll() //查尋全部資料
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM OrderDetails";

            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            var orderDetails = new List<OrderDetails>();
            var properties = typeof(OrderDetails).GetProperties();

            while (reader.Read())
            {
                var orderDetail = new OrderDetails();
                orderDetail = DbReaderModelBinder<OrderDetails>.Bind(reader);

                orderDetails.Add(orderDetail);
            }

            reader.Close();

            return orderDetails;

        }
    }
}
