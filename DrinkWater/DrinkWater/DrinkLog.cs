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
    class DrinkLog
    {
        public double volumeML;

        public int iconRef;

        public string time;

        public DrinkLog()
        {
            volumeML = 0.0;

            iconRef = Resource.Drawable.GlassIcon_Water_Bottle_96;

            time = DateTime.Now.ToLocalTime().ToShortTimeString();
        }

        public DrinkLog(double volume)
        {
            volumeML = volume;

            iconRef = Resource.Drawable.GlassIcon_Water_Bottle_96;

            time = DateTime.Now.ToLocalTime().ToShortTimeString();
        }

        public DrinkLog(double volume, int iconReference)
        {
            volumeML = volume;

            iconRef = iconReference;

            time = DateTime.Now.ToLocalTime().ToShortTimeString();
        }

        public DrinkLog(double volume, int iconReference, string timeOfDrinking)
        {
            volumeML = volume;

            iconRef = iconReference;

            time = timeOfDrinking;
        }
    }
}