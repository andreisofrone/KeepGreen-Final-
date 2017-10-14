using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FieldInspection
{
    static class ApiUitilities
    {
        public static string exApiUrl = "104.155.154.190/api/Cultures/Inspections/1";

        static HttpClient client = new HttpClient();

        public static async Task<Culture> GetProductAsync(string path)
        {
            Culture culture = null;
            var response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                culture = await response.Content.ReadAsAsync<Culture>();
            }

            return culture;
        }

        public static IEnumerable<T> GetData<T>(string path, string key = null)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri("http://104.155.154.190");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync(path + key).Result;

            if (response.IsSuccessStatusCode)
            {
                var entities = response.Content.ReadAsAsync<IEnumerable<T>>().Result;
                return entities.ToList();
            }

            return null;
        }
    }
}