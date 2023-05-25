﻿using System.Globalization;
using SimpleConsole.Exceptions;
using SimpleConsole.Validators;

namespace SimpleConsole;

public class InvalidArrayLengthException : Exception
{
    public InvalidArrayLengthException(string? message) : base(message)
    {
    }
}

public class Console
{
    private readonly int _consoleWidth;
    private readonly char _padChar;
    private readonly string _warningSymbol;
    private readonly int _sleepDelay;
    private readonly bool _defaultClear;
    private readonly ConsoleKey _confirmationKey;
    private readonly ConsoleKey _rejectionKey;

    public Console(
        int consoleWidth = 119,
        string decimalSeparator = ".",
        char padChar = '-',
        string warningSymbol = "[!]",
        int sleepDelay = 1500,
        bool defaultClear = true,
        ConsoleKey confirmationKey = ConsoleKey.S,
        ConsoleKey rejectionKey = ConsoleKey.N
    )
    {
        _consoleWidth = consoleWidth;
        _padChar = padChar;
        _warningSymbol = warningSymbol;
        _sleepDelay = sleepDelay;
        _defaultClear = defaultClear;
        _confirmationKey = confirmationKey;
        _rejectionKey = rejectionKey;

        var cultureInfo = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
        cultureInfo.NumberFormat.NumberDecimalSeparator = decimalSeparator;
        Thread.CurrentThread.CurrentCulture = cultureInfo;
    }
    
    public void Greeting(string title, string summary)
    {
        var bar = String.Concat(Enumerable.Repeat('=', _consoleWidth));
        if (_defaultClear) ClearIfDefault();
        System.Console.WriteLine(
            $"""
            {bar}
            {AddPadding(title)}
            {bar}
            
            {summary}

            Presiona una tecla para continuar...
            """
        );
        ReadKey();
        if (_defaultClear) ClearIfDefault();
    }

    public void Farewell(string message)
    {
        if (_defaultClear) ClearIfDefault();
        System.Console.WriteLine(AddPadding(message));
        Thread.Sleep(_sleepDelay);
    }

    public string ReadString(string message, params Validator<string>[] validators) =>
        GetValueOrRepeat(() => InnerReadString(message), validators);

    public int ReadInt(string message, params Validator<int>[] validators) =>
        GetValueOrRepeat(() => InnerReadInt(message), validators);

    public double ReadDouble(string message, params Validator<double>[] validators) =>
        GetValueOrRepeat(() => InnerReadDouble(message), validators);
    
    public bool Confirm(string message)
    {
        return GetValueOrRepeat(() =>
        {
            System.Console.WriteLine($"{message} [{_confirmationKey}/{_rejectionKey}]");
            var input = ReadKey().Key;
            if (input != _confirmationKey && input != _rejectionKey) throw new InvalidOptionException();
            return input;
        }) == _confirmationKey;
    }

    public T SelectOption<T>(string message, IList<T> options, IList<string>? names = null)
    {
        SelectOption(message, out var selectedOption, options, names);
        return selectedOption;
    }

    public bool SelectOption<T>(string message, out T selectedOption, IList<T> options, IList<string>? names = null,
        string? noOption = null)
    {
        if (options.Count == 0) throw new InvalidArrayLengthException("La cantidad de opciones no puede ser cero");
        selectedOption = default(T)!;
        if (names != null)
        {
            names = new List<string>(names);
            if (names.Count != options.Count)
                throw new InvalidArrayLengthException("La cantidad de nombres debe coincidir con la cantidad de opciones");
        }
        else
        {
            names = new List<string>();
            foreach (var option in options) names.Add(option?.ToString()!);
        }

        if (noOption != null) names.Add(noOption);
        var menu = "";
        for (var i = 0; i < names.Count; i++) menu += $"{i + 1}.- {names[i]}\n";
        menu += message;
        var opIndex = GetValueOrRepeat(() =>
        {
            var opIndex = InnerReadOption(menu) - 1;
            if (opIndex < 0 || opIndex >= names.Count)
                throw new InvalidOptionException($"Debes seleccionar una opción entre 1 y {names.Count}");
            return opIndex;
        });
        if (opIndex >= options.Count) return false;
        selectedOption = options[opIndex];
        return true;
    }

    private T GetValueOrRepeat<T>(Func<T> providerFunction, params Validator<T>[] validators)
    {
        for (;;)
        {
            try
            {
                var value = providerFunction();
                foreach (var validator in validators)
                {
                    validator(value);
                }

                return value;
            }
            catch (Exception e)
            {
                Warning(e.Message);
            }
        }
    }

    private string InnerReadString(string message)
    {
        PrintInputRequest(message);
        var input = ReadLine();
        if (input == null) throw new InvalidValueException("Debes ingresar una cadena de texto");
        return input;
    }

    private int InnerReadInt(string message)
    {
        PrintInputRequest(message);
        if (!int.TryParse(ReadLine(), out var input))
            throw new InvalidValueException("Debes ingresar un número entero");
        return input;
    }

    private double InnerReadDouble(string message)
    {
        PrintInputRequest(message);
        if (!double.TryParse(ReadLine(), out var input))
            throw new InvalidValueException("Debes ingresar un número entero");
        return input;
    }

    private int InnerReadOption(string message)
    {
        PrintInputRequest(message);
        if (!int.TryParse(ReadLine(), out var input)) throw new InvalidValueException("Debes seleccionar una opción");
        return input;
    }

    private static string? ReadLine()
    {
        var input = System.Console.ReadLine();
        System.Console.WriteLine();
        return input;
    }
    
    private static ConsoleKeyInfo ReadKey()
    {
        var input = System.Console.ReadKey(true);
        System.Console.WriteLine();
        return input;
    }

    public void Info(string message) => Alert(AddPadding(message));

    public void Warning(string message) => Alert(AddPadding($" {_warningSymbol} {message} {_warningSymbol} "));

    public void Alert(string message)
    {
        Print(message);
        Thread.Sleep(_sleepDelay);
        ClearIfDefault();
    }
    
    public void ClearIfDefault()
    {
        if (_defaultClear) System.Console.Clear();
    }

    private string AddPadding(string message)
    {
        message = $" {message} ";
        var spaceEnd = (_consoleWidth - message.Length) / 2;
        return message.PadLeft(_consoleWidth - spaceEnd, _padChar).PadRight(_consoleWidth, _padChar);
    }

    public void Print(string message) => System.Console.WriteLine($"{message}\n");

    private void PrintInputRequest(string message) => System.Console.WriteLine($"{message}:");
}