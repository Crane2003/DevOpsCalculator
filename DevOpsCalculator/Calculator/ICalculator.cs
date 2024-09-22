namespace DevOpsCalculator.Calculator;

public interface ICalculator
{
    [Operation("+")]
    double Add(double a, double b);
    
    [Operation("-")]
    double Subtract(double a, double b);
    
    [Operation("*")]
    double Multiply(double a, double b);
    
    [Operation("/")]
    double Divide(double a, double b);
}
