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

        public int OwnerId { get; set; }

        public int BreedId { get; set; }

        public int VaccineHistoricId { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string Name { get; set; }
        
        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public decimal Weight { get; set; }

        public string Specie { get; set; }

        public string Identification { get; set; }

        public string Food { get; set; }

        public string Phobia { get; set; }

        public string FlagSet { get; set; }

        public string VetHistoric { get; set; }
    }
}
