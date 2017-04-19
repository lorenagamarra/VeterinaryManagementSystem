using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryManagementSystem.Classes;
using VeterinaryManagementSystem.DataAccess;

namespace VeterinaryManagementSystem.Business
{
    class OwnerBusiness
    {
        private OwnerDataAccess dataAccess;

        public OwnerBusiness()
        {
            this.dataAccess = new OwnerDataAccess();
        }

        public void Save(Owner owner)
        {
            if (owner == null)
            {
                throw new Exception("Owner is null");
            }

            if (owner.RegistrationDate != DateTime.Now)
            {
                throw new Exception("Owner Registration Date must be the current day");
            }

            if (owner.Person1Id == 0)
            {
                throw new Exception("Owner Person 1 must have an ID");
            }

            if (owner.Person2Id != 0)
            {
                if (owner.Person2Id == owner.Person1Id)
                {
                    throw new Exception("Owner Person 2 must have a diferent ID");
                }
            }

            if ((!String.IsNullOrEmpty(owner.Observation)))
            {
                if (owner.Observation.Length < 2 || owner.Observation.Length > 500)
                {
                    throw new Exception("Owner Observation can be empty or be 2-500 characters long");
                }
            }
            
            if (String.IsNullOrEmpty(owner.Status))
            {
                throw new Exception("Owner has null Status");
            }

            if (owner.Id == 0)
            {
                Insert(owner);
            }
            else
            {
                Update(owner);
            }
        }

        public void Insert(Owner owner)
        {
            dataAccess.Add(owner);
        }

        public void Update(Owner owner)
        {
            dataAccess.Update(owner);
        }

        public void Delete(Owner owner)
        {
            dataAccess.Delete(owner);
        }
    }
}
