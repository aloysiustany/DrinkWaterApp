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
    public class TodayDrinkLogGridAdapter : BaseAdapter
    {
        private readonly Context context;

        String[] web = {
            "250",
            "200",
            "1200",
            "1000",
            "100",
            "900",
            "900",
            "250",
            "250",
            "1000",
            "500",
            "100",
            "300",
            "250",
            "600",
            "1000",
            "250",
            "300",
             "250",
            "250",
            "250",
             "300",
            "1000",
            "250"

    };
        int[] imageId = {
            Resource.Drawable.GlassIcon_Water_Bottle_96,
            Resource.Drawable.GlassIcon_Water_Bottle_96,
            Resource.Drawable.GlassIcon_Water_Bottle_96,
            Resource.Drawable.GlassIcon_Water_Bottle_96,
            Resource.Drawable.GlassIcon_Water_Bottle_96,
            Resource.Drawable.GlassIcon_Water_Bottle_96,
            Resource.Drawable.GlassIcon_Water_Bottle_96,
            Resource.Drawable.GlassIcon_Water_Bottle_96,
            Resource.Drawable.GlassIcon_Water_Bottle_96,
            Resource.Drawable.GlassIcon_Water_Bottle_96,
            Resource.Drawable.GlassIcon_Water_Bottle_96,
            Resource.Drawable.GlassIcon_Water_Bottle_96,
            Resource.Drawable.GlassIcon_Water_Bottle_96,
            Resource.Drawable.GlassIcon_Water_Bottle_96,
            Resource.Drawable.GlassIcon_Water_Bottle_96,
            Resource.Drawable.GlassIcon_Water_Bottle_96,
            Resource.Drawable.GlassIcon_Water_Bottle_96,
            Resource.Drawable.GlassIcon_Water_Bottle_96,
            Resource.Drawable.GlassIcon_Water_Bottle_96,
            Resource.Drawable.GlassIcon_Water_Bottle_96,
            Resource.Drawable.GlassIcon_Water_Bottle_96,
            Resource.Drawable.GlassIcon_Water_Bottle_96,
            Resource.Drawable.GlassIcon_Water_Bottle_96,
            Resource.Drawable.GlassIcon_Water_Bottle_96

    };

        public TodayDrinkLogGridAdapter(Context c)
        {
            context = c;
            
        }

        public override int Count
        {
            get { return imageId.Length; }
        }

       

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return (Java.Lang.Object)null;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View grid = null;
            LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);

            if (convertView == null)
            {
                grid = new View(context);
            }
            else
            {
                grid = (View)convertView;
                grid = inflater.Inflate(Resource.Layout.TodayDrinkLogGridElement, null);
                TextView textViewML = (TextView)grid.FindViewById(Resource.Id.TodayDrinkLogGridML);
                TextView textViewTime = (TextView)grid.FindViewById(Resource.Id.TodayDrinkLogGridTime);
                ImageView imageViewGlass = (ImageView)grid.FindViewById(Resource.Id.TodayDrinkLogGridImage);
                textViewML.Text = web[position] + " ml";
                DateTime now = DateTime.Now.ToLocalTime();
                textViewTime.Text = now.ToShortTimeString();
                imageViewGlass.SetImageResource(imageId[position]);

            //    parent.
            }

            return grid;
        }

        public void itemClicked(Object sender, AdapterView.ItemClickEventArgs args)
        {
            
            Toast.MakeText(this.context, args.Position.ToString() , ToastLength.Short).Show();

            Dialog dialog = new Dialog(context);
            dialog.SetContentView(Resource.Layout.TodayDrinkLogEditModal);
            dialog.SetTitle("Hello There!");
            dialog.Show();
        }

    }
}