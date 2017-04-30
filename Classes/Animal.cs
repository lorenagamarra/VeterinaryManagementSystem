using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryManagementSystem.Classes
{
    public class Animal
    {
        public int Id { get; set; }

        public Byte[] Picture { get; set; }

        public int OwnerID { get; set; }

        public int BreedID { get; set; }

        public int VachistID { get; set; }

        public DateTime Datereg { get; set; }

        public string Name { get; set; }
        
        public Boolean Gender { get; set; }

        public DateTime? Dateofbirth { get; set; }

        public decimal Weight { get; set; }

        public string Identification { get; set; }

        public string Food { get; set; }

        public string Phobia { get; set; }

        public string Vethistoric { get; set; }

        public Boolean Status { get; set; }
    }
}