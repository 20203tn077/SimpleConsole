using System.Numerics;
using SimpleConsole.Exceptions;

namespace SimpleConsole.Validators;

public static class NumericValidators<T> where T : INumber<>
{
    public static void Positive(T value)
    {
        double.Parse()
        if (0.CompareTo(value) <= 0) throw new InvalidValueException("Debes ingresar un número positivo");
    }
    
    public static void PositiveOrZero(T value)
    {
        if (0.CompareTo(value) < 0) throw new InvalidValueException("Debes ingresar un número positivo o cero");
    }
    
    public static void Negative(T value)
    {
        if (0.CompareTo(value) >= 0) throw new InvalidValueException("Debes ingresar un número negativo");
    }
    
    public static void NegativeOrZero(T value)
    {
        if (0.CompareTo(value) > 0) throw new InvalidValueException("Debes ingresar un número negativo o cero");
    }

    public static Validator<T> Min(T minValue) where T : IComparable<T> => value =>
    {
        if (value.CompareTo(minValue) < 0)
            throw new InvalidValueException($"Debes ingresar un número mayor o igual a {minValue}");
    };
    
    public static Validator<T> Max(T maxValue) where T : IComparable<T> => value =>
    {
        if (value.CompareTo(maxValue) > 0)
            throw new InvalidValueException($"Debes ingresar un número menor o igual a {maxValue}");
    };
    
}