using DevOpsCalculator.Calculator;
using DevOpsCalculator.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DevOpsCalculator.Controllers;
public class CalculatorController(CalculatorService calculator) : Controller
{
    /// <summary>
    /// Opens the "/calculator" page by GET request
    /// </summary>
    /// <returns>The "Index" view of the page</returns>
    [Route("/calculator")]
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Opens the "/calculator" page by POST request
    /// </summary>
    /// <param name="expression">The expression to calculate</param>
    /// <returns>The "Index" view of the page</returns>
    /// <exception cref="ArgumentNullException">Thrown when passed the empty <paramref name="expression"/> string</exception>
    /// <exception cref="ArgumentException">Thrown when the passed operation type doesn't exist</exception>
    [HttpPost]
    [Route("/calculator")]
    public IActionResult Calculate(string expression)
    {
        try
        {
            if (string.IsNullOrEmpty(expression))
                throw new ArgumentNullException(expression, "The string must not be empty");

            string allowed = @"[^0-9+\-*/,]";
            Match match = Regex.Match(expression, allowed);
            if (match.Success)
                throw new ArgumentException($"Unknown operation: {match.Value}");

            expression = ProcessOperations(expression, @"(?<num1>-?\d+(?:,\d+)?)(?<op>[\*\/])(?<num2>\d+(?:,\d+)?)");
            expression = ProcessOperations(expression, @"(?<num1>-?\d+(?:,\d+)?)(?<op>[\+\-])(?<num2>\d+(?:,\d+)?)");

            ViewBag.Result = expression;
        } catch (Exception ex)
        {
            ViewBag.Exception = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
        }

        return View(nameof(Index));
    }

    /// <summary>
    /// Processes operations that match a given pattern
    /// </summary>
    /// <param name="input">The expression string</param>
    /// <param name="pattern">The pattern to be followed</param>
    /// <returns>New string with the corresponding operations performed</returns>
    private string ProcessOperations(string input, string pattern)
    {
        while (Regex.IsMatch(input, pattern))
        {
            input = Regex.Replace(input, pattern, match =>
            {
                double num1 = double.Parse(match.Groups["num1"].Value);
                double num2 = double.Parse(match.Groups["num2"].Value);
                string op = match.Groups["op"].Value;

                double result = PerformOperation(num1, num2, op);
                return result.ToString();
            });
        }

        return input;
    }

    /// <summary>
    /// Performs an operation on the passed values
    /// </summary>
    /// <param name="num1">The first number</param>
    /// <param name="num2">The second number</param>
    /// <param name="op">The operation type</param>
    /// <returns>The result of the performed operation</returns>
    private double PerformOperation(double num1, double num2, string op)
    {
        var methods = typeof(CalculatorService).GetMethods(BindingFlags.Public | BindingFlags.Instance);
        var method = methods.FirstOrDefault(m
            => m.GetCustomAttribute<OperationAttribute>()?.Operation == op);

        return Convert.ToDouble(method?.Invoke(calculator, [num1, num2]));
    }
}
