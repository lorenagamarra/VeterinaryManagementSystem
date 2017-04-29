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
using VeterinaryManagementSystem.ViewModels;

namespace VeterinaryManagementSystem
{

    public partial class MainWindow : Window
    {
        AnimalDataAccess dbAnimal;
        BreedDataAccess dbBreed;
        SpecieDataAccess dbSpecie;
        ConsultationDataAccess dbConsultation;
        EmployeeDataAccess dbEmployee;
        OwnerDataAccess dbOwner;
        ServicesProductsDataAccess dbServicesProducts;
        VaccineDataAccess dbVaccine;
        VaccineHistoricDataAccess dbVaccineHistory;

        private List<Specie> allSpecie;

        List<Animal> animalList = new List<Animal>();
        List<Breed> breedList = new List<Breed>();
        List<Specie> specieList = new List<Specie>();
        List<Consultation> consultationList = new List<Consultation>();
        List<Employee> employeeList = new List<Employee>();
        List<Owner> ownerList = new List<Owner>();
        List<ServicesProducts> servicesProductsList = new List<ServicesProducts>();
        List<Vaccine> vaccineList = new List<Vaccine>();
        List<VaccineHistoric> vaccineHistoryList = new List<VaccineHistoric>();

        private bool unsavedChanges = false;

        private AnimalBusiness animalBusiness;
        private BreedBusiness breedBusiness;
        private SpecieBusiness specieBusiness;
        private ConsultationBusiness consultationBusiness;
        private EmployeeBusiness employeeBusiness;
        private OwnerBusiness ownerBusiness;
        private ServicesProductsBusiness servicesproductsBusiness;
        private VaccineBusiness vaccineBusiness;
        private VaccineHistoricBusiness vaccinehistoricBusiness;

        private SpecieNameViewModel SpecieNameViewModel { get; set; }
        private BreedNameViewModel BreedNameViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                dbAnimal = new AnimalDataAccess();
                dbBreed = new BreedDataAccess();
                dbSpecie = new SpecieDataAccess();
                dbConsultation = new ConsultationDataAccess();
                dbEmployee = new EmployeeDataAccess();
                dbOwner = new OwnerDataAccess();
                dbServicesProducts = new ServicesProductsDataAccess();
                dbVaccine = new VaccineDataAccess();
                dbVaccineHistory = new VaccineHistoricDataAccess();

                allSpecie = dbSpecie.GetAllSpecieActives();



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
                specieBusiness = new SpecieBusiness();
                consultationBusiness = new ConsultationBusiness();
                employeeBusiness = new EmployeeBusiness();
                ownerBusiness = new OwnerBusiness();
                servicesproductsBusiness = new ServicesProductsBusiness();
                vaccineBusiness = new VaccineBusiness();
                vaccinehistoricBusiness = new VaccineHistoricBusiness();


                SpecieNameViewModel = new SpecieNameViewModel();
                SpecieNameViewModel.Specie = dbSpecie.GetAllSpecieActives();
                cbRegistryAnimalSpecies.DataContext = SpecieNameViewModel;
                cbTablesBreedsSpecies.DataContext = SpecieNameViewModel;

                BreedNameViewModel = new BreedNameViewModel();
                BreedNameViewModel.Breeds = dbBreed.GetAllBreedActives();
                cbRegistryAnimalBreeds.DataContext = BreedNameViewModel;


                //REFRESH LISTS
                refreshBreedList();
                refreshSpecieList();
                refreshVaccineList();
                refreshServicesProductsList();
                refreshRegistryOwnerList();

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
        private Owner SelectedOwner = new Owner();

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

        //REFRESH OWNER LIST
        private void refreshRegistryOwnerList()
        {
            lvRegistryOwnerSearchResult.ItemsSource = dbOwner.GetAllOwners();
        }

