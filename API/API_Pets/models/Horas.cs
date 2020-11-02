using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.models
{
    public class Horas<T>
    {
        public string inicio { set; get; }
        public string termino { set; get; }
        public int tiempo { set; get; }
        public T arrayHoras { set; get; }
    }
}
