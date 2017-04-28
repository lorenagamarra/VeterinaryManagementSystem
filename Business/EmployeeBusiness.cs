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
    class EmployeeBusiness
    {
        private EmployeeDataAccess dataAccess;

        public EmployeeBusiness()
        {
            this.dataAccess = new EmployeeDataAccess();
        }


        public void Save(Employee employee)
        {
            // regular expressions
            Regex postalcode = new Regex(@"^([a-zA-Z]\d){ 3}$");
            Regex phone = new Regex(@"^[0-9]{ 10}$");
            Regex email = new Regex(@"^([a-zA-Z0-9_-.]{2,30})@([a-zA-Z0-9]{2,15})+\.+[a-zA-Z]{2,5}$");
            Regex sin = new Regex(@"([0-9]{ 9}$");

            if (employee == null)
            {
                throw new Exception("Employee is null");
            }

            if (employee.FirstName.Length < 2 || employee.FirstName.Length > 20)
            {
                throw new Exception("employee first name must be 2-20 characters long");
            }

            if ((!String.IsNullOrEmpty(employee.MiddleName)))
            {
                if (employee.MiddleName.Length < 2 || employee.MiddleName.Length > 15)
                {
                    throw new Exception("employee middle name can be empty or be 2-15 characters long");
                }
            }

            if (employee.LastName.Length < 2 || employee.LastName.Length > 20)
            {
                throw new Exception("employee must be 2-20 characters long");
            }

            if (employee.Number.Length < 2 || employee.Number.Length > 10)
            {
                throw new Exception("employee number must be 2-10 characters long");
            }

            if (employee.Address.Length < 2 || employee.Address.Length > 50)
            {
                throw new Exception("employee address must be 2-50 characters long");
            }

            if ((!String.IsNullOrEmpty(employee.Complement)))
            {
                if (employee.Complement.Length < 2 || employee.Complement.Length > 15)
                {
                    throw new Exception("employee complement can be empty or be 2-15 characters long");
                }
            }

            if (employee.City.Length < 2 || employee.City.Length > 15)
            {
                throw new Exception("employee City must be 2-15 characters long");
            }

            if (employee.Province.Length != 2)
            {
                throw new Exception("employee Province must be 2 characters long");
            }
            
            if (!(postalcode.Match(employee.PostalCode).Success))
            {
                throw new Exception("employee Postal Code invalid");
            }

            if (!(phone.Match(employee.PhoneNumber).Success))
            {
                throw new Exception("employee Phone Number invalid");
            }

            if (!String.IsNullOrEmpty(employee.OtherPhoneNumber))
            {
                if (!(phone.Match(employee.OtherPhoneNumber).Success))
                {
                    throw new Exception("employee Other Phone Number invalid");
                }
            }

            if (!String.IsNullOrEmpty(employee.Email))
            {
                if (!(email.Match(employee.Email).Success))
                {
                    throw new Exception("employee E-mail invalid");
                }
            }

            if (employee.HireDate < DateTime.Now)
            {
                throw new Exception("Employee Hire Date can not be smaller than today");
            }

            if (!(employee.TermDate == null))
            {
                if (employee.TermDate < employee.HireDate)
                {
                    throw new Exception("Employee Term Date can not be smaller than Hire Date");
                }
            }

            if (!(sin.Match(employee.SIN).Success))
            {
                throw new Exception("Employee SIN invalid");
            }

            if (employee.Position.Length < 2 || employee.Position.Length > 20)
            {
                throw new Exception("Employee City must be 2-20 characters long");
            }

            if ((!String.IsNullOrEmpty(employee.Observations)))
            {
                if (employee.Observations.Length < 2 || employee.Observations.Length > 500)
                {
                    throw new Exception("Employee observations can be empty or be 2-500 characters long");
                }
            }
            
            if (employee.Id == 0)
            {
                Insert(employee);
            }
            else
            {
                Update(employee);
            }
        }

        public void Insert(Employee employee)
        {
            dataAccess.Add(employee);
        }

        public void Update(Employee employee)
        {
            dataAccess.Update(employee);
        }
        /*
        public void Delete(Employee employee)
        {
            dataAccess.Delete(employee);
        }
        */
    }
}
