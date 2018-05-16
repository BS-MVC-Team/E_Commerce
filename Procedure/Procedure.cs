using BuildSchool.MvcSolution.OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Procedure
{
    public class Procedure
    {
        public IEnumerable<Orders> GetBuyerOrder(string memberID)
        {
            SqlConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;

            command.CommandText = "dbo.GetBuyerOrder";
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = connection;
            command.Parameters.Add(new SqlParameter("@memberID",memberID));

            connection.Open();

            reader = command.ExecuteReader();
            var properties = typeof(Orders).GetProperties();
            var Orders = new List<Orders>();
            while (reader.Read())
            {
                var Order = new Orders();
                Order = DbReaderModelBinder<Orders>.Bind(reader);
                Orders.Add(Order);
            }

            connection.Close();

            return Orders;
        }
    }
}
