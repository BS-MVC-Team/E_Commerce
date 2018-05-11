using BuildSchool.MvcSolution.OnlineStore.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace BuildSchool.MvcSolution.OnlineStore.Repository
{
    public class EmployeesRepository
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

        public Employees FindById(int EmployeeID)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@EmployeeID", EmployeeID);

            connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            var properties = typeof(Employees).GetProperties();
            Employees employee = null;

            while (reader.Read())
            {
                employee = new Employees();
                employee = DbReaderModelBinder<Employees>.Bind(reader);
            }

            reader.Close();

            return employee;
        }
        public Employees FindByName(string Name)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM Employees WHERE Name = @Name";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Name", Name);

            connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            var properties = typeof(Employees).GetProperties();
            Employees employee = null;

            while (reader.Read())
            {
                employee = new Employees();
                employee = DbReaderModelBinder<Employees>.Bind(reader);
            }

            reader.Close();

            return employee;
        }

        public IEnumerable<Employees> FindByHireYear(int startYear, int endYear)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM Employees WHERE YEAR(HireDate) BETWEEN @startYear AND @endYear ORDER BY HireDate DESC";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@startYear", startYear);
            command.Parameters.AddWithValue("@endYear", endYear);

            connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            var employees = new List<Employees>();

            while (reader.Read())
            {
                Employees employee = new Employees();
                employee = DbReaderModelBinder<Employees>.Bind(reader);
                employees.Add(employee);
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
                employee = DbReaderModelBinder<Employees>.Bind(reader);
                employees.Add(employee);
            }

            reader.Close();

            return employees;

        }
    }
}
