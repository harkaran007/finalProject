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
    public class Favmovies : RealmObject
    {
        public string name { get; set; }
        public string username { get; set; }
    }
}