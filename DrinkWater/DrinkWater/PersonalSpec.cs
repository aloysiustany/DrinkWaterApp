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
using SQLite;

namespace DrinkWater
{
    class PersonalSpec
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string date_time { get; set; }

        public double weight { get; set; }

        public int weight_unit { get; set; }

        public int exercise_duration { get; set; }

        public int isSpecCurrent { get; set; }

        public PersonalSpec()
        {
            date_time = DateTime.Now.ToString();
            weight = 0;
            weight_unit = (int)utils.Weight_Unit.Unit_Kg;
            exercise_duration = 0;
            isSpecCurrent = 0;
        }

        public PersonalSpec(double arg_weight, int arg_exercise_duration, utils.Weight_Unit arg_weight_unit, int spec_is_current)
        {
            date_time = DateTime.Now.ToString();
            weight = arg_weight;
            weight_unit = (int)arg_weight_unit;
            exercise_duration = arg_exercise_duration;
            isSpecCurrent = spec_is_current;
        }
    }
}