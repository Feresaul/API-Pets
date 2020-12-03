using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.models
{
    public class FullApointment
    {
        public int id { set; get; }
        public string username { set; get; }
        public string service { set; get; }
        public string enterDate { set; get; }
        public string outDate { set; get; }
        public string status { set; get; }
    }
}
