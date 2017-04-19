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
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "INSERT INTO TBLEMPLOYEE (PERSONID, HIREDATE, TERMDATE, SIN, POSITION, OBSERVATIONS)" +
                " VALUES (@PersonId, @HireDate, @TermDate, @SIN, @Position, @Observations)";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@PersonId", employee.PersonId));
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
            var sql = "UPDATE TBLEMPLOYEE (PERSONID, HIREDATE, TERMDATE, SIN, POSITION, OBSERVATIONS, STATUS)" +
                " VALUES (@PersonId, @HireDate, @TermDate, @SIN, @Position, @Observations, @Status)";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@PersonId", employee.PersonId));
            command.Parameters.Add(new SqlParameter("@HireDate", employee.HireDate));
            command.Parameters.Add(new SqlParameter("@TermDate", employee.TermDate));
            command.Parameters.Add(new SqlParameter("@SIN", employee.SIN));
            command.Parameters.Add(new SqlParameter("@Position", employee.Position));
            command.Parameters.Add(new SqlParameter("@Observations", employee.Observations));
            command.Parameters.Add(new SqlParameter("@Status", employee.Status));

            command.ExecuteNonQuery();
            connection.Close();
        }

        /*
        public void Delete(Employee employee)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "DELETE FROM TBLEMPLOYEE WHERE ID=@Id";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@Id", employee.Id));

            command.ExecuteNonQuery();
            connection.Close();
        }
        */

        public List<Employee> GetAllEmployeesActives()
        {
            List<Employee> result = new List<Employee>();
            using (SqlCommand command = new SqlCommand("SELECT * FROM TBLEMPLOYEE WHERE STATUS='Active'", connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    int personId = (int)reader["PersonId"];
                    DateTime hireDate = (DateTime)reader["HireDate"];
                    DateTime termDate = (DateTime)reader["TermDate"];
                    int sin = (int)reader["SIN"];
                    string position = (string)reader["Position"];
                    string observations = (string)reader["Observations"];

                    var employee = new Employee
                    {
                        Id = id,
                        PersonId = personId,
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
                    int personId = (int)reader["PersonId"];
                    DateTime hireDate = (DateTime)reader["HireDate"];
                    DateTime termDate = (DateTime)reader["TermDate"];
                    int sin = (int)reader["SIN"];
                    string position = (string)reader["Position"];
                    string observations = (string)reader["Observations"];
                    string status = (string)reader["Status"];

                    var employee = new Employee
                    {
                        Id = id,
                        PersonId = personId,
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
