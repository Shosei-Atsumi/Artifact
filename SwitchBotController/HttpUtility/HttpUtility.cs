using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpUtility
{
    public class HttpUtility
    {
        public static async void Get(string Url)
        {
            var cl = new HttpClient(); 
            
            await cl.GetAsync(Url);
        }
    }
}
