using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using AlertDialog = Android.App.AlertDialog;
using Android.Views;
using Android.Widget;
using Realms;

namespace Movies
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
       public  EditText email;
       public EditText password;
       public Button loginBtn;
       public Button registerBtn;
        string pasword;

        Realm realmDB;
        string usernameValue;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            realmDB = Realm.GetInstance();

            email = FindViewById<EditText>(Resource.Id.etemail);
            password = FindViewById<EditText>(Resource.Id.etpassword);
            loginBtn = FindViewById<Button>(Resource.Id.button1);
            registerBtn = FindViewById<Button>(Resource.Id.button2);


            registerBtn.Click += delegate {

                Intent newRegisterScreen = new Intent(this, typeof(Register));
                StartActivity(newRegisterScreen);
            };


            loginBtn.Click += delegate {


                usernameValue = email.Text.Trim().ToLower();
                pasword = password.Text;

                if ((usernameValue == "" || usernameValue== " ") || (pasword == "" || pasword== " ")) 
                {
                    displayAlertDialog("InValid Info", "Please Enter Valid Name & Password");

                }
                else
                {

                    var validUser = realmDB.All<User>().Where(d => d.email == usernameValue );
                    int cUser = validUser.Count();
                    if ( cUser > 0)
                    {
                        Intent welcomeScreen = new Intent(this, typeof(Home));
                        string uemail = email.Text.ToString();
                        welcomeScreen.PutExtra("mail", uemail);
                        StartActivity(welcomeScreen);
                    }
                    else
                    {
                        displayAlertDialog("User not Found", "No such user in database");
                    }
                }


            };

        }
        public void displayAlertDialog(String title, String message)
        {

            AlertDialog.Builder alert = new AlertDialog.Builder(this);


            alert.SetTitle(title);
            alert.SetMessage(message);


            alert.SetPositiveButton("Yes", (senderAlert, args) =>
            {
                System.Console.WriteLine("Yes Button Clicked");

            });

            alert.SetNegativeButton("NO", (senderAlert, args) =>
            {
                System.Console.WriteLine("NO Button Clicked");

            });

            Dialog dialog = alert.Create();

            dialog.Show();

        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}
