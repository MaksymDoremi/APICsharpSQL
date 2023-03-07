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
    public class Order : IBaseClass
    {
        [XmlIgnore]
        [JsonIgnore]
        private int id;
        [XmlIgnore]
        [JsonIgnore]
        private int customer_id;
        private DateTime datetime;
        private double price;
        private bool delivered;

        public Order() { }
        public Order(int customer_id, DateTime datetime, double price, bool delivered)
        {
            this.Customer_ID = customer_id;
            this.Datetime = datetime;
            this.Price = price;
            this.Delivered = delivered;
        }
        public Order(int id, int customer_id, DateTime datetime, double price, bool delivered)
        {
            this.ID = id;
            this.Customer_ID = customer_id;
            this.Datetime = datetime;
            this.Price = price;
            this.Delivered = delivered;
        }
        [XmlIgnore]
        [JsonIgnore]
        public int ID { get => id; set => id = value; }
        [XmlIgnore]
        [JsonIgnore]
        public int Customer_ID { get => customer_id; set => customer_id = value; }
        public DateTime Datetime { get => datetime; set => datetime = value; }
        public double Price { get => price; set => price = value; }
        public bool Delivered { get => delivered; set => delivered = value; }

        public override string? ToString()
        {
            return "Order ID: " + ID + " Customer_ID: " + Customer_ID + " Datetime: " + Datetime + " Price: " + Price + " Delivered: " + Delivered;
        }
    }
}
