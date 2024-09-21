using DevOpsCalculator.Calculator;
using DevOpsCalculator.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace DevOpsCalculator.Controllers;
public class CalculatorController(ICalculator calculator) : Controller
{
    [Route("/calculator")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Route("/calculator")]
    public IActionResult Calculate(double number1, double number2, string operation)
    {
        var methods = typeof(ICalculator).GetMethods(BindingFlags.Public | BindingFlags.Instance);
        var method = methods.FirstOrDefault(m
            => m.GetCustomAttribute<OperationAttribute>()?.Operation == operation)
            ?? throw new ArgumentException("Unknown operation");

        ViewBag.Result = method.Invoke(calculator, [number1, number2]);

        return View("Index");
    }
}
