namespace SimpleConsole.Exceptions;

public class InvalidOptionException : Exception
{
    public InvalidOptionException() : base("Opción inválida")
    {
    }

    public InvalidOptionException(string? message) : base(message)
    {
    }
}