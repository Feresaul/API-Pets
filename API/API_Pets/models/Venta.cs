using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.models
{
    public class Venta
    {
        public string service { set; get; }
        public string employee { set; get; }
        public string client { set; get; }
        public string date { set; get; }
        public string total { set; get; }
    }
}
