using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Maratonei_xamarin.Data_Storage;
using Maratonei_xamarin.Droid.Data_Storage;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using Xamarin.Forms;
using Environment = System.Environment;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_Android))]
namespace Maratonei_xamarin.Droid.Data_Storage
{
    public class SQLite_Android : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var v_FileName = "MaratoneiLocal.db3";
            var v_Platform = new SQLitePlatformAndroid();
            var v_Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), v_FileName);
            return new SQLiteConnection(v_Platform,v_Path);
        }
    }
}