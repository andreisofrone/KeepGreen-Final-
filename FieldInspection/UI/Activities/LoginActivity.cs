using Android.App;
using Android.OS;
using Android.Widget;
using System.Threading;
using System.Threading.Tasks;

namespace FieldInspection.UI
{
    [Activity(Label = "FieldInspection", Theme = "@style/Theme.Splash")]
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login_Layout);
            var loginButton = FindViewById<Button>(Resource.Id.LoginButton);
            var username = FindViewById<EditText>(Resource.Id.username);
            var password = FindViewById<EditText>(Resource.Id.password);

            loginButton.Click += async delegate
            {
                if (string.IsNullOrWhiteSpace(username.Text))
                {
                    username.Error = "Enter valid username";
                }
                else if (string.IsNullOrWhiteSpace(password.Text))
                {
                    password.Error = "Enter valid password";
                }
                else
                {
                    ProgressDialog progress = new ProgressDialog(this, AlertDialog.ThemeDeviceDefaultLight);
                    progress.SetMessage("Authenticating..");
                    progress.SetTitle("Please wait");
                    progress.Show();

                    if (username.Text == "admin" && password.Text == "admin")
                    {
                        await Task.Run(() =>
                        {
                            StartActivity(typeof(FieldSelectionActivity));
                            return true;
                        });

                        progress.Dismiss();
                        Toast.MakeText(this, "Success", ToastLength.Long).Show();

                    }

                    else
                    {
                        await Task.Run(() =>
                        {
                            Thread.Sleep(1500);
                            return true;
                        });

                        if (username.Text != "admin")
                        {
                            progress.Dismiss();
                            Toast.MakeText(this, "Unknown user, please try again.", ToastLength.Long).Show();
                        }
                        else
                        {
                            progress.Dismiss();
                            Toast.MakeText(this, "Wrong password, please try again.", ToastLength.Long).Show();
                        }                       
                    }
                }
            };
        }
    }
}
