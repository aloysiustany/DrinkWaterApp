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
        private Dialog dialg = null;
        public double volumeML { get; set; }

        public TodayDrinkLogGridAdapter parent;
        public int position;

        public TodayDrinkLogEditModalClass(TodayDrinkLogGridAdapter sender)
        {
            parent = sender;
        }

        public override Dialog OnCreateDialog(Bundle bundle)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this.Activity);
            View view;

            LayoutInflater inflater = (LayoutInflater)this.Activity.GetSystemService(Context.LayoutInflaterService);
            view = inflater.Inflate(Resource.Layout.TodayDrinkLogEditModal, null);
            builder.SetView(view);
       
            view.FindViewById<ImageButton>(Resource.Id.DrinkLogEditModal_OkBtn).Click += onClickOk;
            view.FindViewById<ImageButton>(Resource.Id.DrinkLogEditModal_UndoBtn).Click += onClickUndo;
            view.FindViewById<ImageButton>(Resource.Id.DrinkLogEditModal_DelBtn).Click += onClickDelete;

            view.FindViewById<EditText>(Resource.Id.DrinkLogEditModal_input).Text = volumeML.ToString();

            dialg = builder.Create();
            

            return dialg;
        }

    

        public void onClickUndo(object sender, EventArgs e)
        {
            if (dialg != null)
            {
                if (dialg.IsShowing)
                {
                    dialg.Dismiss();
                }
            }          
        }

        private void onClickOk(object sender, EventArgs e)
        {
             parent.onDialogOKClick(dialg.FindViewById<EditText>(Resource.Id.DrinkLogEditModal_input).Text, position);

            if (dialg != null)
            {
                if (dialg.IsShowing)
                {
                    dialg.Dismiss();
                }
            }
        }

        private void onClickDelete(object sender, EventArgs e)
        {
            parent.onDialogDelClick(position);

            if (dialg != null)
            {
                if (dialg.IsShowing)
                {
                    dialg.Dismiss();
                }
            }
        }
      
    }
}