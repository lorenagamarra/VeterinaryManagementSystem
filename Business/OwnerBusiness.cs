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
            //Regex postalcode = new Regex(@"^[a-zA-Z]\d{ 3}$");
            Regex postalcode = new Regex("^[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]$");
            Regex phone = new Regex("^[0-9]{10}$");
            //Regex email = new Regex(@"^([a-zA-Z0-9_-.]{2,30})@([a-zA-Z0-9]{2,15})+\.+[a-zA-Z]{2,5}$");
            

            if (owner == null)
            {
                throw new Exception("Owner is null");
            }

            //START owner 01
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
                throw new Exception("owner_01 Last Name must be 2-20 characters long");
            }

            if (owner.Number_01.Length < 2 || owner.Number_01.Length > 10)
            {
                throw new Exception("owner_01 Address number must be 2-10 characters long");
            }

            if (owner.Address_01.Length < 2 || owner.Address_01.Length > 50)
            {
                throw new Exception("owner_01 address must be 2-50 characters long");
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

            if (!String.IsNullOrEmpty(owner.OtherPhoneNumber_01))
            {
                if (!(phone.Match(owner.OtherPhoneNumber_01).Success))
                {
                    throw new Exception("owner_01 Other Phone Number invalid");
                }
            }

            if (!String.IsNullOrEmpty(owner.Email_01))
            {
                if (owner.Email_01.Length < 5 || owner.Email_01.Length > 50)
                {
                    throw new Exception("owner_01 E-mail must be 5-50 characters long");
                }
            }
            //END owner 01



            //START owner 02

            if ((!String.IsNullOrEmpty(owner.FirstName_02)))
            {
                if (owner.FirstName_02.Length < 2 || owner.FirstName_02.Length > 20)
                {
                    throw new Exception("owner_02 first name must be 2-20 characters long");
                }
            }

            if ((!String.IsNullOrEmpty(owner.MiddleName_02)))
            {
                if (owner.MiddleName_02.Length < 2 || owner.MiddleName_02.Length > 15)
                {
                    throw new Exception("owner_02 middle name can be empty or be 2-15 characters long");
                }
            }
            if ((!String.IsNullOrEmpty(owner.LastName_02)))
            {
                if (owner.LastName_02.Length < 2 || owner.LastName_02.Length > 20)
                {
                    throw new Exception("owner_02 must be 2-20 characters long");
                }
            }
            if ((!String.IsNullOrEmpty(owner.Number_02)))
            {
                if (owner.Number_02.Length < 2 || owner.Number_02.Length > 10)
                {
                    throw new Exception("owner_02 number must be 2-10 characters long");
                }
            }

            if ((!String.IsNullOrEmpty(owner.Address_02)))
            {
                if (owner.Address_02.Length < 2 || owner.Address_02.Length > 50)
                {
                    throw new Exception("owner_02 address must be 2-50 characters long");
                }
            }
            if ((!String.IsNullOrEmpty(owner.Complement_02)))
            {
                if ((!String.IsNullOrEmpty(owner.Complement_02)))
                {
                    if (owner.Complement_02.Length < 2 || owner.Complement_02.Length > 15)
                    {
                        throw new Exception("owner_02 complement can be empty or be 2-15 characters long");
                    }
                }
            }

            if ((!String.IsNullOrEmpty(owner.City_02)))
            {
                if (owner.City_02.Length < 2 || owner.City_02.Length > 15)
                {
                    throw new Exception("owner_02 City must be 2-15 characters long");
                }
            }

            if ((!String.IsNullOrEmpty(owner.Province_02)))
            {
                if (owner.Province_02.Length != 2)
                {
                    throw new Exception("owner_02 Province must be 2 characters long");
                }
            }

            if ((!String.IsNullOrEmpty(owner.PostalCode_02)))
            {
                if (!(postalcode.Match(owner.PostalCode_02).Success))
                {
                    throw new Exception("owner_02 Postal Code invalid");
                }
            }

            if ((!String.IsNullOrEmpty(owner.PhoneNumber_02)))
            {
                if (!(phone.Match(owner.PhoneNumber_02).Success))
                {
                    throw new Exception("owner_02 Phone Number invalid");
                }
            }

            if ((!String.IsNullOrEmpty(owner.OtherPhoneNumber_02)))
            {
                if (!String.IsNullOrEmpty(owner.OtherPhoneNumber_02))
                {
                    if (!(phone.Match(owner.OtherPhoneNumber_02).Success))
                    {
                        throw new Exception("owner_02 Other Phone Number invalid");
                    }
                }
            }

            if ((!String.IsNullOrEmpty(owner.Email_02)))
            {
                if (!String.IsNullOrEmpty(owner.Email_02))
                {
                    if (owner.Email_02.Length < 5 || owner.Email_02.Length > 50)
                    {
                        throw new Exception("owner_02 E-mail must be 5-50 characters long");
                    }
                }
            }
            //END owner 02


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
        /*
        public void Delete(Owner owner)
        {
            dataAccess.Delete(owner);
        }
        */
    }
}
