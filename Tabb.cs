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
	[Activity(Label = "Tabb")]
	class Tabb : Activity, SearchView.IOnQueryTextListener
    {
        Realm realmDB;
        MyCustomAdapter myAdapter;
        SearchView yourSearchView;
        string title;
        int[] arrayInt = {Resource.Drawable.ap1,
            Resource.Drawable.ap2,
            Resource.Drawable.ap3,
            Resource.Drawable.ap4,
            Resource.Drawable.ap5,
            Resource.Drawable.ap6,
            Resource.Drawable.ap7
        };
        ListView myListview;

        List<MyModel> myOwnList = new List<MyModel>();
        protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.layout2);
            yourSearchView = FindViewById<SearchView>(Resource.Id.searchView1);
            realmDB = Realm.GetInstance();
            string uemail = Intent.GetStringExtra("mail");
            var toolbarBottom = FindViewById<Toolbar>(Resource.Id.toolbar1);

            var tempo = realmDB.All<User>().Where(d => d.email == uemail);
            foreach (User tate in tempo)
            {
                title = tate.name;
            }
            toolbarBottom.Title = " " + title;
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


        }
        public List<MyModel> getDataFromRealmDB()
        {

            string uemail = Intent.GetStringExtra("mail");
            List<MyModel> dbRecordList = new List<MyModel>();
            var resultCollection = realmDB.All<Favmovies>().Where(d => d.username == uemail);


            foreach (Favmovies moviesObj in resultCollection)
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

    }

}