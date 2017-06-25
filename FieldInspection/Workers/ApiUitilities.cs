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
		public static string exApiUrl= "104.155.154.190/api/Cultures/Inspections/1";

        static HttpClient client = new HttpClient();

        public static async Task<Culture>GetProductAsync(string path)
        {
            Culture culture = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                culture = await response.Content.ReadAsAsync<Culture>();
            }
            return culture;
        }

        public static IEnumerable<Inspection>GetInspections( Culture SelectedCulture)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://104.155.154.190");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/Cultures/Inspections/" + SelectedCulture.ID).Result;

            if (response.IsSuccessStatusCode)
            {
                var inspections = response.Content.ReadAsAsync<IEnumerable<Inspection>>().Result;
                return inspections.ToList();
            }
            return null;
        }

        public static IEnumerable<Dashboard>GetDashboardValues()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://104.155.154.190");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/Dashboard").Result;

            if (response.IsSuccessStatusCode)
            {
                var dashboard = response.Content.ReadAsAsync<IEnumerable<Dashboard>>().Result;
                return dashboard.ToList();
            }
            return null;
        }

        public static IEnumerable<Culture> GetCultures()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://104.155.154.190");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/Cultures").Result;

            if (response.IsSuccessStatusCode)
            {
                var cultures = response.Content.ReadAsAsync<IEnumerable<Culture>>().Result;
                return cultures.ToList();
            }
            return null;
        }
    }
}