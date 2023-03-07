using PVProjectAPI1502.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PVProjectAPI1502.Objects
{
    public class Item : IBaseClass
    {
        [XmlIgnore]
        [JsonIgnore]
        private int id;
        [XmlIgnore]
        [JsonIgnore]
        private int order_id;
        [XmlIgnore]
        [JsonIgnore]
        private int product_id;
        private int count;
        private double price;

        public Item() { }
        public Item(int order_id, int product_id, int count, double price)
        {
            this.Order_ID = order_id;
            this.Product_ID = product_id;
            this.Count = count;
            this.Price = price;
        }
        public Item(int id, int order_id, int product_id, int count, double price)
        {
            this.ID = id;
            this.Order_ID = order_id;
            this.Product_ID = product_id;
            this.Count = count;
            this.Price = price;
        }
        [XmlIgnore]
        [JsonIgnore]
        public int ID { get => id; set => id = value; }
        [XmlIgnore]
        [JsonIgnore]
        public int Order_ID { get => order_id; set => order_id = value; }
        [XmlIgnore]
        [JsonIgnore]
        public int Product_ID { get => product_id; set => product_id = value; }
        public int Count { get => count; set => count = value; }
        public double Price { get => price; set => price = value; }

        public override string ToString()
        {
            return "Item ID: " + ID + " Order_ID: " + Order_ID + " Product_ID: " + Product_ID + " Price: " + Price;
        }
    }
}
