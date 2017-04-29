using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryManagementSystem.Classes;

namespace VeterinaryManagementSystem.DataAccess
{
    class AnimalDataAccess
    {
        private string connectionString;
        private SqlConnection connection;

        public AnimalDataAccess()
        {
            connectionString = "Server=tcp:nedis-abbott.database.windows.net,1433;Initial Catalog=VeterinaryDB;Persist Security Info=False;User ID=dbadmin;Password=DBveterinary2017;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }

        public void Add(Animal animal)
        {
            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = @"INSERT INTO TBLANIMAL (PICTURE, OWNERID, BREEDID, VACHISTID, DATEREG, NAME, GENDER, DATEOFBIRTH, WEIGHT, IDENTIFICATION, FOOD, PHOBIA, FLAGSET, VETHISTORIC, STATUS)
                          VALUES (@Picture, @OwnerID, @BreedID, @VachistID, @Datereg, @Name, @Gender, @Dateofbirth, @Weight, @Identification, @Food, @Phobia, @Flagset, @Vethistoric, @Status)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Picture", animal.Picture));
                    command.Parameters.Add(new SqlParameter("@OwnerID", animal.OwnerID));
                    command.Parameters.Add(new SqlParameter("@BreedID", animal.BreedID));
                    command.Parameters.Add(new SqlParameter("@VachistID", animal.VachistID));
                    command.Parameters.Add(new SqlParameter("@Datereg", animal.Datereg));
                    command.Parameters.Add(new SqlParameter("@Name", animal.Name));
                    command.Parameters.Add(new SqlParameter("@Gender", animal.Gender));
                    command.Parameters.Add(new SqlParameter("@Dateofbirth", animal.Dateofbirth));
                    command.Parameters.Add(new SqlParameter("@Weight", animal.Weight));
                    command.Parameters.Add(new SqlParameter("@Identification", animal.Identification));
                    command.Parameters.Add(new SqlParameter("@Food", animal.Food));
                    command.Parameters.Add(new SqlParameter("@Phobia", animal.Phobia));
                    command.Parameters.Add(new SqlParameter("@Flagset", animal.Flagset));
                    command.Parameters.Add(new SqlParameter("@Vethistoric", animal.Vethistoric));
                    command.Parameters.Add(new SqlParameter("@Status", animal.Status));
                    command.ExecuteNonQuery();
                }
            } //close and dispose --> connection
        }

        public void Update(Animal animal)
        {
            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = @"UPDATE TBLANIMAL SET PICTURE=@Picture, OWNERID=@OwnerID, BREEDID=@BreedID, VACHISTID=@VachistID, NAME=@Name, GENDER=@Gender, DATEOFBIRTH=@Dateofbirth, WEIGHT=@Weight, IDENTIFICATION=@Identification, FOOD=@Food, PHOBIA=@Phobia, FLAGSET=@Flagset, VETHISTORIC=@Vethistoric, STATUS=@Status WHERE ID=@Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", animal.Id));
                    command.Parameters.Add(new SqlParameter("@Picture", animal.Picture));
                    command.Parameters.Add(new SqlParameter("@OwnerID", animal.OwnerID));
                    command.Parameters.Add(new SqlParameter("@BreedID", animal.BreedID));
                    command.Parameters.Add(new SqlParameter("@VachistID", animal.VachistID));
                    command.Parameters.Add(new SqlParameter("@Name", animal.Name));
                    command.Parameters.Add(new SqlParameter("@Gender", animal.Gender));
                    command.Parameters.Add(new SqlParameter("@Dateofbirth", animal.Dateofbirth));
                    command.Parameters.Add(new SqlParameter("@Weight", animal.Weight));
                    command.Parameters.Add(new SqlParameter("@Identification", animal.Identification));
                    command.Parameters.Add(new SqlParameter("@Food", animal.Food));
                    command.Parameters.Add(new SqlParameter("@Phobia", animal.Phobia));
                    command.Parameters.Add(new SqlParameter("@Flagset", animal.Flagset));
                    command.Parameters.Add(new SqlParameter("@Vethistoric", animal.Vethistoric));
                    command.Parameters.Add(new SqlParameter("@Status", animal.Status));
                    command.ExecuteNonQuery();
                }
            } //close and dispose --> connection
        }


        /*
        public void Delete(Animal animal)
        {
            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "DELETE FROM TBLANIMAL WHERE ID=@Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", animal.Id));
                    command.ExecuteNonQuery();
                }
            } //close and dispose --> connection
        }
        */


        public List<Animal> GetAllAnimalsActives()
        {
            var result = new List<Animal>();

            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM TBLANIMAL WHERE STATUS=1", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        Byte[] picture = (Byte[])reader["Picture"];
                        int ownerID = (int)reader["OwnerID"];
                        int breedID = (int)reader["BreedID"];
                        int vachistID = (int)reader["VachistID"];
                        DateTime datereg = (DateTime)reader["Datereg"];
                        string name = (string)reader["Name"];
                        Boolean gender = (Boolean)reader["Gender"];
                        DateTime dateofbirth = (DateTime)reader["Dateofbirth"];
                        decimal weight = (decimal)reader["Weight"];
                        string identification = (string)reader["Identification"];
                        string food = (string)reader["Food"];
                        string phobia = (string)reader["Phobia"];
                        string flagset = (string)reader["Flagset"];
                        string vethistoric = (string)reader["Vethistoric"];
                        Boolean status = (Boolean)reader["Status"];

                        var animal = new Animal
                        {
                            Id = id,
                            Picture = picture,
                            OwnerID = ownerID,
                            BreedID = breedID,
                            VachistID = vachistID,
                            Datereg = datereg,
                            Name = name,
                            Gender = gender,
                            Dateofbirth = dateofbirth,
                            Weight = weight,
                            Identification = identification,
                            Food = food,
                            Phobia = phobia,
                            Flagset = flagset,
                            Vethistoric = vethistoric,
                            Status = status
                        };
                        result.Add(animal);
                    }
                }
            } //close and dispose --> connection
            return result;
        }

        public List<Animal> GetAllAnimals()
        {
            var result = new List<Animal>();

            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM TBLANIMAL", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        Byte[] picture = (Byte[])reader["Picture"];
                        int ownerID = (int)reader["OwnerID"];
                        int breedID = (int)reader["BreedID"];
                        int vachistID = (int)reader["VachistID"];
                        DateTime datereg = (DateTime)reader["Datereg"];
                        string name = (string)reader["Name"];
                        Boolean gender = (Boolean)reader["Gender"];
                        DateTime dateofbirth = (DateTime)reader["Dateofbirth"];
                        decimal weight = (decimal)reader["Weight"];
                        string identification = (string)reader["Identification"];
                        string food = (string)reader["Food"];
                        string phobia = (string)reader["Phobia"];
                        string flagset = (string)reader["Flagset"];
                        string vethistoric = (string)reader["Vethistoric"];
                        Boolean status = (Boolean)reader["Status"];

                        var animal = new Animal
                        {
                            Id = id,
                            Picture = picture,
                            OwnerID = ownerID,
                            BreedID = breedID,
                            VachistID = vachistID,
                            Datereg = datereg,
                            Name = name,
                            Gender = gender,
                            Dateofbirth = dateofbirth,
                            Weight = weight,
                            Identification = identification,
                            Food = food,
                            Phobia = phobia,
                            Flagset = flagset,
                            Vethistoric = vethistoric,
                            Status = status
                        };
                        result.Add(animal);
                    }
                }
            } //close and dispose --> connection
            return result;
        }
    }
}
