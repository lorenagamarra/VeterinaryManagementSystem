using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryManagementSystem.Classes
{
    public class Consultation
    {
        public int Id { get; set; }

        public int AnimalID { get; set; }

        public int EmployeeID { get; set; }

        public int VaccineID { get; set; }

        public int ServProdID { get; set; }

        public DateTime Date { get; set; }

        public String Record { get; set; }

        public String Prescription { get; set; }

        public decimal Cost { get; set; }

    }
}
