using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VeterinaryManagementSystem
{
    /// <summary>
    /// Interaction logic for WebCamWindow.xaml
    /// </summary>
    public partial class WebCamWindow : Window
    {
        public WebCamWindow()
        {
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
            //((MainWindow)System.Windows.Application.Current.MainWindow).imgRegistryOwner1Image.Source = imgVideo.Source;
        }

        private void bntSaveImage_Click(object sender, RoutedEventArgs e)
        {
            //Helper.SaveImageCapture((BitmapSource)imgCapture.Source);
            /*
            if (Index == 1)
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).imgRegistryOwner1Image.Source = imgCapture.Source;
            }
            if (Index == 2)
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).imgRegistryOwner2Image.Source = imgCapture.Source;
            }
            */
            //((MainWindow)System.Windows.Application.Current.MainWindow).imgRegistryOwner1Image.Source = imgCapture.Source;
            DialogResult = true;
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
    }
}
