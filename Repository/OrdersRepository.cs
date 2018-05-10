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

            reader.Close();
            return Orders;
        }

        public IEnumerable<Orders> GetStatus(string status)
        {
            SqlConnection connection = new SqlConnection(
                "data source=SZUYUANHUANG-PC; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM Orders WHERE Status = @Status";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Status", status);

            connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            var orders = new List<Orders>();
            var properties = typeof(Orders).GetProperties();

            while (reader.Read())
            {
                var order = new Orders();
                order = DbReaderModelBinder<Orders>.Bind(reader);

                orders.Add(order);
            }

            reader.Close();

            return orders;
        }

    }
}
