using System.Numerics;
using SimpleConsole.Exceptions;

namespace SimpleConsole.Validators;

public static class NumericValidators
{
    public static void Positive<T>(T value) where T : INumber<T>
    {
        var dValue = Convert.ToDouble(value);
        if (dValue <= 0) throw new InvalidValueException("Debes ingresar un número positivo");
    }

    public static void PositiveOrZero<T>(T value) where T : INumber<T>
    {
        var dValue = Convert.ToDouble(value);
        if (dValue < 0) throw new InvalidValueException("Debes ingresar un número positivo o cero");
    }

    public static void Negative<T>(T value) where T : INumber<T>
    {
        var dValue = Convert.ToDouble(value);
        if (dValue >= 0) throw new InvalidValueException("Debes ingresar un número negativo");
    }

    public static void NegativeOrZero<T>(T value) where T : INumber<T>
    {
        var dValue = Convert.ToDouble(value);
        if (dValue > 0) throw new InvalidValueException("Debes ingresar un número negativo o cero");
    }

    public static Validator<T> Min<T>(T minValue) where T : INumber<T> => value =>
    {
        if (value.CompareTo(minValue) < 0)
            throw new InvalidValueException($"Debes ingresar un número mayor o igual a {minValue}");
    };

    public static Validator<T> Max<T>(T maxValue) where T : INumber<T> => value =>
    {
        if (value.CompareTo(maxValue) > 0)
            throw new InvalidValueException($"Debes ingresar un número menor o igual a {maxValue}");
    };
}