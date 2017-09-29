using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maratonei_xamarin.Models;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using Xamarin.Forms;

namespace Maratonei_xamarin.Data_Storage
{
    public class UserDAO
    {
        public SQLiteConnection Connection { get; set; }
        public UserDAO()
        {
            Connection = DependencyService.Get<ISQLite>().GetConnection();
            Connection.CreateTable<User>();
        }

        public void InsertUser(User p_User)
        {
            Connection.InsertOrReplaceWithChildren(p_User);
        }

        public List<User> RecoverUsers()
        {
            return Connection.GetAllWithChildren<User>();
        }
    }
}
