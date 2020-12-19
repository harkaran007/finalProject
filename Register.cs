using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Movies
{
    [Activity(Label = "Register")]
    public class Register : Activity
    {

        EditText userName, userPass, userNum, userAge, userEmail;
        Button registerBtn;

        Realm realmDB; 
        string nameValue,passValue, numValue,ageValue, emailValue;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

         
            SetContentView(Resource.Layout.Register_main);

            realmDB = Realm.GetInstance();
            userEmail = FindViewById<EditText>(Resource.Id.editText1);
            userPass = FindViewById<EditText>(Resource.Id.editText2);
            userName = FindViewById<EditText>(Resource.Id.editText3);
            userNum = FindViewById<EditText>(Resource.Id.editText4);
            userAge = FindViewById<EditText>(Resource.Id.editText5);

            registerBtn = FindViewById<Button>(Resource.Id.button1);



            registerBtn.Click += delegate {

                nameValue = userName.Text;
                passValue = userPass.Text;
                ageValue = userAge.Text;
                emailValue = userEmail.Text;
                numValue = userNum.Text;

                if (nameValue.Trim() == "" && passValue.Trim() == "" && numValue.Trim() == "" && ageValue.Trim() == "" && emailValue.Trim() == "")
                {

                    displayAlertDialog("InValid Info", "Please Enter Valid Name & Password");
                }
                else
                {

                    User userObj = new User();


                    userObj.name = nameValue;
                    userObj.password = passValue;
                    userObj.age = ageValue;
                    userObj.number = numValue;
                    userObj.email = emailValue;


                    realmDB.Write(() =>
                    {
                        realmDB.Add(userObj);
                    });

                    displayAlertDialog("Changes saved", "User Added Succesfully");

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
    }
}