using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryManagementSystem.Classes;

namespace VeterinaryManagementSystem.DataAccess
{
    class SpecieDataAccess
    {
        private string connectionString;
        private SqlConnection connection;

        public SpecieDataAccess()
        {
            connectionString = "Server=tcp:nedis-abbott.database.windows.net,1433;Initial Catalog=VeterinaryDB;Persist Security Info=False;User ID=dbadmin;Password=DBveterinary2017;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }


        public void Add(Specie specie)
        {
            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "INSERT INTO TBLSPECIE (SPECIENAME, STATUS) VALUES (@SpecieName, @Status)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@SpecieName", specie.SpecieName));
                    command.Parameters.Add(new SqlParameter("@Status", specie.Status));
                    command.ExecuteNonQuery();
                }
            } //close and dispose --> connection
        }

        public void Update(Specie specie)
        {
            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "UPDATE TBLSPECIE SET SPECIENAME=@SpecieName, STATUS=@Status  WHERE ID=@Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", specie.Id));
                    command.Parameters.Add(new SqlParameter("@SpecieName", specie.SpecieName));
                    command.Parameters.Add(new SqlParameter("@Status", specie.Status));
                    command.ExecuteNonQuery();
                }
            } //close and dispose --> connection
        }


        public void Delete(Specie specie)
        {
            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "DELETE FROM TBLSPECIE WHERE ID=@Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", specie.Id));
                    command.ExecuteNonQuery();
                }
            } //close and dispose --> connection
        }

        public List<Specie> GetAllSpecieActives()
        {
            var result = new List<Specie>();

            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT DISTINCT SPECIENAME FROM TBLSPECIE WHERE STATUS=1", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string specieName = (string)reader["SpecieName"];

                        var specie = new Specie
                        {
                            SpecieName = specieName
                        };
                        result.Add(specie);
                    }
                }
            } //close and dispose --> connection
            return result;
        }
       

        public List<Specie> GetAllSpecies()
        {
            var result = new List<Specie>();

            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM TBLSPECIE", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        string specieName = (string)reader["SpecieName"];
                        Boolean status = (Boolean)reader["Status"];

                        var specie = new Specie
                        {
                            Id = id,
                            SpecieName = specieName,
                            Status = status
                        };
                        result.Add(specie);
                    }
                }
            }
            return result;
        }
    }
}
