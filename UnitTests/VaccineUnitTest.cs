using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryManagementSystem.Business;
using VeterinaryManagementSystem.Classes;

namespace VeterinaryManagementSystem.UnitTests
{
    public class VaccineUnitTest
    {
        private VaccineBusiness business;

        public VaccineUnitTest()
        {
            business = new VaccineBusiness();
        }


        [Test]
        public void Save_VaccineNull_ShouldThrowException()
        {
            Vaccine vaccine = null;

            try
            {
                business.Save(vaccine);
                Assert.Fail("Vaccine should throw Exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vaccine is null", ex.Message);
            }
        }
        
        [Test]
        public void Save_VaccineNameTooShort_ShouldThrowException()
        {
            Vaccine vaccine = new Vaccine
            {
                Name = "N",
                Price = 1,
                //Status = "Active"
            };

            try
            {
                business.Save(vaccine);
                Assert.Fail("Vaccine Name should throw Exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vaccine has too short name", ex.Message);
            }
        }

        [Test]
        public void Save_VaccineNameTooLong_ShouldThrowException()
        {
            Vaccine vaccine = new Vaccine
            {
                Name = "Vaccine's name exceeds the limit of 30 characters long",
                Price = 1,
                //Status = "Active"
            };

            try
            {
                business.Save(vaccine);
                Assert.Fail("Vaccine Name should throw Exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vaccine has too long name", ex.Message);
            }
        }

        [Test]
        public void Save_VaccinePriceEqualsOrLessThanZero_ShouldThrowException()
        {
            Vaccine vaccine = new Vaccine
            {
                Name = "Name",
                Price = 0,
                //Status = "Active"
            };

            try
            {
                business.Save(vaccine);
                Assert.Fail("Vaccine Price should throw Exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vaccine must have price greater than zero", ex.Message);
            }
        }
        
        [Test]
        public void Save_VaccineStatusTooShort_ShouldThrowException()
        {
            Vaccine vaccine = new Vaccine
            {
                Name = "Name",
                Price = 1,
                //Status = "Short"
            };

            try
            {
                business.Save(vaccine);
                Assert.Fail("Vaccine Status should throw Exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vaccine has too short Status (minimum 6 characters)", ex.Message);
            }
        }


        [Test]
        public void Save_VaccineStatusTooLong_ShouldThrowException()
        {
            Vaccine vaccine = new Vaccine
            {
                Name = "Name",
                Price = 1,
                //Status = "Vaccine's Status too long"
            };

            try
            {
                business.Save(vaccine);
                Assert.Fail("Vaccine Status should throw Exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vaccine has too long Status (maximum 8 characters)", ex.Message);
            }
        }



        // VALID TEST ********************************

        [Test]
        public void Save_VaccineValid()
        {
            Vaccine vaccine = new Vaccine
            {
                Name = "Name",
                Price = 1,
                //Status = "Active"
            };
            
            business.Save(vaccine);
            Assert.Pass("Valid Vaccine");
        }
    }
}
