using Gh0stApp.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Gh0stApp.Models;


namespace Gh0stApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebServiceView : ContentPage
    {
        HTTPService http;
        Random rand = new Random();
        //control giphy api
        private string buscar = "nubes dispersas";
        private string image_url = "";

        //control whather api
        private string descTiempo="";
        
        public WebServiceView()
        {
            InitializeComponent();
            http = new HTTPService();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
        }        

        public async void btnConsumir_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cityEntry.Text))
            {
                WeatherModel weatherData = await http.getData<WeatherModel>("http://api.openweathermap.org/data/2.5/weather?q=" + cityEntry.Text + "&units=metric&lang=es&appid=359d07cf2802c775096b0e6f30e03f70");
                BindingContext = weatherData;
                foreach (var item in weatherData.Weather )
                {                  
                    descTiempo = item.Description;
                }
                GetGifs(descTiempo);
            }   
        }

        private void GetGifs(string q)
        {
            
           Device.BeginInvokeOnMainThread(async () => {
                GiphyModel gif = new GiphyModel();
                var giphyGif = await http.getData<GiphyModel>("https://api.giphy.com/v1/gifs/search?api_key=JjHQg0Jx2beliftpj9GqeGEA3Ff8NsOa&q=" + q + "+weather&limit=" + rand.Next(1, 9));
                foreach (var item in giphyGif.Data)
                {
                    image_url = item.Images.Original.Url.AbsoluteUri;
                }

               if (giphyGif!=null)
               {
                   imgVWR.Source = image_url;
               }
               else
               {
                   await DisplayAlert("Error","null","null");                   
               }

            });
        }        
    }
}