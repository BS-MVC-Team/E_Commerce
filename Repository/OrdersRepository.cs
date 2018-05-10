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
    public class OrdersRepository
    {
        public void Create(Orders model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "INSERT INTO Orders VALUES (@OrderID, @EmployeeID, @MemberID, @ShipName, @ShipAddress, @ShipPhone, @ShippedDate, @OrderDate, @ReceiptedDate, @Status, @Discount)";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@OrderID", model.OrderID);
            command.Parameters.AddWithValue("@EmployeeID", model.EmployeeID);
            command.Parameters.AddWithValue("@MemberID", model.MemberID);
            command.Parameters.AddWithValue("@ShipName", model.ShipName);
            command.Parameters.AddWithValue("@ShipAddress", model.ShipAddress);
            command.Parameters.AddWithValue("@ShipPhone", model.ShipPhone);
            command.Parameters.AddWithValue("@ShippedDate", model.ShippedDate);
            command.Parameters.AddWithValue("@OrderDate", model.OrderDate);
            command.Parameters.AddWithValue("@ReceiptedDate", model.ReceiptedDate);
            command.Parameters.AddWithValue("@Status", model.Status);
            command.Parameters.AddWithValue("@Discount", model.Discount);


            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Orders model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "UPDATE Orders SET OrderID=@OrderID, EmployeeID=@EmployeeID, MemberID=@MemberID, ShipName=@ShipName, ShipAddress=@ShipAddress, ShipPhone=@ShipPhone, ShippedDate=@ShippedDate, OrderDate=@OrderDate, ReceiptedDate=@ReceiptedDate, Status=@Status, Discount=@Discount";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@OrderID", model.OrderID);
            command.Parameters.AddWithValue("@EmployeeID", model.EmployeeID);
            command.Parameters.AddWithValue("@MemberID", model.MemberID);
            command.Parameters.AddWithValue("@ShipName", model.ShipName);
            command.Parameters.AddWithValue("@ShipAddress", model.ShipAddress);
            command.Parameters.AddWithValue("@ShipPhone", model.ShipPhone);
            command.Parameters.AddWithValue("@ShippedDate", model.ShippedDate);
            command.Parameters.AddWithValue("@OrderDate", model.OrderDate);
            command.Parameters.AddWithValue("@ReceiptedDate", model.ReceiptedDate);
            command.Parameters.AddWithValue("@Status", model.Status);
            command.Parameters.AddWithValue("@Discount", model.Discount);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void Delete(Orders model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "DELETE FROM Orders WHERE OrderID = @OrderID ";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@OrderID", model.OrderID);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public Orders FindById(int OrderID)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM Orders WHERE OrderID = @OrderID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@OrderID", OrderID);

            connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            //var Orders = new Orders();
            var properties = typeof(Orders).GetProperties();
            Orders Orders = null;

            while (reader.Read())
            {
                Orders = new Orders();
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var fieldName = reader.GetName(i);
                    var property = properties.FirstOrDefault(
                        p => p.Name == fieldName);

                    if (property == null)
                        continue;

                    if (!reader.IsDBNull(i))
                        property.SetValue(Orders,
                            reader.GetValue(i));
                }

            }
            while (reader.Read())
            {
               
                Orders.OrderID = (int)reader.GetValue(reader.GetOrdinal("OrderID"));
                Orders.EmployeeID = (int)reader.GetValue(reader.GetOrdinal("EmployeeID"));
                Orders.MemberID = reader.GetValue(reader.GetOrdinal("MemberID")).ToString();
                Orders.ShipName = reader.GetValue(reader.GetOrdinal("ShipName")).ToString();
                Orders.ShipAddress = reader.GetValue(reader.GetOrdinal("ShipAddress")).ToString();
                Orders.ShipPhone = reader.GetValue(reader.GetOrdinal("ShipPhone")).ToString();
                Orders.ShippedDate = (DateTime)reader.GetValue(reader.GetOrdinal("ShippedDate"));
                Orders.OrderDate = (DateTime)reader.GetValue(reader.GetOrdinal("OrderDate"));
                Orders.ReceiptedDate = (DateTime)reader.GetValue(reader.GetOrdinal("ReceiptedDate"));
                Orders.Status = reader.GetValue(reader.GetOrdinal("Status")).ToString();
                Orders.Discount = (decimal)reader.GetValue(reader.GetOrdinal("Discount"));

            }

            reader.Close();
            return Orders;
        }

    }
}
