using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryManagementSystem.Classes;

namespace VeterinaryManagementSystem.DataAccess
{
    class OwnerDetailsDataAccess
    {
        private string connectionString;
        private SqlConnection connection;

        public OwnerDetailsDataAccess()
        {
            connectionString = "Server=tcp:nedis-abbott.database.windows.net,1433;Initial Catalog=VeterinaryDB;Persist Security Info=False;User ID=dbadmin;Password=DBveterinary2017;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }


        public void Add(OwnerDetails ownerdetails)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "INSERT INTO TBLOWNERDETAILS (PICTURE, FIRSTNAME, MIDDLETNAME, LASTNAME, NUMBER, ADDRESS, COMPLEMENT, CITY, PROVINCE, POSTALCODE, PHONENUMBER, OTHERPHONENUMBER, EMAIL)" +
                " VALUES (@Picture, @FirstName, @MiddleName, @LastName, @Number, @Address, @Complement, @City, @Province, @PostalCode, @PhoneNumber, @OtherPhoneNumber, @Email)";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@Picture", ownerdetails.Picture));
            command.Parameters.Add(new SqlParameter("@FirstName", ownerdetails.FirstName));
            command.Parameters.Add(new SqlParameter("@MiddletName", ownerdetails.MiddleName));
            command.Parameters.Add(new SqlParameter("@LastName", ownerdetails.LastName));
            command.Parameters.Add(new SqlParameter("@Number", ownerdetails.Number));
            command.Parameters.Add(new SqlParameter("@Address", ownerdetails.Address));
            command.Parameters.Add(new SqlParameter("@Complement", ownerdetails.Complement));
            command.Parameters.Add(new SqlParameter("@City", ownerdetails.City));
            command.Parameters.Add(new SqlParameter("@Province", ownerdetails.Province));
            command.Parameters.Add(new SqlParameter("@PostalCode", ownerdetails.PostalCode));
            command.Parameters.Add(new SqlParameter("@PhoneNumber", ownerdetails.PhoneNumber));
            command.Parameters.Add(new SqlParameter("@OtherPhoneNumber", ownerdetails.OtherPhoneNumber));
            command.Parameters.Add(new SqlParameter("@Email", ownerdetails.Email));

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(OwnerDetails ownerdetails)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "UPDATE TBLOWNERDETAILS (PICTURE, FIRSTNAME, MIDDLETNAME, LASTNAME, NUMBER, ADDRESS, COMPLEMENT, CITY, PROVINCE, POSTALCODE, PHONENUMBER, OTHERPHONENUMBER, EMAIL)" +
                            " VALUES (@Picture, @FirstName, @MiddleName, @LastName, @Number, @Address, @Complement, @City, @Province, @PostalCode, @PhoneNumber, @OtherPhoneNumber, @Email)";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@Picture", ownerdetails.Picture));
            command.Parameters.Add(new SqlParameter("@FirstName", ownerdetails.FirstName));
            command.Parameters.Add(new SqlParameter("@MiddletName", ownerdetails.MiddleName));
            command.Parameters.Add(new SqlParameter("@LastName", ownerdetails.LastName));
            command.Parameters.Add(new SqlParameter("@Number", ownerdetails.Number));
            command.Parameters.Add(new SqlParameter("@Address", ownerdetails.Address));
            command.Parameters.Add(new SqlParameter("@Complement", ownerdetails.Complement));
            command.Parameters.Add(new SqlParameter("@City", ownerdetails.City));
            command.Parameters.Add(new SqlParameter("@Province", ownerdetails.Province));
            command.Parameters.Add(new SqlParameter("@PostalCode", ownerdetails.PostalCode));
            command.Parameters.Add(new SqlParameter("@PhoneNumber", ownerdetails.PhoneNumber));
            command.Parameters.Add(new SqlParameter("@OtherPhoneNumber", ownerdetails.OtherPhoneNumber));
            command.Parameters.Add(new SqlParameter("@Email", ownerdetails.Email));

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(OwnerDetails ownerdetails)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "DELETE FROM TBLOWNERDETAILS WHERE ID=@Id";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@Id", ownerdetails.Id));

            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<OwnerDetails> GetAllOwnerDetails()
        {
            List<OwnerDetails> result = new List<OwnerDetails>();
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

                    var ownerdetails = new OwnerDetails
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
                    result.Add(ownerdetails);
                }
            }
            return result;
        }
    }
}
