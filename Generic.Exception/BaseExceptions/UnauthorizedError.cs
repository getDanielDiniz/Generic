
using System.Net;

namespace Generic.Exception.BaseExceptions
{
    public class UnauthorizedError : GenericBaseException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
        public UnauthorizedError(string error) : base(error){}

    }
}
