using System.Net;

namespace Generic.Exception.BaseExceptions
{
    /// <summary>
    ///     BaseException is the abstract base class for all custom exceptions in the application.
    ///     All custom exceptions should inherit from this class.
    ///     The methods and properties defined in this class is used in exception filter.
    /// </summary>
    public abstract class GenericBaseException : System.Exception
    {
        public List<string> Errors { get; }
        public abstract HttpStatusCode StatusCode { get; }

        public List<string> GetErrors() {
            return Errors;
        }

        public GenericBaseException(List<string> errors)
        {
            Errors = errors;
        }
        public GenericBaseException(string error)
        {
            Errors = [error];
        }

    }
}
