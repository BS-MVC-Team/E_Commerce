﻿using BuildSchool.MvcSolution.OnlineStore.Models;
using Dapper;
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
    public class ProductFormatRepository
    {
        public void Create(ProductFormat model) //新增
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
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
                "data source=.; database=Commerce; integrated security=true");
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
                "data source=.; database=Commerce; integrated security=true");
            var sql = "DELETE FROM ProductFormat WHERE ProductFormatID = @ProductFormatID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductFormatID", model.ProductFormatID);


            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public ProductFormat FindById(int ProductFormatID)
        {
            IDbConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
            var result = connection.Query<ProductFormat>("SELECT * FROM ProductFormat WHERE ProductFormatID = @ProductFormatID", new { ProductFormatID });
            ProductFormat productFormat = null;
            foreach (var item in result)
            {
                productFormat = item;
            }
            return productFormat;
            //SqlConnection connection = new SqlConnection(
            //    "data source=.; database=Commerce; integrated security=true");
            //var sql = "SELECT * FROM ProductFormat WHERE ProductFormatID = @ProductFormatID";

            //SqlCommand command = new SqlCommand(sql, connection);

            //command.Parameters.AddWithValue("@ProductFormatID", ProductFormatID);

            //connection.Open();

            //var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            //ProductFormat productFormat = null;

            //while (reader.Read())
            //{
            //    productFormat = new ProductFormat();
            //    productFormat.ProductFormatID = (int)reader.GetValue(reader.GetOrdinal("ProductFormatID"));
            //    productFormat.ProductID = (int)reader.GetValue(reader.GetOrdinal("ProductID"));
            //    productFormat.Size = reader.GetValue(reader.GetOrdinal("Size")).ToString();
            //    productFormat.Color = reader.GetValue(reader.GetOrdinal("Color")).ToString();
            //    productFormat.StockQuantity = (int)reader.GetValue(reader.GetOrdinal("StockQuantity"));
            //}

            //reader.Close();

            //return productFormat;
        }

        public IEnumerable<ProductFormat> GetAll()
        {
            IDbConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
            return connection.Query<ProductFormat>("SELECT * FROM ProductFormat ");
            //SqlConnection connection = new SqlConnection(
            //    "data source=.; database=Commerce; integrated security=true");
            //var sql = "SELECT * FROM ProductFormat";

            //SqlCommand command = new SqlCommand(sql, connection);
            //connection.Open();

            //var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            //var properties = typeof(ProductFormat).GetProperties();
            //var productFormats = new List<ProductFormat>();
            //while (reader.Read())
            //{
            //    var productFormat = new ProductFormat();
            //    productFormat = DbReaderModelBinder<ProductFormat>.Bind(reader);

            //    productFormats.Add(productFormat);
            //}

            //reader.Close();

            //return productFormats;

        }
    }
}
