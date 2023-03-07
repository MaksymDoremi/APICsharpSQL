using PVProjectAPI1502.DAO;
using PVProjectAPI1502.Objects;
using System.Data;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Type = PVProjectAPI1502.Objects.Type;

namespace API
{
    public class Program
    {
        /// <summary>
        /// There are logged every errors when they occur
        /// </summary>
        public const string LoggerPath = "errorLog.xml";
        public static void Main(string[] args)
        {
            // v App.config
            //pred spustenim si pripojte pocitacovou localni database, PCXXX , XXX je cislo. 
            // a vytvorte database "Firma", nasledne se provede prikaz nize 
            // sql query is here PVProjectAPI1502\PVProjectAPI1502\bin\Debug\net6.0\
            CreateDatabase.CreateDatabaseQuery("SQLQuery1.sql");

            TypeDAO typeDAO = new TypeDAO();
            CustomerDAO customerDAO = new CustomerDAO();
            ManufacturerDAO manufacturerDAO = new ManufacturerDAO();
            //INDEPENDENT ENTITIES
            /*
            typeDAO.Insert(new Type("car"));
            typeDAO.Insert(new Type("clothes"));
            typeDAO.Insert(new Type("food"));
            typeDAO.Insert(new Type("guns"));
            */
            /*
            customerDAO.Insert(new Customer("Karel", "Hlavacek", "Karel@hlavacek.com"));       
            customerDAO.Insert(new Customer("Maksym", "Jilovsky", "maksym@gmail.com"));
            customerDAO.Insert(new Customer("Honza", "Darebak", "honzuvmail.com"));
            */
            /*
            manufacturerDAO.Insert(new Manufacturer("Skoda", "skoda.gmail.com"));
            manufacturerDAO.Insert(new Manufacturer("BMW", "bmw.gmail.com"));
            manufacturerDAO.Insert(new Manufacturer("Wolfswagen", "wolsfwagen.gmail.com"));
            */

            ProductDAO productDAO = new ProductDAO();
            /*
            //let's make cars 
            productDAO.Insert(new Product(1, 1, "Octavia III", 100));
            productDAO.Insert(new Product(1, 1, "Octavia II", 200));
            productDAO.Insert(new Product(1, 1, "Octavia I", 300));
            */
            

            OrderDAO orderDAO= new OrderDAO();

            //orderDAO.Insert(new Order(1,DateTime.Now,600,false));
            

            ItemDAO itemDAO = new ItemDAO();
            /*
            itemDAO.Insert(new Item(1, 1, 1, 100));
            itemDAO.Insert(new Item(1, 2, 1, 200));
            itemDAO.Insert(new Item(1, 3, 1, 300));
            */
            /*
            Item i = itemDAO.GetByID(4);
            Console.WriteLine(i);
            itemDAO.Update(new Item(4, 1, 1, 1, 350));
            i = itemDAO.GetByID(4);
            Console.WriteLine(i);
            */

            


            //productDAO.ExportToJSON(productDAO.GetAllDatatableExportEdition());
            //productDAO.ImportFromJSON("productDataTable.json");
            //test exportu
            //JSON
            //typeDAO.ExportToJSON(typeDAO.GetAllDatatable());
            //typeDAO.ExportToJSON(typeDAO.GetAllList());
            //JSON with custom path
            //typeDAO.ExportToJSON(typeDAO.GetAllDatatable(), "C:\\Users\\kinto\\typeDataTable.json");
            //typeDAO.ExportToJSON(typeDAO.GetAllList(), "C:\\Users\\kinto\\typeList.json");
            //typeDAO.ExportToXML(typeDAO.GetAllList(), "C:\\Users\\kinto\\typeList.xml");
            //typeDAO.ImportFromJSON("C:\\Users\\kinto\\typeList.json");
            //typeDAO.ImportFromXML("C:\\Users\\kinto\\typeList.xml");
            //XML
            //typeDAO.ExportToXML(typeDAO.GetAllDatatable());
            //typeDAO.ExportToXML(typeDAO.GetAllList()); 
            //XML with custom path
            //typeDAO.ExportToXML(typeDAO.GetAllDatatable(), "C:\\Users\\kinto\\typeDataTable.xml");
            //typeDAO.ExportToXML(typeDAO.GetAllList(), "C:\\Users\\kinto\\typeList.xml");
            /*
            //JSON
            productDAO.ExportToJSON(productDAO.GetAllDatatable());
            productDAO.ExportToJSON(productDAO.GetAllList());
            //JSON with custom path
            productDAO.ExportToJSON(productDAO.GetAllDatatable(), "C:\\Users\\kinto\\productDataTable.json");
            productDAO.ExportToJSON(productDAO.GetAllList(), "C:\\Users\\kinto\\productList.json");
            //XML
            productDAO.ExportToXML(productDAO.GetAllDatatable());
            productDAO.ExportToXML(productDAO.GetAllList());
            //XML with custom path
            productDAO.ExportToXML(productDAO.GetAllDatatable(), "C:\\Users\\kinto\\productDataTable.xml");
            productDAO.ExportToXML(productDAO.GetAllList(), "C:\\Users\\kinto\\productList.xml");

            //JSON
            manufacturerDAO.ExportToJSON(manufacturerDAO.GetAllDatatable());
            manufacturerDAO.ExportToJSON(manufacturerDAO.GetAllList());
            //JSON with custom path
            manufacturerDAO.ExportToJSON(manufacturerDAO.GetAllDatatable(), "C:\\Users\\kinto\\manufacturerDataTable.json");
            manufacturerDAO.ExportToJSON(manufacturerDAO.GetAllList(), "C:\\Users\\kinto\\manufacturerList.json");
            //XML
            manufacturerDAO.ExportToXML(manufacturerDAO.GetAllDatatable());
            manufacturerDAO.ExportToXML(manufacturerDAO.GetAllList());
            //XML with custom path
            manufacturerDAO.ExportToXML(manufacturerDAO.GetAllDatatable(), "C:\\Users\\kinto\\manufacturerDataTable.xml");
            manufacturerDAO.ExportToXML(manufacturerDAO.GetAllList(), "C:\\Users\\kinto\\manufacturerList.xml");

            //JSON
            orderDAO.ExportToJSON(orderDAO.GetAllDatatable());
            orderDAO.ExportToJSON(orderDAO.GetAllList());
            //JSON with custom path
            orderDAO.ExportToJSON(orderDAO.GetAllDatatable(), "C:\\Users\\kinto\\orderDataTable.json");
            orderDAO.ExportToJSON(orderDAO.GetAllList(), "C:\\Users\\kinto\\orderList.json");
            //XML
            orderDAO.ExportToXML(orderDAO.GetAllDatatable());
            orderDAO.ExportToXML(orderDAO.GetAllList());
            //XML with custom path
            orderDAO.ExportToXML(orderDAO.GetAllDatatable(), "C:\\Users\\kinto\\orderDataTable.xml");
            orderDAO.ExportToXML(orderDAO.GetAllList(), "C:\\Users\\kinto\\orderList.xml");

            //JSON
            itemDAO.ExportToJSON(itemDAO.GetAllDatatable());
            itemDAO.ExportToJSON(itemDAO.GetAllList());
            //JSON with custom path
            itemDAO.ExportToJSON(itemDAO.GetAllDatatable(), "C:\\Users\\kinto\\itemDataTable.json");
            itemDAO.ExportToJSON(itemDAO.GetAllList(), "C:\\Users\\kinto\\itemList.json");
            //XML
            itemDAO.ExportToXML(itemDAO.GetAllDatatable());
            itemDAO.ExportToXML(itemDAO.GetAllList());
            //XML with custom path
            itemDAO.ExportToXML(itemDAO.GetAllDatatable(), "C:\\Users\\kinto\\itemDataTable.xml");
            itemDAO.ExportToXML(itemDAO.GetAllList(), "C:\\Users\\kinto\\itemList.xml");

            //JSON
            customerDAO.ExportToJSON(customerDAO.GetAllDatatable());
            customerDAO.ExportToJSON(customerDAO.GetAllList());
            //JSON with custom path
            customerDAO.ExportToJSON(customerDAO.GetAllDatatable(), "C:\\Users\\kinto\\cutomerDataTable.json");
            customerDAO.ExportToJSON(customerDAO.GetAllList(), "C:\\Users\\kinto\\customerList.json");
            //XML
            customerDAO.ExportToXML(customerDAO.GetAllDatatable());
            customerDAO.ExportToXML(customerDAO.GetAllList());
            //XML with custom path
            customerDAO.ExportToXML(customerDAO.GetAllDatatable(), "C:\\Users\\kinto\\customerDataTable.xml");
            customerDAO.ExportToXML(customerDAO.GetAllList(), "C:\\Users\\kinto\\customerList.xml");
            */
        }
        /// <summary>
        /// Logs errors into errorLog.xml file
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="path"></param>
        public static void WriteErrorToXml(Exception ex, string path)
        {
            if (!File.Exists(path))
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;
                xmlWriterSettings.NewLineOnAttributes = true;
                using (XmlWriter xmlWriter = XmlWriter.Create(path, xmlWriterSettings))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("Error_Logs");
                    xmlWriter.WriteStartElement("Exception");
                    xmlWriter.WriteAttributeString("Type", ex.GetType().ToString());
                    xmlWriter.WriteElementString("DateTime", DateTime.Now.ToString());
                    xmlWriter.WriteElementString("Description", ex.Message);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();
                    xmlWriter.Flush();
                    xmlWriter.Close();
                }
            }
            else
            {
                XDocument xDocument = XDocument.Load(path);
                XElement root = xDocument.Element("Error_Logs");
                IEnumerable<XElement> rows = root.Descendants("Exception");
                XElement firstRow = rows.First();
                firstRow.AddAfterSelf(
                   new XElement("Exception", new XAttribute("Type", ex.GetType().ToString()),
                   new XElement("DateTime", DateTime.Now.ToString()),
                   new XElement("Description", ex.Message)));
                xDocument.Save(path);
            }
        }
        /// <summary>
        /// Error handler in case that unknown node appears
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void serializer_UnknownNode
        (object sender, XmlNodeEventArgs e)
        {
            Console.WriteLine("Unknown Node:" + e.Name + "\t" + e.Text);
        }
        /// <summary>
        /// Error handler in case that unknown attribute appears
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void serializer_UnknownAttribute
        (object sender, XmlAttributeEventArgs e)
        {
            System.Xml.XmlAttribute attr = e.Attr;
            Console.WriteLine("Unknown attribute " +
            attr.Name + "='" + attr.Value + "'");
        }

    }
}