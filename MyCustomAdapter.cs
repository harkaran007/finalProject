using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Movies
{
    public class MyCustomAdapter : BaseAdapter<MyModel>
    {
        Activity context;
        List<MyModel> arrayList;

        public MyCustomAdapter(Activity myContext, List<MyModel> myListItems)
        {
            this.context = myContext;
            this.arrayList = myListItems;
        }
        public override MyModel this[int position]
        {
            get { return arrayList[position]; }
        }

        public override int Count
        {

            get { return arrayList.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            MyModel myModelObject = arrayList[position];

            View myview = convertView;

            if (myview == null)
            {
                myview = context.LayoutInflater.Inflate(Resource.Layout.myview, null);
            }

            ImageView myImage = myview.FindViewById<ImageView>(Resource.Id.imageView1);
            TextView myText = myview.FindViewById<TextView>(Resource.Id.textView1);


            myText.Text = myModelObject.name;
            myImage.SetImageResource(myModelObject.imageID);



            return myview;
        }
    }
}