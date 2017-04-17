using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryManagementSystem.Classes;

namespace VeterinaryManagementSystem.DataAccess
{
    class ConsultationDataAccess
    {
        private string connectionString;
        private SqlConnection connection;

        public ConsultationDataAccess()
        {
            connectionString = "Server=tcp:nedis-abbott.database.windows.net,1433;Initial Catalog=VeterinaryDB;Persist Security Info=False;User ID=dbadmin;Password=DBveterinary2017;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }


        public void Add(Consultation consultation)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "INSERT INTO TBLCONSULTATION (ANIMALID, EMPLOYEEID, VACCINEID, SERVPRODID, DATE, RECORD, PRESCRIPTION, QUANTITY, COST)" +
                " VALUES (@AnimalID, @EmployeeID, @VaccineID, @ServProdID, @Date, @Record, @Prescription, @Quantity, @Cost)";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@AnimalID", consultation.AnimalID));
            command.Parameters.Add(new SqlParameter("@EmployeeID", consultation.EmployeeID));
            command.Parameters.Add(new SqlParameter("@VaccineID", consultation.VaccineID));
            command.Parameters.Add(new SqlParameter("@ServProdID", consultation.ServProdID));
            command.Parameters.Add(new SqlParameter("@Date", consultation.Date));
            command.Parameters.Add(new SqlParameter("@Record", consultation.Record));
            command.Parameters.Add(new SqlParameter("@Prescription", consultation.Prescription));
            command.Parameters.Add(new SqlParameter("@Quantity", consultation.Quantity));
            command.Parameters.Add(new SqlParameter("@Cost", consultation.Cost));

            command.ExecuteNonQuery();
            connection.Close();
        }



        public List<Consultation> GetAllConsultations()
        {
            List<Consultation> result = new List<Consultation>();
            using (SqlCommand command = new SqlCommand("SELECT * FROM TBLCONSULTATION", connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    int animalID = (int)reader["AnimalID"];
                    int employeeID = (int)reader["EmployeeID"];
                    int vaccineID = (int)reader["VaccineID"];
                    int servProdID = (int)reader["ServProdID"];
                    DateTime date = (DateTime)reader["Date"];
                    string record = (string)reader["Record"];
                    string prescription = (string)reader["Prescription"];
                    int quantity = (int)reader["Quantity"];
                    decimal cost = (decimal)reader["Cost"];

                    var consultation = new Consultation
                    {
                        Id = id,
                        AnimalID = animalID,
                        EmployeeID = employeeID,
                        VaccineID = vaccineID,
                        ServProdID = servProdID,
                        Date = date,
                        Record = record,
                        Prescription = prescription,
                        Quantity = quantity,
                        Cost = cost
                    };
                    result.Add(consultation);
                }
            }
            return result;
        }
    }
}
