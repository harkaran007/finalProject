using Android.App;
using Android.Content;
using System;
using Android.OS;
using Android.Widget;
using Realms;
using System.Linq;


namespace Movies
{
    [Activity(Label = "Home")]
    public class Home : Activity 
    {
        EditText  ednum, edage, edpass;
        TextView edview,eemail;
        Button bemail,bnum, beage, bpass;
        string title;
        Realm realmDB;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.layout1);

            realmDB = Realm.GetInstance();

            edview= FindViewById<TextView>(Resource.Id.textView1);
            eemail = FindViewById<TextView>(Resource.Id.editText1);
            ednum = FindViewById<EditText>(Resource.Id.editText2);
            edage= FindViewById<EditText>(Resource.Id.editText3);
            edpass = FindViewById<EditText>(Resource.Id.editText4);

            bemail = FindViewById<Button>(Resource.Id.button1);
            bnum = FindViewById<Button>(Resource.Id.button2);
            beage = FindViewById<Button>(Resource.Id.button3);
            bpass = FindViewById<Button>(Resource.Id.button4);

            string uemail = Intent.GetStringExtra("mail");
            eemail.Text = uemail;
            var tempo = realmDB.All<User>().Where(d => d.email == uemail);
            foreach(User tate in tempo)
            {
                title = tate.name;
            }
            var toolbarBottom = FindViewById<Toolbar>(Resource.Id.toolbar1);
            toolbarBottom.Title = "Welcome "+title;
            toolbarBottom.InflateMenu(Resource.Menu.photo_edit);
            toolbarBottom.MenuItemClick += (sender, e) => {
                if (e.Item.ItemId == Resource.Id.menu_share)
                {
                    // undo
                    Intent homeScreen = new Intent(this,typeof(Home));
                    homeScreen.PutExtra("mail", uemail);
                    StartActivity(homeScreen);
                }
               else if (e.Item.ItemId == Resource.Id.action_menu_divider)
                {
                    //redo
                    Intent favScreen = new Intent(this, typeof(Tabb));
                    favScreen.PutExtra("mail", uemail);
                    StartActivity(favScreen);
                }
                else
                {
                    //save
                    Intent listScreen= new Intent(this, typeof(Tablist));
                    listScreen.PutExtra("mail", uemail);
                    StartActivity(listScreen);
                }
            };

            

            realmDB = Realm.GetInstance();
            var usrs = realmDB.All<User>().Where(d => d.email == uemail);
            foreach(var v in usrs)
            {
                edview.Text = "Name: "+ v.name;
                ednum.Text = v.number;
                edage.Text = v.age;
                edpass.Text = v.password;
            }

            bnum.Click += delegate
            {
                foreach(var e in usrs)
                {
                    realmDB.Write(() =>
                    {
                        e.number = ednum.Text.ToString();
                        Toast.MakeText(this, "Phone Number Updated", ToastLength.Long).Show();
                    });
                }
            };
            beage.Click += delegate
            {
                foreach (var a in usrs)
                {
                    realmDB.Write(() =>
                    {
                        a.age = edage.Text.ToString();
                        Toast.MakeText(this, "Age Updated", ToastLength.Long).Show();
                    });
                }
            };
            bpass.Click += delegate
            {
                foreach (var p in usrs)
                {
                    realmDB.Write(() =>
                    {
                        p.password = edpass.Text.ToString();
                        Toast.MakeText(this, "Password Updated", ToastLength.Long).Show();
                    });
                }
            };

        }
    }
}