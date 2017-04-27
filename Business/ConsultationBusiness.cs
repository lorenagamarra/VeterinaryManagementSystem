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
         
            if (consultation.ServProdID == 0)
            {
                throw new Exception("Consultation Services & Productis must have an ID");
            }

            if (consultation.Date != DateTime.Now)
            {
                throw new Exception("Consultation Date must be the current day");
            }

            if (consultation.Record.Length < 2 || consultation.Record.Length > 1000)
            {
                throw new Exception("Consultation Record must be 2-1000 characters long");
            }

            if ((!String.IsNullOrEmpty(consultation.Prescription)))
            {
                if (consultation.Prescription.Length < 2 || consultation.Prescription.Length > 500)
                {
                    throw new Exception("Consultation Prescription can be empty or be 2-500 characters long");
                }
            }
            
            if (consultation.Cost == 0)
            {
                throw new Exception("Consultation Cost must be greater than 0");
            }

            Insert(consultation);

            /*
            if (consultation.Id == 0)
            {
                Insert(consultation);
            }
            else
            {
                Update(consultation);
            }
            */
        }

        public void Insert(Consultation consultation)
        {
            dataAccess.Add(consultation);
        }
        /*
        public void Update(Consultation consultation)
        {
            dataAccess.Update(consultation);
        }
        */
    }
}
