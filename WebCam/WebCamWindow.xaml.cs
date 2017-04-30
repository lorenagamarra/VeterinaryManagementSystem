using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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
using System.Windows.Shapes;
using VeterinaryManagementSystem.UIWebCam;


namespace VeterinaryManagementSystem
{
    /// <summary>
    /// Interaction logic for WebCamWindow.xaml
    /// </summary>
    public partial class WebCamWindow : Window
    {
        private int _index;
        public int Index
        {
            get => _index;
            set
            {
                if (_index != value)
                {
                    _index = value;
                    RaisePropertyChanged("Index");
                }
            }
        }
        public WebCamWindow()
        {
            CameraMessenger.Default.Register<String>(this, message =>
            {
                RecivedDoneMessage(message);
            });
            InitializeComponent();
        }
        WebCam webcam;
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
            webcam = new WebCam();
            webcam.InitializeWebCam(ref imgVideo);
            webcam.Start();
        }
        private void RecivedDoneMessage(string message)
        {
            switch (message)
            {
                case "Owner1Picture":
                    Index = 1;
                    break;
                case "Owner2Picture":
                    Index = 2;
                    break;
                case "AnimalPicture":
                    Index = 3;
                    break;
                case "EmployeePicture":
                    Index = 4;
                    break;
                case "ConsultationOwner1Picture":
                    Index = 5;
                    break;
                case "ConsultationOwner2Picture":
                    Index = 6;
                    break;
                case "ConsultationAnimalPicture":
                    Index = 7;
                    break;
                default:
                    break;
            }
        }
        private void bntStart_Click(object sender, RoutedEventArgs e)
        {
            webcam.Start();
        }

        private void bntStop_Click(object sender, RoutedEventArgs e)
        {
            webcam.Stop();
        }

        private void bntContinue_Click(object sender, RoutedEventArgs e)
        {
            webcam.Continue();
        }

        private void bntCapture_Click(object sender, RoutedEventArgs e)
        {
            imgCapture.Source = imgVideo.Source;
        }

        public void bntSaveImage_Click(object sender, RoutedEventArgs e)
        {
            //Helper.SaveImageCapture((BitmapSource)imgCapture.Source);
            //((MainWindow)System.Windows.Application.Current.MainWindow).imgRegistryOwner1Image.Source = imgCapture.Source;
            //DialogResult = true;
            if (Index == 1)
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).imgRegistryOwner1Image.Source = imgCapture.Source;
            }
            if (Index == 2)
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).imgRegistryOwner2Image.Source = imgCapture.Source;
            }
            if (Index == 3)
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).imgRegistryAnimalPicture.Source = imgCapture.Source;
            }
            if (Index == 4)
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).imgRegistryEmployeeImage.Source = imgCapture.Source;
            }
            if (Index == 5)
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).imgConsultationOwner1Image.Source = imgCapture.Source;
            }
            if (Index == 6)
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).imgConsultationOwner2Image.Source = imgCapture.Source;
            }
            if (Index == 7)
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).imgConsultationAnimalImage.Source = imgCapture.Source;
            }
        }

        private void bntResolution_Click(object sender, RoutedEventArgs e)
        {
            webcam.ResolutionSetting();
        }

        private void bntSetting_Click(object sender, RoutedEventArgs e)
        {
            webcam.AdvanceSetting();
        }
        private void webCamWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            webcam.Stop();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ContentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("Index");
        }


    }
}
