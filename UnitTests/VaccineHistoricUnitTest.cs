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
    public class VaccineHistoricUnitTest
    {
        private VaccineHistoricBusiness business;

        public VaccineHistoricUnitTest()
        {
            business = new VaccineHistoricBusiness();
        }

        [Test]
        public void Save_VaccineHistoricNull_ShouldThrowException()
        {
            VaccineHistoric vaccinehistoric = null;

            try
            {
                business.Save(vaccinehistoric);
                Assert.Fail("VaccineHistoric should throw Exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("VaccineHistoric is null", ex.Message);
            }
        }

        [Test]
        public void Save_VaccineHistoricNameTooShort_ShouldThrowException()
        {
            VaccineHistoric vaccinehistoric = new VaccineHistoric
            {
                Name = "N",
                Date = DateTime.Now,
                Details = "Details"
            };

            try
            {
                business.Save(vaccinehistoric);
                Assert.Fail("VaccineHistoric Name should throw Exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("VaccineHistoric has too short name", ex.Message);
            }
        }

        [Test]
        public void Save_VaccineHistoricNameTooLong_ShouldThrowException()
        {
            VaccineHistoric vaccinehistoric = new VaccineHistoric
            {
                Name = "Vaccine's name exceeds the limit of 30 characters long",
                Date = DateTime.Now,
                Details = "Details"
            };

            try
            {
                business.Save(vaccinehistoric);
                Assert.Fail("VaccineHistoric Name should throw Exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("VaccineHistoric has too long name", ex.Message);
            }
        }


        [Test]
        public void Save_VaccineHistoricDateGreaterThanToday_ShouldThrowException()
        {
            VaccineHistoric vaccinehistoric = new VaccineHistoric
            {
                Name = "Name",
                Date = DateTime.Now.AddDays(1),
                Details = "Details"
            };

            try
            {
                business.Save(vaccinehistoric);
                Assert.Fail("VaccineHistoric Date should throw Exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("VaccineHistoric has Date greater than today", ex.Message);
            }
        }

        //with _NullDetails ************************************************************

        [Test]
        public void Save_VaccineHistoricNameTooShort_NullDetails_ShouldThrowException()
        {
            VaccineHistoric vaccinehistoric = new VaccineHistoric
            {
                Name = "N",
                Date = DateTime.Now,
                Details = null
            };

            try
            {
                business.Save(vaccinehistoric);
                Assert.Fail("VaccineHistoric Name should throw Exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("VaccineHistoric has too short name", ex.Message);
            }
        }

        [Test]
        public void Save_VaccineHistoricNameTooLong_NullDetails_ShouldThrowException()
        {
            VaccineHistoric vaccinehistoric = new VaccineHistoric
            {
                Name = "Vaccine's name exceeds the limit of 30 characters long",
                Date = DateTime.Now,
                Details = null
            };

            try
            {
                business.Save(vaccinehistoric);
                Assert.Fail("VaccineHistoric Name should throw Exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("VaccineHistoric has too long name", ex.Message);
            }
        }


        [Test]
        public void Save_VaccineHistoricDateGreaterThanToday_NullDetails_ShouldThrowException()
        {
            VaccineHistoric vaccinehistoric = new VaccineHistoric
            {
                Name = "Name",
                Date = DateTime.Now.AddDays(1),
                Details = null
            };

            try
            {
                business.Save(vaccinehistoric);
                Assert.Fail("VaccineHistoric Date should throw Exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("VaccineHistoric has Date greater than today", ex.Message);
            }
        }

        // finished _NullDetails ************************************************************


        [Test]
        public void Save_VaccineHistoricDetailsTooShort_ShouldThrowException()
        {
            VaccineHistoric vaccinehistoric = new VaccineHistoric
            {
                Name = "Name",
                Date = DateTime.Now,
                Details = "D"
            };

            try
            {
                business.Save(vaccinehistoric);
                Assert.Fail("VaccineHistoric Date should throw Exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("VaccineHistoric has too short details (minimum 2 characters)", ex.Message);
            }
        }
                
        [Test]
        public void Save_VaccineHistoricDetailsTooLong_ShouldThrowException()
        {
            VaccineHistoric vaccinehistoric = new VaccineHistoric
            {
                Name = "Name",
                Date = DateTime.Now,
                Details = "Details too long ***************** *************** ***************  *************** **************** *********"
            };

            try
            {
                business.Save(vaccinehistoric);
                Assert.Fail("VaccineHistoric Date should throw Exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("VaccineHistoric has too long details (maximum 100 characters)", ex.Message);
            }
        }


        // VALID TEST ********************************

        [Test]
        public void Save_VaccineHistoricValid()
        {
            VaccineHistoric vaccinehistoric = new VaccineHistoric
            {
                Name = "Name",
                Date = DateTime.Now,
                Details = "Details"
            };

            business.Save(vaccinehistoric);
            Assert.Pass("Valid VaccineHistoric");
        }

        [Test]
        public void Save_VaccineHistoricValid_NullDetails()
        {
            VaccineHistoric vaccinehistoric = new VaccineHistoric
            {
                Name = "Name",
                Date = DateTime.Now,
                Details = null
            };

            business.Save(vaccinehistoric);
            Assert.Pass("Valid VaccineHistoric");
        }


    }
}
