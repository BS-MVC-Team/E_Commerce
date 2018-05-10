﻿using BuildSchool.MvcSolution.OnlineStore.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSchool.MvcSolution.OnlineStore.Repository
{
    class EmployeesRepository
    {
        public void Create(Employees model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "INSERT INTO Employees VALUES (@EmployeeID, @Name, @Phone, @HireDate)";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@EmployeeID", model.EmployeeID);
            command.Parameters.AddWithValue("@Name", model.Name);
            command.Parameters.AddWithValue("@Phone", model.Phone);
            command.Parameters.AddWithValue("@HireDate", model.HireDate);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Employees model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "UPDATE Employees SET Name=@Name, Phone=@Phone, HireDate=@HireDate WHERE EmployeeID = @EmployeeID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@EmployeeID", model.EmployeeID);
            command.Parameters.AddWithValue("@Name", model.Name);
            command.Parameters.AddWithValue("@Phone", model.Phone);
            command.Parameters.AddWithValue("@HireDate", model.HireDate);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(Employees model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@EmployeeID", model.EmployeeID);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public Employees FindById(string EmployeeID)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@EmployeeID", EmployeeID);

            connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            var properties = typeof(Employees).GetProperties();
            Employees employees = null;

            while (reader.Read())
            {
                //2
                employees = new Employees();
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var fieldName = reader.GetName(i);
                    var property = properties.FirstOrDefault(
                        p => p.Name == fieldName);
                    if (property == null)
                        continue;

                    if (!reader.IsDBNull(i))
                        property.SetValue(employees,
                            reader.GetValue(i));
                }

                //1
                //employees.EmployeeID = (int)reader.GetValue(reader.GetOrdinal("EmployeeID"));
                //employees.Name = reader.GetValue(reader.GetOrdinal("Name")).ToString();
                //employees.Phone = reader.GetValue(reader.GetOrdinal("Phone")).ToString();
                //employees.HireDate = (DateTime)reader.GetValue(reader.GetOrdinal("HireDate"));
            }

            reader.Close();

            return employees;
        }

        public IEnumerable<Employees> GetAll()
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM employees";

            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            var employees = new List<Employees>();

            while (reader.Read())
            {
                var employee = new Employees();
                employee.EmployeeID = int.Parse(reader.GetValue(reader.GetOrdinal("EmployeeID")).ToString());
                employee.Name = reader.GetValue(reader.GetOrdinal("Name")).ToString();
                employee.Phone = reader.GetValue(reader.GetOrdinal("Phone")).ToString();
                employee.HireDate = Parse(reader.GetValue(reader.GetOrdinal("HireDate")).ToString();
                employees.Add(employee);
            }

            reader.Close();

            return employees;

        }
    }
}
