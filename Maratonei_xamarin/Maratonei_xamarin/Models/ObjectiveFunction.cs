using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Maratonei_xamarin.Models {

    public class ObjectiveFunction {
        public List<Tuple<string, double>> Z;
        public FuncType Type;
        public RespType Solution;

        public enum FuncType { Max = 1, Min = 0 };
        public enum RespType { Optimum = 0, Unlimited = 1, Multiple = 2, Impossible = 3, NotASolution = 4 };

        public ObjectiveFunction() { }

        public ObjectiveFunction( FuncType funcType ) {
            Z = new List<Tuple<string, double>>();
            Type = funcType;
            Solution = RespType.NotASolution;
        }

        public ObjectiveFunction( FuncType Type, RespType solution ) {
            Z = new List<Tuple<string, double>>();
            Solution = solution;
        }

        public void Add( string variable, double value ) {
            Z.Add( new Tuple<string, double>( variable, value ) );
        }
        
    }
    public class Restriction {
        public List<Tuple<string, double>> R;
        public FuncType Type;
        public enum FuncType { GreaterEqual = 1, LessEqual = 0 }

        public Restriction()
        {
        }

        public Restriction( FuncType type ) {
            R = new List<Tuple<string, double>>();
            Type = type;
        }

        public void Add( string variable, double value ) {
            R.Add( new Tuple<string, double>( variable, value ) );
        }

        public List<Tuple<string, double>> Transform() {
            var TransformedR = new List<Tuple<string, double>>();
            if( Type == FuncType.GreaterEqual ) {
                foreach( var element in R ) {
                    TransformedR.Add( new Tuple<string, double>( element.Item1, element.Item2 * -1 ) );
                }
                return TransformedR;
            }
            return R;
        }
    }
}
