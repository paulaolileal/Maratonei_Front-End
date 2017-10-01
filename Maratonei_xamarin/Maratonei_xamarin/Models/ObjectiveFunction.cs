using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Maratonei_xamarin.Models {
    
    public class Z {

        [JsonProperty( "item1" )]
        public string Item1 { get; set; }

        [JsonProperty( "item2" )]
        public int Item2 { get; set; }
    }

    public class ObjectiveFunction {
        public enum FuncType { Max = 1, Min = 0 };
        public enum RespType { Optimum = 0, Unlimited = 1, Multiple = 2, Impossible = 3, NotASolution = 4 };

        [JsonProperty( "z" )]
        public IList<Z> Z { get; set; }

        [JsonProperty( "type" )]
        public FuncType Type { get; set; }

        [JsonProperty( "solution" )]
        public RespType Solution { get; set; }
    }

    public class R {

        [JsonProperty( "item1" )]
        public string Item1 { get; set; }

        [JsonProperty( "item2" )]
        public int Item2 { get; set; }
    }

    public class Restriction {
        public enum FuncType { GreaterEqual = 1, LessEqual = 0 }
        [JsonProperty( "r" )]
        public IList<R> R { get; set; }

        [JsonProperty( "type" )]
        public FuncType Type { get; set; }
    }

    public class Example {

        [JsonProperty( "objectiveFunction" )]
        public ObjectiveFunction ObjectiveFunction { get; set; }

        [JsonProperty( "restrictions" )]
        public IList<Restriction> Restrictions { get; set; }
    }
}
