namespace DevOpsCalculator.Calculator;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class OperationAttribute(string operation) : Attribute
{
    public string Operation { get; set; } = operation;
}
