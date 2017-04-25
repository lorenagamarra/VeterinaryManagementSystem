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
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "INSERT INTO TBLBREED (SPECIE, NAME) VALUES (@Specie, @Name)";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@Specie", breed.Specie));
            command.Parameters.Add(new SqlParameter("@Name", breed.Name));


            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Breed breed)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "UPDATE TBLBREED SET SPECIE=@Specie, NAME=@Name, STATUS=@Status  WHERE ID=@Id";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@Specie", breed.Specie));
            command.Parameters.Add(new SqlParameter("@Name", breed.Name));
            command.Parameters.Add(new SqlParameter("@Status", breed.Status));

            command.ExecuteNonQuery();
            connection.Close();
        }


        public void Delete(Breed breed)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "DELETE FROM TBLBREED WHERE ID=@Id NOT IN (SELECT BREEDID FROM TBLANIMAL)";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@Id", breed.Id));

            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<Breed> GetAllSpecieActives()
        {
            List<Breed> result = new List<Breed>();
            using (SqlCommand command = new SqlCommand("SELECT DISTINCT SPECIE FROM TBLBREED WHERE STATUS='Active'", connection))
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
            return result;
        }

        public List<Breed> GetAllBreedsActivesBySpecie()
        {
            List<Breed> result = new List<Breed>();
            using (SqlCommand command = new SqlCommand("SELECT * FROM TBLBREED WHERE STATUS='Active' AND SPECIE=@Specie", connection))
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
            return result;
        }


        public List<Breed> GetAllBreeds()
        {
            List<Breed> result = new List<Breed>();
            using (SqlCommand command = new SqlCommand("SELECT * FROM TBLBREED", connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    string specie = (string)reader["Specie"];
                    string name = (string)reader["Name"];
                    string status = (string)reader["Status"];
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
            return result;
        }

    }
}
