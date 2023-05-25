namespace SimpleConsole.Exceptions;

public class InvalidValueException : Exception
{
    public InvalidValueException() : base("Valor inválido")
    {
    }

    public InvalidValueException(string? message) : base(message)
    {
    }
}