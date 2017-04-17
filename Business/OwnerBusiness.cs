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

            if (owner.OwnerDetails1 == 0)
            {
                throw new Exception("Owner Details 1 must have an ID");
            }

            if (owner.OwnerDetails2 != 0)
            {
                if (owner.OwnerDetails2 == owner.OwnerDetails1)
                {
                    throw new Exception("Owner Details 2 must have a diferent ID");
                }
            }

            if ((!String.IsNullOrEmpty(owner.Observation)))
            {
                if (owner.Observation.Length < 2 || owner.Observation.Length > 500)
                {
                    throw new Exception("Owner Observation name can be empty or be 2-500 characters long");
                }
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
