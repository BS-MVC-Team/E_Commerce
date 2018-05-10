﻿using BuildSchool.MvcSolution.OnlineStore.Models;
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
    }
}