using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
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

        private bool unsavedChanges = false;
        

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
                
        /******************************************************************************************
         * REGISTRY => OWNER
         ******************************************************************************************/
        //Registry -> Owner -> Search Button Event
        //Se já vamos usar LINQ para a pesquisa, precisa colocar botão "Search"?
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
            //General Information for both owners
            tbRegistryOwnerDateRegistration.Text = owner.RegistrationDate + "";
            tbRegistryOwnerID.Text = owner.Id + "";
            //Owner 1
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

            //Owner 2
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
        //Registry -> Owner -> Buttons Save/Add Record Event
        private void btnRegistryOwnerSave_Click(object sender, RoutedEventArgs e)
        {
            SavingOwnerRegistryOnDB();
        }
        //Registry -> Owner -> Method Saving to DB
        private void SavingOwnerRegistryOnDB()
        {
            DateTime registrationDate = DateTime.Now;
            //Owner 1
            //Receiving data from UI
            //TODO: Como armazenar a imagem no BD
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
            //Receiving data from UI
            //TODO: Como armazenar a imagem no BD
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
            //TODO: Status (Active / Inactive)
            string status = gb_rb_OwnerStatus.Content.ToString();
            
            //Sending data to DB
            try
            {
                //Doing this way because we don't have Constructor
                var ownerRegistry = new Owner
                {
                    RegistrationDate = registrationDate,
                    Status = status,
                    //Owner 1
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
                    //Owner 2
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
                    Email_02 = email_02
                };
                ownerList.Add(ownerRegistry);
                lvRegistryOwnerSearchResult.Items.Refresh();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Registry -> Owner -> Button Exit Event
        private void btnRegistryOwnerExit_Click(object sender, RoutedEventArgs e)
        {
            if (unsavedChanges)
            {
                MessageBoxResult result = MessageBox.Show("Save unsaved changes?", "Unsaved changes",
                    MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.No:
                        OwnerForm_clearFields();
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                    case MessageBoxResult.Yes:
                        SavingOwnerRegistryOnDB();
                        break;
                }
            }
        }
        //Registry -> Owner -> Text changed on TextBox around the Registration window
        private void tbOwnerTextChanged(object sender, TextChangedEventArgs e)
        {
            unsavedChanges = true;
        }
        //Registry -> Owner -> Method to clear the fields
        private void OwnerForm_clearFields()
        {            
            tbRegistryOwnerDateRegistration.Text = String.Empty;
            tbRegistryOwnerID.Text = String.Empty;
            //Owner 1
            tbRegistryOwner1FName.Text = String.Empty;
            tbRegistryOwner1MName.Text = String.Empty;
            tbRegistryOwner1LName.Text = String.Empty;
            tbRegistryOwner1NumberAddress.Text = String.Empty;
            tbRegistryOwner1Address.Text = String.Empty;
            tbRegistryOwner1Complement.Text = String.Empty;
            tbRegistryOwner1City.Text = String.Empty;
            //cbRegistryOwner1Province.Text = String.Empty;
            tbRegistryOwner1PostalCode.Text = String.Empty;
            tbRegistryOwner1Phone.Text = String.Empty;
            tbRegistryOwner1OtherNumber.Text = String.Empty;
            tbRegistryOwner1Email.Text = String.Empty;
            //TOD: Descobrir qual é a propriedade da imagem que guarda o que a imagem tem dentro..
            //Content? Text?
            //imgRegistryOwner1Image.Source = owner.Picture_01;

            //Owner 2
            tbRegistryOwner2FName.Text = String.Empty;
            tbRegistryOwner2MName.Text = String.Empty;
            tbRegistryOwner2LName.Text = String.Empty;
            tbRegistryOwner2NumberAddress.Text = String.Empty;
            tbRegistryOwner2Address.Text = String.Empty;
            tbRegistryOwner2Complement.Text = String.Empty;
            tbRegistryOwner2City.Text = String.Empty;
            cbRegistryOwner2Province.Text = String.Empty;
            tbRegistryOwner2PostalCode.Text = String.Empty;
            tbRegistryOwner2Phone.Text = String.Empty;
            tbRegistryOwner2OtherNumber.Text = String.Empty;
            tbRegistryOwner2Email.Text = String.Empty;
            //TOD: Descobrir qual é a propriedade da imagem que guarda o que a imagem tem dentro..
            //Content? Text?
            //imgRegistryOwner1Image.Source = owner.Picture_01;
        }
        /******************************************************************************************
        * REGISTRY => ANIMAL
        ******************************************************************************************/
        private void RegistryAnimalSearchResult_ButtonSearch_Click(object sender, RoutedEventArgs e)
        {

        }
        //Registry -> Animal -> Search List Result
        private void lvRegistryAnimalSearchResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lvRegistryAnimalSearchResult.SelectedIndex;
            if (index < 0)
            {
                tbRegistryAnimalDateRegistration.Text = "";
                return;
            }
            Animal animal = animalList[index];
            tbRegistryAnimalDateRegistration.Text = animal.Datereg + "";
            //FIXME: Ao invés de mostrar o número do Id do Owner, mostrar o nome (se possível completo)
            tbRegistryAnimalOwner1FName.Text = animal.OwnerID + "";
            //TODO: Status
            tbRegistryAnimalName.Text = animal.Name;
            //TODO: Puxar no DB qual Gender está salvo, Male/Female
            //rbRegistryAnimalFemale.IsChecked = 
            dpRegistryAnimalBirthday.Text = animal.Dateofbirth + "";
            tbRegistryAnimalWeight.Text = animal.Weight + "";
            cbRegistryAnimalSpecies.Text = animal.Specie;
            //TODO: Puxar a Raca que esta relacionada com esse ID
            cbRegistryAnimalBreeds.Text = animal.BreedID + "";
            tbRegistryAnimalIdentification.Text = animal.Identification;
            tbRegistryAnimalFood.Text = animal.Food;
            tbRegistryAnimalPhobias.Text = animal.Phobia;
            //TODO: Como puxar do DB o Flagset
            // animal.Flagset;

            /* TODO: Puxar dados da tabela de Historico de Vacinas para a 
            * tab de Vaccines
            * E para o Vet History
            */
        }


        //Registry -> Animal -> Method Saving to DB
        private void SavingAnimalRegistryOnDB()
        {
            DateTime datereg = DateTime.Now;

            //Receiving data from UI
            //TODO: Como armazenar a imagem no BD
            //byte [] picture_01 = imgRegistryOwner1Image.
            //FIXME: arrumar esses ID...
            string ownerID = tbRegistryOwner1LName.Text;
            string breedID = cbRegistryAnimalBreeds.Text;
            string name = tbRegistryAnimalName.Text;
            string specie = cbRegistryAnimalSpecies.Text;
            string gender = rbRegistryAnimalFemale.ContentStringFormat;
            
            DateTime dateofbirth = Convert.ToDateTime(dpRegistryAnimalBirthday.Text);
            decimal weight = decimal.Parse(tbConsultationAnimalWeight.Text, CultureInfo.InvariantCulture);
            Console.WriteLine(weight.ToString(CultureInfo.InvariantCulture));
            string identification = tbRegistryAnimalIdentification.Text;
            string food = tbRegistryAnimalFood.Text;
            string phobia = tbRegistryAnimalPhobias.Text;
            //string flagset = tbRegistryOwner1Phone.Text;
            string vethistoric = tbRegistryAnimalVetHistory.Text;
            //TODO: Status (Active / Inactive)
            string status = gb_rb_OwnerStatus.Content.ToString();

            //Sending data to DB
            try
            {
                //Doing this way because we don't have Constructor
                var animalRegistry = new Animal
                {
                    Datereg = datereg,
                    //Picture = picture,
                   // OwnerID = ownerID,
                    //BreedID = breedID,
                    //VachistID = vachistID,
                    Name = name,
                    Specie = specie,
                    Gender = gender,
                    Dateofbirth = dateofbirth,
                    Weight = weight,
                    
                    Identification = identification,
                    Food = food,
                    Phobia = phobia,
                   // Flagset = flagset,
                    Vethistoric = vethistoric
                };
                animalList.Add(animalRegistry);
                lvRegistryOwnerSearchResult.Items.Refresh();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //TODO: Onde usar isso??? Verifica se o valor e nulo...
        private void dpRegistryAnimalBirthday_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;
            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                this.Title = "No date";
            }
            else
            {
                // ... No need to display the time.
                this.Title = date.Value.ToShortDateString();
            }
        }

        
    }
}
