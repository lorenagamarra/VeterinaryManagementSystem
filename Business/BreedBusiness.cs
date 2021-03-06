﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

            if (breed.SpecieID == 0)
            {
                throw new Exception("Breed Specie must be selected");
            }
            
            if (breed.Name.Length < 2 || breed.Name.Length > 20)
            {
                throw new Exception("Breed Name must be 2-20 characters long");
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
            try
            {
                dataAccess.Delete(breed);
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
