namespace SimpleConsole.Validation.Exceptions;

public class InvalidOptionException : Exception
{
    public override string Message { get; } = "Opción inválida";

    public InvalidOptionException()
    {
    }

    public InvalidOptionException(string? message) : base(message)
    {
    }
}