        //CLEAR FIELDS AND UNSELECT LISTVIEW
        private void RegistryOwner_clearFields_UnselectListView()
        {
            tbRegistryOwnerID.Text = String.Empty;
            tbRegistryOwnerDateRegistration.Text = DateTime.Now.ToString();

            //Owner 1
            imgRegistryOwner1Image.Source = null;
            tbRegistryOwner1FName.Text = String.Empty;
            tbRegistryOwner1MName.Text = String.Empty;
            tbRegistryOwner1LName.Text = String.Empty;
            tbRegistryOwner1NumberAddress.Text = String.Empty;
            tbRegistryOwner1Address.Text = String.Empty;
            tbRegistryOwner1Complement.Text = String.Empty;
            tbRegistryOwner1City.Text = String.Empty;
            //cbRegistryOwner1Province.Items.Clear();
            tbRegistryOwner1PostalCode.Text = String.Empty;
            tbRegistryOwner1Phone.Text = String.Empty;
            tbRegistryOwner1OtherNumber.Text = String.Empty;
            tbRegistryOwner1Email.Text = String.Empty;

            //Owner 2
            imgRegistryOwner2Image.Source = null;
            tbRegistryOwner2FName.Text = String.Empty;
            tbRegistryOwner2MName.Text = String.Empty;
            tbRegistryOwner2LName.Text = String.Empty;
            tbRegistryOwner2NumberAddress.Text = String.Empty;
            tbRegistryOwner2Address.Text = String.Empty;
            tbRegistryOwner2Complement.Text = String.Empty;
            tbRegistryOwner2City.Text = String.Empty;
            //cbRegistryOwner2Province.Items.Clear();
            tbRegistryOwner2PostalCode.Text = String.Empty;
            tbRegistryOwner2Phone.Text = String.Empty;
            tbRegistryOwner2OtherNumber.Text = String.Empty;
            tbRegistryOwner2Email.Text = String.Empty;

            rbOwnerStatus_Active.IsChecked = true;
            lvRegistryOwnerSearchResult.SelectedIndex = -1;
            unsavedChanges = false;
        }

        //LOAD FIELDS FROM OWNERS LIST
        private void lvRegistryOwnerSearchResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lvRegistryOwnerSearchResult.SelectedIndex;
            if (index < 0)
            {
                return;
            }

            SelectedOwner = (Owner)lvRegistryOwnerSearchResult.SelectedItem;

            tbRegistryOwnerID.Text = SelectedOwner.Id.ToString();
            tbRegistryOwnerDateRegistration.Text = SelectedOwner.RegistrationDate.ToString();

            //Owner 1
            //imgRegistryOwner1Image = SelectedOwner.Picture_01.;       //criar codigo de frank
            tbRegistryOwner1FName.Text = SelectedOwner.FirstName_01;
            tbRegistryOwner1MName.Text = SelectedOwner.MiddleName_01;
            tbRegistryOwner1LName.Text = SelectedOwner.LastName_01;
            tbRegistryOwner1NumberAddress.Text = SelectedOwner.Number_01;
            tbRegistryOwner1Address.Text = SelectedOwner.Address_01;
            tbRegistryOwner1Complement.Text = SelectedOwner.Complement_01;
            tbRegistryOwner1City.Text = SelectedOwner.City_01;
            //cbRegistryOwner1Province.ItemsSource = SelectedOwner.Province_01;    //combobox load from list
            tbRegistryOwner1PostalCode.Text = SelectedOwner.PostalCode_01;
            tbRegistryOwner1Phone.Text = SelectedOwner.PhoneNumber_01;
            tbRegistryOwner1OtherNumber.Text = SelectedOwner.OtherPhoneNumber_01;
            tbRegistryOwner1Email.Text = SelectedOwner.Email_01;

            //Owner 2
            //imgRegistryOwner2Image = SelectedOwner.Picture_02.;       //criar codigo de frank
            tbRegistryOwner2FName.Text = SelectedOwner.FirstName_02;
            tbRegistryOwner2MName.Text = SelectedOwner.MiddleName_02;
            tbRegistryOwner2LName.Text = SelectedOwner.LastName_02;
            tbRegistryOwner2NumberAddress.Text = SelectedOwner.Number_02;
            tbRegistryOwner2Address.Text = SelectedOwner.Address_02;
            tbRegistryOwner2Complement.Text = SelectedOwner.Complement_02;
            tbRegistryOwner2City.Text = SelectedOwner.City_02;
            //cbRegistryOwner2Province.ItemsSource = SelectedOwner.Province_02;   //combobox load from list
            tbRegistryOwner2PostalCode.Text = SelectedOwner.PostalCode_02;
            tbRegistryOwner2Phone.Text = SelectedOwner.PhoneNumber_02;
            tbRegistryOwner2OtherNumber.Text = SelectedOwner.OtherPhoneNumber_02;
            tbRegistryOwner2Email.Text = SelectedOwner.Email_02;

