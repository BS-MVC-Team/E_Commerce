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
    public class OrderDetailsRepository
    {
        public OrderDetails FindById(int orderId)
        {
            SqlConnection connection = new SqlConnection(
                "data source=SZUYUANHUANG-PC; database=Commerce; integrated security=true");
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
    }
}
