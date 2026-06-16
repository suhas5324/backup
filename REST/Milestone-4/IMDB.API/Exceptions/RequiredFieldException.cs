using Microsoft.AspNetCore.Http;

public class RequiredFieldException : AppException
{
    public RequiredFieldException(string message)
        : base(message, StatusCodes.Status400BadRequest) { }
}