            if (SelectedOwner.Status)
            {
                rbOwnerStatus_Active.IsChecked = true;
            }
            else
            {
                rbOwnerStatus_Inactive.IsChecked = true;
            }
        }

        //ADD NEW OWNER (clear fields and unselect list)
        private void btnRegistryOwnerAdd_Click(object sender, RoutedEventArgs e)
        {
            RegistryOwner_clearFields_UnselectListView();
        }

        //SAVE(Add/Update) OWNERS
        private void btnRegistryOwnerSave_Click(object sender, RoutedEventArgs e)
        {
            SavingOwnerRegistryOnDB();
            refreshAnimalList();
        }

        //SAVE OWNERS METHOD
        private void SavingOwnerRegistryOnDB()
        {
            MemoryStream memStream1 = new MemoryStream();
            JpegBitmapEncoder encoder1 = new JpegBitmapEncoder();
            if (imgRegistryOwner1Image.Source != null)
            {
                encoder1.Frames.Add(BitmapFrame.Create((BitmapSource)imgRegistryOwner1Image.Source));
                encoder1.Save(memStream1);
            }
            MemoryStream memStream2 = new MemoryStream();
            JpegBitmapEncoder encoder2 = new JpegBitmapEncoder();
            if (imgRegistryOwner2Image.Source != null)
            {
                encoder2.Frames.Add(BitmapFrame.Create((BitmapSource)imgRegistryOwner2Image.Source));
                encoder2.Save(memStream2);
            }

            var id = 0;
            Int32.TryParse(tbRegistryOwnerID.Text, out id);

            SelectedOwner.Id = id;
            SelectedOwner.RegistrationDate = DateTime.Now;

            SelectedOwner.Picture_01 = memStream1.ToArray();
            SelectedOwner.FirstName_01 = tbRegistryOwner1FName.Text;
            SelectedOwner.MiddleName_01 = tbRegistryOwner1MName.Text;
            SelectedOwner.LastName_01 = tbRegistryOwner1LName.Text;
            SelectedOwner.Number_01 = tbRegistryOwner1NumberAddress.Text;
            SelectedOwner.Address_01 = tbRegistryOwner1Address.Text;
            SelectedOwner.Complement_01 = tbRegistryOwner1Complement.Text;
            SelectedOwner.City_01 = tbRegistryOwner1City.Text;
            SelectedOwner.Province_01 = cbRegistryOwner1Province.Text;              //combobox
            SelectedOwner.PostalCode_01 = tbRegistryOwner1PostalCode.Text;
            SelectedOwner.PhoneNumber_01 = tbRegistryOwner1Phone.Text;
            SelectedOwner.OtherPhoneNumber_01 = tbRegistryOwner1OtherNumber.Text;
            SelectedOwner.Email_01 = tbRegistryOwner1Email.Text;

            SelectedOwner.Picture_02 = memStream2.ToArray();
            SelectedOwner.FirstName_02 = tbRegistryOwner2FName.Text;
            SelectedOwner.MiddleName_02 = tbRegistryOwner2MName.Text;
            SelectedOwner.LastName_02 = tbRegistryOwner2LName.Text;
            SelectedOwner.Number_02 = tbRegistryOwner2NumberAddress.Text;
            SelectedOwner.Address_02 = tbRegistryOwner2Address.Text;
            SelectedOwner.Complement_02 = tbRegistryOwner2Complement.Text;
            SelectedOwner.City_02 = tbRegistryOwner2City.Text;
            SelectedOwner.Province_02 = cbRegistryOwner2Province.Text;              //combobox
            SelectedOwner.PostalCode_02 = tbRegistryOwner2PostalCode.Text;
            SelectedOwner.PhoneNumber_02 = tbRegistryOwner2Phone.Text;
            SelectedOwner.OtherPhoneNumber_02 = tbRegistryOwner2OtherNumber.Text;
            SelectedOwner.Email_02 = tbRegistryOwner2Email.Text;

            SelectedOwner.Status = rbOwnerStatus_Active.IsChecked.Value;

            try
            {
                ownerBusiness.Save(SelectedOwner);
                SelectedOwner = new Owner();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            refreshRegistryOwnerList();
            //unsavedChanges = false;
        }

        //Registry -> Owner -> Text changed on TextBox around the Registration window    ???   //e os outros elementos diferentes de TB como combobom ou radiobutton??
        private void tbOwnerTextChanged(object sender, TextChangedEventArgs e)
        {
            unsavedChanges = true;
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
                        RegistryOwner_clearFields_UnselectListView();
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

        private void btnRegistryOwner1TakePicture_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new WebCamWindow();
            newWindow.ShowDialog();
            //unsavedChanges = true;
            if (newWindow.DialogResult.HasValue && newWindow.DialogResult.Value)
            {
                imgRegistryOwner1Image.Source = ((WebCamWindow)Application.Current.MainWindow).imgCapture.Source;
            }
        }

        private void btnRegistryOwner2TakePicture_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new WebCamWindow();
            newWindow.Show();
            unsavedChanges = true;
        }

        
        
        
        /******************************************************************************************
        * REGISTRY => ANIMAL
        ******************************************************************************************/
        private Animal SelectedAnimal = new Animal();

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
                
