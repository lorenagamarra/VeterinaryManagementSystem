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
        List<Consultation> consultationList = new List<Consultation>();
        List<Employee> employeeList = new List<Employee>();
        List<Owner> ownerList = new List<Owner>();
        List<ServicesProducts> servicesProductsList = new List<ServicesProducts>();
        List<Vaccine> vaccineList = new List<Vaccine>();
        List<VaccineHistoric> vaccineHistoryList = new List<VaccineHistoric>();

        private bool unsavedChanges = false;


        private AnimalBusiness animalBusiness;
        private BreedBusiness breedBusiness;
        private ConsultationBusiness consultationBusiness;
        private EmployeeBusiness employeeBusiness;
        private OwnerBusiness ownerBusiness;
        private ServicesProductsBusiness servicesproductsBusiness;
        private VaccineBusiness vaccineBusiness;
        private VaccineHistoricBusiness vaccinehistoricBusiness;




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
                tbRegistryAnimalDateRegistration.Text = strTODAY;

                //RefreshBookList();
                //criar todos os refresh para as todas as listas
                //carregar todos os conteudos dos combo box e 
                // TODO: load genres into combo box
                //allGenres = db.GetAllGenres();


                animalBusiness = new AnimalBusiness();
                breedBusiness = new BreedBusiness();
                consultationBusiness = new ConsultationBusiness();
                employeeBusiness = new EmployeeBusiness();
                ownerBusiness = new OwnerBusiness();
                servicesproductsBusiness = new ServicesProductsBusiness();
                vaccineBusiness = new VaccineBusiness();
                vaccinehistoricBusiness = new VaccineHistoricBusiness();

                //REFRESH ALL LISTS
                //refreshBreedList();
                //refreshVaccineList();
                //refreshServicesProductsList();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("Error opening database connection: " + e.Message);
                Environment.Exit(1);
            }
            
        }





        /******************************************************************************************
         * CONSULTATION
         ******************************************************************************************/
        //LINQ - SEARCH ALL OWNERS/ANIMALS ACTIVES IN CONSULTATION
        private void tbConsultationSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = tbConsultationSearch.Text.ToLower();
            if (filter == "")
            {
                lvConsultationSearchResult.ItemsSource = dbAnimal.GetAllAnimalsActives();
                //HOW TO JOIN OWNERS/ANIMALS WITH 2 DIFERENTES FUNCTIONS??? CREATE A NEW FUNTION WITH JOIN???
            }
            else
            {
                List<Animal> listAnimal = dbAnimal.GetAllAnimalsActives();
                List<Owner> listOwner = dbOwner.GetAllOwnersActives();

                //multiple words LINQ ?????????????????????????????????????????????????????????????????
                string[] searchstrings = filter.Split(' ');
                var filteredList = from animal in listAnimal
                                   join owner in listOwner
                                   on animal.OwnerID equals owner.Id
                                   where searchstrings.All(word => animal.Name.ToLower().Contains(word) || owner.FirstName_01.Contains(word) || owner.FirstName_02.Contains(word))
                                   select animal;
                /*
                //one word LINQ
                var filteredList = from animal in listAnimal
                                   join owner in listOwner 
                                   on animal.OwnerID equals owner.Id
                                   where animal.Name.Contains(filter) || owner.FirstName_01.Contains(filter) || owner.FirstName_02.Contains(filter))
                                   select animal;
                */

                lvRegistryAnimalSearchResult.ItemsSource = filteredList;
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

        //LOAD FIELDS FROM OWNERS LIST
        private void lvRegistryOwnerSearchResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lvRegistryOwnerSearchResult.SelectedIndex;
            if (index < 0)
            {
                tbRegistryOwnerID.ToString();
                return;
            }
            Owner owner = ownerList[index];

            //General Information for both owners
            tbRegistryOwnerDateRegistration.Text = owner.RegistrationDate.ToString();
            tbRegistryOwnerID.Text = owner.Id.ToString();

            //Owner 1
            //imgRegistryOwner1Image.Source = owner.Picture_01;            //TODO: Descobrir qual é a propriedade da imagem que guarda o que a imagem tem dentro..Content? Text?
            tbRegistryOwner1FName.Text = owner.FirstName_01;
            tbRegistryOwner1MName.Text = owner.MiddleName_01;
            tbRegistryOwner1LName.Text = owner.LastName_01;
            tbRegistryOwner1NumberAddress.Text = owner.Number_01;
            tbRegistryOwner1Address.Text = owner.Address_01;
            tbRegistryOwner1Complement.Text = owner.Complement_01;
            tbRegistryOwner1City.Text = owner.City_01;
            cbRegistryOwner1Province.Text = owner.Province_01;              //puxar dado da lista para selecionar opcao do combobox
            tbRegistryOwner1PostalCode.Text = owner.PostalCode_01;
            tbRegistryOwner1Phone.Text = owner.PhoneNumber_01;
            tbRegistryOwner1OtherNumber.Text = owner.OtherPhoneNumber_01;
            tbRegistryOwner1Email.Text = owner.Email_01;

            //Owner 2
            //imgRegistryOwner2Image.Source = owner.Picture_02;             //TODO: Descobrir qual é a propriedade da imagem que guarda o que a imagem tem dentro..Content? Text?
            tbRegistryOwner2FName.Text = owner.FirstName_02;
            tbRegistryOwner2MName.Text = owner.MiddleName_02;
            tbRegistryOwner2LName.Text = owner.LastName_02;
            tbRegistryOwner2NumberAddress.Text = owner.Number_02;
            tbRegistryOwner2Address.Text = owner.Address_02;
            tbRegistryOwner2Complement.Text = owner.Complement_02;
            tbRegistryOwner2City.Text = owner.City_02;
            cbRegistryOwner2Province.Text = owner.Province_02;               //comboBOX
            tbRegistryOwner2PostalCode.Text = owner.PostalCode_02;
            tbRegistryOwner2Phone.Text = owner.PhoneNumber_02;
            tbRegistryOwner2OtherNumber.Text = owner.OtherPhoneNumber_02;
            tbRegistryOwner2Email.Text = owner.Email_02;

            string verifyOwnerCkbStatus = owner.Status;                      //Atualizando Status radiobutton de acordo com o Owner ????????????
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


        //SAVE(Add/Update) OWNERS
        private void btnRegistryOwnerSave_Click(object sender, RoutedEventArgs e)
        {
            SavingOwnerRegistryOnDB();
        }


        //SAVE OWNERS METHOD
        private void SavingOwnerRegistryOnDB()
        {
            var owner = new Owner
            {
                RegistrationDate = DateTime.Now.Date,                   // Add ok. mas update.. mudar a data de registro?????????????????

                //Owner 1
                //Picture_01 = imgRegistryOwner1Image.                  //IMAGE ????????????????????????????????????????????
                FirstName_01 = tbRegistryOwner1LName.Text,
                MiddleName_01 = tbRegistryOwner1MName.Text,
                LastName_01 = tbRegistryOwner1LName.Text,
                Number_01 = tbRegistryOwner1NumberAddress.Text,
                Address_01 = tbRegistryOwner1Address.Text,
                Complement_01 = tbRegistryOwner1Complement.Text,
                City_01 = tbRegistryOwner1City.Text,
                Province_01 = cbRegistryOwner1Province.Text,            //combobox
                PostalCode_01 = tbRegistryOwner1PostalCode.Text,      
                PhoneNumber_01 = tbRegistryOwner1Phone.Text,
                OtherPhoneNumber_01 = tbRegistryOwner1OtherNumber.Text,
                Email_01 = tbRegistryOwner1Email.Text,

                //Owner 2
                //Picture_02 = imgRegistryOwner2Image,                  //IMAGE ????????????????????????????????????????????
                FirstName_02 = tbRegistryOwner2LName.Text,
                MiddleName_02 = tbRegistryOwner2MName.Text,
                LastName_02 = tbRegistryOwner2LName.Text,
                Number_02 = tbRegistryOwner2NumberAddress.Text,
                Address_02 = tbRegistryOwner2Address.Text,
                Complement_02 = tbRegistryOwner2Complement.Text,
                City_02 = tbRegistryOwner2City.Text,
                Province_02 = cbRegistryOwner2Province.Text,             //combobox
                PostalCode_02 = tbRegistryOwner2PostalCode.Text,
                PhoneNumber_02 = tbRegistryOwner2Phone.Text,
                OtherPhoneNumber_02 = tbRegistryOwner2OtherNumber.Text,
                Email_02 = tbRegistryOwner2Email.Text,

                Status = gb_rb_OwnerStatus.Content.ToString()            //radio button group
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


        //SAVE OWNERS METHOD
        //Registry -> Owner -> Text changed on TextBox around the Registration window           //e os outros elementos diferentes de TB como combobom ou radiobutton??
        private void tbOwnerTextChanged(object sender, TextChangedEventArgs e)
        {
            unsavedChanges = true;
        }

        private void cbOwnerSelectionChanged(object sender, TextChangedEventArgs e)
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
                        SwitchToTabHome();
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                    case MessageBoxResult.Yes:
                        SavingOwnerRegistryOnDB();
                        break;
                }
                unsavedChanges = false;                             //zerando unsaved changes apos uso do botam exit
            }
        }



        /******************************************************************************************
        * REGISTRY => ANIMAL
        ******************************************************************************************/

        //LINQ - SEARCH ALL ANIMALS
        private void tbRegistryAnimalSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = tbRegistryAnimalSearch.Text.ToLower();
            if (filter == "")
            {
                lvRegistryAnimalSearchResult.ItemsSource = dbAnimal.GetAllAnimals();
            }
            else
            {
                List<Animal> listAnimal = dbAnimal.GetAllAnimals();
                List<Owner> listOwner = dbOwner.GetAllOwners();

                //multiple words LINQ ?????????????????????????????????????????????????????????????????
                string[] searchstrings = filter.Split(' ');
                var filteredList = from animal in listAnimal
                                   join owner in listOwner 
                                   on animal.OwnerID equals owner.Id
                                   where searchstrings.All(word => animal.Name.ToLower().Contains(word) || owner.FirstName_01.Contains(word) || owner.FirstName_02.Contains(word))
                                   select animal;
                /*
                //one word LINQ
                var filteredList = from animal in listAnimal
                                   join owner in listOwner 
                                   on animal.OwnerID equals owner.Id
                                   where animal.Name.Contains(filter) || owner.FirstName_01.Contains(filter) || owner.FirstName_02.Contains(filter))
                                   select animal;
                */

                lvRegistryAnimalSearchResult.ItemsSource = filteredList;
            }
        }

        private void refreshAnimalList()
        {
            lvRegistryAnimalSearchResult.ItemsSource = dbAnimal.GetAllAnimals();
        }


        //Registry -> Animal -> Search List Result
        private void lvRegistryAnimalSearchResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lvRegistryAnimalSearchResult.SelectedIndex;
            if (index < 0)
            {
                tbRegistryAnimalID.ToString();
                return;
            }
            Animal animal = animalList[index];

            tbRegistryAnimalDateRegistration.Text = animal.Datereg.ToString();
            tbRegistryAnimalID.Text = animal.Id.ToString();
            //imgRegistryAnimalPicture.Source = animal.Picture;     //TODO: Descobrir qual é a propriedade da imagem que guarda o que a imagem tem dentro..Content? Text?
            tbRegistryAnimalName.Text = animal.Name;
            dpRegistryAnimalBirthday.Text = animal.Dateofbirth.ToString();
            tbRegistryAnimalWeight.Text = animal.Weight.ToString();
            cbRegistryAnimalSpecies.Text = animal.Specie;
            //cbRegistryAnimalBreeds.Text = animal.BreedID;                                         //COMO PEGAR VALOR DA COLUNA NAME BY BREEDID?
            lvRegistryAnimalVaccines.ItemsSource = dbVaccineHistory.GetAllVaccineHistorics();       //COMO PEGAR VALOR DA TABELA E POR NA list BY VachistID?
            tbRegistryAnimalIdentification.Text = animal.Identification;
            tbRegistryAnimalFood.Text = animal.Food;
            tbRegistryAnimalPhobias.Text = animal.Phobia;
            //FLAGSET
            tbRegistryAnimalVetHistory.Text = animal.Vethistoric;


            //Atualizando Gender radiobutton de acordo com o Animal ????????????
            string verifyAnimalCkbGender = animal.Gender;   
            if (verifyAnimalCkbGender == "MALE")
            {
                rbRegistryAnimalMale.IsChecked = true;
                rbRegistryAnimalFemale.IsChecked = false;
            }
            else
            {
                rbRegistryAnimalMale.IsChecked = false;
                rbRegistryAnimalFemale.IsChecked = true;
            }

            //Atualizando Status radiobutton de acordo com o Animal ????????????
            string verifyAnimalCkbStatus = animal.Status;   
            if (verifyAnimalCkbStatus == "ACTIVE")
            {
                rbAnimalStatus_Active.IsChecked = true;
                rbAnimalStatus_Inactive.IsChecked = false;
            }
            else
            {
                rbAnimalStatus_Active.IsChecked = false;
                rbAnimalStatus_Inactive.IsChecked = true;
            }
        }


        //Registry -> Animal -> Buttons Save/Add Record Event
        private void btnRegistryAnimalSave_Click(object sender, RoutedEventArgs e)
        {
            SavingAnimalRegistryOnDB();
            refreshAnimalList();
        }

        //Registry -> Animal -> Method Saving to DB
        private void SavingAnimalRegistryOnDB()
        {
            var animal = new Animal
            {
                //Picture = imgRegistryAnimalPicture.  ,       //imagem
                //OwnerID = ?????????  ,                       //nao se tem campo com ownerID
                //BreedID = cbRegistryAnimalBreeds.Text,       //combobox
                //VachistID = lvRegistryAnimalVaccines,        //outra tabela
                Datereg = DateTime.Now.Date,                   // Add ok. mas update.. mudar a data de registro?????????????????
                Name = tbRegistryAnimalName.Text,
                //Fixme
                //Gender = gb_rb_AnimalGender.Content.ToString(), //radio button group,
                //Dateofbirth = dpRegistryAnimalBirthday  ,     // date picker 
                //Weight = tbRegistryAnimalWeight ,             //decimal
                Specie = cbRegistryAnimalSpecies.Text,
                Identification = tbRegistryAnimalIdentification.Text,
                Food = tbRegistryAnimalFood.Text,
                Phobia = tbRegistryAnimalPhobias.Text,
                //Flagset = ?????,                               //Conjunto de checkbox salvos em uma coluda separados por ,
                Vethistoric = tbRegistryAnimalVetHistory.Text,
                //Fixme
                //Status = gb_rb_AnimalStatus.Content.ToString()   //radio button group
            };

            try
            {
                animalBusiness.Save(animal);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonAddNewAnimal_Click(object sender, RoutedEventArgs e)
        {
            AnimalForm_clearFields();
        }

        //Registry -> Animal -> Button Exit Event
        private void btnRegistryAnimalExit_Click(object sender, RoutedEventArgs e)
        {
            if (unsavedChanges)
            {
                MessageBoxResult result = MessageBox.Show("Save unsaved changes?", "Unsaved changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.No:
                        AnimalForm_clearFields();
                        SwitchToTabHome();
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                    case MessageBoxResult.Yes:
                        SavingOwnerRegistryOnDB();
                        break;
                }
            }
        }
        
        //Registry -> Animal -> Text changed on TextBox around the Registration window           //e os outros elementos diferentes de TB como combobom ou radiobutton??
        private void tbAnimalTextChanged(object sender, TextChangedEventArgs e)
        {
            unsavedChanges = true;
        }

        private void AnimalForm_clearFields()
        {
            tbRegistryAnimalDateRegistration.Text = DateTime.Now.ToString();   //Data no campo Data Registration????????????
            tbRegistryAnimalID.Text = String.Empty;                             
            imgRegistryAnimalPicture.Source = null;                            //Limpar imagem????????????????????????????????????????????????
            tbRegistryAnimalName.Text = String.Empty;
            //dpRegistryAnimalBirthday.Text = DatePicker.
            //tbRegistryAnimalWeight.Text = ;                                   //Decimal
            cbRegistryAnimalSpecies.Items.Clear();                              //comboBOX
            cbRegistryAnimalBreeds.Items.Clear();                               //comboBOX
            lvRegistryAnimalVaccines.Items.Clear();                             //LIST VIEW
            tbRegistryAnimalIdentification.Text = String.Empty;
            tbRegistryAnimalFood.Text = String.Empty;
            tbRegistryAnimalPhobias.Text = String.Empty;
            //FLAGSET.Items.Clear();                                            //CHECKBOXES FLAG SET
            tbRegistryAnimalVetHistory.Text = String.Empty;
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
        


        /******************************************************************************************
         * REGISTRY => EMPLOYEE
         ******************************************************************************************/
          
        //LINQ - SEARCH ALL EMPLOYEE
        private void tbRegistryEmployeeSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = tbRegistryEmployeeSearch.Text.ToLower();
            if (filter == "")
            {
                lvRegistryEmployeeSearchResult.ItemsSource = dbEmployee.GetAllEmployees();
            }
            else
            {
                List<Employee> list = dbEmployee.GetAllEmployees();
                var filteredList = from emp in list where emp.Id.ToString().ToLower().Contains(filter) || (emp.FirstName + " " + emp.MiddleName + " " + emp.LastName).ToString().Contains(filter) select emp;  //primeiro "E" em vermelho

                lvRegistryOwnerSearchResult.ItemsSource = filteredList;
            }
        }

        //Registry -> Employee -> Search List Result
        private void lvRegistryEmployeeSearchResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lvRegistryEmployeeSearchResult.SelectedIndex;
            if (index < 0)
            {
                tbRegistryEmployeeID.ToString();
                return;
            }
            Employee employee = employeeList[index];


            tbRegistryOwnerID.Text = employee.Id.ToString();
            //imgRegistryOwner1Image.Source = owner.Picture_01;  //TODO: Descobrir qual é a propriedade da imagem que guarda o que a imagem tem dentro..Content? Text?
            tbRegistryOwner1FName.Text = employee.FirstName;
            tbRegistryOwner1MName.Text = employee.MiddleName;
            tbRegistryOwner1LName.Text = employee.LastName;
            tbRegistryOwner1NumberAddress.Text = employee.Number;
            tbRegistryOwner1Address.Text = employee.Address;
            tbRegistryOwner1Complement.Text = employee.Complement;
            tbRegistryOwner1City.Text = employee.City;
            cbRegistryOwner1Province.Text = employee.Province;
            tbRegistryOwner1PostalCode.Text = employee.PostalCode;
            tbRegistryOwner1Phone.Text = employee.PhoneNumber;
            tbRegistryOwner1OtherNumber.Text = employee.PhoneNumber;
            tbRegistryOwner1Email.Text = employee.Email;


            string verifyOwnerCkbStatus = employee.Status;   //Atualizando Status radiobutton de acordo com o Owner ????????????
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
        private void btnRegistryEmployeeSave_Click(object sender, RoutedEventArgs e)
        {
            SavingEmployeeRegistryOnDB();
        }


        //Registry -> Owner -> Method Saving to DB
        private void SavingEmployeeRegistryOnDB()
        {
            var owner = new Owner
            {
                RegistrationDate = DateTime.Now.Date,                   // Add ok. mas update.. mudar a data de registro?????????????????

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


        //Registry -> Employee -> Method to clear the fields
        private void EmployeeForm_clearFields()
        {
            tbRegistryEmployeeID.Text = String.Empty;
            imgRegistryEmployeeImage = null;                    //Limpar imagem????????????????????????????????????????????????
            tbRegistryEmployeeFName.Text = String.Empty;
            tbRegistryEmployeeMName.Text = String.Empty;
            tbRegistryEmployeeLName.Text = String.Empty;
            tbRegistryEmployeeNumberAddress.Text = String.Empty;
            tbRegistryEmployeeAddress.Text = String.Empty;
            tbRegistryEmployeeComplement.Text = String.Empty;
            tbRegistryEmployeeCity.Text = String.Empty;
            cbRegistryEmployeeProvince.Items.Clear();             //ComboBOX solucao que encontrei na internert para limpar item selecionado do elemento. certo??????
            tbRegistryEmployeePostalCode.Text = String.Empty;
            tbRegistryEmployeePhone.Text = String.Empty;
            tbRegistryEmployeeOtherNumber.Text = String.Empty;
            tbRegistryEmployeeEmail.Text = String.Empty;
            rbEmployeeStatus_Active.IsChecked = true;                   //STATUS Deixar como default o ACTIVE
            dpRegistryEmployeeHire.Text = null;   //Data no campo Data Registration????????????
            cbRegistryEmployeePositions.Items.Clear();             //ComboBOX solucao que encontrei na internert para limpar item selecionado do elemento. certo??????
            dpRegistryEmployeeTerm.Text = null;   //Data no campo Data Registration????????????
            tbRegistryEmployeeObservations.Text = String.Empty;
        }

        //Registry -> Employee -> Button Exit Event
        private void btnRegistryEmployeeExit_Click(object sender, RoutedEventArgs e)
        {
            if (unsavedChanges)
            {
                MessageBoxResult result = MessageBox.Show("Save unsaved changes?", "Unsaved changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.No:
                        EmployeeForm_clearFields();
                        SwitchToTabHome();
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                    case MessageBoxResult.Yes:
                        SavingEmployeeRegistryOnDB();
                        break;
                }
            }
        }

        //Registry -> Owner -> Text changed on TextBox around the Registration window           //e os outros elementos diferentes de TB como combobom ou radiobutton??
        private void tbEmployeeTextChanged(object sender, TextChangedEventArgs e)
        {
            unsavedChanges = true;
        }


        

        private void tbRegistrySearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        /******************************************************************************************
        * SECONDARY TABLES > BREEDS
        ******************************************************************************************/
        

        //LINQ - SEARCH ALL BREEDS
        private void tbTablesBreedSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = tbTablesBreedSearch.Text.ToLower();
            if (filter == "")
            {
                lvTableRegisterBreeds.ItemsSource = dbBreed.GetAllBreeds();
            }
            else
            {
                List<Breed> list = dbBreed.GetAllBreeds();
                var filteredList = from breed in list where breed.Name.ToLower().Contains(filter) select breed;
                lvTableRegisterBreeds.ItemsSource = filteredList;
            }
        }


        //REFRESH BREEDS LIST
        private void refreshBreedList()
        {
            lvTableRegisterBreeds.ItemsSource = dbBreed.GetAllBreeds();
        }


        //LOAD FIELDS FROM BREEDS LIST
        private void lvTableRegisterBreeds_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lvTableRegisterBreeds.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            Breed breed = (Breed)lvTableRegisterBreeds.Items[index];
            tbTablesBreedsID.Text = breed.Id.ToString();
            tbTablesBreedsName.Text = breed.Name;
            tbTablesBreedsSpecies.Text = breed.Specie;             

            string verifyBreedCkbStatus = breed.Status;                  //Atualizando Status radiobutton de acordo com o Breed ????????????
            if (verifyBreedCkbStatus == "ACTIVE")
            {
                rbTablesBreedsStatus_Active.IsChecked = true;
                rbTablesBreedsStatus_Inactive.IsChecked = false;
            }
            else
            {
                rbTablesBreedsStatus_Active.IsChecked = false;
                rbTablesBreedsStatus_Inactive.IsChecked = true;
            }
        }


        //SAVE(Add/Update) BREEDS
        private void btnTablesBreedsAdd_Click(object sender, RoutedEventArgs e)
        {
            var breed = new Breed
            {
                Specie = tbTablesBreedsSpecies.Text,
                Name = tbTablesBreedsName.Text,
                Status = gb_rb_TablesBreedsStatus.Content.ToString()       //radio button group
            };

            try
            {
                breedBusiness.Save(breed);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            refreshBreedList();
        }


        //DELETE BREEDS
        private void btnTablesBreedsDelete_Click(object sender, RoutedEventArgs e)
        {
            int index = lvTableRegisterBreeds.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            Breed breed = (Breed)lvTableRegisterBreeds.Items[index];
            try
            {
                breedBusiness.Delete(breed);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            refreshBreedList();
        }



        /******************************************************************************************
        * SECONDARY TABLES > VACCINES
        ******************************************************************************************/

        //LINQ - SEARCH ALL VACCINES
        private void tbTablesVaccineSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = tbTablesVaccineSearch.Text.ToLower();
            if (filter == "")
            {
                lvTableRegisterVaccines.ItemsSource = dbVaccine.GetAllVaccines();
            }
            else
            {
                List<Vaccine> list = dbVaccine.GetAllVaccines();
                var filteredList = from vaccine in list where vaccine.Name.ToLower().Contains(filter) select vaccine;
                lvTableRegisterVaccines.ItemsSource = filteredList;
            }
        }


        //REFRESH VACCINES LIST
        private void refreshVaccineList()
        {
            lvTableRegisterVaccines.ItemsSource = dbVaccine.GetAllVaccines();
        }


        //LOAD FIELDS FROM VACCINES LIST
        private void lvTableRegisterVaccines_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lvTableRegisterVaccines.SelectedIndex;
            if (index < 0)
            {
                return;
            }

            Vaccine vaccine = (Vaccine)lvTableRegisterVaccines.Items[index];
            tbTablesVaccinesID.Text = vaccine.Id.ToString();
            tbTablesVaccinesPrice.Text = vaccine.Price.ToString();
            tbTablesVaccinesName.Text = vaccine.Name;

            string verifyVaccineCkbStatus = vaccine.Status;                  //Atualizando Status radiobutton de acordo com o vaccine ????????????
            if (verifyVaccineCkbStatus == "ACTIVE")
            {
                rbTablesVaccinesStatus_Active.IsChecked = true;
                rbTablesVaccinesStatus_Inactive.IsChecked = false;
            }
            else
            {
                rbTablesVaccinesStatus_Active.IsChecked = false;
                rbTablesVaccinesStatus_Inactive.IsChecked = true;
            }
        }


        //SAVE(Add/Update) VACCINES
        private void btnTablesVaccinesAdd_Click(object sender, RoutedEventArgs e)
        {
            Decimal price = 0;
            Decimal.TryParse(tbTablesVaccinesPrice.Text, out price);

            var vaccine = new Vaccine
            {
                Name = tbTablesVaccinesName.Text,
                Price = price,
                Status = gb_rb_TablesVaccinesStatus.Content.ToString()       //radio button group
            };

            try
            {
                vaccineBusiness.Save(vaccine);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            refreshVaccineList();
        }


        //DELETE VACCINES
        private void btnTablesVaccinesDelete_Click(object sender, RoutedEventArgs e)
        {
            int index = lvTableRegisterVaccines.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            Vaccine vaccine = (Vaccine)lvTableRegisterVaccines.Items[index];
            try
            {
                vaccineBusiness.Delete(vaccine);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            refreshVaccineList();
        }


        /******************************************************************************************
        * SECONDARY TABLES > SERVICES & PRODUCTS
        ******************************************************************************************/
        
        //LINQ - SEARCH ALL SERVICES & PRODUCTS
        private void tbTablesProdServSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = tbTablesProdServSearch.Text.ToLower();
            if (filter == "")
            {
                lvTablesRegisterServicesProducts.ItemsSource = dbServicesProducts.GetAllServicesProducts();
            }
            else
            {
                List<ServicesProducts> list = dbServicesProducts.GetAllServicesProducts();
                var filteredList = from servicesproducts in list where servicesproducts.Name.ToLower().Contains(filter) select servicesproducts;
                lvTablesRegisterServicesProducts.ItemsSource = filteredList;
            }
        }


        //REFRESH SERVICES & PRODUCTS LIST
        private void refreshServicesProductsList()
        {
            lvTablesRegisterServicesProducts.ItemsSource = dbServicesProducts.GetAllServicesProducts();
        }


        //LOAD FIELDS FROM SERVICES & PRODUCTS LIST
        private void lvTableRegisterServicesProducts_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lvTablesRegisterServicesProducts.SelectedIndex;
            if (index < 0)
            {
                return;
            }

            ServicesProducts servicesproducts = (ServicesProducts)lvTableRegisterVaccines.Items[index];
            tbTablesServicesProductsID.Text = servicesproducts.Id.ToString();
            tbTablesServicesProductsPrice.Text = servicesproducts.Price.ToString();
            tbTablesServicesProductsName.Text = servicesproducts.Name;

            string verifyVaccineCkbStatus = servicesproducts.Status;                  //Atualizando Status radiobutton de acordo com o vaccine ????????????
            if (verifyVaccineCkbStatus == "ACTIVE")
            {
                rbTablesVaccinesStatus_Active.IsChecked = true;
                rbTablesVaccinesStatus_Inactive.IsChecked = false;
            }
            else
            {
                rbTablesVaccinesStatus_Active.IsChecked = false;
                rbTablesVaccinesStatus_Inactive.IsChecked = true;
            }
        }

        //SAVE(Add/Update) SERVICES & PRODUCTS
        private void btnTablesServicesProductsAdd_Click(object sender, RoutedEventArgs e)
        {
            Decimal price = 0;
            Decimal.TryParse(tbTablesServicesProductsPrice.Text, out price);

            var servicesproducts = new ServicesProducts
            {
                Name = tbTablesServicesProductsName.Text,
                Price = price,
                Status = gb_rb_TablesServProdStatus.Content.ToString()       //radio button group
            };

            try
            {
                servicesproductsBusiness.Save(servicesproducts);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            refreshServicesProductsList();
        }

        //DELETE Tables ServicesProducts
        private void btnTablesServicesProductsDelete_Click(object sender, RoutedEventArgs e)
        {
            int index = lvTablesRegisterServicesProducts.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            ServicesProducts servicesproducts = (ServicesProducts)lvTablesRegisterServicesProducts.Items[index];
            try
            {
                servicesproductsBusiness.Delete(servicesproducts);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            refreshServicesProductsList();
        }




        /******************************************************************************************
        * COMMON METHODS
        ******************************************************************************************/

        //RETURN HOME
        private void SwitchToTabHome()
        {
            Home.IsSelected = true;
        }















    }
}




            