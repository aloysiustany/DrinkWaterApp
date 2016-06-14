using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using static Android.Widget.AbsListView;

namespace DrinkWater
{
    [Activity(Label = "Drink Water", MainLauncher = true, Icon = "@drawable/icon")]
    class DW_MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            // Button button = FindViewById<Button>(Resource.Id.MyButton);

            //    button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            TextView tv = FindViewById<TextView>(Resource.Id.textView_defaultTextBeforeAdd);
            TextView tv1 = FindViewById<TextView>(Resource.Id.textView_defaultTextBeforeAdd_1);

            //tv1.BringToFront();

            var metrics = Resources.DisplayMetrics;
            FrameLayout fv = FindViewById<FrameLayout>(Resource.Id.frameLayout_middleMan);
           
             fv.LayoutParameters.Width = (int)(metrics.WidthPixels / Resources.DisplayMetrics.Density);

        }
    }
}