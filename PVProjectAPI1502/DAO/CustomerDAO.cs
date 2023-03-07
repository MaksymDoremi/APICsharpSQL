using API;
using PVProjectAPI1502.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PVProjectAPI1502.DAO
{
    public class CustomerDAO : IDAO<Customer>
    {
        #region SQL Commands
        #region Create
        private const string CREATE_CUSTOMER = "insert into Customer(Name, Surname, Email) values(@name, @surname, @email)";
        #endregion
        #region Read
        private const string READ_CUSTOMER = "select * from Customer where ID = @customerID";
        private const string READ_ALL_CUSTOMERS = "select * from Customer";
        #endregion
        #region Update
        private const string UPDATE_CUSTOMER = "update Customer set Name = @name, Surname = @surname, Email = @email Where ID = @customerID";
        #endregion
        #region Delete
        private const string DELETE_CUSTOMER = "delete from Customer where ID = @customerID";
        #endregion
        #endregion

        public bool Delete(int id)
        {
            DataBaseConnection db = new DataBaseConnection();

            if (db.connection.State == ConnectionState.Closed)
            {
                db.connection.Open();
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(DELETE_CUSTOMER, db.connection))
                {
                    cmd.Parameters.AddWithValue("@customerID", id);
                    cmd.ExecuteNonQuery();
                }
                db.CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                Program.WriteErrorToXml(ex, Program.LoggerPath);
                db.CloseConnection();
                return false;

            }

        }

        public string ExportToJSON(DataTable dataTable)
        {
            List<Customer> customers = new List<Customer>();
            foreach (DataRow row in dataTable.Rows)
            {
                customers.Add(new Customer((int)row["ID"], (string)row["Name"], (string)row["Surname"], (string)row["Email"]));
            }

            string fileName = "customerDataTable.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(customers, options);

            File.WriteAllText(fileName, jsonString);

            return jsonString;
        }

        public string ExportToJSON(List<Customer> list)
        {
            string fileName = "customerList.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(list, options);

            File.WriteAllText(fileName, jsonString);

            return jsonString;
        }

        public string ExportToJSON(DataTable dataTable, string path)
        {
            List<Customer> customers = new List<Customer>();
            foreach (DataRow row in dataTable.Rows)
            {
                customers.Add(new Customer((int)row["ID"], (string)row["Name"], (string)row["Surname"], (string)row["Email"]));
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(customers, options);

            File.WriteAllText(path, jsonString);

            return jsonString;
        }

        public string ExportToJSON(List<Customer> list, string path)
        {  
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(list, options);

            File.WriteAllText(path, jsonString);

            return jsonString;
        }

        public string ExportToXML(DataTable dataTable)
        {
            List<Customer> customers = new List<Customer>();
            foreach (DataRow row in dataTable.Rows)
            {
                customers.Add(new Customer((int)row["ID"], (string)row["Name"], (string)row["Surname"], (string)row["Email"]));
            }


            string fileName = "customerDataTable.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(List<Customer>), new XmlRootAttribute("Customers"));
            TextWriter writer = new StreamWriter(fileName);

            serializer.Serialize(writer, customers);
            writer.Close();

            return File.ReadAllText(fileName);

        }

        public string ExportToXML(List<Customer> list)
        {
            string fileName = "customerList.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(List<Customer>), new XmlRootAttribute("Customers"));
            TextWriter writer = new StreamWriter(fileName);

            serializer.Serialize(writer, list);
            writer.Close();

            return File.ReadAllText(fileName);
        }

        public string ExportToXML(DataTable dataTable, string path)
        {
            List<Customer> customers = new List<Customer>();
            foreach (DataRow row in dataTable.Rows)
            {
                customers.Add(new Customer((int)row["ID"], (string)row["Name"], (string)row["Surname"], (string)row["Email"]));
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<Customer>), new XmlRootAttribute("Customers"));
            TextWriter writer = new StreamWriter(path);

            serializer.Serialize(writer, customers);
            writer.Close();

            return File.ReadAllText(path);
        }

        public string ExportToXML(List<Customer> list, string path)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(List<Customer>), new XmlRootAttribute("Customers"));
            TextWriter writer = new StreamWriter(path);

            serializer.Serialize(writer, list);
            writer.Close();

            return File.ReadAllText(path);
        }

        public DataTable? GetAllDatatable()
        {
            DataBaseConnection db = new DataBaseConnection();

            if (db.connection.State == ConnectionState.Closed)
            {
                db.connection.Open();
            }

            try
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(new SqlCommand(READ_ALL_CUSTOMERS, db.connection)))
                {
                    DataTable dt = new DataTable();

                    sda.Fill(dt);
                    db.CloseConnection();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Program.WriteErrorToXml(ex, Program.LoggerPath);
                db.CloseConnection();
                return null;
            }
        }

        public List<Customer>? GetAllList()
        {
            List<Customer> customers = new List<Customer>();
            DataTable dataTable = GetAllDatatable();
            foreach (DataRow row in dataTable.Rows)
            {
                customers.Add(new Customer((int)row["ID"], (string)row["Name"], (string)row["Surname"], (string)row["Email"]));
            }

            return customers;
        }

        public Customer? GetByID(int id)
        {
            DataBaseConnection db = new DataBaseConnection();

            if (db.connection.State == ConnectionState.Closed)
            {
                db.connection.Open();
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(READ_CUSTOMER, db.connection))
                {
                    cmd.Parameters.AddWithValue("@customerID", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    if (reader.HasRows)
                    {
                        Customer customer = new Customer((int)reader[0], (string)reader[1], (string)reader[2], (string)reader[3]);
                        db.CloseConnection();
                        return customer;
                    }
                    db.CloseConnection();
                    return null;
                }
            }
            catch (Exception ex)
            {
                Program.WriteErrorToXml(ex, Program.LoggerPath);
                db.CloseConnection();
                return null;
            }
        }

        public void ImportFromJSON(string path)
        {
            string jsonString = "";
            jsonString = File.ReadAllText(path);
            List<Customer> customers = JsonSerializer.Deserialize<List<Customer>>(jsonString);

            Console.WriteLine(jsonString);

            foreach (Customer c in customers)
            {
                Insert(c);
            }
        }

        public void ImportFromXML(string path)
        {
            //set root for the xml file, cause it will fail deserializing
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Customers";
            xRoot.IsNullable = true;


            XmlSerializer serializer = new XmlSerializer(typeof(List<Customer>), xRoot);

            serializer.UnknownNode += new
            XmlNodeEventHandler(Program.serializer_UnknownNode);
            serializer.UnknownAttribute += new
            XmlAttributeEventHandler(Program.serializer_UnknownAttribute);

            List<Customer> customers = (List<Customer>)serializer.Deserialize(new FileStream(path, FileMode.Open));

            foreach (Customer c in customers)
            {
                Insert(c);
            }
        }

        public bool Insert(Customer element)
        {
            DataBaseConnection db = new DataBaseConnection();

            if (db.connection.State == ConnectionState.Closed)
            {
                db.connection.Open();
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(CREATE_CUSTOMER, db.connection))
                {
                    cmd.Parameters.AddWithValue("@name", element.Name);
                    cmd.Parameters.AddWithValue("@surname", element.Surname);
                    cmd.Parameters.AddWithValue("@email", element.Email);

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

        public bool Update(Customer element)
        {
            DataBaseConnection db = new DataBaseConnection();

            if (db.connection.State == ConnectionState.Closed)
            {
                db.connection.Open();
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(UPDATE_CUSTOMER, db.connection))
                {
                    cmd.Parameters.AddWithValue("@customerID", element.ID);
                    cmd.Parameters.AddWithValue("@name", element.Name);
                    cmd.Parameters.AddWithValue("@surname", element.Surname);
                    cmd.Parameters.AddWithValue("@email", element.Email);

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
