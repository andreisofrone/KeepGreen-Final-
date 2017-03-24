using Android.App;
using Android.OS;
using Android.Widget;
using FR.Ganfra.Materialspinner;
using System;

namespace FieldInspection.UI
{
    [Activity(Label = "FieldSelection", Theme = "@style/Theme.Splash")]
    public class FieldSelection : Activity
    {
        private static readonly string[] ITEMS = { "Fild 1", "Fild 2", "Fild 3", "Fild 4", "Fild 5", "Fild 6" };

        private ArrayAdapter<String> _adapter;

        MaterialSpinner _fieldsSpineer;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.field_selection);

            var startBtn = FindViewById<Button>(Resource.Id.startBtn);
            startBtn.Click += BtnStart_Click;

            _adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, ITEMS);
            _adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            InitSpinnerMultiline();

        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            _fieldsSpineer = FindViewById<MaterialSpinner>(Resource.Id.fieldSpinner);

            if (_fieldsSpineer.SelectedItemPosition > 0)
            {
                StartActivity(typeof(MainActivity));
            }
        }

        private void InitSpinnerMultiline()
        {
            _fieldsSpineer = FindViewById<MaterialSpinner>(Resource.Id.fieldSpinner);
            _fieldsSpineer.Adapter = _adapter;
            _fieldsSpineer.Hint = "Select a field";
        }
    }
}