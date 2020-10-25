using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pets.models
{
    public class Usuario
    {
        public int id { set; get; }
        public string usuario { set; get; }
        public string contrasenia { set; get; }
        public string nombreCompleto { set; get; }
        public string email { set; get; }
        public string telefono { set; get; }
    }
}
