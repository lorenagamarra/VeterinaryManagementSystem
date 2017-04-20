using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryManagementSystem.Classes;

namespace VeterinaryManagementSystem.DataAccess
{
    class OwnerDataAccess
    {
        private string connectionString;
        private SqlConnection connection;

        public OwnerDataAccess()
        {
            connectionString = "Server=tcp:nedis-abbott.database.windows.net,1433;Initial Catalog=VeterinaryDB;Persist Security Info=False;User ID=dbadmin;Password=DBveterinary2017;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
        
        public void Add(Owner owner)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "INSERT INTO TBLOWNER (REGISTRATIONDATE, PERSON1ID, PERSON2ID, OBSERVATION)" +
                " VALUES (@RegistrationDate, @Person1Id, @Person2Id, @Observation)";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@RegistrationDate", owner.RegistrationDate));
            command.Parameters.Add(new SqlParameter("@OwnerDetails1", owner.Person1Id));
            command.Parameters.Add(new SqlParameter("@OwnerDetails2", owner.Person2Id));
            command.Parameters.Add(new SqlParameter("@Observation", owner.Observation));
            
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Owner owner)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "UPDATE TBLOWNER (REGISTRATIONDATE, OWNERDETAILS1, OWNERDETAILS2, OBSERVATION, STATUS)" +
                            " VALUES (@RegistrationDate, @OwnerDetails1, @OwnerDetails2, @Observation, @Status)";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@RegistrationDate", owner.RegistrationDate));
            command.Parameters.Add(new SqlParameter("@OwnerDetails1", owner.Person1Id));
            command.Parameters.Add(new SqlParameter("@OwnerDetails2", owner.Person2Id));
            command.Parameters.Add(new SqlParameter("@Observation", owner.Observation));
            command.Parameters.Add(new SqlParameter("@Status", owner.Status));

            command.ExecuteNonQuery();
            connection.Close();
        }
        
        public void Delete(Owner owner)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "DELETE FROM TBLOWNER WHERE ID=@Id NOT IN (SELECT OWNERID FROM TBLANIMAL)";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@Id", owner.Id));

            command.ExecuteNonQuery();
            connection.Close();
        }
        
        public List<Owner> GetAllOwnersActives()
        {
            List<Owner> result = new List<Owner>();
            using (SqlCommand command = new SqlCommand("SELECT * FROM TBLOWNER WHERE STATUS='Active'", connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    DateTime registrationDate = (DateTime)reader["RegistrationDate"];
                    int person1Id = (int)reader["Person1Id"];
                    int person2Id = (int)reader["Person2Id"];
                    string observation = (string)reader["Observation"];
                    
                    var owner = new Owner
                    {
                        Id = id,
                        RegistrationDate = registrationDate,
                        Person1Id = person1Id,
                        Person2Id = person2Id,
                        Observation = observation
                    };
                    result.Add(owner);
                }
            }
            return result;
        }


        public List<Owner> GetAllOwners()
        {
            List<Owner> result = new List<Owner>();
            using (SqlCommand command = new SqlCommand("SELECT * FROM TBLOWNER", connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    DateTime registrationDate = (DateTime)reader["RegistrationDate"];
                    int person1Id = (int)reader["Person1Id"];
                    int person2Id = (int)reader["Person2Id"];
                    string observation = (string)reader["Observation"];
                    string status = (string)reader["Status"];

                    var owner = new Owner
                    {
                        Id = id,
                        RegistrationDate = registrationDate,
                        Person1Id = person1Id,
                        Person2Id = person2Id,
                        Observation = observation,
                        Status = status
                    };
                    result.Add(owner);
                }
            }
            return result;
        }

    }
}
