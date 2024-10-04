using System.Runtime.Serialization;
using System.Text.Json;
using Newtonsoft.Json;

namespace Api.Models;

public class CustomException : Exception
{
    //public String ErrorJsonString { get; set; }
    /*public string Canonical { get; set; }
    public string? Exception { get; set; }*/

    public CustomException(string message, Exception? inner = null)
        : base(message, inner) { }
}

public class CustomInternalException : CustomException
{
    public CustomInternalException(string message, Exception? inner = null)
        : base(message, inner) { }
}

public class CustomAuthenticationException : CustomException
{
    public CustomAuthenticationException(string message, Exception? inner = null)
        : base(message, inner) { }
}

public class CustomForbiddenException : CustomException
{
    public CustomForbiddenException(string message, Exception? inner = null)
        : base(message, inner) { }
}

public class CustomTooManyRequestsException : CustomException
{
    public CustomTooManyRequestsException(string canonical, Exception? inner = null)
        : base(canonical, inner) { }
}