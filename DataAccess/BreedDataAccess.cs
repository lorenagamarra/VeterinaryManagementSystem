using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryManagementSystem.Classes;

namespace VeterinaryManagementSystem.DataAccess
{
    class BreedDataAccess
    {
        private string connectionString;
        private SqlConnection connection;

        public BreedDataAccess()
        {
            connectionString = "Server=tcp:nedis-abbott.database.windows.net,1433;Initial Catalog=VeterinaryDB;Persist Security Info=False;User ID=dbadmin;Password=DBveterinary2017;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }


        public void Add(Breed breed)
        {
            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "INSERT INTO TBLBREED (SPECIE, NAME, STATUS) VALUES (@Specie, @Name, @Status)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Specie", breed.Specie));
                    command.Parameters.Add(new SqlParameter("@Name", breed.Name));
                    command.Parameters.Add(new SqlParameter("@Status", breed.Status));
                    command.ExecuteNonQuery();
                }
            } //close and dispose --> connection
        }

        public void Update(Breed breed)
        {
            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "UPDATE TBLBREED SET SPECIE=@Specie, NAME=@Name, STATUS=@Status  WHERE ID=@Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", breed.Id));
                    command.Parameters.Add(new SqlParameter("@Specie", breed.Specie));
                    command.Parameters.Add(new SqlParameter("@Name", breed.Name));
                    command.Parameters.Add(new SqlParameter("@Status", breed.Status));
                    command.ExecuteNonQuery();
                }
            } //close and dispose --> connection
        }


        public void Delete(Breed breed)
        {
            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "DELETE FROM TBLBREED WHERE ID=@Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", breed.Id));
                    command.ExecuteNonQuery();
                }
            } //close and dispose --> connection
        }

        public List<Breed> GetAllSpecieActives()
        {
            var result = new List<Breed>();

            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
              
                using (SqlCommand command = new SqlCommand("SELECT DISTINCT SPECIE FROM TBLBREED WHERE STATUS=1", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string specie = (string)reader["Specie"];

                        var breed = new Breed
                        {
                            Specie = specie
                        };
                        result.Add(breed);
                    }
                }
            } //close and dispose --> connection
            return result;
        }

        public List<Breed> GetAllBreedsActivesBySpecie()
        {
            var result = new List<Breed>();

            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM TBLBREED WHERE STATUS=1 AND SPECIE=@Specie", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string specie = (string)reader["Specie"];
                        string name = (string)reader["Name"];

                        var breed = new Breed
                        {
                            Name = name,
                        };
                        result.Add(breed);
                    }
                }
            }
            return result;
        }


        public List<Breed> GetAllBreeds()
        {
            var result = new List<Breed>();

            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM TBLBREED", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        string specie = (string)reader["Specie"];
                        string name = (string)reader["Name"];
                        Boolean status = (Boolean)reader["Status"];

                        var breed = new Breed
                        {
                            Id = id,
                            Name = name,
                            Specie = specie,
                            Status = status
                        };
                        result.Add(breed);
                    }
                }
            }
            return result;
        }
    }
}
