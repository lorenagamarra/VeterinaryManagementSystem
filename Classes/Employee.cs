using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryManagementSystem.Classes
{
    public class Employee
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime TermDate { get; set; }

        public int SIN { get; set; }

        public String Position { get; set; }

        public String Observations { get; set; }

        public string Status { get; set; }
    }
}
