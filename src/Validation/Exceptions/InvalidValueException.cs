namespace SimpleConsole.Validation.Exceptions;

public class InvalidValueException : Exception
{
    public InvalidValueException()
    {
    }

    public InvalidValueException(string? message) : base(message)
    {
    }

    public override string Message { get; } = "Valor inválido";
}