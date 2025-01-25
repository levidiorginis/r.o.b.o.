using System.Net;

namespace Robo.Core.Exceptions;

public class DomainException : Exception
{
    internal List<string> _errors = new List<string>();

    public IReadOnlyList<string> Errors => _errors;

    public HttpStatusCode StatusCode { get; private set; }

    public DomainException(HttpStatusCode? statusCode = null)
    {
        StatusCode = statusCode ?? HttpStatusCode.BadRequest;
    }

    public DomainException(string message, List<string> errors, HttpStatusCode? statusCode = null) : base(message)
    {
        _errors = errors;
        StatusCode = statusCode ?? HttpStatusCode.BadRequest;
    }

    public DomainException(string message, HttpStatusCode? statusCode = null) : base(message)
    {
        StatusCode = statusCode ?? HttpStatusCode.BadRequest;
    }

    public DomainException(string message, Exception innerException, HttpStatusCode? statusCode = null) : base(message, innerException)
    {
        StatusCode = statusCode ?? HttpStatusCode.BadRequest;
    }
}