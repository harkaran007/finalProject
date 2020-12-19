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
    public class Movies : RealmObject
    {
        public string name { get; set; }
    }
}