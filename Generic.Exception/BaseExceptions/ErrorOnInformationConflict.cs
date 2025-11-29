using System.Net;

namespace Generic.Exception.BaseExceptions
{
    public class ErrorOnInformationConflict : GenericBaseException
    {
        public ErrorOnInformationConflict(string error) : base(error){}

        public override HttpStatusCode StatusCode { get; } = HttpStatusCode.Conflict;
    }
}
