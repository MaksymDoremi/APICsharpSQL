using API;
using PVProjectAPI1502.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Type = PVProjectAPI1502.Objects.Type;

namespace PVProjectAPI1502.DAO
{
    public class TypeDAO : IDAO<Type>
    {
        #region SQL Commands
        #region Create
        private const string CREATE_TYPE = "insert into Type(Name) values (@name)";
        #endregion
        #region Read
        private const string READ_TYPE = "select * from Type where ID = @typeID";
        private const string READ_ALL_TYPES = "select * from Type";
        #endregion
        #region Update
        private const string UPDATE_TYPE = "update Type set Name = @name where ID = @typeID";
        #endregion
        #region Delete
        private const string DELETE_TYPE = "delete from Type where ID = @typeID";
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
                using (SqlCommand cmd = new SqlCommand(DELETE_TYPE, db.connection))
                {
                    cmd.Parameters.AddWithValue("@typeID", id);
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
            List<Type> types = new List<Type>();
            foreach (DataRow row in dataTable.Rows)
            {
                types.Add(new Type((int)row["ID"], (string)row["Name"]));
            }


            string fileName = "typeDataTable.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(types, options);

            File.WriteAllText(fileName, jsonString);

            return jsonString;
        }

        public string ExportToJSON(List<Type> list)
        {
            string fileName = "typeList.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(list, options);

            File.WriteAllText(fileName, jsonString);

            return jsonString;
        }

        public string ExportToJSON(DataTable dataTable, string path)
        {
            List<Type> types = new List<Type>();
            foreach (DataRow row in dataTable.Rows)
            {
                types.Add(new Type((int)row["ID"], (string)row["Name"]));
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(types, options);

            File.WriteAllText(path, jsonString);

            return jsonString;
        }

        public string ExportToJSON(List<Type> list, string path)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(list, options);

            File.WriteAllText(path, jsonString);

            return jsonString;
        }

        public string ExportToXML(DataTable dataTable)
        {
            List<Type> types = new List<Type>();
            foreach (DataRow row in dataTable.Rows)
            {
                types.Add(new Type((int)row["ID"], (string)row["Name"]));
            }

            string fileName = "typeDataTable.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(List<Type>), new XmlRootAttribute("Types"));
            TextWriter writer = new StreamWriter(fileName);

            serializer.Serialize(writer, types);
            writer.Close();

            return File.ReadAllText(fileName);

        }

        public string ExportToXML(List<Type> list)
        {
            string fileName = "typeList.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(List<Type>), new XmlRootAttribute("Types"));
            TextWriter writer = new StreamWriter(fileName);

            serializer.Serialize(writer, list);
            writer.Close();

            return File.ReadAllText(fileName);
        }

        public string ExportToXML(DataTable dataTable, string path)
        {
            List<Type> types = new List<Type>();
            foreach (DataRow row in dataTable.Rows)
            {
                types.Add(new Type((int)row["ID"], (string)row["Name"]));
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<Type>), new XmlRootAttribute("Types"));
            TextWriter writer = new StreamWriter(path);

            serializer.Serialize(writer, types);
            writer.Close();

            return File.ReadAllText(path);
        }

        public string ExportToXML(List<Type> list, string path)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(List<Type>), new XmlRootAttribute("Types"));
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
                using (SqlDataAdapter sda = new SqlDataAdapter(new SqlCommand(READ_ALL_TYPES, db.connection)))
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

        public List<Type>? GetAllList()
        {
            List<Type> types = new List<Type>();

            DataTable dataTable = GetAllDatatable();

            foreach (DataRow row in dataTable.Rows)
            {
                types.Add(new Type((int)row["ID"], (string)row["Name"]));
            }
            return types;
        }

        public Type? GetByID(int id)
        {
            DataBaseConnection db = new DataBaseConnection();

            if (db.connection.State == ConnectionState.Closed)
            {
                db.connection.Open();
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(READ_TYPE, db.connection))
                {
                    cmd.Parameters.AddWithValue("@typeID", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    if (reader.HasRows)
                    {
                        Type type = new Type((int)reader[0], (string)reader[1]);
                        db.CloseConnection();
                        return type;
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
            List<Type> types = JsonSerializer.Deserialize<List<Type>>(jsonString);

            Console.WriteLine(jsonString);

            foreach (Type t in types)
            {
                //Console.WriteLine(t);
                Insert(t);
            }
        }

        public void ImportFromXML(string path)
        {
            //set root for the xml file, cause it will fail deserializing
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Types";
            xRoot.IsNullable = true;

          
            XmlSerializer serializer = new XmlSerializer(typeof(List<Type>), xRoot);

            serializer.UnknownNode += new
            XmlNodeEventHandler(Program.serializer_UnknownNode);
            serializer.UnknownAttribute += new
            XmlAttributeEventHandler(Program.serializer_UnknownAttribute);

            List<Type> types = (List<Type>)serializer.Deserialize(new FileStream(path, FileMode.Open));

            foreach (Type t in types)
            {
                //Console.WriteLine(t);   
                Insert(t);
            }
        }

        public bool Insert(Type element)
        {
            DataBaseConnection db = new DataBaseConnection();

            if (db.connection.State == ConnectionState.Closed)
            {
                db.connection.Open();
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(CREATE_TYPE, db.connection))
                {
                    cmd.Parameters.AddWithValue("@name", element.Name);

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

        public bool Update(Type element)
        {
            DataBaseConnection db = new DataBaseConnection();

            if (db.connection.State == ConnectionState.Closed)
            {
                db.connection.Open();
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(UPDATE_TYPE, db.connection))
                {
                    cmd.Parameters.AddWithValue("@typeID", element.ID);
                    cmd.Parameters.AddWithValue("@name", element.Name);


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