using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryManagementSystem
{
    class Owner
    {
        public Owner(DateTime registrationDate_, Byte pictureOwner1_, String firstName1_, String middleName1_, string lastName1_, string number1_,
            string address1_, string complement1_, string city1_, string province1_, string postalCode1_, int phoneNumber1_, int otherNumber1_,
            string email1_)
        {

            this.registrationDate = registrationDate_;
            this.pictureOwner1 = pictureOwner1_;
            this.firstName1 = firstName1_;
            this.middleName1 = middleName1_;
            this.lastName1 = lastName1_;
            this.number1 = number1_;
            this.address1 = address1_;
            this.complement1 = complement1_;
            this.city1 = city1_;
            this.province1 = province1_;
            this.postalCode1 = postalCode1_;
            this.phoneNumber1 = phoneNumber1_;
            this.otherNumber1 = otherNumber1_;
            this.email1 = email1_;





            



            _ownerID = ++instanceCount;
        }



        private static int instanceCount;
        private int _ownerID;
        public int ownerID
        {
            get
            {
                return _ownerID;
            }
        }





        private DateTime _registrationDate;
        public DateTime registrationDate
        {
            get
            {
                return _registrationDate;
            }
            set
            {
                if (registrationDate.Date != DateTime.Now)
                {
                    throw new ArgumentException("Registration Date must be the current day");
                }
                _registrationDate = value;
            }
        }


        private Byte _pictureOwner1;
        public Byte pictureOwner1
        {
            get
            {
                return _pictureOwner1;
            }
            set
            {
                if (pictureOwner1.ToString() == "")
                {
                    throw new ArgumentException("Owner's picture can't be empty");
                }
                _pictureOwner1 = value;
            }
        }



        private string _firstName1;
        public string firstName1
        {
            get
            {
                return _firstName1;
            }
            set
            {
                if (value.Length < 2 || value.Length > 30)
                {
                    throw new ArgumentException("First Name must be 2-30 characters long");
                }
                _firstName1 = value;
            }
        }


        private string _middleName1;
        public string middleName1
        {
            get
            {
                return _middleName1;
            }
            set
            {
                if (value.Length != 0) //**************************************************************** RIGHT LOGIC???
                {
                    if (value.Length < 2 || value.Length > 30)
                    {
                        throw new ArgumentException("Middle Name must be 2-30 characters long");
                    }
                }
                _middleName1 = value;
            }
        }



        private string _lastName1;
        public string lastName1
        {
            get
            {
                return _lastName1;
            }
            set
            {
                if (value.Length < 2 || value.Length > 30)
                {
                    throw new ArgumentException("Last Name must be 2-30 characters long");
                }
                _lastName1 = value;
            }
        }



        private string _number1;
        public string number1
        {
            get
            {
                return _number1;
            }
            set
            {
                if (value.Length < 1 || value.Length > 10)
                {
                    throw new ArgumentException("Number must be 1-10 characters long");
                }
                _number1 = value;
            }
        }




        private string _address1;
        public string address1
        {
            get
            {
                return _address1;
            }
            set
            {
                if (value.Length < 2 || value.Length > 40)
                {
                    throw new ArgumentException("Address must be 1-40 characters long");
                }
                _address1 = value;
            }
        }




        private string _complement1;
        public string complement1
        {
            get
            {
                return _complement1;
            }
            set
            {
                if (value.Length != 0) //**************************************************************** RIGHT LOGIC???
                {
                    if (value.Length < 2 || value.Length > 20)
                    {
                        throw new ArgumentException("Complement can be empty or be 2-20 characters long");
                    }
                }
                _complement1 = value;
            }
        }



        private string _city1;
        public string city1
        {
            get
            {
                return _city1;
            }
            set
            {
                if (value.Length < 2 || value.Length > 20)
                {
                    throw new ArgumentException("City must be 1-20 characters long");
                }
                _city1 = value;
            }
        }




        private string _province1;
        public string province1
        {
            get
            {
                return _province1;
            }
            set
            {
                if (value.Length != 2)
                {
                    throw new ArgumentException("Province must be 2 characters long");
                }
                _province1 = value;
            }
        }




        private string _postalCode1;
        public string postalCode1
        {
            get
            {
                return _postalCode1;
            }
            set
            {
                if (value.Length != 6)
                {
                    throw new ArgumentException("Postal Code must be 6 characters long");
                }
                _postalCode1 = value;
            }
        }





        private int _phoneNumber1;
        public int phoneNumber1
        {
            get
            {
                return _phoneNumber1;
            }
            set
            {
                if (value.ToString().Length != 10) //**************************************************************** RIGHT LOGIC???
                {
                    throw new ArgumentException("Phone Number must be 10 characters long");
                }
                _phoneNumber1 = value;
            }
        }





        private int _otherNumber1;
        public int otherNumber1
        {
            get
            {
                return _otherNumber1;
            }
            set
            {
                if (value.ToString().Length != 0) //**************************************************************** RIGHT LOGIC???
                {
                    if (value.ToString().Length != 10) //**************************************************************** RIGHT LOGIC???
                    {
                        throw new ArgumentException("Other Number can be empty or be 10 characters long");
                    }
                    _otherNumber1 = value;
                }

            }
        }




        private string _email1;
        public string email1
        {
            get
            {
                return _email1;
            }
            set
            {
                if (value.Length < 5 || value.Length > 30)
                {
                    throw new ArgumentException("E-mail must be 5-30 characters long");
                }
                _email1 = value;
            }
        }







    }
}


/*

CREATE TABLE [dbo].[tblOwner] (
[ownerID]          INT             IDENTITY (1, 1) NOT NULL,
[registrationDate] DATE            NOT NULL,
[pictureOwner1]    VARBINARY (MAX) NOT NULL,
[firstName1]       NVARCHAR (50)   NOT NULL,
[middleName1]      NVARCHAR (50)   NULL,
[lastName1]        NVARCHAR (50)   NOT NULL,
[number1]          NVARCHAR (50)   NOT NULL,
[address1]         NVARCHAR (50)   NOT NULL,
[complement1]      NVARCHAR (50)   NULL,
[city1]            NVARCHAR (50)   NOT NULL,
[province1]        NVARCHAR (50)   NOT NULL,
[postalCode1]      NVARCHAR (50)   NOT NULL,
[phoneNumber1]     INT             NOT NULL,
[otherNumber1]     INT             NULL,
[email1]           NVARCHAR (50)   NULL UNIQUE,
[pictureOwner2]    VARBINARY (MAX) NULL,
[firstName2]       NVARCHAR (50)   NULL,
[middleName2]      NVARCHAR (50)   NULL,
[lastName2]        NVARCHAR (50)   NULL,
[number2]          NVARCHAR (50)   NULL UNIQUE,
[address2]         NVARCHAR (50)   NULL,
[complement2]      NVARCHAR (50)   NULL,
[city2]            NVARCHAR (50)   NULL,
[province]         NVARCHAR (50)   NULL,
[postalCode2]      NVARCHAR (50)   NULL,
[phoneNumber2]     INT             NULL,
[otherNumber2]     INT             NULL,
[email2]           NVARCHAR (50)   NULL,
[observations]     TEXT            NULL,
PRIMARY KEY CLUSTERED ([ownerID] ASC)
);


 */
