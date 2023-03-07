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
    public class ManufacturerDAO : IDAO<Manufacturer>
    {
        #region SQL Commands
        #region Create
        private const string CREATE_MANUFACTURER = "insert into Manufacturer(Name, Email) values (@name, @email)";
        #endregion
        #region Read
        private const string READ_MANUFACTURER = "select * from Manufacturer where ID = @manufacturerID";
        private const string READ_ALL_MANUFACTURERS = "select * from Manufacturer";
        #endregion
        #region Update
        private const string UPDATE_MANUFACTURER = "update Manufacturer set Name = @name, Email = @email where ID = @manufacturerID";
        #endregion
        #region Delete
        private const string DELETE_MANUFACTURER = "delete from Manufacturer where ID = @manufacturerID";
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
                using (SqlCommand cmd = new SqlCommand(DELETE_MANUFACTURER, db.connection))
                {
                    cmd.Parameters.AddWithValue("@manufacturerID", id);
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
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            foreach (DataRow row in dataTable.Rows)
            {
                manufacturers.Add(new Manufacturer((int)row["ID"], (string)row["Name"], (string)row["Email"]));
            }


            string fileName = "manufacturerDataTable.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(manufacturers, options);

            File.WriteAllText(fileName, jsonString);

            return jsonString;
        }

        public string ExportToJSON(List<Manufacturer> list)
        {
            string fileName = "manufacturerList.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(list, options);

            File.WriteAllText(fileName, jsonString);

            return jsonString;
        }

        public string ExportToJSON(DataTable dataTable, string path)
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            foreach (DataRow row in dataTable.Rows)
            {
                manufacturers.Add(new Manufacturer((int)row["ID"], (string)row["Name"], (string)row["Email"]));
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(manufacturers, options);

            File.WriteAllText(path, jsonString);

            return jsonString;
        }

        public string ExportToJSON(List<Manufacturer> list, string path)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(list, options);

            File.WriteAllText(path, jsonString);

            return jsonString;
        }

        public string ExportToXML(DataTable dataTable)
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            foreach (DataRow row in dataTable.Rows)
            {
                manufacturers.Add(new Manufacturer((int)row["ID"], (string)row["Name"], (string)row["Email"]));
            }

            string fileName = "manufacturerDataTable.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(List<Manufacturer>), new XmlRootAttribute("Manufacturers"));
            TextWriter writer = new StreamWriter(fileName);

            serializer.Serialize(writer, manufacturers);
            writer.Close();

            return File.ReadAllText(fileName);

        }

        public string ExportToXML(List<Manufacturer> list)
        {
            string fileName = "manufacturerList.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(List<Manufacturer>), new XmlRootAttribute("Manufacturers"));
            TextWriter writer = new StreamWriter(fileName);

            serializer.Serialize(writer, list);
            writer.Close();

            return File.ReadAllText(fileName);
        }

        public string ExportToXML(DataTable dataTable, string path)
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            foreach (DataRow row in dataTable.Rows)
            {
                manufacturers.Add(new Manufacturer((int)row["ID"], (string)row["Name"], (string)row["Email"]));
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<Manufacturer>), new XmlRootAttribute("Manufacturers"));
            TextWriter writer = new StreamWriter(path);

            serializer.Serialize(writer, manufacturers);
            writer.Close();

            return File.ReadAllText(path);
        }

        public string ExportToXML(List<Manufacturer> list, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Manufacturer>), new XmlRootAttribute("Manufacturers"));
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
                using (SqlDataAdapter sda = new SqlDataAdapter(new SqlCommand(READ_ALL_MANUFACTURERS, db.connection)))
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

        public List<Manufacturer>? GetAllList()
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            DataTable dataTable = GetAllDatatable();
            foreach (DataRow row in dataTable.Rows)
            {
                manufacturers.Add(new Manufacturer((int)row["ID"], (string)row["Name"], (string)row["Email"]));
            }

            return manufacturers;
        }

        public Manufacturer? GetByID(int id)
        {
            DataBaseConnection db = new DataBaseConnection();

            if (db.connection.State == ConnectionState.Closed)
            {
                db.connection.Open();
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(READ_MANUFACTURER, db.connection))
                {
                    cmd.Parameters.AddWithValue("@manufacturerID", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    if (reader.HasRows)
                    {
                        Manufacturer manufacturer = new Manufacturer((int)reader[0], (string)reader[1], (string)reader[2]);
                        db.CloseConnection();
                        return manufacturer;
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
            List<Manufacturer> manufacturers = JsonSerializer.Deserialize<List<Manufacturer>>(jsonString);

            Console.WriteLine(jsonString);

            foreach (Manufacturer m in manufacturers)
            {
                Insert(m);
            }
        }

        public void ImportFromXML(string path)
        {
            //set root for the xml file, cause it will fail deserializing
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Manufacturers";
            xRoot.IsNullable = true;


            XmlSerializer serializer = new XmlSerializer(typeof(List<Manufacturer>), xRoot);

            serializer.UnknownNode += new
            XmlNodeEventHandler(Program.serializer_UnknownNode);
            serializer.UnknownAttribute += new
            XmlAttributeEventHandler(Program.serializer_UnknownAttribute);

            List<Manufacturer> manufacturers = (List<Manufacturer>)serializer.Deserialize(new FileStream(path, FileMode.Open));

            foreach (Manufacturer m in manufacturers)
            {       
                Insert(m);
            }
        }

        public bool Insert(Manufacturer element)
        {
            DataBaseConnection db = new DataBaseConnection();

            if (db.connection.State == ConnectionState.Closed)
            {
                db.connection.Open();
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(CREATE_MANUFACTURER, db.connection))
                {
                    cmd.Parameters.AddWithValue("@name", element.Name);
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

        public bool Update(Manufacturer element)
        {
            DataBaseConnection db = new DataBaseConnection();

            if (db.connection.State == ConnectionState.Closed)
            {
                db.connection.Open();
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(UPDATE_MANUFACTURER, db.connection))
                {
                    cmd.Parameters.AddWithValue("@manufacturerID", element.ID);
                    cmd.Parameters.AddWithValue("@name", element.Name);
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
