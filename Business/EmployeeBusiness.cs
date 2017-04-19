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
            if (employee == null)
            {
                throw new Exception("Employee is null");
            }

            if (employee.PersonId == 0)
            {
                throw new Exception("Employee Person must have an ID");
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

            Regex sin = new Regex(@"^(\d{3}-\d{3}-\d{3})|(\d{9})$");
            if (!(sin.Match(employee.SIN.ToString()).Success))
            {
                throw new Exception("Employee Phone Number invalid");
            }

            if (String.IsNullOrEmpty(employee.Position) || employee.Position.Length < 2 || employee.Position.Length > 15)
            {
                throw new Exception("Employee City must be 2-50 characters long");
            }

            if ((!String.IsNullOrEmpty(employee.Observations)))
            {
                if (employee.Observations.Length < 2 || employee.Observations.Length > 500)
                {
                    throw new Exception("Employee observations can be empty or be 2-500 characters long");
                }
            }
            
            if (String.IsNullOrEmpty(employee.Status))
            {
                throw new Exception("Employee has null Status");
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
    }
}
