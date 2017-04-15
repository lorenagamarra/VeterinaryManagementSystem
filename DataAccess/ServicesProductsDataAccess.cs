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
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "INSERT INTO TBLSERVICES&PRODUCTS (NAME, PRICE) VALUES (@Name, @Price)";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@Name", servicesproducts.Name));
            command.Parameters.Add(new SqlParameter("@Price", servicesproducts.Price));

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(ServicesProducts servicesproducts)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "UPDATE TBLSERVICES&PRODUCTS SET NAME=@Name, PRICE=@Price WHERE ID=@Id";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@Id", servicesproducts.Id));
            command.Parameters.Add(new SqlParameter("@Name", servicesproducts.Name));
            command.Parameters.Add(new SqlParameter("@Price", servicesproducts.Price));

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(ServicesProducts servicesproducts)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = "DELETE FROM TBLSERVICES&PRODUCTS WHERE ID=@Id";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("@Id", servicesproducts.Id));

            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<ServicesProducts> GetAllServicesProducts()
        {
            List<ServicesProducts> result = new List<ServicesProducts>();
            using (SqlCommand command = new SqlCommand("SELECT * FROM TBLSERVICES&PRODUCTS", connection))
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
            return result;
        }
    }
}
