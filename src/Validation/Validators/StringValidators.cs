using System.Text.RegularExpressions;
using SimpleConsole.Validation.Exceptions;

namespace SimpleConsole.Validation.Validators;

public static class StringValidators
{
    public static void NotBlank(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new InvalidValueException("Debes ingresar un texto con contenido");
    }

    public static Validation<string> MinLength(int minLength) => value =>
    {
        if (value.Length < minLength)
            throw new InvalidValueException($"Debes ingresar un texto mayor a {minLength} caracteres");
    };
    
    public static Validation<string> MaxLength(int maxLength) => value =>
    {
        if (value.Length < maxLength)
            throw new InvalidValueException($"Debes ingresar un texto menor a {maxLength} caracteres");
    };
    
    public static Validation<string> Pattern(Regex pattern) => value =>
    {
        if (!pattern.IsMatch(value))
            throw new InvalidValueException("El texto no cumple con el formato requerido");
    };
}