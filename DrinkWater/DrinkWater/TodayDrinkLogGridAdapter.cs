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

     //   private List<DrinkLog> drinkLogList = new List<DrinkLog>();

        public TodayDrinkLogGridAdapter(Context c)
        {
            context = c;

            for(int i=0;i<10;i++)
            {
        //        drinkLogList.Add(new DrinkLog(120 * i));
            }
            
        }

        public override int Count
        {
            get { return ((DW_MainActivity)context).drinkLogList.Count; }

        //    get { return (DBServices.Instance.SelectDrinkLogEntries(utils.getDateLongString(0)).Count); }
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
           //     TextView textViewPKHidden = (TextView)grid.FindViewById(Resource.Id.TodayDrinkLogGrid_PrimaryKeyHidden);

                if (position < ((DW_MainActivity)context).drinkLogList.Count)
                {
                    textViewML.Text = ((DW_MainActivity)context).drinkLogList[position].volumeML.ToString() + " ml";
                    textViewTime.Text = ((DW_MainActivity)context).drinkLogList[position].time;
                    imageViewGlass.SetImageResource(((DW_MainActivity)context).drinkLogList[position].iconRef);
           //         textViewPKHidden.Text = ((DW_MainActivity)context).drinkLogList[position].ID.ToString();
                }
            }

            return grid;
        }

        public void itemClicked(Object sender, AdapterView.ItemClickEventArgs args)
        {
            TodayDrinkLogEditModalClass obj = new TodayDrinkLogEditModalClass(this);
            obj.volumeML = ((DW_MainActivity)context).drinkLogList[args.Position].volumeML;
            obj.position = args.Position;
            obj.Show(((Activity)context).FragmentManager, "EditDrinkLog");
        }

        public void onDialogOKClick(string text, int position)
        {
            ((DW_MainActivity)context).drinkLogList[position].volumeML = double.Parse(text);
            this.NotifyDataSetChanged();

            DBServices.Instance.UpdateDrinkLogEntry(((DW_MainActivity)context).drinkLogList[position]);

            ((DW_MainActivity)context).computeProgressTexts();
        }

        public void onDialogDelClick(int position)
        {
            DBServices.Instance.DeleteDrinkLogEntry(((DW_MainActivity)context).drinkLogList[position]);

            ((DW_MainActivity)context).drinkLogList.RemoveAt(position);
            this.NotifyDataSetChanged();

            ((DW_MainActivity)context).showHideNoDrinkDefaultText();
            ((DW_MainActivity)context).computeProgressTexts();
        }
    }
}