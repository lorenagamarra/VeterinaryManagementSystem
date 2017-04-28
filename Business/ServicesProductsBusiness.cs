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
    class ServicesProductsBusiness
    {
        private ServicesProductsDataAccess dataAccess;

        public ServicesProductsBusiness()
        {
            this.dataAccess = new ServicesProductsDataAccess();
        }


        public void Save(ServicesProducts servicesproducts)
        {
            if (servicesproducts == null)
            {
                throw new Exception("Services & Products is null");
            }

            if (servicesproducts.Name.Length < 2 || servicesproducts.Name.Length > 30)
            {
                throw new Exception("Services & Products must be 2-30 characters long");
            }

            if (servicesproducts.Price <= 0)
            {
                throw new Exception("Services & Products must have price greater than zero");
            }

            if (servicesproducts.Id == 0)
            {
                Insert(servicesproducts);
            }
            else
            {
                Update(servicesproducts);
            }
        }

        public void Insert(ServicesProducts servicesproducts)
        {
            dataAccess.Add(servicesproducts);
        }

        public void Update(ServicesProducts servicesproducts)
        {
            dataAccess.Update(servicesproducts);
        }

        public void Delete(ServicesProducts servicesproducts)
        {
            try
            {
                dataAccess.Delete(servicesproducts);
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
