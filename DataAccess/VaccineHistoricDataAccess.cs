using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryManagementSystem.Classes;

namespace VeterinaryManagementSystem.DataAccess
{
    public class VaccineHistoricDataAccess
    {
        private string connectionString;
        private SqlConnection connection;

        public VaccineHistoricDataAccess()
        {
            connectionString = "Server=tcp:nedis-abbott.database.windows.net,1433;Initial Catalog=VeterinaryDB;Persist Security Info=False;User ID=dbadmin;Password=DBveterinary2017;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }


        public void Add(VaccineHistoric vaccinehistoric)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "INSERT INTO TBLVACCINEHISTORIC (NAME, DATE, DETAILS) VALUES (@Name, @Date, @Details)";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@Name", vaccinehistoric.Name));
            command.Parameters.Add(new SqlParameter("@Date", vaccinehistoric.Date));
            command.Parameters.Add(new SqlParameter("@Details", vaccinehistoric.Details));

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(VaccineHistoric vaccinehistoric)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "UPDATE TBLVACCINEHISTORIC SET NAME=@Name, DATE=@Date, DETAILS=@Details  WHERE ID=@Id";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@Name", vaccinehistoric.Name));
            command.Parameters.Add(new SqlParameter("@Date", vaccinehistoric.Date));
            command.Parameters.Add(new SqlParameter("@Details", vaccinehistoric.Details));

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(VaccineHistoric vaccinehistoric)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "DELETE FROM TBLVACCINEHISTORIC WHERE ID=@Id";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@Id", vaccinehistoric.Id));

            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<VaccineHistoric> GetAllVaccineHistorics()
        {
            List<VaccineHistoric> result = new List<VaccineHistoric>();
            using (SqlCommand command = new SqlCommand("SELECT * FROM TBLVACCINEHISTORIC", connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    string name = (string)reader["Name"];
                    DateTime date = (DateTime)reader["Date"];
                    string details = (string)reader["Details"];
                    var vaccinehistoric = new VaccineHistoric
                    {
                        Id = id,
                        Name = name,
                        Date = date,
                        Details = details
                    };
                    result.Add(vaccinehistoric);
                }
            }
            return result;
        }
    }
}
