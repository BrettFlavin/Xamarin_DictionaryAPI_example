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
            CheckConnection();
        }

        // checks for an internet connection and alerts user if no connection is exists
        async private void CheckConnection()
        {
            // alert when internet is disconnected and disable the buttons
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Checking Internet Status...Disconnected!", "Please connect to Wi-Fi to continue!", "OK");
                TextEntryField.IsEnabled = false;
                GoButton.IsEnabled = false;
                ClearButton.IsEnabled = false;               
            }
        }

        // event fired on 'Go' button click
        // searches dictionary API for text entered into entry field
        async private void GoButton_Clicked(object sender, EventArgs e)
        {
            if (TextEntryField.Text != "")
            {
                // create a new HttpClient to handle the request
                HttpClient client = new HttpClient();

                // store the entry field text
                string toSearch = TextEntryField.Text;

                // ***SANITIZE THE USER INPUT***

                //  create a new URI for the request
                var uri = new Uri(string.Format($"https://owlbot.info/api/v4/dictionary/" + toSearch));

                // create the HttpRequest message
                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Get;
                request.RequestUri = uri;
                //request.Headers.Add("Authorization: Token", "e779b93d8d67ff8d21dece3835eb9a3573822376");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", "e779b93d8d67ff8d21dece3835eb9a3573822376");

                // create the HttpResponse message
                HttpResponseMessage response = await client.SendAsync(request);

                Word searchWord = null;
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    searchWord = Word.FromJson(content);
                }
            }
        }

        // event fired on 'Clear' Button click
        // clears all text from the entry field
        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            TextEntryField.Text = "";
        }
    }
}
