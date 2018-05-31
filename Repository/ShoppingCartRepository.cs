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
            var sql = "INSERT INTO ShoppingCart VALUES ( @MemberID, @ProductID, @ProductFormatID, @ProductName, @UnitPrice, @Color, @Size, @Image, @Quantity)";

            SqlCommand command = new SqlCommand(sql, connection);


            command.Parameters.AddWithValue("@MemberID", model.MemberID);
            command.Parameters.AddWithValue("@ProductID", model.ProductID);
            command.Parameters.AddWithValue("@ProductFormatID", model.ProductFormatID);
            command.Parameters.AddWithValue("@ProductName", model.ProductName);
            command.Parameters.AddWithValue("@UnitPrice", model.UnitPrice);
            command.Parameters.AddWithValue("@Color", model.Color);
            command.Parameters.AddWithValue("@Size", model.Size);
            command.Parameters.AddWithValue("@Image", model.Image);
            command.Parameters.AddWithValue("@Quantity", model.Quantity);


            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }


        public void Update(ShoppingCart model, IDbConnection connection)
        {
            connection.Execute("UPDATE ShoppingCart SET MemberID = @MemberID, ProductFormatID = @ProductFormatID, Quantity = @Quantity, UnitPrice = @UnitPrice WHERE ShoppingCartID = @ShoppingCartID",
                new
                {
                    model.MemberID,
                    model.ProductFormatID,
                    model.Quantity,
                    model.UnitPrice
                });
        }

        public void Delete(ShoppingCart model, IDbConnection connection)
        {
            //connection.Execute("DELETE FROM ShoppingCart WHERE ShoppingCartID = @ShoppingCartID",
            //    new
            //    {
            //        model.ShoppingCartID
            //    });
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
    }
}
