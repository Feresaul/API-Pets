using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.models
{
    public class Cita
    {
        public int idServicio { set; get; }
        public int idUsuario { set; get; }
        public string fechaInicio { set; get; }
    }
}
