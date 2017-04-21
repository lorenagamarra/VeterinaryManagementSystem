using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VeterinaryManagementSystem.Business;
using VeterinaryManagementSystem.Classes;
using VeterinaryManagementSystem.DataAccess;

namespace VeterinaryManagementSystem
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        AnimalDataAccess dbAnimal;
        BreedDataAccess dbBreed;
        ConsultationDataAccess dbConsultation;
        EmployeeDataAccess dbEmployee;
        OwnerDataAccess dbOwner;
        ServicesProductsDataAccess dbServicesProducts;
        VaccineDataAccess dbVaccine;
        VaccineHistoricDataAccess dbVaccineHistory;

        List<Animal> animalList = new List<Animal>();
        List<Breed> breedList = new List<Breed>();
        List<Employee> employeeList = new List<Employee>();
        List<Owner> ownerList = new List<Owner>();
        List<ServicesProducts> servicesProductsList = new List<ServicesProducts>();
        List<Vaccine> vaccineList = new List<Vaccine>();
        List<VaccineHistoric> vaccineHistoryList = new List<VaccineHistoric>();

        public MainWindow()
        {
            try
            {
                dbAnimal = new AnimalDataAccess();
                dbBreed = new BreedDataAccess();
                dbConsultation = new ConsultationDataAccess();
                dbEmployee = new EmployeeDataAccess();
                dbOwner = new OwnerDataAccess();
                dbServicesProducts = new ServicesProductsDataAccess();
                dbVaccine = new VaccineDataAccess();
                dbVaccineHistory = new VaccineHistoricDataAccess();

                InitializeComponent();

                lvRegistryOwnerSearchResult.ItemsSource = ownerList;
                //RefreshBookList();
                // TODO: load genres into combo box
                //allGenres = db.GetAllGenres();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("Error opening database connection: " + e.Message);
                Environment.Exit(1);
            }
        }

        private void Button_Click_ConsultationSearch(object sender, RoutedEventArgs e)
        {

        }

        private void btnConsultationNewConsult_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lvConsultationSearchResult_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

       

        private void RegistryOwnerSearchResult_ButtonSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        //Registry -> Owner -> Search List Result
        private void lvRegistryOwnerSearchResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lvRegistryOwnerSearchResult.SelectedIndex;
            if (index < 0)
            {
                tbRegistryOwnerID.Text = "";
                return;
            }
            Owner owner = ownerList[index];
            tbRegistryOwnerDateRegistration.Text = owner.RegistrationDate + "";
            tbRegistryOwnerID.Text = owner.Id + "";


            tbRegistryOwner1FName.Text = owner.FirstName_01;
            tbRegistryOwner1MName.Text = owner.MiddleName_01;
            tbRegistryOwner1LName.Text = owner.LastName_01;
            tbRegistryOwner1NumberAddress.Text = owner.Number_01;
            tbRegistryOwner1Address.Text = owner.Address_01;
            tbRegistryOwner1Complement.Text = owner.Complement_01;
            tbRegistryOwner1City.Text = owner.City_01;
            cbRegistryOwner1Province.Text = owner.Province_01;
            tbRegistryOwner1PostalCode.Text = owner.PostalCode_01;
            tbRegistryOwner1Phone.Text = owner.PhoneNumber_01 + "";
            tbRegistryOwner1OtherNumber.Text = owner.OtherPhoneNumber_01 + "";
            tbRegistryOwner1Email.Text = owner.Email_01;
            //TOD: Descobrir qual é a propriedade da imagem que guarda o que a imagem tem dentro..
            //Content? Text?
            //imgRegistryOwner1Image.Source = owner.Picture_01;
            
            tbRegistryOwner2FName.Text = owner.FirstName_02;
            tbRegistryOwner2MName.Text = owner.MiddleName_02;
            tbRegistryOwner2LName.Text = owner.LastName_02;
            tbRegistryOwner2NumberAddress.Text = owner.Number_02;
            tbRegistryOwner2Address.Text = owner.Address_02;
            tbRegistryOwner2Complement.Text = owner.Complement_02;
            tbRegistryOwner2City.Text = owner.City_02;
            cbRegistryOwner2Province.Text = owner.Province_02;
            tbRegistryOwner2PostalCode.Text = owner.PostalCode_02;
            tbRegistryOwner2Phone.Text = owner.PhoneNumber_02 + "";
            tbRegistryOwner2OtherNumber.Text = owner.OtherPhoneNumber_02 + "";
            tbRegistryOwner2Email.Text = owner.Email_02;
            //TOD: Descobrir qual é a propriedade da imagem que guarda o que a imagem tem dentro..
            //Content? Text?
            //imgRegistryOwner2Image.Source = owner.Picture_02;

        }
        //Registry -> Owner -> Save/Add
        private void btnRegistryOwnerSave_Click(object sender, RoutedEventArgs e)
        {
            DateTime registrationDate = DateTime.Now;
            //Owner 1
            //TODO Como armazenar a imagem no BD
            //byte [] picture_01 = imgRegistryOwner1Image.
            string firstName_01 = tbRegistryOwner1LName.Text;
            string middleName_01 = tbRegistryOwner1MName.Text;
            string lastName_01 = tbRegistryOwner1LName.Text;
            string number_01 = tbRegistryOwner1NumberAddress.Text;
            string address_01 = tbRegistryOwner1Address.Text;
            string complement_01 = tbRegistryOwner1Complement.Text;
            string city_01 = tbRegistryOwner1City.Text;
            string province_01 = cbRegistryOwner1Province.Text;
            string postalCode_01 = tbRegistryOwner1PostalCode.Text;
            string phone1 = tbRegistryOwner1Phone.Text;
            int phoneNumber_01;
            if (!int.TryParse(phone1, out phoneNumber_01))
            {
                MessageBox.Show("Phone Number must contain only numbers");
                return;
            }
            string phoneOther1 = tbRegistryOwner1OtherNumber.Text;
            int otherPhoneNumber_01;
            if (!int.TryParse(phoneOther1, out otherPhoneNumber_01))
            {
                MessageBox.Show("Other Number must contain only numbers");
                return;
            }
            string email_01 = tbRegistryOwner1Email.Text;


            //Owner 2
            //TODO Como armazenar a imagem no BD
            //byte [] picture_02 = imgRegistryOwner1Image.
            string firstName_02 = tbRegistryOwner2LName.Text;
            string middleName_02 = tbRegistryOwner2MName.Text;
            string lastName_02 = tbRegistryOwner2LName.Text;
            string number_02 = tbRegistryOwner2NumberAddress.Text;
            string address_02 = tbRegistryOwner2Address.Text;
            string complement_02 = tbRegistryOwner2Complement.Text;
            string city_02 = tbRegistryOwner2City.Text;
            string province_02 = cbRegistryOwner2Province.Text;
            string postalCode_02 = tbRegistryOwner2PostalCode.Text;
            string phone2 = tbRegistryOwner2Phone.Text;
            int phoneNumber_02;
            if (!int.TryParse(phone2, out phoneNumber_02))
            {
                MessageBox.Show("Phone Number must contain only numbers");
                return;
            }
            string phoneOther2 = tbRegistryOwner2OtherNumber.Text;
            int otherPhoneNumber_02;
            if (!int.TryParse(phoneOther2, out otherPhoneNumber_02))
            {
                MessageBox.Show("Other Number must contain only numbers");
                return;
            }
            string email_02 = tbRegistryOwner2Email.Text;

            //TODO Status (Active / Inactive)
            string status = gb_rb_OwnerStatus.Content.ToString();


            try
            {
                var ownerRegistry = new Owner
                {
                    RegistrationDate = registrationDate,

                    //Picture_01 = picture_01,
                    FirstName_01 = firstName_01,
                    MiddleName_01 = middleName_01,
                    LastName_01 = lastName_01,
                    Number_01 = number_01,
                    Address_01 = address_01,
                    Complement_01 = complement_01,
                    City_01 = city_01,
                    Province_01 = province_01,
                    PostalCode_01 = postalCode_01,
                    PhoneNumber_01 = phoneNumber_01,
                    OtherPhoneNumber_01 = otherPhoneNumber_01,
                    Email_01 = email_01,

                    //Picture_02 = picture_02,
                    FirstName_02 = firstName_02,
                    MiddleName_02 = middleName_02,
                    LastName_02 = lastName_02,
                    Number_02 = number_02,
                    Address_02 = address_02,
                    Complement_02 = complement_02,
                    City_02 = city_02,
                    Province_02 = province_02,
                    PostalCode_02 = postalCode_02,
                    PhoneNumber_02 = phoneNumber_02,
                    OtherPhoneNumber_02 = otherPhoneNumber_02,
                    Email_02 = email_02,

                    Status = status
                };

                //Owner ownerRegistry = new Owner();
                ownerList.Add(ownerRegistry);
                lvRegistryOwnerSearchResult.Items.Refresh();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
     
        
    }
}
