using BuildSchool.MvcSolution.OnlineStore.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSchool.MvcSolution.OnlineStore.Repository
{
    public class MemberRepository
    {
        public void Create(Members model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "INSERT INTO Members VALUES (@MemberID, @Password, @Name, @Phone, @Address, @Email)";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@MemberID", model.MemberID);
            command.Parameters.AddWithValue("@Password", model.Password);
            command.Parameters.AddWithValue("@Name", model.Name);
            command.Parameters.AddWithValue("@Phone", model.Phone);
            command.Parameters.AddWithValue("@Address", model.Address);
            command.Parameters.AddWithValue("@Email", model.Email);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Members model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "UPDATE Members SET Password=@Password, Name=@Name, Phone=@Phone, Address=@Address, Email=@Email WHERE MemberID = @MemberID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@MemberID", model.MemberID);
            command.Parameters.AddWithValue("@Password", model.Password);
            command.Parameters.AddWithValue("@Name", model.Name);
            command.Parameters.AddWithValue("@Phone", model.Phone);
            command.Parameters.AddWithValue("@Address", model.Address);
            command.Parameters.AddWithValue("@Email", model.Email);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(Members model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "DELETE FROM Members WHERE MemberID = @MemberID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@MemberID", model.MemberID);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public Members FindById(string MemberID)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM Members WHERE MemberID = @MemberID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@MemberID", MemberID);

            connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            var members = new Members();

            while (reader.Read())
            {
                members.MemberID = reader.GetValue(reader.GetOrdinal("MemberID")).ToString();
                members.Password = reader.GetValue(reader.GetOrdinal("Password")).ToString();
                members.Name = reader.GetValue(reader.GetOrdinal("Name")).ToString();
                members.Phone = reader.GetValue(reader.GetOrdinal("Phone")).ToString();
                members.Address = reader.GetValue(reader.GetOrdinal("Address")).ToString();
                members.Email = reader.GetValue(reader.GetOrdinal("Email")).ToString();
            }

            reader.Close();

            return members;
        }

        public IEnumerable<Members> GetAll()
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM Members";

            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            var members = new List<Members>();

            while (reader.Read())
            {
                var member = new Members();
                member.MemberID = reader.GetValue(reader.GetOrdinal("MemberID")).ToString();
                member.Password = reader.GetValue(reader.GetOrdinal("Password")).ToString();
                member.Name = reader.GetValue(reader.GetOrdinal("Name")).ToString();
                member.Phone = reader.GetValue(reader.GetOrdinal("Phone")).ToString();
                member.Address = reader.GetValue(reader.GetOrdinal("Address")).ToString();
                member.Email = reader.GetValue(reader.GetOrdinal("Email")).ToString();
                members.Add(member);
            }

            reader.Close();

            return members;

        }
    }
}
