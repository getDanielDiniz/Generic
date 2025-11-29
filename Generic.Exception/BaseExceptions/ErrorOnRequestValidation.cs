
using System.Net;

namespace Generic.Exception.BaseExceptions
{
    /// <summary>
    ///     This exception results in a Bad Request HTTP response.
    /// </summary>
    public class ErrorOnRequestValidation : GenericBaseException
    {
        public override HttpStatusCode StatusCode { get; } = HttpStatusCode.BadRequest;

        public ErrorOnRequestValidation(List<string> errors) : base(errors){}

    }
}
