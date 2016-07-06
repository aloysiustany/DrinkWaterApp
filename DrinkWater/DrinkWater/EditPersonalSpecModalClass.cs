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
    class EditPersonalSpecModalClass : DialogFragment
    {
        private Dialog dialg = null;
        private DW_MainActivity parent = null;
        private utils.Weight_Unit curr_weight_unit;

        public EditPersonalSpecModalClass(DW_MainActivity activity)
        {
            parent = activity;
        }

        public override Dialog OnCreateDialog(Bundle bundle)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this.Activity);
            View view;

            LayoutInflater inflater = (LayoutInflater)this.Activity.GetSystemService(Context.LayoutInflaterService);
            view = inflater.Inflate(Resource.Layout.EditPersonalSpecModal, null);
            builder.SetView(view);

            view.FindViewById<ImageButton>(Resource.Id.EditPersonalSpecModal_OkBtn).Click += onClickOk;
            view.FindViewById<ImageButton>(Resource.Id.EditPersonalSpecModal_UndoBtn).Click += onClickUndo;

            PersonalSpec spec = DBServices.Instance.SelectCurrentPersonalSpec();

            if (spec != null)
            {
                view.FindViewById<EditText>(Resource.Id.EditPersonalSpec_WeightInput).Text = spec.weight.ToString();
                view.FindViewById<EditText>(Resource.Id.EditPersonalSpec_ExerciseInput).Text = spec.exercise_duration.ToString();

                curr_weight_unit = (utils.Weight_Unit)spec.weight_unit;
            }
            else
            {
                curr_weight_unit = utils.Weight_Unit.Unit_Kg;
            }

            ArrayAdapter adapter = ArrayAdapter.CreateFromResource(parent, Resource.Array.Weight_Units_String_Array, Resource.Drawable.units_spinner_item);

            adapter.SetDropDownViewResource(Resource.Drawable.units_spinner_dropdown_item);
           
            view.FindViewById<Spinner>(Resource.Id.EditPersonalSpec_textWeightUnitSpinner).Adapter = adapter;
            view.FindViewById<Spinner>(Resource.Id.EditPersonalSpec_textWeightUnitSpinner).ItemSelected += 
                new EventHandler<AdapterView.ItemSelectedEventArgs>(onWeightUnitItemSelected);
            view.FindViewById<Spinner>(Resource.Id.EditPersonalSpec_textWeightUnitSpinner).SetSelection((int)curr_weight_unit, true);
            dialg = builder.Create();

            return dialg;
        }

        public void onWeightUnitItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            curr_weight_unit = (utils.Weight_Unit)e.Position;
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
            // Weight NOT entered
            if (dialg.FindViewById<EditText>(Resource.Id.EditPersonalSpec_WeightInput).Text.Trim().Equals(""))
            {
                dialg.FindViewById<TextView>(Resource.Id.EditPersonalSpec_textWeight).SetTextColor(Android.Graphics.Color.Red);
                return;
            }

            int entered_exercise_duration;
            if (dialg.FindViewById<EditText>(Resource.Id.EditPersonalSpec_ExerciseInput).Text.Trim().Equals(""))
            {
                entered_exercise_duration = 0;
            }
            else
            {
                entered_exercise_duration = int.Parse(dialg.FindViewById<EditText>(Resource.Id.EditPersonalSpec_ExerciseInput).Text.Trim());
            }

            //last argument to the PersonalSpec constructor should be 1 - to indicate that this is the current spec of user.
            DBServices.Instance.UpdateCurrentPersonalSpec(new PersonalSpec(
                double.Parse(dialg.FindViewById<EditText>(Resource.Id.EditPersonalSpec_WeightInput).Text.Trim()),
                entered_exercise_duration,
                curr_weight_unit,
                1));


            parent.updateWeight();

            parent.computeProgressTexts();

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