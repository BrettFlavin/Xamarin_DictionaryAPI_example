using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Connectivity;
using System.Net.Http;
using Newtonsoft.Json;

namespace HW6_API
{
    
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            checkConnection();
        }

        // checks for an active internet connection
        private void checkConnection()
        {
            var isConnected = CrossConnectivity.Current.IsConnected;
            if (isConnected == true)
            {
                ConnectionLabel.Text = "Internet connected!";
            }
            else
            {
                ConnectionLabel.Text = "Internet *NOT* connected!";
            }
        }

        async private void GetButton_Clicked(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();

            var uri = new Uri(
                string.Format(
                    $"https://owlbot.info/api/v4/dictionary/" +
                    $"API_KEY"  ));

            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            request.RequestUri = uri;

            HttpResponseMessage response = await client.SendAsync(request);
            //Word theWord = null;
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                //theWord = Word.FromJson(content);
            }
        }
    }
}
