namespace DevOpsCalculator.Calculator;

public interface ICalculator
{
    [Operation("add")]
    double Add(double a, double b);
    
    [Operation("subtract")]
    double Subtract(double a, double b);
    
    [Operation("multiply")]
    double Multiply(double a, double b);
    
    [Operation("divide")]
    double Divide(double a, double b);
}
