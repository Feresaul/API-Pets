using System;
namespace DemoApiUsers.models
{
    public class ResponseBase<T>
    {
        public bool TieneError { set; get; }
        public string Mensaje { set; get; }
        public T Modelo { set; get; }
    }
}
