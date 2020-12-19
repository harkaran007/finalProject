﻿using Android.App;
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
    public class User : RealmObject
    {


        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string age { get; set; }

        public string number { get; set; }

    }
}