                /*
                //multiple words LINQ ?????????????????????????????????????????????????????????????????
                string[] searchstrings = filter.Split(' ');
                var filteredList = from animal in listAnimal
                                   join owner in listOwner
                                   on animal.OwnerID equals owner.Id
                                   where searchstrings.All(word => animal.Name.ToLower().Contains(word) || owner.FirstName_01.Contains(word) || owner.FirstName_02.Contains(word)
                                   select animal;
                */

                //one word LINQ
                var filteredList = from animal in listAnimal
                                   join owner in listOwner 
                                   on animal.OwnerID equals owner.Id
                                   where animal.Name.Contains(filter) || owner.FirstName_01.Contains(filter) || owner.FirstName_02.Contains(filter)
                                   select animal;
                

                lvRegistryAnimalSearchResult.ItemsSource = filteredList;
            }
        }

        //REFRESH ANIMALS LIST
        private void refreshAnimalList()
        {
            lvRegistryAnimalSearchResult.ItemsSource = dbAnimal.GetAllAnimals();
        }

        //CLEAR FIELDS AND UNSELECT LISTVIEW
        private void AnimalForm_clearFields()
        {
            tbRegistryAnimalDateRegistration.Text = DateTime.Now.ToString();   
            tbRegistryAnimalID.Text = String.Empty;
            imgRegistryAnimalPicture.Source = null;                            
            tbRegistryAnimalName.Text = String.Empty;
            rbRegistryAnimalMale.IsChecked = false;
            rbRegistryAnimalFemale.IsChecked = false;
            dpRegistryAnimalBirthday.SelectedDate = DateTime.Today;
            tbRegistryAnimalWeight.Text = null;
            cbRegistryAnimalSpecies.DataContext = SpecieNameViewModel;
            cbRegistryAnimalBreeds.DataContext = BreedNameViewModel;
            tbRegistryAnimalIdentification.Text = String.Empty;
            tbRegistryAnimalFood.Text = String.Empty;
            tbRegistryAnimalPhobias.Text = String.Empty;
            //FLAGSET.Items.Clear();                                            //CHECKBOX FLAG SET
            lvRegistryAnimalVaccines.Items.Clear();
            tbRegistryAnimalVetHistory.Text = String.Empty;
            rbAnimalStatus_Active.IsChecked = true;
            unsavedChanges = false;
        }
        
        //LOAD FIELDS FROM ANIMALS LIST
        private void lvRegistryAnimalSearchResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lvRegistryAnimalSearchResult.SelectedIndex;
            if (index < 0)
            {
                return;
            }

            SelectedAnimal = (Animal)lvRegistryAnimalSearchResult.SelectedItem;


            tbRegistryAnimalDateRegistration.Text = SelectedAnimal.Datereg.ToString();
            tbRegistryAnimalID.Text = SelectedAnimal.Id.ToString();
            //imgRegistryAnimalPicture.Source = null;                                       //criar codigo de frank
            tbRegistryAnimalName.Text = SelectedAnimal.Name;
            dpRegistryAnimalBirthday.SelectedDate = SelectedAnimal.Dateofbirth;
            tbRegistryAnimalWeight.Text = SelectedAnimal.Weight.ToString();

            //cbRegistryAnimalSpecies.DataContext = SelectedAnimal.BreedID (breed Name) ;                 //combobox como carregar dado do database vindo de outra tabela?
            //cbRegistryAnimalBreeds.DataContext = SelectedAnimal.BreedID (specie SpecieName;                  //combobox como carregar dado do database vindo de outra tabela?

            tbRegistryAnimalIdentification.Text = SelectedAnimal.Identification;
            tbRegistryAnimalFood.Text = SelectedAnimal.Food;
            tbRegistryAnimalPhobias.Text = SelectedAnimal.Phobia;
            //FLAGSET.Items.Clear();                                                       //CHECKBOX FLAG SET
            lvRegistryAnimalVaccines.Items.Clear();
            tbRegistryAnimalVetHistory.Text = String.Empty;
            rbAnimalStatus_Active.IsChecked = true;

            if (SelectedAnimal.Gender)
            {
                rbRegistryAnimalMale.IsChecked = true;
            }
            else
            {
                rbRegistryAnimalFemale.IsChecked = true;
            }

            if (SelectedAnimal.Status)
            {
                rbAnimalStatus_Active.IsChecked = true;
            }
            else
            {
                rbAnimalStatus_Inactive.IsChecked = true;
            }
            refreshAnimalList();            
        }
        
        //ADD NEW ANIMALS (clear fields)
        private void ButtonAddNewAnimal_Click(object sender, RoutedEventArgs e)
        {
            AnimalForm_clearFields();
        }

        //SAVE(Add/Update) ANIMALS
        private void btnRegistryAnimalSave_Click(object sender, RoutedEventArgs e)
        {
            SavingAnimalRegistryOnDB();
            refreshAnimalList();
        }

        //SAVE ANIMALS METHOD
        private void SavingAnimalRegistryOnDB()
        {
            MemoryStream memStream1 = new MemoryStream();
            JpegBitmapEncoder encoder1 = new JpegBitmapEncoder();
            if (imgRegistryOwner1Image.Source != null)
            {
                encoder1.Frames.Add(BitmapFrame.Create((BitmapSource)imgRegistryOwner1Image.Source));
                encoder1.Save(memStream1);
            }

            var id = 0;
            Int32.TryParse(tbRegistryOwnerID.Text, out id);

            Decimal weight = 0;
            Decimal.TryParse(tbTablesVaccinesPrice.Text, out weight);
            

            SelectedAnimal.Id = id;
            SelectedAnimal.Datereg = DateTime.Now;
            SelectedAnimal.Picture = memStream1.ToArray();
            //SelectedAnimal.OwnerID = ????;                                            //como pegar Owner ID????
            SelectedAnimal.BreedID = cbRegistryAnimalBreeds.SelectedIndex;              //pegar o ID nao Index da lista
            //SelectedAnimal.VachistID = ????                                           //Como pegar VacHist ID associado ao animal
            SelectedAnimal.Name = tbRegistryAnimalName.Text;
            SelectedAnimal.Gender = rbRegistryAnimalMale.IsChecked.Value;
            SelectedAnimal.Dateofbirth = dpRegistryAnimalBirthday.SelectedDate.Value;
            SelectedAnimal.Weight = weight;
            SelectedAnimal.Identification = tbRegistryAnimalIdentification.Text;
            SelectedAnimal.Food = tbRegistryAnimalFood.Text;
            SelectedAnimal.Phobia = tbRegistryAnimalPhobias.Text;
            //SelectedAnimal.Flagset = ?????;                                           //como salvar ???
            SelectedAnimal.Vethistoric = tbRegistryAnimalVetHistory.Text;
            SelectedAnimal.Status = rbAnimalStatus_Active.IsChecked.Value;

            try
            {
                animalBusiness.Save(SelectedAnimal);
                SelectedAnimal = new Animal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            refreshAnimalList();
            unsavedChanges = false;
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
                        SavingAnimalRegistryOnDB();
                        break;
                }
            }
        }

        //Registry -> Animal -> Text changed on TextBox around the Registration window           //e os outros elementos diferentes de TB como combobom ou radiobutton??
        private void tbAnimalTextChanged(object sender, TextChangedEventArgs e)
        {
            unsavedChanges = true;
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

            /*
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
            */
        }

        //Registry -> Employee -> Buttons Save/Add Record Event
        private void btnRegistryEmployeeSave_Click(object sender, RoutedEventArgs e)
        {
            SavingEmployeeRegistryOnDB();
        }

        //ADD NEW Employee (clear fields and unselect list)
        private void btnRegistryEmployeeAdd_Click(object sender, RoutedEventArgs e)
        {
            EmployeeForm_clearFields();
        }

        //Registry -> Employee -> Method Saving to DB
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

                //Status = gb_rb_OwnerStatus.Content.ToString()       //radio button group
            };

            try
            {
                ownerBusiness.Save(owner);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            unsavedChanges = false;
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
            //cbRegistryEmployeePositions.Items.Clear();             //ComboBOX solucao que encontrei na internert para limpar item selecionado do elemento. certo??????
            dpRegistryEmployeeTerm.Text = null;   //Data no campo Data Registration????????????
            tbRegistryEmployeeObservations.Text = String.Empty;

            lvRegistryEmployeeSearchResult.SelectedIndex = -1;
            unsavedChanges = false;
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

        private void tbRegistrySearch_TextChanged(object sender, TextChangedEventArgs e)         //?????????????????????
        {

        }



        /******************************************************************************************
        * SECONDARY TABLES > BREEDS
        ******************************************************************************************/
        private Breed SelectedBreed = new Breed();

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
            refreshBreedList();
        }

        //REFRESH BREED LIST
        private void refreshBreedList()
        {
            lvTableRegisterBreeds.ItemsSource = dbBreed.GetAllBreeds();
        }

        //CLEAR FIELDS AND UNSELECT LISTVIEW
        private void TableBreeds_clearFields_UnselectListView()
        {
            tbTablesBreedSearch.Text = String.Empty;
            tbTablesBreedsID.Text = String.Empty;
            //cbTablesBreedsSpecies.DataContext = SpecieNameViewModel;
            tbTablesBreedsName.Text = String.Empty;
            rbTablesBreedsStatus_Active.IsChecked = true;
            lvTableRegisterBreeds.SelectedIndex = -1;
        }

        //LOAD FIELDS FROM BREED LIST
        private void lvTableRegisterBreeds_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lvTableRegisterBreeds.SelectedIndex;
            if (index < 0)
            {
                return;
            }

            SelectedBreed = (Breed)lvTableRegisterBreeds.SelectedItem;

            tbTablesBreedsID.Text = SelectedBreed.Id.ToString();
            tbTablesBreedsName.Text = SelectedBreed.Name;
            cbTablesBreedsSpecies.SelectedItem = allSpecie[0];       //Carregar combobox com valor salvo na tblBreed (SpecieID)

            if (SelectedBreed.Status)
            {
                rbTablesBreedsStatus_Active.IsChecked = true;
            }
            else
            {
                rbTablesBreedsStatus_Inactive.IsChecked = true;
            }
            refreshBreedList();
        }

        //ADD NEW BREED (clear fields and unselect list)
        private void btnTablesBreedsAdd_Click(object sender, RoutedEventArgs e)
        {
            TableBreeds_clearFields_UnselectListView();
            refreshBreedList();
        }

        //SAVE(Add/Update) BREED
        private void btnTablesBreedsSave_Click(object sender, RoutedEventArgs e)
        {
            var id = 0;
            Int32.TryParse(tbTablesBreedsID.Text, out id);

            SelectedBreed.Id = id;
            SelectedBreed.Name = tbTablesBreedsName.Text;
            SelectedBreed.SpecieID = cbTablesBreedsSpecies.SelectedIndex;
            SelectedBreed.Status = rbTablesBreedsStatus_Active.IsChecked.Value;

            try
            {
                breedBusiness.Save(SelectedBreed);
                SelectedBreed = new Breed();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            refreshBreedList();
        }

        //DELETE BREED
        private void btnTablesBreedsDelete_Click(object sender, RoutedEventArgs e)
        {
            int index = lvTableRegisterBreeds.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            SelectedBreed = (Breed)lvTableRegisterBreeds.SelectedItem;
            try
            {
                breedBusiness.Delete(SelectedBreed);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            refreshBreedList();
        }


        /******************************************************************************************
        * SECONDARY TABLES > SPECIES
        ******************************************************************************************/
        private Specie SelectedSpecie = new Specie();

        private string FindSpecieNameById(int Id)
        {
            var result = from specie in allSpecie where specie.Id == Id select specie.SpecieName;
            return result.First();
        }

        //LINQ - SEARCH ALL SPECIES
        private void tbTablesSpecieSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = tbTablesSpeciesSearch.Text.ToLower();
            if (filter == "")
            {
                lvTableRegisterSpecies.ItemsSource = dbSpecie.GetAllSpecies();
            }
            else
            {
                List<Specie> list = dbSpecie.GetAllSpecies();
                var filteredList = from specie in list where specie.SpecieName.ToLower().Contains(filter) select specie;
                lvTableRegisterSpecies.ItemsSource = filteredList;
            }
        }

        //REFRESH SPECIES LIST
        private void refreshSpecieList()
        {
            lvTableRegisterSpecies.ItemsSource = dbSpecie.GetAllSpecies();
        }

        //CLEAR FIELDS AND UNSELECT LISTVIEW
        private void TableSpecies_clearFields_UnselectListView()
        {
            tbTablesSpeciesSearch.Text = String.Empty;
            tbTablesSpeciesID.Text = String.Empty;
            tbTablesSpeciesName.Text = String.Empty;
            rbTablesSpeciesStatus_Active.IsChecked = true;
            lvTableRegisterSpecies.SelectedIndex = -1;
        }

        //LOAD FIELDS FROM SPECIES LIST
        private void lvTableRegisterSpecies_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lvTableRegisterSpecies.SelectedIndex;
            if (index < 0)
            {
                return;
            }

            SelectedSpecie = (Specie)lvTableRegisterSpecies.SelectedItem;

            tbTablesSpeciesID.Text = SelectedSpecie.Id.ToString();
            tbTablesSpeciesName.Text = SelectedSpecie.SpecieName;

            if (SelectedSpecie.Status)
            {
                rbTablesSpeciesStatus_Active.IsChecked = true;
            }
            else
            {
                rbTablesSpeciesStatus_Inactive.IsChecked = true;
            }
        }

        //ADD NEW SPECIES (clear fields and unselect list)
        private void btnTablesSpeciesAdd_Click(object sender, RoutedEventArgs e)
        {
            TableSpecies_clearFields_UnselectListView();
        }

        //SAVE(Add/Update) SPECIES
        private void btnTablesSpeciesSave_Click(object sender, RoutedEventArgs e)
        {
            var id = 0;
            Int32.TryParse(tbTablesSpeciesID.Text, out id);

            SelectedSpecie.Id = id;
            SelectedSpecie.SpecieName = tbTablesSpeciesName.Text;
            SelectedSpecie.Status = rbTablesSpeciesStatus_Active.IsChecked.Value;

            try
            {
                specieBusiness.Save(SelectedSpecie);
                SelectedSpecie = new Specie();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            refreshSpecieList();
        }

        //DELETE SPECIES
        private void btnTablesSpeciesDelete_Click(object sender, RoutedEventArgs e)
        {
            int index = lvTableRegisterSpecies.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            SelectedSpecie = (Specie)lvTableRegisterSpecies.SelectedItem;
            try
            {
                specieBusiness.Delete(SelectedSpecie);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            refreshSpecieList();
        }




        /******************************************************************************************
        * SECONDARY TABLES > VACCINES
        ******************************************************************************************/
        private Vaccine SelectedVaccine = new Vaccine();

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

        //REFRESH VACCINE LIST
        private void refreshVaccineList()
        {
            lvTableRegisterVaccines.ItemsSource = dbVaccine.GetAllVaccines();
        }

        //CLEAR FIELDS AND UNSELECT LISTVIEW
        private void TableVaccines_clearFields_UnselectListView()
        {
            tbTablesVaccineSearch.Text = String.Empty;
            tbTablesVaccinesID.Text = String.Empty;
            tbTablesVaccinesPrice.Text = String.Empty;
            tbTablesVaccinesName.Text = String.Empty;
            rbTablesVaccinesStatus_Active.IsChecked = true;
            lvTableRegisterVaccines.SelectedIndex = -1;
        }

        //LOAD FIELDS FROM VACCINE LIST
        private void lvTableRegisterVaccines_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lvTableRegisterVaccines.SelectedIndex;
            if (index < 0)
            {
                return;
            }

            SelectedVaccine = (Vaccine)lvTableRegisterVaccines.SelectedItem;

            tbTablesVaccinesID.Text = SelectedVaccine.Id.ToString();
            tbTablesVaccinesName.Text = SelectedVaccine.Name;
            tbTablesVaccinesPrice.Text = SelectedVaccine.Price.ToString();

            if (SelectedVaccine.Status)
            {
                rbTablesVaccinesStatus_Active.IsChecked = true;
            }
            else
            {
                rbTablesVaccinesStatus_Inactive.IsChecked = true;
            }
        }

        //ADD NEW VACCINE (clear fields and unselect list)
        private void btnTablesVaccinesAdd_Click(object sender, RoutedEventArgs e)
        {
            TableVaccines_clearFields_UnselectListView();
        }

        //SAVE VACCINES
        private void btnTablesVaccinesSave_Click(object sender, RoutedEventArgs e)
        {
            var id = 0;
            Int32.TryParse(tbTablesVaccinesID.Text, out id);

            Decimal price = 0;
            Decimal.TryParse(tbTablesVaccinesPrice.Text, out price);

            SelectedVaccine.Id = id;
            SelectedVaccine.Name = tbTablesVaccinesName.Text;
            SelectedVaccine.Price = price;
            SelectedVaccine.Status = rbTablesVaccinesStatus_Active.IsChecked.Value;

            try
            {
                vaccineBusiness.Save(SelectedVaccine);
                SelectedVaccine = new Vaccine();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            refreshVaccineList();
        }

        //DELETE VACCINE
        private void btnTablesVaccinesDelete_Click(object sender, RoutedEventArgs e)
        {
            int index = lvTableRegisterVaccines.SelectedIndex;
            if (index < 0)
            {
                return;
            }

            SelectedVaccine = (Vaccine)lvTableRegisterVaccines.SelectedItem;

            try
            {
                vaccineBusiness.Delete(SelectedVaccine);
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
        private ServicesProducts SelectedServicesProducts = new ServicesProducts();

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

        //CLEAR FIELDS AND UNSELECT LISTVIEW
        private void TableServicesProducts_clearFields_UnselectListView()
        {
            tbTablesProdServSearch.Text = String.Empty;
            tbTablesServicesProductsID.Text = String.Empty;
            tbTablesServicesProductsPrice.Text = String.Empty;
            tbTablesServicesProductsName.Text = String.Empty;
            rbTablesServProdStatus_Active.IsChecked = true;
            lvTablesRegisterServicesProducts.SelectedIndex = -1;
        }
        
        //LOAD FIELDS FROM SERVICES & PRODUCTS LIST
        private void lvTableRegisterServicesProducts_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lvTablesRegisterServicesProducts.SelectedIndex;
            if (index < 0)
            {
                return;
            }

            SelectedServicesProducts = (ServicesProducts)lvTablesRegisterServicesProducts.SelectedItem;

            tbTablesServicesProductsID.Text = SelectedServicesProducts.Id.ToString();
            tbTablesServicesProductsName.Text = SelectedServicesProducts.Name;
            tbTablesServicesProductsPrice.Text = SelectedServicesProducts.Price.ToString();

            if (SelectedServicesProducts.Status)
            {
                rbTablesServProdStatus_Active.IsChecked = true;
            }
            else
            {
                rbTablesServProdStatus_Inactive.IsChecked = true;
            }
        }

        //ADD NEW SERVICES & PRODUCTS (clear fields and unselect list)
        private void btnTablesServicesProductsAdd_Click(object sender, RoutedEventArgs e)
        {
            TableServicesProducts_clearFields_UnselectListView();
        }

        //SAVE SERVICES & PRODUCTS
        private void btnTablesServicesProductsSave_Click(object sender, RoutedEventArgs e)
        {
            var id = 0;
            Int32.TryParse(tbTablesServicesProductsID.Text, out id);

            Decimal price = 0;
            Decimal.TryParse(tbTablesServicesProductsPrice.Text, out price);

            SelectedServicesProducts.Id = id;
            SelectedServicesProducts.Name = tbTablesServicesProductsName.Text;
            SelectedServicesProducts.Price = price;
            SelectedServicesProducts.Status = rbTablesServProdStatus_Active.IsChecked.Value;

            try
            {
                servicesproductsBusiness.Save(SelectedServicesProducts);
                SelectedServicesProducts = new ServicesProducts();
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

            SelectedServicesProducts = (ServicesProducts)lvTablesRegisterServicesProducts.SelectedItem;

            try
            {
                servicesproductsBusiness.Delete(SelectedServicesProducts);
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




