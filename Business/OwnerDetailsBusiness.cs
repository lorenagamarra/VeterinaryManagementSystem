using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VeterinaryManagementSystem.Classes;
using VeterinaryManagementSystem.DataAccess;

namespace VeterinaryManagementSystem.Business
{
    class OwnerDetailsBusiness
    {
        private OwnerDetailsDataAccess dataAccess;

        public OwnerDetailsBusiness()
        {
            this.dataAccess = new OwnerDetailsDataAccess();
        }


        public void Save(OwnerDetails ownerdetails)
        {
            if (ownerdetails == null)
            {
                throw new Exception("Owner Details is null");
            }

            if (ownerdetails.Picture == null)
            {
                throw new Exception("Owner Details has null picture");
            }

            if (String.IsNullOrEmpty(ownerdetails.FirstName) || ownerdetails.FirstName.Length < 2 || ownerdetails.FirstName.Length > 20)
            {
                throw new Exception("Owner Details first name must be 2-20 characters long");
            }

            if ((!String.IsNullOrEmpty(ownerdetails.MiddleName)))
            {
                if (ownerdetails.MiddleName.Length < 2 || ownerdetails.MiddleName.Length > 15)
                {
                    throw new Exception("Owner Details middle name can be empty or be 2-15 characters long");
                }
            }

            if (String.IsNullOrEmpty(ownerdetails.LastName) || ownerdetails.LastName.Length < 2 || ownerdetails.LastName.Length > 20)
            {
                throw new Exception("Owner Details must be 2-20 characters long");
            }

            if (String.IsNullOrEmpty(ownerdetails.Number) || ownerdetails.Number.Length < 2 || ownerdetails.Number.Length > 10)
            {
                throw new Exception("Owner Details number must be 2-10 characters long");
            }

            if (String.IsNullOrEmpty(ownerdetails.Address) || ownerdetails.Address.Length < 2 || ownerdetails.Address.Length > 50)
            {
                throw new Exception("Owner Details number must be 2-50 characters long");
            }

            if ((!String.IsNullOrEmpty(ownerdetails.Complement)))
            {
                if (ownerdetails.Complement.Length < 2 || ownerdetails.Complement.Length > 15)
                {
                    throw new Exception("Owner Details complement can be empty or be 2-15 characters long");
                }
            }

            if (String.IsNullOrEmpty(ownerdetails.City) || ownerdetails.City.Length < 2 || ownerdetails.City.Length > 15)
            {
                throw new Exception("Owner Details City must be 2-15 characters long");
            }

            if (ownerdetails.Province.Length != 2)
            {
                throw new Exception("Owner Details Province must be 2 characters long");
            }

            Regex pc = new Regex (@"^([abceghjklmnprstvxyABCEGHJKLMNPRSTVXY]\d){ 1}[ ]?(a-zA-Z]\d){ 2}$");
            if (!(pc.Match(ownerdetails.PostalCode).Success))
            {
                throw new Exception("Owner Details Postal Code invalid");
            }

            Regex phone = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
            if (!(phone.Match(ownerdetails.PhoneNumber.ToString()).Success))
            {
                throw new Exception("Owner Details Phone Number invalid");
            }

            if (!(ownerdetails.OtherPhoneNumber.ToString() == null))
            {
                if (!(phone.Match(ownerdetails.OtherPhoneNumber.ToString()).Success))
                {
                    throw new Exception("Owner Details Other Phone Number invalid");
                }
            }

            Regex email = new Regex(@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$");
            if (!(pc.Match(ownerdetails.Email).Success) || ownerdetails.Email.Length > 50)
            {
                throw new Exception("Owner Details E-mail invalid (max 50 characters long)");
            }

            if (ownerdetails.Id == 0)
            {
                Insert(ownerdetails);
            }
            else
            {
                Update(ownerdetails);
            }
        }

        public void Insert(OwnerDetails ownerdetails)
        {
            dataAccess.Add(ownerdetails);
        }

        public void Update(OwnerDetails ownerdetails)
        {
            dataAccess.Update(ownerdetails);
        }

        public void Delete(OwnerDetails ownerdetails)
        {
            dataAccess.Delete(ownerdetails);
        }
    }
}
