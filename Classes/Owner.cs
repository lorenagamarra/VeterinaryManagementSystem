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

        public int Person1Id { get; set; }

        public int Person2Id { get; set; }

        public string Observation { get; set; }
        
        public string Status { get; set; }

    }
}
