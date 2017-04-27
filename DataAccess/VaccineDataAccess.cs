using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VeterinaryManagementSystem.Classes;

namespace VeterinaryManagementSystem.DataAccess
{
    public class VaccineDataAccess
    {
        private string connectionString;
        private SqlConnection connection;

        public VaccineDataAccess()
        {
            connectionString = "Server=tcp:nedis-abbott.database.windows.net,1433;Initial Catalog=VeterinaryDB;Persist Security Info=False;User ID=dbadmin;Password=DBveterinary2017;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }


        public void Add(Vaccine vaccine)
        {
            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "INSERT INTO TBLVACCINE (NAME, PRICE, STATUS) VALUES (@Name, @Price, @Status)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Name", vaccine.Name));
                    command.Parameters.Add(new SqlParameter("@Price", vaccine.Price));
                    command.Parameters.Add(new SqlParameter("@Status", vaccine.Status));
                    command.ExecuteNonQuery();
                }
            } //close and dispose --> connection
        }

        public void Update(Vaccine vaccine)
        {
            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "UPDATE TBLVACCINE SET NAME=@Name, PRICE=@Price, STATUS=@Status WHERE ID=@Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", vaccine.Id));
                    command.Parameters.Add(new SqlParameter("@Name", vaccine.Name));
                    command.Parameters.Add(new SqlParameter("@Price", vaccine.Price));
                    command.Parameters.Add(new SqlParameter("@Status", vaccine.Status));
                    command.ExecuteNonQuery();
                }
            } //close and dispose --> connection
        }

        public void Delete (Vaccine vaccine)
        {
            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "DELETE FROM TBLVACCINE WHERE ID=@Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", vaccine.Id));
                    command.ExecuteNonQuery();
                }
            } //close and dispose --> connection
        }

        public List<Vaccine> GetAllVaccinesActives()
        {
            var result = new List<Vaccine>();

            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM TBLBREED WHERE STATUS=1", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        string name = (string)reader["Name"];
                        decimal price = (decimal)reader["Price"];

                        var vaccine = new Vaccine
                        {
                            Id = id,
                            Name = name,
                            Price = price
                        };
                        result.Add(vaccine);
                    }
                }
            } //close and dispose --> connection
            return result;
        }

        public List<Vaccine> GetAllVaccines()
        {
            var result = new List<Vaccine>();

            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM TBLBREED", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        string name = (string)reader["Name"];
                        decimal price = (decimal)reader["Price"];
                        Boolean status = (Boolean)reader["Status"];

                        var vaccine = new Vaccine
                        {
                            Id = id,
                            Name = name,
                            Price = price,
                            Status = status
                        };
                        result.Add(vaccine);
                    }
                }
            } //close and dispose --> connection
            return result;
        }
    }
}
