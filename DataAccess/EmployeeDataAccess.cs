﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryManagementSystem.Classes;

namespace VeterinaryManagementSystem.DataAccess
{
    class EmployeeDataAccess
    {
        private string connectionString;
        private SqlConnection connection;

        public EmployeeDataAccess()
        {
            connectionString = "Server=tcp:nedis-abbott.database.windows.net,1433;Initial Catalog=VeterinaryDB;Persist Security Info=False;User ID=dbadmin;Password=DBveterinary2017;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }

        public void Add(Employee employee)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "INSERT INTO TBLEMPLOYEE (PICTURE, FIRSTNAME, MIDDLETNAME, LASTNAME, NUMBER, ADDRESS, COMPLEMENT, CITY, PROVINCE, POSTALCODE, PHONENUMBER, OTHERPHONENUMBER, EMAIL, HIREDATE, TERMDATE, SIN, POSITION, OBSERVATIONS)" +
                " VALUES (@Picture, @FirstName, @MiddleName, @LastName, @Number, @Address, @Complement, @City, @Province, @PostalCode, @PhoneNumber, @OtherPhoneNumber, @Email, @HireDate, @TermDate, @SIN, @Position, @Observations)";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.Add(new SqlParameter("@Picture", employee.Picture));
            command.Parameters.Add(new SqlParameter("@FirstName", employee.FirstName));
            command.Parameters.Add(new SqlParameter("@MiddletName", employee.MiddleName));
            command.Parameters.Add(new SqlParameter("@LastName", employee.LastName));
            command.Parameters.Add(new SqlParameter("@Number", employee.Number));
            command.Parameters.Add(new SqlParameter("@Address", employee.Address));
            command.Parameters.Add(new SqlParameter("@Complement", employee.Complement));
            command.Parameters.Add(new SqlParameter("@City", employee.City));
            command.Parameters.Add(new SqlParameter("@Province", employee.Province));
            command.Parameters.Add(new SqlParameter("@PostalCode", employee.PostalCode));
            command.Parameters.Add(new SqlParameter("@PhoneNumber", employee.PhoneNumber));
            command.Parameters.Add(new SqlParameter("@OtherPhoneNumber", employee.OtherPhoneNumber));
            command.Parameters.Add(new SqlParameter("@Email", employee.Email));

            command.Parameters.Add(new SqlParameter("@HireDate", employee.HireDate));
            command.Parameters.Add(new SqlParameter("@TermDate", employee.TermDate));
            command.Parameters.Add(new SqlParameter("@SIN", employee.SIN));
            command.Parameters.Add(new SqlParameter("@Position", employee.Position));
            command.Parameters.Add(new SqlParameter("@Observations", employee.Observations));

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Employee employee)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "UPDATE TBLEMPLOYEE (PICTURE, FIRSTNAME, MIDDLETNAME, LASTNAME, NUMBER, ADDRESS, COMPLEMENT, CITY, PROVINCE, POSTALCODE, PHONENUMBER, OTHERPHONENUMBER, EMAIL, HIREDATE, TERMDATE, SIN, POSITION, OBSERVATIONS, STATUS)" +
                " VALUES (@Picture, @FirstName, @MiddleName, @LastName, @Number, @Address, @Complement, @City, @Province, @PostalCode, @PhoneNumber, @OtherPhoneNumber, @Email, @HireDate, @TermDate, @SIN, @Position, @Observations, @Status)";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.Add(new SqlParameter("@Picture", employee.Picture));
            command.Parameters.Add(new SqlParameter("@FirstName", employee.FirstName));
            command.Parameters.Add(new SqlParameter("@MiddletName", employee.MiddleName));
            command.Parameters.Add(new SqlParameter("@LastName", employee.LastName));
            command.Parameters.Add(new SqlParameter("@Number", employee.Number));
            command.Parameters.Add(new SqlParameter("@Address", employee.Address));
            command.Parameters.Add(new SqlParameter("@Complement", employee.Complement));
            command.Parameters.Add(new SqlParameter("@City", employee.City));
            command.Parameters.Add(new SqlParameter("@Province", employee.Province));
            command.Parameters.Add(new SqlParameter("@PostalCode", employee.PostalCode));
            command.Parameters.Add(new SqlParameter("@PhoneNumber", employee.PhoneNumber));
            command.Parameters.Add(new SqlParameter("@OtherPhoneNumber", employee.OtherPhoneNumber));
            command.Parameters.Add(new SqlParameter("@Email", employee.Email));

            command.Parameters.Add(new SqlParameter("@HireDate", employee.HireDate));
            command.Parameters.Add(new SqlParameter("@TermDate", employee.TermDate));
            command.Parameters.Add(new SqlParameter("@SIN", employee.SIN));
            command.Parameters.Add(new SqlParameter("@Position", employee.Position));
            command.Parameters.Add(new SqlParameter("@Observations", employee.Observations));
            command.Parameters.Add(new SqlParameter("@Status", employee.Status));

            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<Employee> GetAllEmployeesActives()
        {
            List<Employee> result = new List<Employee>();
            using (SqlCommand command = new SqlCommand("SELECT * FROM TBLEMPLOYEE WHERE STATUS='Active'", connection))
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
                    string phoneNumber = (string)reader["PhoneNumber"];
                    string otherPhoneNumber = (string)reader["OtherPhoneNumber"];
                    string email = (string)reader["Email"];

                    DateTime hireDate = (DateTime)reader["HireDate"];
                    DateTime termDate = (DateTime)reader["TermDate"];
                    string sin = (string)reader["SIN"];
                    string position = (string)reader["Position"];
                    string observations = (string)reader["Observations"];

                    var employee = new Employee
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
                        Email = email,

                        HireDate = hireDate,
                        TermDate = termDate,
                        SIN = sin,
                        Position = position,
                        Observations = observations
                    };
                    result.Add(employee);
                }
            }
            return result;
        }


        public List<Employee> GetAllEmployees()
        {
            List<Employee> result = new List<Employee>();
            using (SqlCommand command = new SqlCommand("SELECT * FROM TBLEMPLOYEE", connection))
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
                    string phoneNumber = (string)reader["PhoneNumber"];
                    string otherPhoneNumber = (string)reader["OtherPhoneNumber"];
                    string email = (string)reader["Email"];

                    DateTime hireDate = (DateTime)reader["HireDate"];
                    DateTime termDate = (DateTime)reader["TermDate"];
                    string sin = (string)reader["SIN"];
                    string position = (string)reader["Position"];
                    string observations = (string)reader["Observations"];
                    string status = (string)reader["Status"];

                    var employee = new Employee
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
                        Email = email,

                        HireDate = hireDate,
                        TermDate = termDate,
                        SIN = sin,
                        Position = position,
                        Observations = observations,
                        Status = status
                    };
                    result.Add(employee);
                }
            }
            return result;
        }
    }
}
