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
    class OwnerBusiness
    {
        private OwnerDataAccess dataAccess;

        public OwnerBusiness()
        {
            this.dataAccess = new OwnerDataAccess();
        }

        public void Save(Owner owner)
        {
            // regular expressions
            Regex postalcode = new Regex(@"^([abceghjklmnprstvxyABCEGHJKLMNPRSTVXY]\d){ 1}[ ]?(a-zA-Z]\d){ 2}$");
            Regex phone = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
            Regex email = new Regex(@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$");





            if (owner == null)
            {
                throw new Exception("Owner is null");
            }

            if (owner.RegistrationDate != DateTime.Now.Date)
            {
                throw new Exception("Owner Registration Date must be the current day");
            }

            //START owner 01

            if ((!String.IsNullOrEmpty(owner.Picture_01.ToString())))
            {
                if (owner.Picture_01 == null)
                {
                    throw new Exception("owner_01 has null picture");
                }
            }

            if (owner.FirstName_01.Length < 2 || owner.FirstName_01.Length > 20)
            {
                throw new Exception("owner_01 first name must be 2-20 characters long");
            }

            if ((!String.IsNullOrEmpty(owner.MiddleName_01)))
            {
                if (owner.MiddleName_01.Length < 2 || owner.MiddleName_01.Length > 15)
                {
                    throw new Exception("owner_01 middle name can be empty or be 2-15 characters long");
                }
            }

            if (owner.LastName_01.Length < 2 || owner.LastName_01.Length > 20)
            {
                throw new Exception("owner_01 must be 2-20 characters long");
            }

            if (owner.Number_01.Length < 2 || owner.Number_01.Length > 10)
            {
                throw new Exception("owner_01 number must be 2-10 characters long");
            }

            if (owner.Address_01.Length < 2 || owner.Address_01.Length > 50)
            {
                throw new Exception("owner_01 number must be 2-50 characters long");
            }

            if ((!String.IsNullOrEmpty(owner.Complement_01)))
            {
                if (owner.Complement_01.Length < 2 || owner.Complement_01.Length > 15)
                {
                    throw new Exception("owner_01 complement can be empty or be 2-15 characters long");
                }
            }

            if (owner.City_01.Length < 2 || owner.City_01.Length > 15)
            {
                throw new Exception("owner_01 City must be 2-15 characters long");
            }

            if (owner.Province_01.Length != 2)
            {
                throw new Exception("owner_01 Province must be 2 characters long");
            }
            
            if (!(postalcode.Match(owner.PostalCode_01).Success))
            {
                throw new Exception("owner_01 Postal Code invalid");
            }

            if (!(phone.Match(owner.PhoneNumber_01).Success))
            {
                throw new Exception("owner_01 Phone Number invalid");
            }

            if (!(owner.OtherPhoneNumber_01 == null))
            {
                if (!(phone.Match(owner.OtherPhoneNumber_01).Success))
                {
                    throw new Exception("owner_01 Other Phone Number invalid");
                }
            }

            if (!(owner.Email_01 == null))
            {
                if (!(email.Match(owner.Email_01).Success) || owner.Email_01.Length > 50)
                {
                    throw new Exception("owner_01 E-mail invalid (max 50 characters long");
                }
            }




            //END owner 01

            //START owner 02

            if ((!String.IsNullOrEmpty(owner.Picture_02.ToString())))
            {
                if (owner.Picture_02 == null)
                {
                    throw new Exception("owner_02 has null picture");
                }
            }

            if (owner.FirstName_02.Length < 2 || owner.FirstName_02.Length > 20)
            {
                throw new Exception("owner_02 first name must be 2-20 characters long");
            }

            if ((!String.IsNullOrEmpty(owner.MiddleName_02)))
            {
                if (owner.MiddleName_02.Length < 2 || owner.MiddleName_02.Length > 15)
                {
                    throw new Exception("owner_02 middle name can be empty or be 2-15 characters long");
                }
            }

            if (owner.LastName_02.Length < 2 || owner.LastName_02.Length > 20)
            {
                throw new Exception("owner_02 must be 2-20 characters long");
            }

            if (owner.Number_02.Length < 2 || owner.Number_02.Length > 10)
            {
                throw new Exception("owner_02 number must be 2-10 characters long");
            }

            if (owner.Address_02.Length < 2 || owner.Address_02.Length > 50)
            {
                throw new Exception("owner_02 number must be 2-50 characters long");
            }

            if ((!String.IsNullOrEmpty(owner.Complement_02)))
            {
                if (owner.Complement_02.Length < 2 || owner.Complement_02.Length > 15)
                {
                    throw new Exception("owner_02 complement can be empty or be 2-15 characters long");
                }
            }

            if (owner.City_02.Length < 2 || owner.City_02.Length > 15)
            {
                throw new Exception("owner_02 City must be 2-15 characters long");
            }

            if (owner.Province_02.Length != 2)
            {
                throw new Exception("owner_02 Province must be 2 characters long");
            }

            if (!(postalcode.Match(owner.PostalCode_02).Success))
            {
                throw new Exception("owner_02 Postal Code invalid");
            }

            if (!(phone.Match(owner.PhoneNumber_02).Success))
            {
                throw new Exception("owner_02 Phone Number invalid");
            }

            if (!(owner.OtherPhoneNumber_02 == null))
            {
                if (!(phone.Match(owner.OtherPhoneNumber_02).Success))
                {
                    throw new Exception("owner_02 Other Phone Number invalid");
                }
            }

            if (!(owner.Email_02 == null))
            {
                if (!(email.Match(owner.Email_02).Success) || owner.Email_02.Length > 50)
                {
                    throw new Exception("owner_02 E-mail invalid (max 50 characters long");
                }
            }

            //END owner 02


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
