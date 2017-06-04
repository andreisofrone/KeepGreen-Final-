using System;
using System.Collections.Generic;
using System.Json;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
namespace FieldInspection
{
    static class ApiUitilities
    {
		public static string exApiUrl= "104.155.154.190/api/Cultures/Inspections/1";

        static HttpClient client = new HttpClient();
        public static async Task<JsonValue> FetchWeatherAsync(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    return jsonDoc;
                }
            }
        }
		public static void ParseAndDisplay(JsonValue json)
        {
            // Get the weather reporting fields from the layout resource:
            var location = "";
            var temperature = "";
            var humidity = "";
            var conditions = "";

            // Extract the array of name/value results for the field name "weatherObservation". 
            JsonValue weatherResults = json["status"];

            // Extract the "stationName" (location string) and write it to the location TextBox:
            location = weatherResults["stationName"];

            // The temperature is expressed in Celsius:
            double temp = weatherResults["temperature"];
            // Convert it to Fahrenheit:
            temp = ((9.0 / 5.0) * temp) + 32;
            // Write the temperature (one decimal place) to the temperature TextBox:
            temperature = String.Format("{0:F1}", temp) + "° F";

            // Get the percent humidity and write it to the humidity TextBox:
            double humidPercent = weatherResults["humidity"];
            humidity = humidPercent.ToString() + "%";

            // Get the "clouds" and "weatherConditions" strings and 
            // combine them. Ignore strings that are reported as "n/a":
            string cloudy = weatherResults["clouds"];
            if (cloudy.Equals("n/a"))
                cloudy = "";
            string cond = weatherResults["weatherCondition"];
            if (cond.Equals("n/a"))
                cond = "";

            // Write the result to the conditions TextBox:
            conditions = cloudy + " " + cond;
        }

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
    }
}