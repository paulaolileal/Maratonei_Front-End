using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maratonei_xamarin.Models
{
    public class Comida
    {
        string nome;
        double quantidade;

        public string Nome { get => nome; set => nome = value; }
        public double Quantidade { get => quantidade; set => quantidade = value; }
    }
}
