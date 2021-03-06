﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryManagementSystem.Classes;
using VeterinaryManagementSystem.DataAccess;

namespace VeterinaryManagementSystem.Business
{
    class AnimalBusiness
    {
        private AnimalDataAccess dataAccess;

        public AnimalBusiness()
        {
            this.dataAccess = new AnimalDataAccess();
        }


        public void Save(Animal animal)
        {
            if (animal == null)
            {
                throw new Exception("Animal is null");
            }

            if (animal.OwnerID == 0)
            {
                throw new Exception("Animal must have an Owner ID");
            }

            if (animal.BreedID == 0)
            {
                throw new Exception("Animal must have a Breed ID");
            }

            if (animal.Name.Length < 2 || animal.Name.Length > 20)
            {
                throw new Exception("Animal name must be 2-20 characters long");
            }

            if (!animal.Dateofbirth.HasValue)
            {
                throw new Exception("Select a Date of birth");
            }

            if (animal.Dateofbirth > DateTime.Now)
            {
                throw new Exception("Employee Hire Date can not be greater than today");
            }

            if (animal.Weight <= 0)
            {
                throw new Exception("Animal can not have 0kg");
            }

            if ((!String.IsNullOrEmpty(animal.Identification)))
            {
                if (animal.Identification.Length < 2 || animal.Identification.Length > 50)
                {
                    throw new Exception("Animal Identification can be empty or be 2-50 characters long");
                }
            }

            if ((!String.IsNullOrEmpty(animal.Food)))
            {
                if (animal.Food.Length < 2 || animal.Food.Length > 50)
                {
                    throw new Exception("Animal Food can be empty or be 2-50 characters long");
                }
            }

            if ((!String.IsNullOrEmpty(animal.Phobia)))
            {
                if (animal.Phobia.Length < 2 || animal.Phobia.Length > 50)
                {
                    throw new Exception("Animal Phobia can be empty or be 2-50 characters long");
                }
            }

            if ((!String.IsNullOrEmpty(animal.Vethistoric)))
            {
                if (animal.Vethistoric.Length < 2)
                {
                    throw new Exception("Animal VetHistoric can be empty or at least be 2 characters long");
                }
            }


            if (animal.Id == 0)
            {
                Insert(animal);
            }
            else
            {
                Update(animal);
            }
        }

        public void Insert(Animal animal)
        {
            dataAccess.Add(animal);
        }

        public void Update(Animal animal)
        {
            dataAccess.Update(animal);
        }
        /*
        public void Delete(Animal animal)
        {
            dataAccess.Delete(animal);
        }
        */
    }
}
