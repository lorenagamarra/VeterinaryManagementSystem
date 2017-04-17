using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryManagementSystem.Classes;
using VeterinaryManagementSystem.DataAccess;

namespace VeterinaryManagementSystem.Business
{
    class VaccineHistoricBusiness
    {

        private VaccineHistoricDataAccess dataAccess;

        public VaccineHistoricBusiness()
        {
            this.dataAccess = new VaccineHistoricDataAccess();
        }


        public void Save(VaccineHistoric vaccinehistoric)
        {
            if (vaccinehistoric == null)
            {
                throw new Exception("Vaccine Historic is null");
            }

            if (String.IsNullOrEmpty(vaccinehistoric.Name))
            {
                throw new Exception("Vaccine Historic has null name");
            }

            if (vaccinehistoric.Date <= DateTime.Now)
            {
                throw new Exception("Vaccine Historic can not have date bigger than today");
            }

            if ((!String.IsNullOrEmpty(vaccinehistoric.Details)))
            {
                if (vaccinehistoric.Details.Length < 2 || vaccinehistoric.Details.Length > 30)
                {
                    throw new Exception("Vaccine Historic has null name");
                }
            }

            if (vaccinehistoric.Id == 0)
            {
                Insert(vaccinehistoric);
            }
            else
            {
                Update(vaccinehistoric);
            }
        }

        public void Insert(VaccineHistoric vaccinehistoric)
        {
            dataAccess.Add(vaccinehistoric);
        }

        public void Update(VaccineHistoric vaccinehistoric)
        {
            dataAccess.Update(vaccinehistoric);
        }

        public void Delete(VaccineHistoric vaccinehistoric)
        {
            dataAccess.Delete(vaccinehistoric);
        }



    }
}
