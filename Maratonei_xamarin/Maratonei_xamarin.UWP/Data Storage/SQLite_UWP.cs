using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Maratonei_xamarin.Data_Storage;
using Maratonei_xamarin.UWP.Data_Storage;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;


[assembly: Dependency(typeof(SQLite_UWP))]
namespace Maratonei_xamarin.UWP.Data_Storage
{
    class SQLite_UWP : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var v_Platform = new SQLitePlatformWinRT();
            var v_Filename = "MaratoneiLocal.db3";
            var v_Path = Path.Combine(ApplicationData.Current.LocalFolder.Path, v_Filename);
            return new SQLiteConnection(v_Platform, v_Path);
        }
    }
}
