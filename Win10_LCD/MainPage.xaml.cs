using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Win10_LCD
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        LCD lcd;
        public MainPage()
        {
            this.InitializeComponent();

            initLCD();

        }

        public string CurrentComputerName()
        {
            var hostNames = NetworkInformation.GetHostNames();
            var localName = hostNames.FirstOrDefault(name => name.DisplayName.Contains(".local"));
            return localName.DisplayName.Replace(".local", "");
        }

        public string CurrentIPAddress()
        {
            var icp = NetworkInformation.GetInternetConnectionProfile();

            if (icp != null && icp.NetworkAdapter != null)
            {
                var hostname =
                    NetworkInformation.GetHostNames()
                        .SingleOrDefault(
                            hn =>
                            hn.IPInformation != null && hn.IPInformation.NetworkAdapter != null
                            && hn.IPInformation.NetworkAdapter.NetworkAdapterId
                            == icp.NetworkAdapter.NetworkAdapterId);

                if (hostname != null)
                {
                    // the ip address
                    return hostname.CanonicalName;
                }
            }

            return string.Empty;
        }
        private async void initLCD()
        {
            lcd = new LCD(20, 4);
            await lcd.InitAsync(18, 23, 24, 5, 6, 13);
            await lcd.clearAsync();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            lcd.WriteLine(textBox.Text);
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            await lcd.clearAsync();
            lcd.write("Windows 10 IoT Core ");
            lcd.setCursor(0, 1);
            lcd.write("IP:" + CurrentIPAddress());
            lcd.setCursor(0, 2);
            lcd.write("Name:" + CurrentComputerName());
            while (true)
            {
                lcd.setCursor(0, 3);
                lcd.write("Time :" + DateTime.Now.ToString("HH:mm:ss.ffff"));
            }
        }
    }
}
