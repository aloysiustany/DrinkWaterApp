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
using Android.Util;

namespace DrinkWater
{
    /// <summary>
    /// Singleton class. Do not create instance. Use 'Instance' property to get instance of this class.
    /// </summary>
    class DBServices
    {
        private static readonly DBServices instance;
        private SQLiteConnection db_connection;

        public SQLiteConnection DB_Connection
        {
            get
            {
                return db_connection;
            }
        }

        public static DBServices Instance
        {
            get
            {
                return instance;
            }
        }

        static DBServices()
        {
            instance = new DBServices();
        }

        private DBServices()
        {
            var dir = new Java.IO.File(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/DrinkWater/DataBase/");
            bool b;
            
            if (!dir.Exists())
            {
                b=dir.Mkdirs();
            }
            
            // createDatabase(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData));

            createDatabase(dir.AbsolutePath + "/DrinkLog.db");

            createTable<DrinkLog>();

            createTable<DrinkLogPrev>();
        }

        private string createDatabase(string path)
        {
            try
            {
                db_connection = new SQLiteConnection(path);

                Log.Info("DBServices", "Database created");
                return "Database created";
            }
            catch (SQLiteException ex)
            {
                Log.Warn("DBServices", ex.Message);
                return ex.Message;
            }
        }

        private string createTable<T>()
        {
            try
            {
                if (db_connection != null)
                {
                    if (!TableExists<T>())
                    {
                        db_connection.CreateTable<T>();

                        Log.Info("DBServices", "Table created");
                        return "Table created";
                    }
                    Log.Info("DBServices", "Table already exists");
                    return "Table already exists";
                }
                Log.Warn("DBServices", "DB connection does not exist");
                return "DB connection does not exist";
            }
            catch (SQLiteException ex)
            {
                Log.Warn("DBServices", ex.Message);
                return ex.Message;
            }
        }

        private bool TableExists<T>()
        {
            if (db_connection != null)
            {
                const string cmdText = "SELECT name FROM sqlite_master WHERE type='table' AND name=?";
                var cmd = db_connection.CreateCommand(cmdText, typeof(T).Name);

                return cmd.ExecuteScalar<string>() != null;
            }
            return false;
        }

        public string AddDrinkLogEntry(DrinkLog obj)
        {
            try
            {
                if (db_connection != null)
                {
                    return db_connection.Insert(obj).ToString();
                }
                Log.Warn("DBServices", "DB connection does not exist");
                return "DB connection does not exist";
            }
            catch (SQLiteException ex)
            {
                Log.Warn("DBServices", ex.Message);
                return ex.Message;
            }
        }

        public string UpdateDrinkLogEntry(DrinkLog obj)
        {
            try
            {
                if (db_connection != null)
                {
                    int rowsAffected = db_connection.Update(obj);

                    if(rowsAffected==0)
                    {
                        Log.Warn("DBServices", "No records found with the primary key of the obj");
                        return "No records found with the primary key of the obj";
                    }
                    return rowsAffected.ToString();
                }
                Log.Warn("DBServices", "DB connection does not exist");
                return "DB connection does not exist";
            }
            catch (SQLiteException ex)
            {
                Log.Warn("DBServices", ex.Message);
                return ex.Message;
            }
        }

        public List<DrinkLog> SelectDrinkLogEntries(string date)
        {
            try
            {
                if (db_connection != null)
                {
                    return db_connection.Query<DrinkLog>("SELECT * FROM DrinkLog WHERE date=\'" + date + "\'");
                }
                Log.Warn("DBServices", "DB connection does not exist");
                return null;
            }
            catch (SQLiteException ex)
            {
                Log.Warn("DBServices", ex.Message);
                return null;
            }
        }

        public int DeleteDrinkLogEntry(DrinkLog obj)
        {
            try
            {
                if (db_connection != null)
                {
                    return db_connection.CreateCommand("DELETE FROM DrinkLog WHERE ID=?", obj.ID).ExecuteNonQuery();
                }
                Log.Warn("DBServices", "DB connection does not exist");
                return -1;
            }
            catch (SQLiteException ex)
            {
                Log.Warn("DBServices", ex.Message);
                return -1;
            }
        }

        public string UpdatePrevDrinkLog(DrinkLog obj)
        {
            try
            {
                if (db_connection != null)
                {
                    // Only one row should be there in DrinkLogPrev table

                    db_connection.CreateCommand("DELETE FROM DrinkLogPrev").ExecuteNonQuery();

                    return db_connection.Insert(new DrinkLogPrev(obj)).ToString();
                }
                Log.Warn("DBServices", "DB connection does not exist");
                return "DB connection does not exist";
            }
            catch (SQLiteException ex)
            {
                Log.Warn("DBServices", ex.Message);
                return ex.Message;
            }
        }

        public DrinkLog SelectDrinkLogPrev()
        {
            try
            {
                if (db_connection != null)
                {
                    List<DrinkLogPrev> list = db_connection.Query<DrinkLogPrev>("SELECT * FROM DrinkLogPrev");
                    if (list != null && list.Count != 0)
                    {
                        // Only one row should be there in DrinkLogPrev table
                        return new DrinkLog(list[0]);
                    }
                    Log.Warn("DBServices", "DrinkLogPrev did not return anything");
                    return null;
                }
                Log.Warn("DBServices", "DB connection does not exist");
                return null;
            }
            catch (SQLiteException ex)
            {
                Log.Warn("DBServices", ex.Message);
                return null;
            }
        }
    }
}