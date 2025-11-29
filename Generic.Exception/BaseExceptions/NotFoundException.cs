using System.Net;

namespace Generic.Exception.BaseExceptions
{
    public class NotFoundException : GenericBaseException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public NotFoundException(String msg) : base(msg) { }
    }
}
