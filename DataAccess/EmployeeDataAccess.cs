using System;
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
            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "INSERT INTO TBLEMPLOYEE (PICTURE, FIRSTNAME, MIDDLENAME, LASTNAME, NUMBER, ADDRESS, COMPLEMENT, CITY," +
                    " PROVINCE, POSTALCODE, PHONENUMBER, OTHERPHONENUMBER, EMAIL, HIREDATE, TERMDATE, SIN, POSITION, OBSERVATIONS, STATUS)" +
                " VALUES (@Picture, @FirstName, @MiddleName, @LastName, @Number, @Address, @Complement, @City," +
                " @Province, @PostalCode, @PhoneNumber, @OtherPhoneNumber, @Email, @HireDate, @TermDate, @SIN, @Position, @Observations, @Status)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Picture", employee.Picture));
                    command.Parameters.Add(new SqlParameter("@FirstName", employee.FirstName));
                    command.Parameters.Add(new SqlParameter("@MiddleName", employee.MiddleName));
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
                }
            } //close and dispose --> connection
        }

        public void Update(Employee employee)
        {
            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "UPDATE TBLEMPLOYEE SET PICTURE=@Picture, FIRSTNAME=@FirstName, MIDDLENAME=@MiddleName, LASTNAME=@LastName," +
                    " NUMBER=@Number, ADDRESS=@Address, COMPLEMENT=@Complement, CITY=@City, PROVINCE=@Province, POSTALCODE=@PostalCode," +
                    " PHONENUMBER=@PhoneNumber, OTHERPHONENUMBER=@OtherPhoneNumber, EMAIL=@Email, HIREDATE=@HireDate, TERMDATE=@TermDate," +
                    " SIN=@SIN, POSITION=@Position, OBSERVATIONS=@Observations, STATUS=@Status WHERE ID=@Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", employee.Id));
                    command.Parameters.Add(new SqlParameter("@Picture", employee.Picture));
                    command.Parameters.Add(new SqlParameter("@FirstName", employee.FirstName));
                    command.Parameters.Add(new SqlParameter("@MiddleName", employee.MiddleName));
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
                }
            } //close and dispose --> connection
        }

        public List<Employee> GetAllEmployeesActives()
        {
            var result = new List<Employee>();

            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT FIRSTNAME FROM TBLEMPLOYEE WHERE STATUS=1", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string firstName = (string)reader["FirstName"];

                        var employee = new Employee
                        {
                            FirstName = firstName,
                        };
                        result.Add(employee);
                    }
                }
            } //close and dispose --> connection
            return result;
        }


        public List<Employee> GetAllEmployees()
        {
            var result = new List<Employee>();

            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();

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
                        Boolean status = (Boolean)reader["Status"];

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
            } //close and dispose --> connection
            return result;
        }
    }
}
