using PVProjectAPI1502.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PVProjectAPI1502.Objects
{
    public class Product : IBaseClass
    {
        [XmlIgnore]
        [JsonIgnore]
        private int id;
        [XmlIgnore]
        [JsonIgnore]
        private int manufacturer_id;
        [XmlIgnore]
        [JsonIgnore]
        private int type_id;
        private string manufacturer_name;
        private string type_name;
        private string name;
        private double price;

        public Product() { }
        public Product(int manufacturer_id, int type_id, string name, double price)
        {
            this.Manufacturer_ID = manufacturer_id;
            this.Type_ID = type_id;
            this.Name = name;
            this.Price = price;
        }
        public Product(int id, int manufacturer_id, int type_id, string name, double price)
        {
            this.ID = id;
            this.Manufacturer_ID = manufacturer_id;
            this.Type_ID = type_id;
            this.Name = name;
            this.Price = price;
        }

        public Product(int id, string manufacturer_name, string type_name, string name, double price)
        {
            this.ID = id;
            this.Manufacturer_Name = manufacturer_name;
            this.Type_Name = type_name;
            this.Name = name;
            this.Price = price;
        }

        public Product(int id, int manufacturer_id, int type_id, string manufacturer_name, string type_name, string name, double price)
        {
            this.ID = id;
            this.Manufacturer_ID = manufacturer_id;
            this.Type_ID = type_id;
            this.Manufacturer_Name = manufacturer_name;
            this.Type_Name = type_name;
            this.Name = name;
            this.Price = price;
        }
        [XmlIgnore]
        [JsonIgnore]
        public int ID { get => id; set => id = value; }
        [XmlIgnore]
        [JsonIgnore]
        public int Manufacturer_ID { get => manufacturer_id; set => manufacturer_id = value; }
        [XmlIgnore]
        [JsonIgnore]
        public int Type_ID { get => type_id; set => type_id = value; }
        public string Manufacturer_Name { get => manufacturer_name; set => manufacturer_name = value; }
        public string Type_Name { get => type_name; set => type_name = value; }
        public string Name { get => name; set => name = value; }
        public double Price { get => price; set => price = value; }

        public override string? ToString()
        {
            return "Product ID: " + ID + " Manufacturer_ID: " + Manufacturer_ID + " Type_ID: " + Type_ID + " Name: " + Name + " Price: " + Price+" Manufacturer_Name: "+Manufacturer_Name+" Type_Name: "+Type_Name;
        }
    }
}
