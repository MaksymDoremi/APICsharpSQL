using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVProjectAPI1502
{
    public class DataBaseConnection
    {
        public SqlConnection connection;
        //skolniConnection => PCXXX zmente XXX
        //connectionKantnerova => login a password
        //zvolte co budete mit radi
        public DataBaseConnection()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["skolniConnection"].ConnectionString);
        }

        public void CloseConnection()
        {
            connection.Close();
            connection.Dispose();
        }

    }
}
