namespace Kolokwium2.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(string? message = null) : base(message) { }
}