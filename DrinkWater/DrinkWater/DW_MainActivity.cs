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
using Android.Support.V7.App;
using Android.Views.InputMethods;

namespace DrinkWater
{
    [Activity(Label = "Drink Water", MainLauncher = true, Icon = "@drawable/GlassIcon_Water_Bottle_96")]
    class DW_MainActivity : Activity
    {
        public List<DrinkLog> drinkLogList = new List<DrinkLog>();
        GridView gridview;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button buttonAddDrink = FindViewById<Button>(Resource.Id.button_addCutomWater);

            buttonAddDrink.Click += onClickAddDrink;

            TextView tv = FindViewById<TextView>(Resource.Id.textView_defaultTextBeforeAdd);
            //  TextView tv1 = FindViewById<TextView>(Resource.Id.textView_defaultTextBeforeAdd_1);
            TableRow tl = FindViewById<TableRow>(Resource.Id.tableRow5);
            FrameLayout fl = FindViewById<FrameLayout>(Resource.Id.frameLayout_middleMan);
            //  tv.SetWidth(tl.Width);



            //tv1.BringToFront();

            //   var metrics = Resources.DisplayMetrics;
            //  FrameLayout fv = FindViewById<FrameLayout>(Resource.Id.frameLayout_middleMan);

            //   fv.LayoutParameters.Width = (int)(metrics.WidthPixels / Resources.DisplayMetrics.Density);
            tv.Visibility = ViewStates.Gone;
            //   tv1.Visibility = ViewStates.Gone;

            gridview = FindViewById<GridView>(Resource.Id.TodayDrinkLogGrid);
            gridview.Adapter = new TodayDrinkLogGridAdapter(this);

            gridview.ItemClick += ((TodayDrinkLogGridAdapter)gridview.Adapter).itemClicked;

        }

        public void onClickAddDrink(object sender, EventArgs e)
        {
            drinkLogList.Add(new DrinkLog(double.Parse(FindViewById<EditText>(Resource.Id.editText_customAddWater).Text)));

            ((TodayDrinkLogGridAdapter)gridview.Adapter).NotifyDataSetChanged();

            FindViewById<EditText>(Resource.Id.editText_customAddWater).Text = "";
            FindViewById<EditText>(Resource.Id.editText_customAddWater).ClearFocus();

            //remove keyboard
            InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(FindViewById<EditText>(Resource.Id.editText_customAddWater).WindowToken, 0);
        }
    }
}