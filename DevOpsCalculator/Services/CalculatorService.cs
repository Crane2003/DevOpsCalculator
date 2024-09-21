using DevOpsCalculator.Calculator;

namespace DevOpsCalculator.Services;

public class CalculatorService : ICalculator
{
    double ICalculator.Add(double a, double b) => a + b;
    double ICalculator.Subtract(double a, double b) => a - b;
    double ICalculator.Multiply(double a, double b) => a * b;

    double ICalculator.Divide(double a, double b)
    {
        if (b == 0) throw new DivideByZeroException("Division by zero is not allowed.");
        return a / b;
    }

}
