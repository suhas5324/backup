using Microsoft.AspNetCore.Http;

public class OutOfRangeException : AppException
{
    public OutOfRangeException(string message)
        : base(message, StatusCodes.Status400BadRequest) { }
}