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
    public class Type : IBaseClass
    {
        [XmlIgnore]
        [JsonIgnore]
        private int id;
        private string name;

        public Type() { }
        public Type(string name)
        {
            this.Name = name;
        }
        public Type(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
        [XmlIgnore]
        [JsonIgnore]
        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }

        public override string? ToString()
        {
            return "Type ID: " + ID + " Name: " + Name;
        }
    }
}
