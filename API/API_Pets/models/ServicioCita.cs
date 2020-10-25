using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApiUsers.models
{
    public class ServicioCita : Servicio
    {
        public string tipoServicio { set; get; }
        public string status { set; get; }
        public string fechaInicio { set; get; }
        public string fechaTermino { set; get; }
    }
}
