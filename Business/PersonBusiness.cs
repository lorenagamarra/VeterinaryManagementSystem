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
    class PersonBusiness
    {
        private PersonDataAccess dataAccess;

        public PersonBusiness()
        {
            this.dataAccess = new PersonDataAccess();
        }


        public void Save(Person person)
        {
            if (person == null)
            {
                throw new Exception("Person is null");
            }

            if (person.Picture == null)
            {
                throw new Exception("Person has null picture");
            }

            if (String.IsNullOrEmpty(person.FirstName) || person.FirstName.Length < 2 || person.FirstName.Length > 20)
            {
                throw new Exception("Person first name must be 2-20 characters long");
            }

            if ((!String.IsNullOrEmpty(person.MiddleName)))
            {
                if (person.MiddleName.Length < 2 || person.MiddleName.Length > 15)
                {
                    throw new Exception("Person middle name can be empty or be 2-15 characters long");
                }
            }

            if (String.IsNullOrEmpty(person.LastName) || person.LastName.Length < 2 || person.LastName.Length > 20)
            {
                throw new Exception("Person must be 2-20 characters long");
            }

            if (String.IsNullOrEmpty(person.Number) || person.Number.Length < 2 || person.Number.Length > 10)
            {
                throw new Exception("Person number must be 2-10 characters long");
            }

            if (String.IsNullOrEmpty(person.Address) || person.Address.Length < 2 || person.Address.Length > 50)
            {
                throw new Exception("Person number must be 2-50 characters long");
            }

            if ((!String.IsNullOrEmpty(person.Complement)))
            {
                if (person.Complement.Length < 2 || person.Complement.Length > 15)
                {
                    throw new Exception("Person complement can be empty or be 2-15 characters long");
                }
            }

            if (String.IsNullOrEmpty(person.City) || person.City.Length < 2 || person.City.Length > 15)
            {
                throw new Exception("Person City must be 2-15 characters long");
            }

            if (person.Province.Length != 2)
            {
                throw new Exception("Person Province must be 2 characters long");
            }

            Regex pc = new Regex (@"^([abceghjklmnprstvxyABCEGHJKLMNPRSTVXY]\d){ 1}[ ]?(a-zA-Z]\d){ 2}$");
            if (!(pc.Match(person.PostalCode).Success))
            {
                throw new Exception("Person Postal Code invalid");
            }

            Regex phone = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
            if (!(phone.Match(person.PhoneNumber.ToString()).Success))
            {
                throw new Exception("Person Phone Number invalid");
            }

            if (!(person.OtherPhoneNumber.ToString() == null))
            {
                if (!(phone.Match(person.OtherPhoneNumber.ToString()).Success))
                {
                    throw new Exception("Person Other Phone Number invalid");
                }
            }

            Regex email = new Regex(@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$");
            if (!(pc.Match(person.Email).Success) || person.Email.Length > 50)
            {
                throw new Exception("Person E-mail invalid (max 50 characters long)");
            }

            if (person.Id == 0)
            {
                Insert(person);
            }
            else
            {
                Update(person);
            }
        }

        public void Insert(Person person)
        {
            dataAccess.Add(person);
        }

        public void Update(Person person)
        {
            dataAccess.Update(person);
        }
    }
}
