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
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "INSERT INTO TBLVACCINE (NAME, PRICE) VALUES (@Name, @Price)";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@Name", vaccine.Name));
            command.Parameters.Add(new SqlParameter("@Price", vaccine.Price));

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Vaccine vaccine)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "UPDATE TBLVACCINE SET NAME=@Name, PRICE=@Price, STATUS=@Status WHERE ID=@Id";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@Id", vaccine.Id));
            command.Parameters.Add(new SqlParameter("@Name", vaccine.Name));
            command.Parameters.Add(new SqlParameter("@Price", vaccine.Price));
            command.Parameters.Add(new SqlParameter("@Status", vaccine.Status));

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete (Vaccine vaccine)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "DELETE FROM TBLVACCINE WHERE ID=@Id NOT IN (SELECT VACCINEID FROM TBLCONSULTATION)";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@Id", vaccine.Id));

            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<Vaccine> GetAllVaccinesActives()
        {
            List<Vaccine> result = new List<Vaccine>();
            using (SqlCommand command = new SqlCommand("SELECT * FROM TBLVACCINE WHERE STATUS='Active'", connection))
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
            return result;
        }

        public List<Vaccine> GetAllVaccines()
        {
            List<Vaccine> result = new List<Vaccine>();
            using (SqlCommand command = new SqlCommand("SELECT * FROM TBLVACCINE", connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    string name = (string)reader["Name"];
                    decimal price = (decimal)reader["Price"];
                    string status = (string)reader["Status"];
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
            return result;
        }


    }
}
