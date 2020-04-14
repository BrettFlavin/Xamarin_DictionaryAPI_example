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

        /* checks for an internet connection on initial startup 
           and alerts user if no connection exists */
        async private void CheckConnection()
        {
            // alert if internet is disconnected and disable the buttons
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Checking Internet Status...Disconnected!", "Please connect to Wi-Fi to continue!", "OK");
                TextEntryField.IsEnabled = false;
                GoButton.IsEnabled = false;
                ClearButton.IsEnabled = false;               
            }
        }

        /* event is fired when 'Go' button is clicked - uses the dictionary API to search 
           for the text entered into the entry field and then displays the results */
        async private void GoButton_Clicked(object sender, EventArgs e)
        {          
            if (TextEntryField.Text != "")
            {            
                // store entry field's text 
                string toSearch = TextEntryField.Text;

                // create a new HttpClient to handle the request
                HttpClient client = new HttpClient();

                //  create a new URI for the request
                var uri = new Uri(string.Format($"https://owlbot.info/api/v4/dictionary/" + toSearch));

                // create the HttpRequest message and add the API's authorization token 
                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Get;
                request.RequestUri = uri;
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", "e779b93d8d67ff8d21dece3835eb9a3573822376");

                // create the HttpResponse message from the request
                HttpResponseMessage response = await client.SendAsync(request);

                // status code 200 (success)
                if (response.IsSuccessStatusCode)
                {                  
                    // read in json string and deserialize it into a Word object
                    var json = await response.Content.ReadAsStringAsync();
                    Word theWord = JsonConvert.DeserializeObject<Word>(json);

                    // set the image and labels
                    if (theWord.definitions[0].image_url == null)
                    {
                        imagelabel.IsVisible = false;
                    }
                    else
                    {
                        imagelabel.IsVisible = true;
                        imagelabel.Source = theWord.definitions[0].image_url;
                    }                   
                    typelabel.Text = "Type: " + theWord.definitions[0].type;
                    line1.IsVisible = true;
                    definitionlabel.Text = "Definition: " + theWord.definitions[0].definition;
                    line2.IsVisible = true;
                    examplelabel.Text = "Example: " +  "\"" + theWord.definitions[0].example + "\"";
                }
                // status code 404 (not found) - null json response
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await DisplayAlert("Not Found!", "Sorry \"" + TextEntryField.Text + "\" " + "is not in the dictionary","OK");            
                }
                // default alert for any other status code
                else
                {
                    await DisplayAlert("*ERROR*", response.ReasonPhrase, "OK");
                }
            }
        }

        /* event fires when 'Clear' Button is clicked to clear all 
           text from the entry field and clear the image and labels */
        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            // clear the entry field
            TextEntryField.Text = "";

            // clear the image and labels
            imagelabel.IsVisible = false;
            typelabel.Text = "";
            line1.IsVisible = false;
            definitionlabel.Text = "";
            line2.IsVisible = false;
            examplelabel.Text = "";
        }
    }
}
