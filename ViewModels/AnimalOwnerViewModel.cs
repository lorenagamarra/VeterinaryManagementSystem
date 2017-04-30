using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryManagementSystem.Classes;

namespace VeterinaryManagementSystem.ViewModels
{
    class AnimalOwnerViewModel
    {
        public int OwnerId { get; set; }

        public string Owner1 { get; set; }

        public string Owner2 { get; set; }

        public Animal Animal { get; set; }
    }
}
