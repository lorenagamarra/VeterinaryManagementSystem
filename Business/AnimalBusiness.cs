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

            if (animal.Picture == null)
            {
                throw new Exception("Animal has null picture");
            }
            
            if (animal.OwnerID == 0)
            {
                throw new Exception("Animal must have an Owner ID");
            }

            if (animal.BreedID == 0)
            {
                throw new Exception("Animal must have a Breed ID");
            }

            if (animal.VachistID != 0)
            {
                if (!(animal.VachistID >= 1))
                {
                    throw new Exception("Animal Vaccine Historic can be empty or an existent VacHist ID ");
                }
            }

            if (animal.Datereg != DateTime.Now)
            {
                throw new Exception("Animal Registration Date must be the current day");
            }

            if (String.IsNullOrEmpty(animal.Name) || animal.Name.Length < 2 || animal.Name.Length > 15)
            {
                throw new Exception("Animal name must be 2-15 characters long");
            }

            if (String.IsNullOrEmpty(animal.Gender) || animal.Gender.Length < 4 || animal.Gender.Length > 6)
            {
                throw new Exception("Animal gender must be male or female");
            }

            if ((DateTime.Now.Year - animal.Dateofbirth.Year) > 100)
            {
                throw new Exception("Animal can not be more than 100 years old");
            }

            if (animal.Weight <= 0 || animal.Weight > 400)
            {
                throw new Exception("Animal can not have more than 400 pounds");
            }

            if (String.IsNullOrEmpty(animal.Specie) || animal.Specie.Length < 2 || animal.Specie.Length > 10)
            {
                throw new Exception("Animal must have an specie");
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
                    throw new Exception("Animal Food be empty or be 2-50 characters long");
                }
            }

            if ((!String.IsNullOrEmpty(animal.Phobia)))
            {
                if (animal.Phobia.Length < 2 || animal.Phobia.Length > 50)
                {
                    throw new Exception("Animal Food be empty or be 2-50 characters long");
                }
            }

            if ((!String.IsNullOrEmpty(animal.Flagset)))
            {
                if (animal.Flagset.Length < 2 || animal.Flagset.Length > 200)
                {
                    throw new Exception("Animal Flagset can be empty or have many itens");
                }
            }

            if ((!String.IsNullOrEmpty(animal.Vethistoric)))
            {
                if (animal.Vethistoric.Length < 2 || animal.Vethistoric.Length > 500)
                {
                    throw new Exception("Animal VetHistoric can be empty or be 2-500 characters long");
                }
            }
            
            if (String.IsNullOrEmpty(animal.Status))
            {
                throw new Exception("Animal has null Status");
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

        public void Delete(Animal animal)
        {
            dataAccess.Delete(animal);
        }
    }
}
