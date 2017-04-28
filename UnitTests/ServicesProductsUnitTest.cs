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
    public class ServicesProductsUnitTest
    {
        private ServicesProductsBusiness business;

        public ServicesProductsUnitTest()
        {
            business = new ServicesProductsBusiness();
        }

        [Test]
        public void Save_ServicesProductsNull_ShouldThrowException()
        {
            ServicesProducts servicesproducts = null;

            try
            {
                business.Save(servicesproducts);
                Assert.Fail("ServicesProducts should throw Exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("ServicesProducts is null", ex.Message);
            }
        }

        [Test]
        public void Save_ServicesProductsNameTooShort_ShouldThrowException()
        {
            ServicesProducts servicesproducts = new ServicesProducts
            {
                Name = "N",
                Price = 1,
                Status = "Active"
            };

            try
            {
                business.Save(servicesproducts);
                Assert.Fail("ServicesProducts Name should throw Exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("ServicesProducts has too short name", ex.Message);
            }
        }

        [Test]
        public void Save_ServicesProductsNameTooLong_ShouldThrowException()
        {
            ServicesProducts servicesproducts = new ServicesProducts
            {
                Name = "Vaccine's name exceeds the limit of 30 characters long",
                Price = 1,
                Status = "Active"
            };

            try
            {
                business.Save(servicesproducts);
                Assert.Fail("ServicesProducts Name should throw Exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("ServicesProducts has too long name", ex.Message);
            }
        }

        [Test]
        public void Save_ServicesProductsPriceEqualsOrLessThanZero_ShouldThrowException()
        {
            ServicesProducts servicesproducts = new ServicesProducts
            {
                Name = "Name",
                Price = 0,
                Status = "Active"
            };

            try
            {
                business.Save(servicesproducts);
                Assert.Fail("ServicesProducts Price should throw Exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("ServicesProducts must have price greater than zero", ex.Message);
            }
        }

        [Test]
        public void Save_ServicesProductsStatusTooShort_ShouldThrowException()
        {
            ServicesProducts servicesproducts = new ServicesProducts
            {
                Name = "Name",
                Price = 1,
                Status = "Short"
            };

            try
            {
                business.Save(servicesproducts);
                Assert.Fail("ServicesProducts Status should throw Exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("ServicesProducts has too short Status (minimum 6 characters)", ex.Message);
            }
        }


        [Test]
        public void Save_ServicesProductsTooLong_ShouldThrowException()
        {
            ServicesProducts servicesproducts = new ServicesProducts
            {
                Name = "Name",
                Price = 1,
                Status = "ServicesProducts's Status too long"
            };

            try
            {
                business.Save(servicesproducts);
                Assert.Fail("ServicesProducts Status should throw Exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("ServicesProducts has too long Status (maximum 8 characters)", ex.Message);
            }
        }



        [Test]
        public void Save_ServicesProductsValid()
        {
            ServicesProducts servicesproducts = new ServicesProducts
            {
                Name = "Name",
                Price = 1,
                Status = "Active"
            };

            business.Save(servicesproducts);
            Assert.Pass("Valid ServicesProducts");
        }
    }
}
