using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VeterinaryManagementSystem.Classes;
using VeterinaryManagementSystem.DataAccess;

namespace VeterinaryManagementSystem.Business
{
    class SpecieBusiness
    {
        private SpecieDataAccess dataAccess;

        public SpecieBusiness()
        {
            this.dataAccess = new SpecieDataAccess();
        }


        public void Save(Specie specie)
        {
            if (specie == null)
            {
                throw new Exception("Specie is null");
            }

            if (specie.SpecieName.Length > 15)
            {
                throw new Exception("Specie Name must be 2-15 characters long");
            }

            if (specie.Id == 0)
            {
                Insert(specie);
            }
            else
            {
                Update(specie);
            }
        }

        public void Insert(Specie specie)
        {
            dataAccess.Add(specie);
        }

        public void Update(Specie specie)
        {
            dataAccess.Update(specie);
        }

        public void Delete(Specie specie)
        {
            try
            {
                dataAccess.Delete(specie);
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
