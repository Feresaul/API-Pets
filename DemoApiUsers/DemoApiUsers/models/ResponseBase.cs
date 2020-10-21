using System;
namespace DemoApiUsers.models
{
    public class ResponseBase<T>
    {
        public bool TieneResultado { set; get; }
        public string Mensaje { set; get; }
        public T Modelo { set; get; }
    }
}
