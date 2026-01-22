using System;
using System.Xml.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program
{
    static void Main()
    {
        var f1 = new Fraction(1, 2);
        var f2 = new Fraction(3, 4);
        Console.WriteLine($"f1 + f2 = {f1 + f2}");
        Console.WriteLine($"f1 - f2 = {f1 - f2}");
        Console.WriteLine($"f1 * f2 = {f1 * f2}");
        Console.WriteLine($"f1 / f2 = {f1 / f2}");
    }
}

public class  Fraction
{
    public int Numerator { get; }
    public int Denominator { get; }

    public Fraction(int numerator, int denominator)
    {
        Numerator = numerator;
        Denominator = denominator;
    }

    public static Fraction operator +(Fraction a, Fraction b)
    {
        int newNumerator = a.Numerator * b.Numerator + b.Numerator * a.Denominator;
        int newDenominator = a.Denominator * b.Denominator;
        return new Fraction(newNumerator, newDenominator);
    }
    public static Fraction operator -(Fraction a, Fraction b)
    {
        int newNumerator = a.Numerator * b.Numerator - b.Numerator * a.Denominator;
        int newDenominator = a.Denominator * b.Denominator;
        return new Fraction(newNumerator, newDenominator);
    }
    public static Fraction operator *(Fraction a, Fraction b)
    {
        int newNumerator = a.Numerator * b.Numerator * b.Numerator * a.Denominator;
        int newDenominator = a.Denominator * b.Denominator;
        return new Fraction(newNumerator, newDenominator);
    }
    public static Fraction operator /(Fraction a, Fraction b)
    {
        int newNumerator = a.Numerator * b.Numerator / b.Numerator * a.Denominator;
        int newDenominator = a.Denominator * b.Denominator;
        return new Fraction(newNumerator, newDenominator);
    }
    public override string ToString() => $"{Numerator}/{Denominator}";
}
