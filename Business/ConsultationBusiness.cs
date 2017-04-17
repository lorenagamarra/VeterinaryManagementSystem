using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryManagementSystem.Classes;
using VeterinaryManagementSystem.DataAccess;

namespace VeterinaryManagementSystem.Business
{
    class ConsultationBusiness
    {
        private ConsultationDataAccess dataAccess;

        public ConsultationBusiness()
        {
            this.dataAccess = new ConsultationDataAccess();
        }

        public void Save(Consultation consultation)
        {
            if (consultation == null)
            {
                throw new Exception("Consultation is null");
            }

            if (consultation.AnimalID == 0)
            {
                throw new Exception("Consultation must have an Animal ID");
            }

            if (consultation.EmployeeID == 0)
            {
                throw new Exception("Consultation must have an Employee ID");
            }
            
            if (consultation.VaccineID != 0)
            {
                if (consultation.VaccineID == consultation.VaccineID)
                {
                    throw new Exception("Owner Details 2 must have a diferent ID");
                }
            }
            
            if (consultation.ServProdID == 0)
            {
                throw new Exception("Owner Details 1 must have an ID");
            }

            if (consultation.Date != DateTime.Now)
            {
                throw new Exception("Owner Registration Date must be the current day");
            }

            if (consultation.Record.Length < 2 || consultation.Record.Length > 500)
            {
                throw new Exception("Owner Observation name can be empty or be 2-500 characters long");
            }

            if ((!String.IsNullOrEmpty(consultation.Prescription)))
            {
                if (consultation.Prescription.Length < 2 || consultation.Prescription.Length > 500)
                {
                    throw new Exception("Owner Observation name can be empty or be 2-500 characters long");
                }
            }

            if (consultation.Quantity == 0)
            {
                throw new Exception("Owner Details 1 must have an ID");
            }
            
            if (consultation.Cost == 0)
            {
                throw new Exception("Owner Details 1 must have an ID");
            }

            if (consultation.Id == 0)
            {
                Insert(consultation);
            }
            else
            {
                Update(consultation);
            }
        }

        public void Insert(Consultation consultation)
        {
            dataAccess.Add(consultation);
        }

        public void Update(Consultation consultation)
        {
            dataAccess.Update(consultation);
        }

        public void Delete(Consultation consultation)
        {
            dataAccess.Delete(consultation);
        }




























    }
}
