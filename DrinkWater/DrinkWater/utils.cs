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
    /// <summary>
    /// Static class. Do not instantiate object.
    /// </summary>
    public static class utils
    {
        public enum Weight_Unit
        {
            Unit_Kg = 0,
            Unit_Pound = 1
        }

        public enum Volume_Unit
        {
            Unit_mL = 0,
            Unit_Ounce = 1
        }

        public static double kg_to_pounds(double kg)
        {
            return kg / 0.45359237;
        }

        public static double pounds_to_kg(double pounds)
        {
            return pounds * 0.45359237;
        }

        public static double ounces_to_ml(double ounces)
        {
            return ounces * 29.573529562499997696;
        }

        public static double ml_to_ounces(double ml)
        {
            return ml / 29.573529562499997696;
        }

        /// <summary>
        /// Returns in ml.
        /// </summary>
        /// <param name="weight">Weight in KG</param>
        /// <returns></returns>
        public static double waterReqmntKg(double weight)
        {
            // www.umsystem.edu/newscentral/totalrewards/2014/06/19/how-to-calculate-how-much-water-you-should-drink
            return ounces_to_ml(kg_to_pounds(weight) * 0.5);
        }

        /// <summary>
        /// Returns in ml.
        /// </summary>
        /// <param name="weight">Weight in Pounds</param>
        /// <returns></returns>
        public static double waterReqmntPounds(double weight)
        {
            // www.umsystem.edu/newscentral/totalrewards/2014/06/19/how-to-calculate-how-much-water-you-should-drink
            return ounces_to_ml(weight * 0.5);
        }


        /// <summary>
        /// Returns in ml.
        /// </summary>
        /// <param name="minutes">Workout duration in minutes</param>
        /// <returns></returns>
        public static double waterReqmntExerciseML(int minutes)
        {
            // www.umsystem.edu/newscentral/totalrewards/2014/06/19/how-to-calculate-how-much-water-you-should-drink
            return ounces_to_ml((minutes / 30) * 12);
        }

        /// <summary>
        /// Returns in ounces.
        /// </summary>
        /// <param name="minutes">Workout duration in minutes</param>
        /// <returns></returns>
        public static double waterReqmntExerciseOunce(int minutes)
        {
            // www.umsystem.edu/newscentral/totalrewards/2014/06/19/how-to-calculate-how-much-water-you-should-drink
            return (minutes / 30) * 12;
        }

        /// <summary>
        /// Returns water requirement per day.
        /// </summary>
        /// <param name="weight">Weight in Kg or Pounds</param>
        /// <param name="weight_unit"></param>
        /// <param name="water_volume_unit"</param>
        /// <param name="exercise_minutes">Workout duration in minutes (optional)</param>
        /// <returns>Unit depends on water_volume_unit parameter</returns>
        public static double calcWaterRequirement(double weight, Weight_Unit weight_unit, Volume_Unit water_volume_unit, int exercise_minutes = 0)
        {
            double weight_pounds;
            double water_volume_ml = 0;
            double ret_val;

            if (weight_unit == Weight_Unit.Unit_Kg)
            {
                weight_pounds = kg_to_pounds(weight);
            }
            else
            {
                weight_pounds = weight;
            }

            water_volume_ml += waterReqmntPounds(weight_pounds);
            water_volume_ml += waterReqmntExerciseML(exercise_minutes);

            if (water_volume_unit == Volume_Unit.Unit_Ounce)
            {
                ret_val = ml_to_ounces(water_volume_ml);
            }
            else
            {
                ret_val = water_volume_ml;
            }

            return ret_val;
        }
    }
}