using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;

namespace Maratonei_xamarin.Data_Storage
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
