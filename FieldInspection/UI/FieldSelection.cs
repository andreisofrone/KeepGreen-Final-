using Android.App;
using Android.OS;
using Android.Widget;
using FR.Ganfra.Materialspinner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Android.Content;
using Java.IO;
using Newtonsoft.Json;

namespace FieldInspection.UI
{
    [Activity(Label = "FieldSelection", Theme = "@style/Theme.Splash")]
    public class FieldSelection : Activity,ISerializable
    {
        public static string exApiUrl = "http://104.155.154.190/api/Cultures/Inspections/1";
      
        private ArrayAdapter<String> _adapter;

        private IEnumerable<Culture> Cultures { get; set; }

        MaterialSpinner _fieldsSpineer;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Field_Selection);

            Cultures = GetCultures();

            var cul=new List<String>();

            foreach (var culture in Cultures)
            {
                cul.Add(culture.Name);
            }

            var startBtn = FindViewById<Button>(Resource.Id.startBtn);
            startBtn.Click += BtnStart_Click;

            _adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, cul);
            _adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            InitSpinnerMultiline();

        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            _fieldsSpineer = FindViewById<MaterialSpinner>(Resource.Id.fieldSpinner);

            if (_fieldsSpineer.SelectedItemPosition > 0)
            {
                Intent i = new Intent(Application.Context, typeof(MainActivity));
                i.PutExtra("key", JsonConvert.SerializeObject(Cultures.ToList().FirstOrDefault(x=>x.Name== $"{_fieldsSpineer.SelectedItem}")));
                StartActivity(i);
            }
        }

        private void InitSpinnerMultiline()
        {
            _fieldsSpineer = FindViewById<MaterialSpinner>(Resource.Id.fieldSpinner);
            _fieldsSpineer.Adapter = _adapter;
            _fieldsSpineer.Hint = "Select a field";
        }

        private IEnumerable<Culture> GetCultures()
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