using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gh0stApp.Services
{
    class HTTPService
    {       

        public async Task<T> getData<T>(string URL)
        {
            try
            {
                HttpClient cliente = new HttpClient();
                var respuesta = await cliente.GetAsync(URL);

                if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await respuesta.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            return default(T);
        }
        
    }
}
