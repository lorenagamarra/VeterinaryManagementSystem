using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryManagementSystem.Classes
{
    public class Person
    {
        public int Id { get; set; }

        public Byte[] Picture { get; set; }

        public String FirstName { get; set; }

        public String MiddleName { get; set; }

        public String LastName { get; set; }

        public String Number { get; set; }

        public String Address { get; set; }

        public String Complement { get; set; }
        
        public String City{ get; set; }

        public String Province { get; set; }

        public String PostalCode { get; set; }

        public int PhoneNumber { get; set; }

        public int OtherPhoneNumber { get; set; }
        
        public String Email { get; set; }
    }
}
