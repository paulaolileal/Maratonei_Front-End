using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maratonei_xamarin.Models
{
    public class Comida : BaseDataObject
    {
        string nome;
        double quantidade = 0;
        double min = 0;
        List<ComidaSerie> comidaSeries;

        public string Nome { get => nome; set => SetProperty(ref nome, value); }
        public double Quantidade { get => quantidade; set => SetProperty(ref quantidade,value); }
        public double MaxPorEpi { get => min; set => SetProperty(ref min, value); }
        public List<ComidaSerie> ComidaSeries { get => comidaSeries; set => SetProperty(ref comidaSeries, value); }
    }
}
