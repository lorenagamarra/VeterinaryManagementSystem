using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryManagementSystem.Classes;

namespace VeterinaryManagementSystem.DataAccess
{
    public class ServicesProductsDataAccess
    {
        private string connectionString;
        private SqlConnection connection;

        public ServicesProductsDataAccess()
        {
            connectionString = "Server=tcp:nedis-abbott.database.windows.net,1433;Initial Catalog=VeterinaryDB;Persist Security Info=False;User ID=dbadmin;Password=DBveterinary2017;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }


        public void Add(ServicesProducts servicesproducts)
        {
            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "INSERT INTO TBLSERVICESPRODUCTS (NAME, PRICE, STATUS) VALUES (@Name, @Price, @Status)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Name", servicesproducts.Name));
                    command.Parameters.Add(new SqlParameter("@Price", servicesproducts.Price));
                    command.Parameters.Add(new SqlParameter("@Status", servicesproducts.Status));
                    command.ExecuteNonQuery();
                }
            } //close and dispose --> connection
        }

        public void Update(ServicesProducts servicesproducts)
        {
            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "UPDATE TBLSERVICESPRODUCTS SET NAME=@Name, PRICE=@Price, STATUS=@Status WHERE ID=@Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", servicesproducts.Id));
                    command.Parameters.Add(new SqlParameter("@Name", servicesproducts.Name));
                    command.Parameters.Add(new SqlParameter("@Price", servicesproducts.Price));
                    command.Parameters.Add(new SqlParameter("@Status", servicesproducts.Status));
                    command.ExecuteNonQuery();
                }
            } //close and dispose --> connection
        }

        public void Delete(ServicesProducts servicesproducts)
        {
            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "DELETE FROM TBLSERVICESPRODUCTS WHERE ID=@Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", servicesproducts.Id));
                    command.ExecuteNonQuery();
                }
            } //close and dispose --> connection
        }

        public List<ServicesProducts> GetAllServicesProductsActives()
        {
            var result = new List<ServicesProducts>();

            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM TBLSERVICESPRODUCTS WHERE STATUS=1", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        string name = (string)reader["Name"];
                        decimal price = (decimal)reader["Price"];

                        var servicesproducts = new ServicesProducts
                        {
                            Id = id,
                            Name = name,
                            Price = price
                        };
                        result.Add(servicesproducts);
                    }
                }
            } //close and dispose --> connection
            return result;
        }

        public List<ServicesProducts> GetAllServicesProducts()
        {
            var result = new List<ServicesProducts>();

            //'using' block calls Dispose method at the end of the structure.
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM TBLSERVICESPRODUCTS", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        string name = (string)reader["Name"];
                        decimal price = (decimal)reader["Price"];
                        Boolean status = (Boolean)reader["Status"];

                        var servicesproducts = new ServicesProducts
                        {
                            Id = id,
                            Name = name,
                            Price = price,
                            Status = status
                        };
                        result.Add(servicesproducts);
                    }
                }
            } //close and dispose --> connection
            return result;
        }
    }
}
