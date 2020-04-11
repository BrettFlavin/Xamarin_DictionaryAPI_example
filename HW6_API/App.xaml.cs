using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Connectivity;

namespace HW6_API
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            base.OnStart();
            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
        }

        // event fires when internet connectivivty changes
        // displays an alert and enables or disables buttons
        async private void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            Button gobutton = (Button)MainPage.FindByName("GoButton");
            Button clearbutton = (Button)MainPage.FindByName("ClearButton");
            Entry userentry = (Entry)MainPage.FindByName("TextEntryField");

            // alert internet connected and enable buttons
            if (e.IsConnected)
            {
                await MainPage.DisplayAlert("Internet Connected", "Enjoy the app!", "OK");
                gobutton.IsEnabled = true;
                clearbutton.IsEnabled = true;
                userentry.IsEnabled = true;
            }
            // alert internet disconnected and disable buttons
            else
            {
                await MainPage.DisplayAlert("*** Warning ***", "Internet Disconnected! - Please connect to Wi-Fi to continue!", "OK");                
                gobutton.IsEnabled = false;                
                clearbutton.IsEnabled = false;               
                userentry.IsEnabled = false;
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
