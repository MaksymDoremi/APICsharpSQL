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
    public class ProductDAO : IDAO<Product>
    {
        #region SQL Commands
        #region Create
        private const string CREATE_PRODUCT = "insert into Product(Manufacturer_ID, Type_ID, Name, Price) values (@manufacturerID, @typeID, @name, @price)";
        private const string CREATE_PRODUCT_EXPORT_EDITION = "insert into Product(Manufacturer_ID, Type_ID, Name, Price) values ((select Manufacturer.ID from Manufacturer where Manufacturer.Name = @manufacturer_name), (select Type.ID from Type where Type.Name = @type_name), @name, @price)";
        #endregion
        #region Read
        private const string READ_PRODUCT = "select * from Product where ID = @productID";
        private const string READ_ALL_PRODUCTS = "select * from Product";
        private const string READ_ALL_PRODUCTS_EXPORT_EDITION = "select m.Name as Manufacturer_Name, t.Name as Type_Name, p.ID, p.Name, p.Price from Product p join Manufacturer m on p.Manufacturer_ID = m.ID join Type t on p.Type_ID = t.ID";
        #endregion
        #region Update
        private const string UPDATE_PRODUCT = "update Product set Manufacturer_ID = @manufacturerID, Type_ID = @typeID, Name = @name, Price = @price where ID = @productID";
        #endregion
        #region Delete
        private const string DELETE_PRODUCT = "delete from Product where ID = @productID";
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
                using (SqlCommand cmd = new SqlCommand(DELETE_PRODUCT, db.connection))
                {
                    cmd.Parameters.AddWithValue("@productID", id);
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
        /// <summary>
        /// Use GetAllDatatableExportEdition
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public string ExportToJSON(DataTable dataTable)
        {
            List<Product> products = new List<Product>();
            foreach (DataRow row in dataTable.Rows)
            {
                products.Add(new Product((int)row["ID"], (string)row["Manufacturer_Name"], (string)row["Type_Name"], (string)row["Name"], (double)row["Price"]));
            }


            string fileName = "productDataTable.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(products, options);

            File.WriteAllText(fileName, jsonString);

            return jsonString;
        }
        /// <summary>
        /// Use GetAllListExportEdition
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string ExportToJSON(List<Product> list)
        {
            string fileName = "productList.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(list, options);

            File.WriteAllText(fileName, jsonString);

            return jsonString;
        }
        /// <summary>
        /// Use GetAllDatatableExportEdition
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public string ExportToJSON(DataTable dataTable, string path)
        {
            List<Product> products = new List<Product>();
            foreach (DataRow row in dataTable.Rows)
            {
                products.Add(new Product((int)row["ID"], (string)row["Manufacturer_Name"], (string)row["Type_Name"], (string)row["Name"], (double)row["Price"]));
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(products, options);

            File.WriteAllText(path, jsonString);

            return jsonString;
        }
        /// <summary>
        /// Use GetAllListExportEdition
        /// </summary>
        /// <param name="list"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ExportToJSON(List<Product> list, string path)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(list, options);

            File.WriteAllText(path, jsonString);

            return jsonString;
        }
        /// <summary>
        /// Use GetAllDatatableExportEdition
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public string ExportToXML(DataTable dataTable)
        {
            List<Product> products = new List<Product>();
            foreach (DataRow row in dataTable.Rows)
            {
                products.Add(new Product((int)row["ID"], (string)row["Manufacturer_Name"], (string)row["Type_Name"], (string)row["Name"], (double)row["Price"]));
            }

            string fileName = "productDataTable.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(List<Product>), new XmlRootAttribute("Products"));
            TextWriter writer = new StreamWriter(fileName);

            serializer.Serialize(writer, products);
            writer.Close();

            return File.ReadAllText(fileName);

        }
        /// <summary>
        /// Use GetAllListExportEdition
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string ExportToXML(List<Product> list)
        {
            string fileName = "productList.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(List<Product>), new XmlRootAttribute("Products"));
            TextWriter writer = new StreamWriter(fileName);

            serializer.Serialize(writer, list);
            writer.Close();

            return File.ReadAllText(fileName);
        }
        /// <summary>
        /// Use GetAllDatatableExportEdition
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ExportToXML(DataTable dataTable, string path)
        {
            List<Product> products = new List<Product>();
            foreach (DataRow row in dataTable.Rows)
            {
                products.Add(new Product((int)row["ID"], (string)row["Manufacturer_Name"], (string)row["Type_Name"], (string)row["Name"], (double)row["Price"]));
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<Product>), new XmlRootAttribute("Products"));
            TextWriter writer = new StreamWriter(path);

            serializer.Serialize(writer, products);
            writer.Close();

            return File.ReadAllText(path);
        }
        /// <summary>
        /// Use GetAllListExportEdition
        /// </summary>
        /// <param name="list"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ExportToXML(List<Product> list, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Product>), new XmlRootAttribute("Products"));
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
                using (SqlDataAdapter sda = new SqlDataAdapter(new SqlCommand(READ_ALL_PRODUCTS, db.connection)))
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
        public DataTable? GetAllDatatableExportEdition()
        {
            DataBaseConnection db = new DataBaseConnection();

            if (db.connection.State == ConnectionState.Closed)
            {
                db.connection.Open();
            }

            try
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(new SqlCommand(READ_ALL_PRODUCTS_EXPORT_EDITION, db.connection)))
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

        public List<Product>? GetAllList()
        {
            List<Product> products = new List<Product>();
            DataTable dataTable = GetAllDatatable();
            foreach (DataRow row in dataTable.Rows)
            {
                products.Add(new Product((int)row["ID"], (int)row["Manufacturer_ID"], (int)row["Type_ID"], (string)row["Name"], (double)row["Price"]));
            }
            return products;
        }

        public List<Product>? GetAllListExportEdition()
        {
            List<Product> products = new List<Product>();
            DataTable dataTable = GetAllDatatableExportEdition();
            foreach (DataRow row in dataTable.Rows)
            {
                products.Add(new Product((int)row["ID"], (string)row["Manufacturer_Name"], (string)row["Type_Name"], (string)row["Name"], (double)row["Price"]));
            }
            return products;
        }

        public Product? GetByID(int id)
        {
            DataBaseConnection db = new DataBaseConnection();

            if (db.connection.State == ConnectionState.Closed)
            {
                db.connection.Open();
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(READ_PRODUCT, db.connection))
                {
                    cmd.Parameters.AddWithValue("@productID", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    if (reader.HasRows)
                    {
                        Product product = new Product((int)reader[0], (int)reader[1], (int)reader[2], (string)reader[3], (double)reader[4]);
                        db.CloseConnection();
                        return product;
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
            List<Product> products = JsonSerializer.Deserialize<List<Product>>(jsonString);

            Console.WriteLine(jsonString);

            foreach (Product p in products)
            {
                Console.WriteLine(p);
                InsertExportEdition(p);
            }
        }

        public void ImportFromXML(string path)
        {
            //set root for the xml file, cause it will fail deserializing
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Products";
            xRoot.IsNullable = true;


            XmlSerializer serializer = new XmlSerializer(typeof(List<Product>), xRoot);

            serializer.UnknownNode += new
            XmlNodeEventHandler(Program.serializer_UnknownNode);
            serializer.UnknownAttribute += new
            XmlAttributeEventHandler(Program.serializer_UnknownAttribute);

            List<Product> products = (List<Product>)serializer.Deserialize(new FileStream(path, FileMode.Open));

            foreach (Product p in products)
            {
                Console.WriteLine(p);   
                InsertExportEdition(p);
            }
        }

        public bool Insert(Product element)
        {
            DataBaseConnection db = new DataBaseConnection();

            if (db.connection.State == ConnectionState.Closed)
            {
                db.connection.Open();
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(CREATE_PRODUCT, db.connection))
                {
                    cmd.Parameters.AddWithValue("@manufacturerID", element.Manufacturer_ID);
                    cmd.Parameters.AddWithValue("@typeID", element.Type_ID);
                    cmd.Parameters.AddWithValue("@name", element.Name);
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
        public bool InsertExportEdition(Product element)
        {
            DataBaseConnection db = new DataBaseConnection();

            if (db.connection.State == ConnectionState.Closed)
            {
                db.connection.Open();
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(CREATE_PRODUCT_EXPORT_EDITION, db.connection))
                {
                    cmd.Parameters.AddWithValue("@manufacturer_name", element.Manufacturer_Name);
                    cmd.Parameters.AddWithValue("@type_name", element.Type_Name);
                    cmd.Parameters.AddWithValue("@name", element.Name);
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

        public bool Update(Product element)
        {
            DataBaseConnection db = new DataBaseConnection();

            if (db.connection.State == ConnectionState.Closed)
            {
                db.connection.Open();
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(UPDATE_PRODUCT, db.connection))
                {
                    cmd.Parameters.AddWithValue("@productID", element.ID);
                    cmd.Parameters.AddWithValue("@manufacturerID", element.Manufacturer_ID);
                    cmd.Parameters.AddWithValue("@typeID", element.Type_ID);
                    cmd.Parameters.AddWithValue("@name", element.Name);
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