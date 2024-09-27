using DevOpsCalculator.Controllers;
using DevOpsCalculator.Services;
using Microsoft.AspNetCore.Mvc;
namespace Tests
{
    [TestClass]
    public class CalculatorServiceTests
    {
        private CalculatorService calculatorService;

        [TestInitialize]
        public void Setup()
        {
            calculatorService = new CalculatorService();
        }

        [TestMethod]
        public void Add_TwoPositiveNumbers_ReturnsCorrectSum()
        {
            // Arrange
            double a = 5;
            double b = 10;

            // Act
            double result = calculatorService.Add(a, b);

            // Assert
            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void Subtract_TwoPositiveNumbers_ReturnsCorrectDifference()
        {
            // Arrange
            double a = 10;
            double b = 5;

            // Act
            double result = calculatorService.Subtract(a, b);

            // Assert
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void Multiply_TwoPositiveNumbers_ReturnsCorrectProduct()
        {
            // Arrange
            double a = 5;
            double b = 4;

            // Act
            double result = calculatorService.Multiply(a, b);

            // Assert
            Assert.AreEqual(20, result);
        }

        [TestMethod]
        public void Divide_TwoPositiveNumbers_ReturnsCorrectQuotient()
        {
            // Arrange
            double a = 10;
            double b = 2;

            // Act
            double result = calculatorService.Divide(a, b);

            // Assert
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void Divide_DivisionByZero_ThrowsDivideByZeroException()
        {
            // Arrange
            double a = 10;
            double b = 0;

            // Act
            calculatorService.Divide(a, b);
        }
    }


    [TestClass]
    public class CalculatorControllerTests
    {
        private CalculatorController calculatorController;
        private CalculatorService calculatorService;

        [TestInitialize]
        public void Setup()
        {
            calculatorService = new CalculatorService();
            calculatorController = new CalculatorController(calculatorService);
        }

        [TestMethod]
        public void Calculate_ValidExpression_Addition_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "5+3";

            // Act
            var result = calculatorController.Calculate(expression) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("8", result.ViewData["Result"]);  // Доступ к результату через ViewData
        }

        [TestMethod]
        public void Calculate_ValidExpression_Subtraction_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "10-5";

            // Act
            var result = calculatorController.Calculate(expression) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("5", result.ViewData["Result"]);  // Доступ к результату через ViewData
        }

        [TestMethod]
        public void Calculate_ValidExpression_Multiplication_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "4*3";

            // Act
            var result = calculatorController.Calculate(expression) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("12", result.ViewData["Result"]);  // Доступ к результату через ViewData
        }

        [TestMethod]
        public void Calculate_ValidExpression_Division_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "9/3";

            // Act
            var result = calculatorController.Calculate(expression) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("3", result.ViewData["Result"]);  // Доступ к результату через ViewData
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        public void Calculate_EmptyExpression_ThrowsArgumentNullException()
        {
            // Arrange
            string expression = "";

            // Act
            var result = calculatorController.Calculate(expression) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("The string must not be empty", result.ViewData["Exception"]);  // Доступ к ошибке через ViewData
        }

        [TestMethod]
        public void Calculate_InvalidExpression_ThrowsArgumentException()
        {
            // Arrange
            string expression = "10$5";

            // Act
            var result = calculatorController.Calculate(expression) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Unknown operation: $", result.ViewData["Exception"]);  // Доступ к ошибке через ViewData
        }

        [TestMethod]
        public void Calculate_DivisionByZero_ThrowsDivideByZeroException()
        {
            // Arrange
            string expression = "9/0";

            // Act
            var result = calculatorController.Calculate(expression) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Division by zero is not allowed.", result.ViewData["Exception"]);  // Доступ к ошибке через ViewData
        }
    }

}