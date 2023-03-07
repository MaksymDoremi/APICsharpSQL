using API;
using PVProjectAPI1502.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PVProjectAPI1502.DAO
{
    public class OrderDAO : IDAO<Order>
    {
        #region SQL Commands
        #region Create
        private const string CREATE_ORDER = "insert into [Order](Customer_ID, Datetime, Price, Delivered) values (@customerID, @datetime, @price, @delivered)";
        #endregion
        #region Read
        private const string READ_ORDER = "select * from [Order] where ID = @orderID";
        private const string READ_ALL_ORDERS = "select * from [Order]";
        #endregion
        #region Update
        private const string UPDATE_ORDER = "update [Order] set Customer_ID = @customerID, Datetime = @datetime, Price = @price, Delivered = @delivered where ID = @orderID";
        #endregion
        #region Delete
        private const string DELETE_ORDER = "delete from [Order] where ID = @orderID";
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
                using (SqlCommand cmd = new SqlCommand(DELETE_ORDER, db.connection))
                {
                    cmd.Parameters.AddWithValue("@orderID", id);
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
            List<Order> orders = new List<Order>();
            foreach (DataRow row in dataTable.Rows)
            {
                orders.Add(new Order((int)row["ID"], (int)row["Customer_ID"], (DateTime)row["Datetime"], (double)row["Price"], (bool)row["Delivered"]));
            }

            string fileName = "orderDataTable.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(orders, options);

            File.WriteAllText(fileName, jsonString);

            return jsonString;
        }

        public string ExportToJSON(List<Order> list)
        {
            string fileName = "orderList.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(list, options);

            File.WriteAllText(fileName, jsonString);

            return jsonString;
        }

        public string ExportToJSON(DataTable dataTable, string path)
        {
            List<Order> orders = new List<Order>();
            foreach (DataRow row in dataTable.Rows)
            {
                orders.Add(new Order((int)row["ID"], (int)row["Customer_ID"], (DateTime)row["Datetime"], (double)row["Price"], (bool)row["Delivered"]));
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(orders, options);

            File.WriteAllText(path, jsonString);

            return jsonString;
        }

        public string ExportToJSON(List<Order> list, string path)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(list, options);

            File.WriteAllText(path, jsonString);

            return jsonString;
        }

        public string ExportToXML(DataTable dataTable)
        {
            List<Order> orders = new List<Order>();
            foreach (DataRow row in dataTable.Rows)
            {
                orders.Add(new Order((int)row["ID"], (int)row["Customer_ID"], (DateTime)row["Datetime"], (double)row["Price"], (bool)row["Delivered"]));
            }

            string fileName = "orderDataTable.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(List<Order>), new XmlRootAttribute("Orders"));
            TextWriter writer = new StreamWriter(fileName);

            serializer.Serialize(writer, orders);
            writer.Close();

            return File.ReadAllText(fileName);

        }

        public string ExportToXML(List<Order> list)
        {
            string fileName = "orderList.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(List<Order>), new XmlRootAttribute("Orders"));
            TextWriter writer = new StreamWriter(fileName);

            serializer.Serialize(writer, list);
            writer.Close();

            return File.ReadAllText(fileName);
        }

        public string ExportToXML(DataTable dataTable, string path)
        {
            List<Order> orders = new List<Order>();
            foreach (DataRow row in dataTable.Rows)
            {
                orders.Add(new Order((int)row["ID"], (int)row["Customer_ID"], (DateTime)row["Datetime"], (double)row["Price"], (bool)row["Delivered"]));
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<Order>), new XmlRootAttribute("Orders"));
            TextWriter writer = new StreamWriter(path);

            serializer.Serialize(writer, orders);
            writer.Close();

            return File.ReadAllText(path);
        }

        public string ExportToXML(List<Order> list, string path)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(List<Order>), new XmlRootAttribute("Orders"));
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
                using (SqlDataAdapter sda = new SqlDataAdapter(new SqlCommand(READ_ALL_ORDERS, db.connection)))
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

        public List<Order>? GetAllList()
        {
            List<Order> orders = new List<Order>();
            DataTable dataTable = GetAllDatatable();
            foreach (DataRow row in dataTable.Rows)
            {
                orders.Add(new Order((int)row["ID"], (int)row["Customer_ID"], (DateTime)row["Datetime"], (double)row["Price"], (bool)row["Delivered"]));
            }
            return orders;
        }

        public Order? GetByID(int id)
        {
            DataBaseConnection db = new DataBaseConnection();

            if (db.connection.State == ConnectionState.Closed)
            {
                db.connection.Open();
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(READ_ORDER, db.connection))
                {
                    cmd.Parameters.AddWithValue("@OrderID", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    if (reader.HasRows)
                    {
                        Order order = new Order((int)reader[0], (int)reader[1], (DateTime)reader[2], (int)reader[3], (bool)reader[4]);
                        db.CloseConnection();
                        return order;
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
            Console.WriteLine("Order not implemented. Wait for the update");
        }

        public void ImportFromXML(string path)
        {
            Console.WriteLine("Order not implemented. Wait for the update");
        }

        public bool Insert(Order element)
        {
            DataBaseConnection db = new DataBaseConnection();

            if (db.connection.State == ConnectionState.Closed)
            {
                db.connection.Open();
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(CREATE_ORDER, db.connection))
                {
                    cmd.Parameters.AddWithValue("@customerID", element.Customer_ID);
                    cmd.Parameters.AddWithValue("@datetime", element.Datetime);
                    cmd.Parameters.AddWithValue("@price", element.Price);
                    cmd.Parameters.AddWithValue("@delivered", element.Delivered);



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

        public bool Update(Order element)
        {
            DataBaseConnection db = new DataBaseConnection();

            if (db.connection.State == ConnectionState.Closed)
            {
                db.connection.Open();
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(UPDATE_ORDER, db.connection))
                {
                    cmd.Parameters.AddWithValue("@orderID", element.ID);
                    cmd.Parameters.AddWithValue("@customerID", element.Customer_ID);
                    cmd.Parameters.AddWithValue("@datetime", element.Datetime);
                    cmd.Parameters.AddWithValue("@price", element.Price);
                    cmd.Parameters.AddWithValue("@delivered", element.Delivered);

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