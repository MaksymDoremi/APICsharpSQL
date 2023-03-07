using PVProjectAPI1502.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PVProjectAPI1502.Objects
{
    public class Manufacturer : IBaseClass
    {
        [XmlIgnore]
        [JsonIgnore]
        private int id;
        private string name;
        private string email;

        public Manufacturer() { }
        public Manufacturer(string name, string email)
        {
            this.Name = name;
            this.Email = email;
        }
        public Manufacturer(int id, string name, string email)
        {
            this.ID = id;
            this.Name = name;
            this.Email = email;
        }
        [XmlIgnore]
        [JsonIgnore]
        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }

        public override string? ToString()
        {
            return "Manufacturer ID: " + ID + " Name: " + Name + " Email: " + Email;
        }
    }
}
