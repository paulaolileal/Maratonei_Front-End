using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maratonei_xamarin.Models {
    public class Requisicao
    {
        public ObjectiveFunction ObjectiveFunction { get; set; }
        public List<Restriction > Restrictions { get; set; }
    }
}
