using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryManagementSystem.Classes;
using VeterinaryManagementSystem.DataAccess;

namespace VeterinaryManagementSystem.Business
{
    class BreedBusiness
    {
        private BreedDataAccess dataAccess;

        public BreedBusiness()
        {
            this.dataAccess = new BreedDataAccess();
        }


        public void Save(Breed breed)
        {
            if (breed == null)
            {
                throw new Exception("Breed is null");
            }

            if (breed.Specie.Length < 2 || breed.Specie.Length > 15)
            {
                throw new Exception("Breed Specie must be 2-15 characters long");
            }
            
            if (breed.Name.Length < 2 || breed.Name.Length > 20)
            {
                throw new Exception("Breed Name must be 2-20 characters long");
            }
            
            if (breed.Status.Length < 6 || breed.Status.Length > 8)
            {
                throw new Exception("Breed Status must be ACTIVE or INACTIVE");
            }

            if (breed.Id == 0)
            {
                Insert(breed);
            }
            else
            {
                Update(breed);
            }
        }

        public void Insert(Breed breed)
        {
            dataAccess.Add(breed);
        }

        public void Update(Breed breed)
        {
            dataAccess.Update(breed);
        }

        public void Delete(Breed breed)
        {
            dataAccess.Delete(breed);
        }
    }
}
