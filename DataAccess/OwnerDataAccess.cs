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
            var sql = "INSERT INTO TBLOWNER (REGISTRATIONDATE, PICTURE_01, FIRSTNAME_01, MIDDLENAME_01, LASTNAME_01, NUMBER_01, ADDRESS_01, COMPLEMENT_01, CITY_01, PROVINCE_01, POSTALCODE_01, PHONENUMBER_01, OTHERPHONENUMBER_01, EMAIL_01, PICTURE_02, FIRSTNAME_02, MIDDLENAME_02, LASTNAME_02, NUMBER_02, ADDRESS_02, COMPLEMENT_02, CITY_02, PROVINCE_02, POSTALCODE_02, PHONENUMBER_02, OTHERPHONENUMBER_02, EMAIL_02, OBSERVATION)" +
                " VALUES (@RegistrationDate, @Picture_01, @FirstName_01, @MiddleName_01, @LastName_01, @Number_01, @Address_01, @Complement_01, @City_01, @Province_01, @PostalCode_01, @PhoneNumber_01, @OtherPhoneNumber_01, @Email_01, @Picture_02, @FirstName_02, @MiddleName_02, @LastName_02, @Number_02, @Address_02, @Complement_02, @City_02, @Province_02, @PostalCode_02, @PhoneNumber_02, @OtherPhoneNumber_02, @Email_02, @Observation)";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.Add(new SqlParameter("@RegistrationDate", owner.RegistrationDate));

            command.Parameters.Add(new SqlParameter("@Picture_01", owner.Picture_01));
            command.Parameters.Add(new SqlParameter("@FirstName_01", owner.FirstName_01));
            command.Parameters.Add(new SqlParameter("@MiddletName_01", owner.MiddleName_01));
            command.Parameters.Add(new SqlParameter("@LastName_01", owner.LastName_01));
            command.Parameters.Add(new SqlParameter("@Number_01", owner.Number_01));
            command.Parameters.Add(new SqlParameter("@Address_01", owner.Address_01));
            command.Parameters.Add(new SqlParameter("@Complement_01", owner.Complement_01));
            command.Parameters.Add(new SqlParameter("@City_01", owner.City_01));
            command.Parameters.Add(new SqlParameter("@Province_01", owner.Province_01));
            command.Parameters.Add(new SqlParameter("@PostalCode_01", owner.PostalCode_01));
            command.Parameters.Add(new SqlParameter("@PhoneNumber_01", owner.PhoneNumber_01));
            command.Parameters.Add(new SqlParameter("@OtherPhoneNumber_01", owner.OtherPhoneNumber_01));
            command.Parameters.Add(new SqlParameter("@Email_01", owner.Email_01));

            command.Parameters.Add(new SqlParameter("@Picture_02", owner.Picture_02));
            command.Parameters.Add(new SqlParameter("@FirstName_02", owner.FirstName_02));
            command.Parameters.Add(new SqlParameter("@MiddletName_02", owner.MiddleName_02));
            command.Parameters.Add(new SqlParameter("@LastName_02", owner.LastName_02));
            command.Parameters.Add(new SqlParameter("@Number_02", owner.Number_02));
            command.Parameters.Add(new SqlParameter("@Address_02", owner.Address_02));
            command.Parameters.Add(new SqlParameter("@Complement_02", owner.Complement_02));
            command.Parameters.Add(new SqlParameter("@City_02", owner.City_02));
            command.Parameters.Add(new SqlParameter("@Province_02", owner.Province_02));
            command.Parameters.Add(new SqlParameter("@PostalCode_02", owner.PostalCode_02));
            command.Parameters.Add(new SqlParameter("@PhoneNumber_02", owner.PhoneNumber_02));
            command.Parameters.Add(new SqlParameter("@OtherPhoneNumber_02", owner.OtherPhoneNumber_02));
            command.Parameters.Add(new SqlParameter("@Email_02", owner.Email_02));

            command.Parameters.Add(new SqlParameter("@Observation", owner.Observation));

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Owner owner)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "UPDATE TBLOWNER (PICTURE_01, FIRSTNAME_01, MIDDLENAME_01, LASTNAME_01, NUMBER_01, ADDRESS_01, COMPLEMENT_01, CITY_01, PROVINCE_01, POSTALCODE_01, PHONENUMBER_01, OTHERPHONENUMBER_01, EMAIL_01, PICTURE_02, FIRSTNAME_02, MIDDLENAME_02, LASTNAME_02, NUMBER_02, ADDRESS_02, COMPLEMENT_02, CITY_02, PROVINCE_02, POSTALCODE_02, PHONENUMBER_02, OTHERPHONENUMBER_02, EMAIL_02, OBSERVATION, STATUS)" +
                            " VALUES (@Picture_01, @FirstName_01, @MiddleName_01, @LastName_01, @Number_01, @Address_01, @Complement_01, @City_01, @Province_01, @PostalCode_01, @PhoneNumber_01, @OtherPhoneNumber_01, @Email_01, @Picture_02, @FirstName_02, @MiddleName_02, @LastName_02, @Number_02, @Address_02, @Complement_02, @City_02, @Province_02, @PostalCode_02, @PhoneNumber_02, @OtherPhoneNumber_02, @Email_02, @Observation, @Status)";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.Add(new SqlParameter("@Picture_01", owner.Picture_01));
            command.Parameters.Add(new SqlParameter("@FirstName_01", owner.FirstName_01));
            command.Parameters.Add(new SqlParameter("@MiddletName_01", owner.MiddleName_01));
            command.Parameters.Add(new SqlParameter("@LastName_01", owner.LastName_01));
            command.Parameters.Add(new SqlParameter("@Number_01", owner.Number_01));
            command.Parameters.Add(new SqlParameter("@Address_01", owner.Address_01));
            command.Parameters.Add(new SqlParameter("@Complement_01", owner.Complement_01));
            command.Parameters.Add(new SqlParameter("@City_01", owner.City_01));
            command.Parameters.Add(new SqlParameter("@Province_01", owner.Province_01));
            command.Parameters.Add(new SqlParameter("@PostalCode_01", owner.PostalCode_01));
            command.Parameters.Add(new SqlParameter("@PhoneNumber_01", owner.PhoneNumber_01));
            command.Parameters.Add(new SqlParameter("@OtherPhoneNumber_01", owner.OtherPhoneNumber_01));
            command.Parameters.Add(new SqlParameter("@Email_01", owner.Email_01));

            command.Parameters.Add(new SqlParameter("@Picture_02", owner.Picture_02));
            command.Parameters.Add(new SqlParameter("@FirstName_02", owner.FirstName_02));
            command.Parameters.Add(new SqlParameter("@MiddletName_02", owner.MiddleName_02));
            command.Parameters.Add(new SqlParameter("@LastName_02", owner.LastName_02));
            command.Parameters.Add(new SqlParameter("@Number_02", owner.Number_02));
            command.Parameters.Add(new SqlParameter("@Address_02", owner.Address_02));
            command.Parameters.Add(new SqlParameter("@Complement_02", owner.Complement_02));
            command.Parameters.Add(new SqlParameter("@City_02", owner.City_02));
            command.Parameters.Add(new SqlParameter("@Province_02", owner.Province_02));
            command.Parameters.Add(new SqlParameter("@PostalCode_02", owner.PostalCode_02));
            command.Parameters.Add(new SqlParameter("@PhoneNumber_02", owner.PhoneNumber_02));
            command.Parameters.Add(new SqlParameter("@OtherPhoneNumber_02", owner.OtherPhoneNumber_02));
            command.Parameters.Add(new SqlParameter("@Email_02", owner.Email_02));

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

                    Byte[] picture_01 = (Byte[])reader["Picture_01"];
                    string firstName_01 = (string)reader["FirstName_01"];
                    string middleName_01 = (string)reader["MiddleName_01"];
                    string lastName_01 = (string)reader["LastName_01"];
                    string number_01 = (string)reader["Number_01"];
                    string address_01 = (string)reader["Address_01"];
                    string complement_01 = (string)reader["Complement_01"];
                    string city_01 = (string)reader["City_01"];
                    string province_01 = (string)reader["Province_01"];
                    string postalCode_01 = (string)reader["PostalCode_01"];
                    int phoneNumber_01 = (int)reader["PhoneNumber_01"];
                    int otherPhoneNumber_01 = (int)reader["OtherPhoneNumber_01"];
                    string email_01 = (string)reader["Email_01"];

                    Byte[] picture_02 = (Byte[])reader["Picture_02"];
                    string firstName_02 = (string)reader["FirstName_02"];
                    string middleName_02 = (string)reader["MiddleName_02"];
                    string lastName_02 = (string)reader["LastName_02"];
                    string number_02 = (string)reader["Number_02"];
                    string address_02 = (string)reader["Address_02"];
                    string complement_02 = (string)reader["Complement_02"];
                    string city_02 = (string)reader["City_02"];
                    string province_02 = (string)reader["Province_02"];
                    string postalCode_02 = (string)reader["PostalCode_02"];
                    int phoneNumber_02 = (int)reader["PhoneNumber_02"];
                    int otherPhoneNumber_02 = (int)reader["OtherPhoneNumber_02"];
                    string email_02 = (string)reader["Email_02"];

                    string observation = (string)reader["Observation"];
                    
                    var owner = new Owner
                    {
                        Id = id,
                        RegistrationDate = registrationDate,

                        Picture_01 = picture_01,
                        FirstName_01 = firstName_01,
                        MiddleName_01 = middleName_01,
                        LastName_01 = lastName_01,
                        Number_01 = number_01,
                        Address_01 = address_01,
                        Complement_01 = complement_01,
                        City_01 = city_01,
                        Province_01 = province_01,
                        PostalCode_01 = postalCode_01,
                        PhoneNumber_01 = phoneNumber_01,
                        OtherPhoneNumber_01 = otherPhoneNumber_01,
                        Email_01 = email_01,

                        Picture_02 = picture_02,
                        FirstName_02 = firstName_02,
                        MiddleName_02 = middleName_02,
                        LastName_02 = lastName_02,
                        Number_02 = number_02,
                        Address_02 = address_02,
                        Complement_02 = complement_02,
                        City_02 = city_02,
                        Province_02 = province_02,
                        PostalCode_02 = postalCode_02,
                        PhoneNumber_02 = phoneNumber_02,
                        OtherPhoneNumber_02 = otherPhoneNumber_02,
                        Email_02 = email_02,

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

                    Byte[] picture_01 = (Byte[])reader["Picture_01"];
                    string firstName_01 = (string)reader["FirstName_01"];
                    string middleName_01 = (string)reader["MiddleName_01"];
                    string lastName_01 = (string)reader["LastName_01"];
                    string number_01 = (string)reader["Number_01"];
                    string address_01 = (string)reader["Address_01"];
                    string complement_01 = (string)reader["Complement_01"];
                    string city_01 = (string)reader["City_01"];
                    string province_01 = (string)reader["Province_01"];
                    string postalCode_01 = (string)reader["PostalCode_01"];
                    int phoneNumber_01 = (int)reader["PhoneNumber_01"];
                    int otherPhoneNumber_01 = (int)reader["OtherPhoneNumber_01"];
                    string email_01 = (string)reader["Email_01"];

                    Byte[] picture_02 = (Byte[])reader["Picture_02"];
                    string firstName_02 = (string)reader["FirstName_02"];
                    string middleName_02 = (string)reader["MiddleName_02"];
                    string lastName_02 = (string)reader["LastName_02"];
                    string number_02 = (string)reader["Number_02"];
                    string address_02 = (string)reader["Address_02"];
                    string complement_02 = (string)reader["Complement_02"];
                    string city_02 = (string)reader["City_02"];
                    string province_02 = (string)reader["Province_02"];
                    string postalCode_02 = (string)reader["PostalCode_02"];
                    int phoneNumber_02 = (int)reader["PhoneNumber_02"];
                    int otherPhoneNumber_02 = (int)reader["OtherPhoneNumber_02"];
                    string email_02 = (string)reader["Email_02"];

                    string observation = (string)reader["Observation"];
                    string status = (string)reader["Status"];

                    var owner = new Owner
                    {
                        Id = id,
                        RegistrationDate = registrationDate,

                        Picture_01 = picture_01,
                        FirstName_01 = firstName_01,
                        MiddleName_01 = middleName_01,
                        LastName_01 = lastName_01,
                        Number_01 = number_01,
                        Address_01 = address_01,
                        Complement_01 = complement_01,
                        City_01 = city_01,
                        Province_01 = province_01,
                        PostalCode_01 = postalCode_01,
                        PhoneNumber_01 = phoneNumber_01,
                        OtherPhoneNumber_01 = otherPhoneNumber_01,
                        Email_01 = email_01,

                        Picture_02 = picture_02,
                        FirstName_02 = firstName_02,
                        MiddleName_02 = middleName_02,
                        LastName_02 = lastName_02,
                        Number_02 = number_02,
                        Address_02 = address_02,
                        Complement_02 = complement_02,
                        City_02 = city_02,
                        Province_02 = province_02,
                        PostalCode_02 = postalCode_02,
                        PhoneNumber_02 = phoneNumber_02,
                        OtherPhoneNumber_02 = otherPhoneNumber_02,
                        Email_02 = email_02,

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
