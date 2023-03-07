using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using API;

namespace PVProjectAPI1502.DAO
{
    public class CreateDatabase
    {
        /// <summary>
        /// Creates database with all entities
        /// </summary>
        /// <param name="path">Path where is your sql query</param>
        /// <returns>True if success</returns>
        public static bool CreateDatabaseQuery(string path)
        {
            string query = File.ReadAllText(path);
            //Console.WriteLine(query);
            DataBaseConnection db = new DataBaseConnection();

            if (db.connection.State == ConnectionState.Closed)
            {
                db.connection.Open();
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, db.connection))
                {
                    cmd.ExecuteNonQuery();
                    db.CloseConnection();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Program.WriteErrorToXml(ex, Program.LoggerPath);
                db.CloseConnection();
                return false;
            }
        }
    }
}
