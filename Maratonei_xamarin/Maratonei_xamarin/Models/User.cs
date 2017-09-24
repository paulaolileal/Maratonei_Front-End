using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace Maratonei_xamarin.Models
{
    [Table("Usuario")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Nome { get; set; }

    }
}
