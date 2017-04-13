using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryManagementSystem
{
    class Database
    {
        SqlConnection conn;

        public Database()
        {
            conn = new SqlConnection(@"Data Source=nedis-abbott.database.windows.net;Initial Catalog=VeterinaryDB;Persist Security Info=True;User ID=dbadmin;Password=DBveterinary2017");
            conn.Open();
        }








    }
}
