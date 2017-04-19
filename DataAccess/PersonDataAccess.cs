using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryManagementSystem.Classes;

namespace VeterinaryManagementSystem.DataAccess
{
    class PersonDataAccess
    {
        private string connectionString;
        private SqlConnection connection;

        public PersonDataAccess()
        {
            connectionString = "Server=tcp:nedis-abbott.database.windows.net,1433;Initial Catalog=VeterinaryDB;Persist Security Info=False;User ID=dbadmin;Password=DBveterinary2017;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }


        public void Add(Person person)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "INSERT INTO TBLPERSON (PICTURE, FIRSTNAME, MIDDLETNAME, LASTNAME, NUMBER, ADDRESS, COMPLEMENT, CITY, PROVINCE, POSTALCODE, PHONENUMBER, OTHERPHONENUMBER, EMAIL)" +
                " VALUES (@Picture, @FirstName, @MiddleName, @LastName, @Number, @Address, @Complement, @City, @Province, @PostalCode, @PhoneNumber, @OtherPhoneNumber, @Email)";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@Picture", person.Picture));
            command.Parameters.Add(new SqlParameter("@FirstName", person.FirstName));
            command.Parameters.Add(new SqlParameter("@MiddletName", person.MiddleName));
            command.Parameters.Add(new SqlParameter("@LastName", person.LastName));
            command.Parameters.Add(new SqlParameter("@Number", person.Number));
            command.Parameters.Add(new SqlParameter("@Address", person.Address));
            command.Parameters.Add(new SqlParameter("@Complement", person.Complement));
            command.Parameters.Add(new SqlParameter("@City", person.City));
            command.Parameters.Add(new SqlParameter("@Province", person.Province));
            command.Parameters.Add(new SqlParameter("@PostalCode", person.PostalCode));
            command.Parameters.Add(new SqlParameter("@PhoneNumber", person.PhoneNumber));
            command.Parameters.Add(new SqlParameter("@OtherPhoneNumber", person.OtherPhoneNumber));
            command.Parameters.Add(new SqlParameter("@Email", person.Email));

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Person person)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "UPDATE TBLPERSON (PICTURE, FIRSTNAME, MIDDLETNAME, LASTNAME, NUMBER, ADDRESS, COMPLEMENT, CITY, PROVINCE, POSTALCODE, PHONENUMBER, OTHERPHONENUMBER, EMAIL)" +
                            " VALUES (@Picture, @FirstName, @MiddleName, @LastName, @Number, @Address, @Complement, @City, @Province, @PostalCode, @PhoneNumber, @OtherPhoneNumber, @Email)";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@Picture", person.Picture));
            command.Parameters.Add(new SqlParameter("@FirstName", person.FirstName));
            command.Parameters.Add(new SqlParameter("@MiddletName", person.MiddleName));
            command.Parameters.Add(new SqlParameter("@LastName", person.LastName));
            command.Parameters.Add(new SqlParameter("@Number", person.Number));
            command.Parameters.Add(new SqlParameter("@Address", person.Address));
            command.Parameters.Add(new SqlParameter("@Complement", person.Complement));
            command.Parameters.Add(new SqlParameter("@City", person.City));
            command.Parameters.Add(new SqlParameter("@Province", person.Province));
            command.Parameters.Add(new SqlParameter("@PostalCode", person.PostalCode));
            command.Parameters.Add(new SqlParameter("@PhoneNumber", person.PhoneNumber));
            command.Parameters.Add(new SqlParameter("@OtherPhoneNumber", person.OtherPhoneNumber));
            command.Parameters.Add(new SqlParameter("@Email", person.Email));

            command.ExecuteNonQuery();
            connection.Close();
        }
/*
        public void Delete(Person person)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "DELETE FROM TBLPERSON WHERE ID=@Id";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@Id", person.Id));

            command.ExecuteNonQuery();
            connection.Close();
        }
*/
        public List<Person> GetAllPeople()
        {
            List<Person> result = new List<Person>();
            using (SqlCommand command = new SqlCommand("SELECT * FROM TBLOWNERDETAILS", connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    Byte[] picture = (Byte[])reader["Picture"];
                    string firstName = (string)reader["FirstName"];
                    string middleName = (string)reader["MiddleName"];
                    string lastName = (string)reader["LastName"];
                    string number = (string)reader["Number"];
                    string address = (string)reader["Address"];
                    string complement = (string)reader["Complement"];
                    string city = (string)reader["City"];
                    string province = (string)reader["Province"];
                    string postalCode = (string)reader["PostalCode"];
                    int phoneNumber = (int)reader["PhoneNumber"];
                    int otherPhoneNumber = (int)reader["OtherPhoneNumber"];
                    string email = (string)reader["Email"];

                    var person = new Person
                    {
                        Id = id,
                        Picture = picture,
                        FirstName = firstName,
                        MiddleName = middleName,
                        LastName = lastName,
                        Number = number,
                        Address = address,
                        Complement = complement,
                        City = city,
                        Province = province,
                        PostalCode = postalCode,
                        PhoneNumber = phoneNumber,
                        OtherPhoneNumber = otherPhoneNumber,
                        Email = email
                    };
                    result.Add(person);
                }
            }
            return result;
        }
    }
}
