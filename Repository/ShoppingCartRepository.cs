using BuildSchool.MvcSolution.OnlineStore.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSchool.MvcSolution.OnlineStore.Repository
{
    public class ShoppingCartRepository
    {
        public void Create(ShoppingCart model)
        {
            //connection.Execute("INSERT INTO ShoppingCart VALUES ( @MemberID, @ProductFormatID, @Quantity, @UnitPrice )",
            //    new
            //    {
            //        model.MemberID,
            //        model.ProductFormatID,
            //        model.Quantity,
            //        model.UnitPrice
            //    });
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "INSERT INTO ShoppingCart VALUES ( @MemberID, @ProductID, @ProductFormatID, @Quantity)";

            SqlCommand command = new SqlCommand(sql, connection);


            command.Parameters.AddWithValue("@MemberID", model.MemberID);
            command.Parameters.AddWithValue("@ProductID", model.ProductID);
            command.Parameters.AddWithValue("@ProductFormatID", model.ProductFormatID);
            command.Parameters.AddWithValue("@Quantity", model.Quantity);


            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        //    connection.Execute("INSERT INTO ShoppingCart VALUES ( @MemberID, @ProductFormatID, @Quantity, @UnitPrice )",
        //            new
        //            {
        //                model.MemberID,
        //                model.ProductFormatID,
        //                model.Quantity,
        //                model.UnitPrice
        //});}



        public void Update(int ShoppingCartID, int Quantity)
        {
            IDbConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
            connection.Execute("UPDATE ShoppingCart SET Quantity = @Quantity WHERE ShoppingCartID = @ShoppingCartID",
                new
                {
                    Quantity,
                    ShoppingCartID,
                });
        }

        public void Delete(int ShoppingCartID)
        {
            IDbConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
            connection.Execute("DELETE FROM ShoppingCart WHERE ShoppingCartID = @ShoppingCartID",
                new
                {
                    ShoppingCartID
                });
        }

        public ShoppingCart FindById(int CategoryID, IDbConnection connection)
        {
            var result = connection.Query<ShoppingCart>("SELECT * FROM ShoppingCart WHERE ShoppingCartID = @ShoppingCartID",
                new
                {
                    CategoryID
                });
            ShoppingCart shoppingCart = null;
            foreach (var item in result)
            {
                shoppingCart = item;
            }
            return shoppingCart;
        }
        public IEnumerable<ShoppingCart> GetAll(IDbConnection connection)
        {
            return connection.Query<ShoppingCart>("SELECT * FROM ShoppingCart");
        }

        public IEnumerable<ShoppingCart> FindByMemberID(string MemberID)
        {
            IDbConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
            var result = connection.Query<ShoppingCart>("SELECT * FROM ShoppingCart WHERE MemberID = @MemberID", new { MemberID });
            return result;
        }
    }
}
