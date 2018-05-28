using BuildSchool.MvcSolution.OnlineStore.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSchool.MvcSolution.OnlineStore.Repository
{
    public class ShoppingCartRepository
    {
        public void Create(ShoppingCart model, IDbConnection connection)
        {
            connection.Execute("INSERT INTO ShoppingCart VALUES ( @MemberID, @ProductFormatID, @Quantity, @UnitPrice )",
                new
                {
                    model.MemberID,
                    model.ProductFormatID,
                    model.Quantity,
                    model.UnitPrice
                });
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
            connection.Execute("DELETE FROM ShoppingCart WHERE ShoppingCartID = @ShoppingCartID",
                new
                {
                    model.ShoppingCartID
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
    }
}
