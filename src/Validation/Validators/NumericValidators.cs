using SimpleConsole.Validation.Exceptions;

namespace SimpleConsole.Validation.Validators;

public class NumericValidators
{
    public static void Positive<T>(T value) where T : IComparable<int>
    {
        if (0.CompareTo(value) <= 0) throw new InvalidValueException("Debes ingresar un número positivo");
    }
    
    public static void PositiveOrZero<T>(T value) where T : IComparable<int>
    {
        if (0.CompareTo(value) < 0) throw new InvalidValueException("Debes ingresar un número positivo o cero");
    }
    
    public static void Negative<T>(T value) where T : IComparable<int>
    {
        if (0.CompareTo(value) >= 0) throw new InvalidValueException("Debes ingresar un número negativo");
    }
    
    public static void NegativeOrZero<T>(T value) where T : IComparable<int>
    {
        if (0.CompareTo(value) > 0) throw new InvalidValueException("Debes ingresar un número negativo o cero");
    }

    public static Validation<string> Min<T>(T minValue) where T : IComparable<T> => value =>
    {
        if (value.CompareTo(minValue) < 0)
            throw new InvalidValueException($"Debes ingresar un número mayor o igual a {minValue}");
    };
    
    public static Validation<string> Max<T>(T maxValue) where T : IComparable<T> => value =>
    {
        if (value.CompareTo(maxValue) > 0)
            throw new InvalidValueException($"Debes ingresar un número menor o igual a {maxValue}");
    };
    
}