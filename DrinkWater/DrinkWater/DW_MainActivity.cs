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
using System.Threading.Tasks;
using SQLite;
using Android.Support.V4.Widget;

namespace DrinkWater
{
    [Activity(Label = "Drink Water", MainLauncher = true, Icon = "@drawable/GlassIcon_Water_Bottle_96")]
    class DW_MainActivity : Activity
    {
        public List<DrinkLog> drinkLogList = null;
   //     public double Curr_Weight = 0;
   //     public int Exercise_Min = 0;
   //     public utils.Weight_Unit Pref_Weight_Unit = utils.Weight_Unit.Unit_Kg;

        //todo
        public utils.Volume_Unit Pref_Volume_Unit = utils.Volume_Unit.Unit_mL;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //drawer
         //   var mPlanetTitles = Resource.Array.Weight_Units_String_Array;
         //   var mDrawerLayout = (DrawerLayout)FindViewById(Resource.Id.drawer_layout);
 //           var mDrawerList = FindViewById<ListView>(Resource.Id.left_drawer);

            // Set the adapter for the list view
  //          mDrawerList.Adapter = new ArrayAdapter<String>(this,
    //                Resource.Drawable.units_spinner_item, mPlanetTitles);
            // Set the list's click listener
        //    mDrawerList.OnItemClickListenerstener(new DrawerItemClickListener());


            // Asssign on click events to add water and repeat drink buttons
            FindViewById<Button>(Resource.Id.button_addCutomWater).Click += onClickAddDrink;
            FindViewById<Button>(Resource.Id.button_addPrevWater).Click += onClickRepeatDrink;

            drinkLogList = DBServices.Instance.SelectDrinkLogEntries(utils.getDateLongString(0));

            FindViewById<GridView>(Resource.Id.TodayDrinkLogGrid).Adapter = new TodayDrinkLogGridAdapter(this);
            FindViewById<GridView>(Resource.Id.TodayDrinkLogGrid).ItemClick +=
                ((TodayDrinkLogGridAdapter)FindViewById<GridView>(Resource.Id.TodayDrinkLogGrid).Adapter).itemClicked;

            showHideNoDrinkDefaultText();

            //todo
       //     Curr_Weight = 112;

            //set target text
            updateWeight();

            computeProgressTexts();

            FindViewById<ImageButton>(Resource.Id.ImageButton_EditTarget).Click += onClickEditTarget;            
        }

        public void onClickEditTarget(object sender, EventArgs e)
        {
            new EditPersonalSpecModalClass(this).Show(this.FragmentManager, "EditPersonalSpec");
        }

        public void onClickAddDrink(object sender, EventArgs e)
        {
            if (!FindViewById<EditText>(Resource.Id.editText_customAddWater).Text.Trim().Equals(""))
            {
                DrinkLog newDrinkLog = new DrinkLog(double.Parse(FindViewById<EditText>(Resource.Id.editText_customAddWater).Text));

                drinkLogList.Add(newDrinkLog);

                DBServices.Instance.AddDrinkLogEntry(newDrinkLog);

                ((TodayDrinkLogGridAdapter)FindViewById<GridView>(Resource.Id.TodayDrinkLogGrid).Adapter).NotifyDataSetChanged();

                showHideNoDrinkDefaultText();

                computeProgressTexts();

                //clear editbox
                FindViewById<EditText>(Resource.Id.editText_customAddWater).Text = "";
                FindViewById<EditText>(Resource.Id.editText_customAddWater).ClearFocus();

                //remove keyboard
                InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(FindViewById<EditText>(Resource.Id.editText_customAddWater).WindowToken, 0);

                //record last drink log
                DBServices.Instance.UpdatePrevDrinkLog(newDrinkLog);
            }
        }

        public void onClickRepeatDrink(object sender, EventArgs e)
        {
            DrinkLog prev_drink_log = DBServices.Instance.SelectDrinkLogPrev();

            if (prev_drink_log != null)
            {
                drinkLogList.Add(prev_drink_log);

                DBServices.Instance.AddDrinkLogEntry(prev_drink_log);

                ((TodayDrinkLogGridAdapter)FindViewById<GridView>(Resource.Id.TodayDrinkLogGrid).Adapter).NotifyDataSetChanged();

                showHideNoDrinkDefaultText();

                computeProgressTexts();
            }

        }

        public void showHideNoDrinkDefaultText()
        {
            if (FindViewById<GridView>(Resource.Id.TodayDrinkLogGrid).Adapter.Count > 0)
            {
                FindViewById<TextView>(Resource.Id.textView_defaultTextBeforeAdd).Visibility = ViewStates.Gone;
            }
            else
            {
                FindViewById<TextView>(Resource.Id.textView_defaultTextBeforeAdd).Visibility = ViewStates.Visible;
            }
        }

        public void computeProgressTexts()
        {
            FindViewById<TextView>(Resource.Id.textView_ProgressActualsML).Text = drinkLogList.Sum(x => x.volumeML).ToString();

            double percentage = ((double.Parse(FindViewById<TextView>(Resource.Id.textView_ProgressActualsML).Text) /
                      double.Parse(FindViewById<TextView>(Resource.Id.textView_TargetML).Text)) * 100);

            if (double.IsNaN(percentage) || double.IsInfinity(percentage))
            {
                percentage = 0;
            }

            if (((int)percentage).ToString().Length > 3)
            {
                FindViewById<TextView>(Resource.Id.textView_ProgressPercent).SetTextSize(ComplexUnitType.Dip, 21);
                FindViewById<TextView>(Resource.Id.textView_PercentTextPercent).SetTextSize(ComplexUnitType.Dip, 14);
            }
            else if (((int)percentage).ToString().Length > 2)
            {
                FindViewById<TextView>(Resource.Id.textView_ProgressPercent).SetTextSize(ComplexUnitType.Dip, 25);
                FindViewById<TextView>(Resource.Id.textView_PercentTextPercent).SetTextSize(ComplexUnitType.Dip, 17);
            }
            else
            {
                FindViewById<TextView>(Resource.Id.textView_ProgressPercent).SetTextSize(ComplexUnitType.Dip, 30);
                FindViewById<TextView>(Resource.Id.textView_PercentTextPercent).SetTextSize(ComplexUnitType.Dip, 20);
            }

            FindViewById<TextView>(Resource.Id.textView_ProgressPercent).Text = percentage.ToString("#.#");

            if (FindViewById<TextView>(Resource.Id.textView_ProgressPercent).Text.Trim().Equals(""))
            {
                FindViewById<TextView>(Resource.Id.textView_ProgressPercent).Text = "0";
            }

            FindViewById<ProgressBar>(Resource.Id.progressBar_DrinkProgress).Progress = (int)percentage;
        }

        public void updateWeight()
        {
            PersonalSpec spec = DBServices.Instance.SelectCurrentPersonalSpec();

            //todo - volume unit

            if (spec != null)
            {
                FindViewById<TextView>(Resource.Id.textView_TargetML).Text = ((int)(utils.calcWaterRequirement(spec.weight, (utils.Weight_Unit)spec.weight_unit, Pref_Volume_Unit, spec.exercise_duration))).ToString();
            }
            else
            {
                // all defaults
                FindViewById<TextView>(Resource.Id.textView_TargetML).Text = ((int)(utils.calcWaterRequirement(0.0, utils.Weight_Unit.Unit_Kg, Pref_Volume_Unit, 0))).ToString();
            }
        }

        //only for debugging. remove on final build.
        public void showToast(string text)
        {
            Toast.MakeText(this, text, ToastLength.Long).Show();
        }

    }
}