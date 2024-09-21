using DevOpsCalculator.Calculator;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace DevOpsCalculator.Controllers;
public class CalculatorController(ICalculator calculator) : Controller
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
    /// <param name="number1">The first value</param>
    /// <param name="number2">The second value</param>
    /// <param name="operation">The type of operation. The following types can be used:
    ///     <list type="table">
    ///         <item>add</item>
    ///         <item>subtract</item>
    ///         <item>multiply</item>
    ///         <item>divide</item>
    ///     </list>
    /// </param>
    /// <returns>The "Index" view of the page</returns>
    /// <exception cref="ArgumentException">Thrown when the passed data type doesn't exist</exception>
    [HttpPost]
    [Route("/calculator")]
    public IActionResult Calculate(double number1, double number2, string operation)
    {
        try
        {
            var methods = typeof(ICalculator).GetMethods(BindingFlags.Public | BindingFlags.Instance);
            var method = methods.FirstOrDefault(m
                => m.GetCustomAttribute<OperationAttribute>()?.Operation == operation)
                ?? throw new ArgumentException($"Unknown operation: {operation}");

            ViewBag.Result = method.Invoke(calculator, [number1, number2]);
        } catch (Exception ex)
        {
            ViewBag.Exception = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
        }

        return View("Index");
    }
}
