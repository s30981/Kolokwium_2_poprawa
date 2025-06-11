namespace Kolokwium2.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string? message = null) : base(message) { }
}