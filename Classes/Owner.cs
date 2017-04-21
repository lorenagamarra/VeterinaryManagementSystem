using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryManagementSystem.Classes
{
    public class Owner
    {
        public int Id { get; set; }

        public DateTime RegistrationDate { get; set; }

        public Byte[] Picture_01 { get; set; }

        public String FirstName_01 { get; set; }

        public String MiddleName_01 { get; set; }

        public String LastName_01 { get; set; }

        public String Number_01 { get; set; }

        public String Address_01 { get; set; }

        public String Complement_01 { get; set; }

        public String City_01 { get; set; }

        public String Province_01 { get; set; }

        public String PostalCode_01 { get; set; }

        public int PhoneNumber_01 { get; set; }

        public int OtherPhoneNumber_01 { get; set; }

        public String Email_01 { get; set; }

        public Byte[] Picture_02 { get; set; }

        public String FirstName_02 { get; set; }

        public String MiddleName_02 { get; set; }

        public String LastName_02 { get; set; }

        public String Number_02 { get; set; }

        public String Address_02 { get; set; }

        public String Complement_02 { get; set; }

        public String City_02 { get; set; }

        public String Province_02 { get; set; }

        public String PostalCode_02 { get; set; }

        public int PhoneNumber_02 { get; set; }

        public int OtherPhoneNumber_02 { get; set; }

        public String Email_02 { get; set; }

        public string Observation { get; set; }
        
        public string Status { get; set; }

    }
}
