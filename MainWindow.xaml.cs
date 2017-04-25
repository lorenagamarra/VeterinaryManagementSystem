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
                
                //Add date on date fields
                string strTODAY = Convert.ToString(DateTime.Now);
                tbRegistryOwnerDateRegistration.Text = strTODAY;


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
        

        //LINQ - SEARCH ALL OWNERS
        private void tbRegistryOwnerSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = tbRegistryOwnerSearch.Text.ToLower();
            if (filter == "")
            {
                lvRegistryOwnerSearchResult.ItemsSource = dbOwner.GetAllOwners();
            }
            else
            {
                List<Owner> list = dbOwner.GetAllOwners();
                var filteredList = from o in list where o.FirstName_01.ToLower().Contains(filter) || o.PhoneNumber_01.ToString().Contains(filter) || o.FirstName_02.ToLower().Contains(filter) || o.PhoneNumber_02.ToString().Contains(filter) select o;

                lvRegistryOwnerSearchResult.ItemsSource = filteredList;
            }
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
            tbRegistryOwnerDateRegistration.Text = owner.RegistrationDate.ToString();
            tbRegistryOwnerID.Text = owner.Id.ToString();

            //Owner 1
            //imgRegistryOwner1Image.Source = owner.Picture_01;  //TODO: Descobrir qual é a propriedade da imagem que guarda o que a imagem tem dentro..Content? Text?
            tbRegistryOwner1FName.Text = owner.FirstName_01;
            tbRegistryOwner1MName.Text = owner.MiddleName_01;
            tbRegistryOwner1LName.Text = owner.LastName_01;
            tbRegistryOwner1NumberAddress.Text = owner.Number_01;
            tbRegistryOwner1Address.Text = owner.Address_01;
            tbRegistryOwner1Complement.Text = owner.Complement_01;
            tbRegistryOwner1City.Text = owner.City_01;
            cbRegistryOwner1Province.Text = owner.Province_01;
            tbRegistryOwner1PostalCode.Text = owner.PostalCode_01;
            tbRegistryOwner1Phone.Text = owner.PhoneNumber_01;
            tbRegistryOwner1OtherNumber.Text = owner.OtherPhoneNumber_01;
            tbRegistryOwner1Email.Text = owner.Email_01;

            //Owner 2
            //imgRegistryOwner2Image.Source = owner.Picture_02;
            tbRegistryOwner2FName.Text = owner.FirstName_02;
            tbRegistryOwner2MName.Text = owner.MiddleName_02;
            tbRegistryOwner2LName.Text = owner.LastName_02;
            tbRegistryOwner2NumberAddress.Text = owner.Number_02;
            tbRegistryOwner2Address.Text = owner.Address_02;
            tbRegistryOwner2Complement.Text = owner.Complement_02;
            tbRegistryOwner2City.Text = owner.City_02;
            cbRegistryOwner2Province.Text = owner.Province_02;
            tbRegistryOwner2PostalCode.Text = owner.PostalCode_02;
            tbRegistryOwner2Phone.Text = owner.PhoneNumber_02;
            tbRegistryOwner2OtherNumber.Text = owner.OtherPhoneNumber_02;
            tbRegistryOwner2Email.Text = owner.Email_02;

            string verifyOwnerCkbStatus = owner.Status;   //Atualizando Status radiobutton de acordo com o Owner ????????????
            if (verifyOwnerCkbStatus == "ACTIVE")
            {
                rbOwnerStatus_Active.IsChecked = true;
                rbOwnerStatus_Inactive.IsChecked = false;
            }
            else
            {
                rbOwnerStatus_Active.IsChecked = false;
                rbOwnerStatus_Inactive.IsChecked = true;
            }
        }

        //Registry -> Owner -> Buttons Save/Add Record Event
        private void btnRegistryOwnerSave_Click(object sender, RoutedEventArgs e)
        {
            SavingOwnerRegistryOnDB();
        }

        private OwnerBusiness ownerBusiness;
        //Registry -> Owner -> Method Saving to DB
        private void SavingOwnerRegistryOnDB()
        {
            var owner = new Owner
            {
                RegistrationDate = DateTime.Now,                   // Add ok. mas update.. mudar a data de registro?????????????????

                //Owner 1
                //Picture_01 = imgRegistryOwner1Image.              ????????????????????????????????????????????
                FirstName_01 = tbRegistryOwner1LName.Text,
                MiddleName_01 = tbRegistryOwner1MName.Text,
                LastName_01 = tbRegistryOwner1LName.Text,
                Number_01 = tbRegistryOwner1NumberAddress.Text,
                Address_01 = tbRegistryOwner1Address.Text,
                Complement_01 = tbRegistryOwner1Complement.Text,
                City_01 = tbRegistryOwner1City.Text,
                Province_01 = cbRegistryOwner1Province.Text,          //combobox
                PostalCode_01 = tbRegistryOwner1PostalCode.Text,      
                PhoneNumber_01 = tbRegistryOwner1Phone.Text,
                OtherPhoneNumber_01 = tbRegistryOwner1OtherNumber.Text,
                Email_01 = tbRegistryOwner1Email.Text,

                //Owner 2
                //Picture_02 = imgRegistryOwner2Image,                 ????????????????????????????????????????????
                FirstName_02 = tbRegistryOwner2LName.Text,
                MiddleName_02 = tbRegistryOwner2MName.Text,
                LastName_02 = tbRegistryOwner2LName.Text,
                Number_02 = tbRegistryOwner2NumberAddress.Text,
                Address_02 = tbRegistryOwner2Address.Text,
                Complement_02 = tbRegistryOwner2Complement.Text,
                City_02 = tbRegistryOwner2City.Text,
                Province_02 = cbRegistryOwner2Province.Text,         //combobox
                PostalCode_02 = tbRegistryOwner2PostalCode.Text,
                PhoneNumber_02 = tbRegistryOwner2Phone.Text,
                OtherPhoneNumber_02 = tbRegistryOwner2OtherNumber.Text,
                Email_02 = tbRegistryOwner2Email.Text,

                Status = gb_rb_OwnerStatus.Content.ToString()       //radio button group
            };

            try
            {
                ownerBusiness.Save(owner);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //Registry -> Owner -> Button Exit Event
        private void btnRegistryOwnerExit_Click(object sender, RoutedEventArgs e)
        {
            if (unsavedChanges)
            {
                MessageBoxResult result = MessageBox.Show("Save unsaved changes?", "Unsaved changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
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
            tbRegistryOwnerDateRegistration.Text = DateTime.Now.ToString();   //Data no campo Data Registration????????????
            tbRegistryOwnerID.Text = String.Empty;

            //Owner 1
            imgRegistryOwner1Image = null;                    //Limpar imagem????????????????????????????????????????????????
            tbRegistryOwner1FName.Text = String.Empty;
            tbRegistryOwner1MName.Text = String.Empty;
            tbRegistryOwner1LName.Text = String.Empty;
            tbRegistryOwner1NumberAddress.Text = String.Empty;
            tbRegistryOwner1Address.Text = String.Empty;
            tbRegistryOwner1Complement.Text = String.Empty;
            tbRegistryOwner1City.Text = String.Empty;
            cbRegistryOwner1Province.Items.Clear();             //ComboBOX solucao que encontrei na internert para limpar item selecionado do elemento. certo??????
            tbRegistryOwner1PostalCode.Text = String.Empty;
            tbRegistryOwner1Phone.Text = String.Empty;
            tbRegistryOwner1OtherNumber.Text = String.Empty;
            tbRegistryOwner1Email.Text = String.Empty;


            //Owner 2
            imgRegistryOwner1Image = null;                    //Limpar imagem????????????????????????????????????????????????
            tbRegistryOwner2FName.Text = String.Empty;
            tbRegistryOwner2MName.Text = String.Empty;
            tbRegistryOwner2LName.Text = String.Empty;
            tbRegistryOwner2NumberAddress.Text = String.Empty;
            tbRegistryOwner2Address.Text = String.Empty;
            tbRegistryOwner2Complement.Text = String.Empty;
            tbRegistryOwner2City.Text = String.Empty;
            cbRegistryOwner2Province.Items.Clear();             //ComboBOX solucao que encontrei na internert para limpar item selecionado do elemento. certo??????
            tbRegistryOwner2PostalCode.Text = String.Empty;
            tbRegistryOwner2Phone.Text = String.Empty;
            tbRegistryOwner2OtherNumber.Text = String.Empty;
            tbRegistryOwner2Email.Text = String.Empty;

            rbOwnerStatus_Active.IsChecked = true;                   //STATUS Deixar como default o ACTIVE
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


//List<Owner> ownerList = new List<Owner>();