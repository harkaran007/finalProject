using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Movies
{
    [Activity(Label = "Tablist")]
    class Tablist: Activity, SearchView.IOnQueryTextListener
    {
        ListView myListview;
        string title;
        SearchView yourSearchView;
        Realm realmDB;
        MyCustomAdapter myAdapter;

        int[] arrayInt = {Resource.Drawable.ap1,
            Resource.Drawable.ap2,
            Resource.Drawable.ap3,
            Resource.Drawable.ap4,
            Resource.Drawable.ap5,
            Resource.Drawable.ap6,
            Resource.Drawable.ap7
        };

        List<MyModel> myOwnList = new List<MyModel>();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.search);
            yourSearchView = FindViewById<SearchView>(Resource.Id.searchView1);


            realmDB = Realm.GetInstance();
            string uemail = Intent.GetStringExtra("mail");
            var toolbarBottom = FindViewById<Toolbar>(Resource.Id.toolbar1);
            var tempo = realmDB.All<User>().Where(d => d.email == uemail);
            foreach (User tate in tempo)
            {
                title = tate.name;
            }
            toolbarBottom.Title = "Welcome " + title;
            toolbarBottom.InflateMenu(Resource.Menu.photo_edit);
            toolbarBottom.MenuItemClick += (sender, e) => {
                if (e.Item.ItemId == Resource.Id.menu_share)
                {
                    // undo
                    Intent homeScreen = new Intent(this, typeof(Home));
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
                    Intent listScreen = new Intent(this, typeof(Tablist));
                    listScreen.PutExtra("mail", uemail);
                    StartActivity(listScreen);
                }
            };



            myListview = FindViewById<ListView>(Resource.Id.listView1);            
            myOwnList = getDataFromRealmDB();
            myAdapter = new MyCustomAdapter(this, myOwnList);
            myListview.Adapter = myAdapter; 
            myListview.TextFilterEnabled = false;
            setupSearchView();
            myListview.ItemClick += listViewClickIteam;

        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.photo_edit, menu);
            return base.OnPrepareOptionsMenu(menu);

        }

        private void setupSearchView()
        {

            yourSearchView.SetIconifiedByDefault(false);
            yourSearchView.SetOnQueryTextListener(this);
            yourSearchView.SubmitButtonEnabled = true;
            yourSearchView.SetQueryHint("Search");
        }
        public bool OnQueryTextChange(string newText)
        {
            try
            {
                if (TextUtils.IsEmpty(newText))
                {
                    myListview.ClearTextFilter();
                    myAdapter = new MyCustomAdapter(this, myOwnList);
                    myListview.Adapter = myAdapter;
                }
                else
                {

                    myListview.ClearTextFilter();
                    List<MyModel> yourListViewItems2 = (from i in myOwnList
                                                                          where i.name.ToLower().Contains(newText.ToLower())
                                                                          select i).ToList();
                    myAdapter = new MyCustomAdapter(this, yourListViewItems2);
                    myListview.Adapter = myAdapter;
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Long).Show();
            }
            return true;
        }

        public bool OnQueryTextSubmit(string query)
        {
            return false;
        }

        public void listViewClickIteam(object sender, AdapterView.ItemClickEventArgs args)
        {

            int index = args.Position;

            MyModel aPerson = myOwnList[index];
            string favname = aPerson.name;

            displayAlertDialog("Add Movie", "Do you want to add this movie as favourite?",favname);

        }
        public void displayAlertDialog(String title, String message, String favour)
        {
            string uemail = Intent.GetStringExtra("mail");
            AlertDialog.Builder alert = new AlertDialog.Builder(this);;

            alert.SetTitle(title);
            alert.SetMessage(message);


            alert.SetPositiveButton("Yes", (senderAlert, args) =>
            {
                Favmovies favname = new Favmovies();
                favname.name = favour;
                favname.username = uemail;
                realmDB.Write(() =>
                {
                    realmDB.Add(favname);
                });
                Toast.MakeText(this, "Added to Favourite List", ToastLength.Long).Show();
            });

            alert.SetNegativeButton("NO", (senderAlert, args) =>
            {
                System.Console.WriteLine("No clciked");
            });

            Dialog dialog = alert.Create();

            dialog.Show();

        }

        public List<MyModel> getDataFromRealmDB()
        {

            List<MyModel> dbRecordList = new List<MyModel>();
            var resultCollection = realmDB.All<Movies>();


            foreach (Movies moviesObj in resultCollection)
            {

                String nameFromDB = moviesObj.name;

                Random r = new Random();
                int rint = r.Next(1, 7);
                int valueFromArray = arrayInt[rint];

                MyModel temp = new MyModel(nameFromDB, valueFromArray);
                dbRecordList.Add(temp);
            }

            return dbRecordList;
        }
    }
}