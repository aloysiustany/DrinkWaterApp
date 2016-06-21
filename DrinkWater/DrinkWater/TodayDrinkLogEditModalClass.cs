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

namespace DrinkWater
{
    class TodayDrinkLogEditModalClass : DialogFragment
    {
        public override Dialog OnCreateDialog(Bundle bundle)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this.Activity);
            builder.SetTitle("Hello there 1 !!");

            LayoutInflater inflater = (LayoutInflater)this.Activity.GetSystemService(Context.LayoutInflaterService);
            builder.SetView(inflater.Inflate(Resource.Layout.TodayDrinkLogEditModal, null));

            return builder.Create();
        }
    }
}