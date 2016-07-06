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

        public string time { get; set; }

        public string date { get; set; }

        public double volumeML { get; set; }

        public int iconRef { get; set; }

        public DrinkLog()
        {
            volumeML = 0.0;

            iconRef = Resource.Drawable.GlassIcon_Water_Bottle_96;

            time = DateTime.Now.ToLocalTime().ToShortTimeString();

            date = utils.getDateLongString(0);
        }

        public DrinkLog(double volume)
        {
            volumeML = volume;

            iconRef = Resource.Drawable.GlassIcon_Water_Bottle_96;

            time = DateTime.Now.ToLocalTime().ToShortTimeString();

            date = utils.getDateLongString(0);
        }

        public DrinkLog(double volume, int iconReference)
        {
            volumeML = volume;

            iconRef = iconReference;

            time = DateTime.Now.ToLocalTime().ToShortTimeString();

            date = utils.getDateLongString(0);
        }

        public DrinkLog(double volume, int iconReference, string timeOfDrinking)
        {
            volumeML = volume;

            iconRef = iconReference;

            time = timeOfDrinking;

            date = utils.getDateLongString(0);
        }

        public DrinkLog(double volume, int iconReference, string timeOfDrinking, string dateOfDrinking, int PK_ID)
        {
            volumeML = volume;

            iconRef = iconReference;

            time = timeOfDrinking;

            date = dateOfDrinking;

            ID = PK_ID;
        }

        public DrinkLog(DrinkLogPrev obj)
        {
            this.volumeML = obj.volumeML;

            this.iconRef = obj.iconRef;

            time = DateTime.Now.ToLocalTime().ToShortTimeString();

            date = utils.getDateLongString(0);
        }
    }

    class DrinkLogPrev
    {
        public double volumeML { get; set; }

        public int iconRef { get; set; }

        public DrinkLogPrev(DrinkLog obj)
        {
            this.volumeML = obj.volumeML;
            this.iconRef = obj.iconRef;
        }

        // DB Querry needs a public parameterless constructor.

        public DrinkLogPrev()
        {
            volumeML = 0.0;

            iconRef = Resource.Drawable.GlassIcon_Water_Bottle_96;
        }
    }
}