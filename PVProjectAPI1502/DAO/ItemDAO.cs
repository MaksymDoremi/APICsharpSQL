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
    public class ItemDAO : IDAO<Item>
    {
        #region SQL Commands
        #region Create
        private const string CREATE_ITEM = "insert into Item(Order_ID, Product_ID, [Count], Price) values (@orderID, @productID, @count, @price)";
        #endregion
        #region Read
        private const string READ_ITEM = "select * from Item where ID = @itemID";
        private const string READ_ALL_ITEMS = "select * from Item";
        #endregion
        #region Update
        private const string UPDATE_ITEM = "update Item set Order_ID = @orderID, Product_ID = @productID, [Count] = @count, Price = @price where ID = @itemID";
        #endregion
        #region Delete
        private const string DELETE_ITEM = "delete from Item where ID = @itemID";
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
                using (SqlCommand cmd = new SqlCommand(DELETE_ITEM, db.connection))
                {
                    cmd.Parameters.AddWithValue("@itemID", id);
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
            List<Item> items = new List<Item>();
            foreach (DataRow row in dataTable.Rows)
            {
                items.Add(new Item((int)row["ID"], (int)row["Order_ID"], (int)row["Product_ID"], (int)row["Count"], (double)row["Price"]));
            }


            string fileName = "itemDataTable.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(items, options);

            File.WriteAllText(fileName, jsonString);

            return jsonString;
        }

        public string ExportToJSON(List<Item> list)
        {
            string fileName = "itemList.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(list, options);

            File.WriteAllText(fileName, jsonString);

            return jsonString;
        }

        public string ExportToJSON(DataTable dataTable, string path)
        {
            List<Item> items = new List<Item>();
            foreach (DataRow row in dataTable.Rows)
            {
                items.Add(new Item((int)row["ID"], (int)row["Order_ID"], (int)row["Product_ID"], (int)row["Count"], (double)row["Price"]));
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(items, options);

            File.WriteAllText(path, jsonString);

            return jsonString;
        }

        public string ExportToJSON(List<Item> list, string path)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(list, options);

            File.WriteAllText(path, jsonString);

            return jsonString;
        }

        public string ExportToXML(DataTable dataTable)
        {
            List<Item> items = new List<Item>();
            foreach (DataRow row in dataTable.Rows)
            {
                items.Add(new Item((int)row["ID"], (int)row["Order_ID"], (int)row["Product_ID"], (int)row["Count"], (double)row["Price"]));
            }

            string fileName = "itemDataTable.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(List<Item>), new XmlRootAttribute("Items"));
            TextWriter writer = new StreamWriter(fileName);

            serializer.Serialize(writer, items);
            writer.Close();

            return File.ReadAllText(fileName);

        }

        public string ExportToXML(List<Item> list)
        {
            string fileName = "itemList.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(List<Item>), new XmlRootAttribute("Items"));
            TextWriter writer = new StreamWriter(fileName);

            serializer.Serialize(writer, list);
            writer.Close();

            return File.ReadAllText(fileName);
        }

        public string ExportToXML(DataTable dataTable, string path)
        {
            List<Item> items = new List<Item>();
            foreach (DataRow row in dataTable.Rows)
            {
                items.Add(new Item((int)row["ID"], (int)row["Order_ID"], (int)row["Product_ID"], (int)row["Count"], (double)row["Price"]));
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<Item>), new XmlRootAttribute("Items"));
            TextWriter writer = new StreamWriter(path);

            serializer.Serialize(writer, items);
            writer.Close();

            return File.ReadAllText(path);
        }

        public string ExportToXML(List<Item> list, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Item>), new XmlRootAttribute("Items"));
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
                using (SqlDataAdapter sda = new SqlDataAdapter(new SqlCommand(READ_ALL_ITEMS, db.connection)))
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

        public List<Item>? GetAllList()
        {
            List<Item> items = new List<Item>();
            DataTable dataTable = GetAllDatatable();
            foreach (DataRow row in dataTable.Rows)
            {
                items.Add(new Item((int)row["ID"], (int)row["Order_ID"], (int)row["Product_ID"], (int)row["Count"], (double)row["Price"]));
            }

            return items;
        }

        public Item? GetByID(int id)
        {
            DataBaseConnection db = new DataBaseConnection();

            if (db.connection.State == ConnectionState.Closed)
            {
                db.connection.Open();
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(READ_ITEM, db.connection))
                {
                    cmd.Parameters.AddWithValue("@itemID", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    if (reader.HasRows)
                    {
                        Item item = new Item((int)reader[0], (int)reader[1], (int)reader[2], (int)reader[3], (double)reader[4]);
                        db.CloseConnection();
                        return item;
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
            Console.WriteLine("Item not implemented yet.");
        }

        public void ImportFromXML(string path)
        {
            Console.WriteLine("Item not implemented yet.");
        }

        public bool Insert(Item element)
        {
            DataBaseConnection db = new DataBaseConnection();

            if (db.connection.State == ConnectionState.Closed)
            {
                db.connection.Open();
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(CREATE_ITEM, db.connection))
                {
                    cmd.Parameters.AddWithValue("@orderID", element.Order_ID);
                    cmd.Parameters.AddWithValue("@productID", element.Product_ID);
                    cmd.Parameters.AddWithValue("@count", element.Count);
                    cmd.Parameters.AddWithValue("@price", element.Price);

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

        public bool Update(Item element)
        {
            DataBaseConnection db = new DataBaseConnection();

            if (db.connection.State == ConnectionState.Closed)
            {
                db.connection.Open();
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(UPDATE_ITEM, db.connection))
                {
                    cmd.Parameters.AddWithValue("@itemID", element.ID);
                    cmd.Parameters.AddWithValue("@orderID", element.Order_ID);
                    cmd.Parameters.AddWithValue("@productID", element.Product_ID);
                    cmd.Parameters.AddWithValue("@count", element.Count);
                    cmd.Parameters.AddWithValue("@price", element.Price);

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
