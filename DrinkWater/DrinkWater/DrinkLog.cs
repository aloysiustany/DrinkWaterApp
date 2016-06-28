using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

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
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public double volumeML { get; set; }

        public int iconRef { get; set; }

        public string time { get; set; }

        public string date { get; set; }


        public DrinkLog()
        {
            volumeML = 0.0;

            iconRef = Resource.Drawable.GlassIcon_Water_Bottle_96;

            time = DateTime.Now.ToLocalTime().ToShortTimeString();

            date = DateTime.Now.ToLongDateString();
        }

        public DrinkLog(double volume)
        {
            volumeML = volume;

            iconRef = Resource.Drawable.GlassIcon_Water_Bottle_96;

            time = DateTime.Now.ToLocalTime().ToShortTimeString();

            date = DateTime.Now.ToLongDateString();
        }

        public DrinkLog(double volume, int iconReference)
        {
            volumeML = volume;

            iconRef = iconReference;

            time = DateTime.Now.ToLocalTime().ToShortTimeString();

            date = DateTime.Now.ToLongDateString();
        }

        public DrinkLog(double volume, int iconReference, string timeOfDrinking)
        {
            volumeML = volume;

            iconRef = iconReference;

            time = timeOfDrinking;

            date = DateTime.Now.ToLongDateString();
        }

        public override string ToString()
        {
            return string.Format("[DrinkLog: ID={0}, volumeML={1}, iconRef={2}, time={3}, date={4}]", ID, volumeML, iconRef, time, date);
        }

    }
}