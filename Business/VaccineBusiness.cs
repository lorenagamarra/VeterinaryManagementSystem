using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VeterinaryManagementSystem.Classes;
using VeterinaryManagementSystem.DataAccess;
using VeterinaryManagementSystem.UnitTests;

namespace VeterinaryManagementSystem.Business
{
    public class VaccineBusiness
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

            if (vaccine.Name.Length < 2 || vaccine.Name.Length > 30)
            {
                throw new Exception("Vaccine Name must be 2-30 characters long");
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
            try
            {
                dataAccess.Delete(vaccine);
            }
            catch (InvalidOperationException)
            {
                throw new Exception("This item can not be deleted because it is being used in another table." +
                    " You can change your status to inactive.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show(ex.Message);
            }
        }
    }
}
