using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SQLite.Net.Attributes;

namespace Maratonei_xamarin.Models
{
    [Table("Usuario")]
    public class User
    {
        [PrimaryKey, AutoIncrement, JsonIgnore]
        public int ID { get; set; }
        [JsonProperty(PropertyName = "nome")]
        public string Nome { get; set; }
        [JsonProperty(PropertyName = "senha")]
        public string Senha { get; set; }
        [JsonProperty(PropertyName = "traktUser")]
        public string TraktUser { get; set; }
        [JsonIgnore]
        public bool EstaLogado { get; set; }
    }
}
