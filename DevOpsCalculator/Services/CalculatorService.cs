﻿using DevOpsCalculator.Calculator;

namespace DevOpsCalculator.Services;

public class CalculatorService : ICalculator
{
    /// <summary>
    /// Adds a value <paramref name="a"/> to a value <paramref name="b"/>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns>The sum of the values <paramref name="a"/> and <paramref name="b"/></returns>
    double ICalculator.Add(double a, double b) => a + b;

    /// <summary>
    /// Subtracts a value <paramref name="b"/> from a value <paramref name="a"/>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns>The result of subtracting the value <paramref name="b"/> from the value <paramref name="a"/></returns>
    double ICalculator.Subtract(double a, double b) => a - b;

    /// <summary>
    /// Multiplies a value <paramref name="a"/> by a value <paramref name="b"/>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns>The product of the values <paramref name="a"/> and <paramref name="b"/></returns>
    double ICalculator.Multiply(double a, double b) => a * b;

    /// <summary>
    /// Divides a value <paramref name="a"/> by a value <paramref name="b"/>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns>The quotient of dividing  the value <paramref name="a"/> by the value <paramref name="b"/></returns>
    /// <exception cref="DivideByZeroException">Thrown when value <paramref name="b"/> is equal to 0</exception>
    double ICalculator.Divide(double a, double b)
    {
        if (b == 0) throw new DivideByZeroException("Division by zero is not allowed.");
        return a / b;
    }

}
