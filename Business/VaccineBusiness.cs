using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryManagementSystem.Classes;
using VeterinaryManagementSystem.DataAccess;

namespace VeterinaryManagementSystem.Business
{
    class VaccineBusiness
    {
        private VaccineDataAccess dataAccess;

        public VaccineBusiness()
        {
            this.dataAccess = new VaccineDataAccess();
        }


        public void Save(Vaccine vaccine)
        {
            if (vaccine == null)
            {
                throw new Exception("Vaccine is null");
            }

            if (String.IsNullOrEmpty(vaccine.Name))
            {
                throw new Exception("Vaccine has null name");
            }

            if (vaccine.Price <= 0)
            {
                throw new Exception("Vaccine must have price greater than zero");
            }

            if (vaccine.Id == 0)
            {
                Insert(vaccine);
            }
            else
            {
                Update(vaccine);
            }
        }

        public void Insert(Vaccine vaccine)
        {
            dataAccess.Add(vaccine);
        }

        public void Update(Vaccine vaccine)
        {
            dataAccess.Update(vaccine);
        }

        public void Delete(Vaccine vaccine)
        {
            dataAccess.Delete(vaccine);
        }
    }
}
