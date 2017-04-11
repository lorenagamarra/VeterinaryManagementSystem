using System;
using System.Collections.Generic;
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

namespace VeterinaryManagementSystem
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        
        private void RibbonMenuItem_AnimalRegistry_Click(object sender, RoutedEventArgs e)
        {
             AnimalRegistryForm newWindow = new AnimalRegistryForm();
            newWindow.Show();
        }
        private void RibbonMenuItem_ClientRegistry_Click(object sender, RoutedEventArgs e)
        {
            ClientRegistryForm newWindow = new ClientRegistryForm();
            newWindow.Show();
        }

        private void RibbonMenuItem_EmployeeRegistry_Click(object sender, RoutedEventArgs e)
        {

        }
        private void RibbonMenuItem_ServiceAppointment_Click(object sender, RoutedEventArgs e)
        {

        }
        private void RibbonMenuItem_SetCalendar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
