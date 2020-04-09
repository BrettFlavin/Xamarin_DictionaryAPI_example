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
                DisplayAlert("Internet Connected!", "Enjoy the App!", "OK");
            }
            else
            {
                DisplayAlert("Internet Not Connected!", "Please establish internet connection to continue", "OK");
            }
        }


        // event fired on Go Button click
        // searches dictionary API for text entered into entry field
        async private void GoButton_Clicked(object sender, EventArgs e)
        {
            if (TextEntryField.Text != "")
            {
                // create a new HttpClient to handle the request
                HttpClient client = new HttpClient();

                //  create the URI
                var uri = new Uri(
                    string.Format(
                        $"https://owlbot.info/api/v4/dictionary/" +
                        $"API_KEY"));

                // create the HttpRequest message
                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Get;
                request.RequestUri = uri;

                // create the HttpResponse message
                HttpResponseMessage response = await client.SendAsync(request);

                //Word theWord = null;
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    //theWord = Word.FromJson(content);
                }
            }
        }

        // event fired on Clear Button click
        // clears all text from the entry field
        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            TextEntryField.Text = "";
        }
    }
}
