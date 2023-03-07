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
    public class Customer : IBaseClass
    {
        [XmlIgnore]
        [JsonIgnore]
        private int id;
        private string name;
        private string surname;
        private string email;

        public Customer()
        {

        }
        public Customer(string name, string surname, string email)
        {
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
        }

        public Customer(int id, string name, string surname, string email)
        {
            this.ID = id;
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
        }
        [XmlIgnore]
        [JsonIgnore]
        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Email { get => email; set => email = value; }

        public override string ToString()
        {
            return "Customer ID: " + ID + " Name: " + Name + " Surname: " + Surname + " Email: " + Email;
        }
    }
}
