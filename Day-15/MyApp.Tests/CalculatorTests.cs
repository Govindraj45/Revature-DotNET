using MyApp;

namespace MyApp.Tests;

// Req -> Analysis/Design -> Development -> Testing -> Deployment
// Req -> Analysis/Design -> Testing -> Development -> Deployment
public class CalculatorTests
{
    [Theory]
    [InlineData(2, 3, 5)]   // 2 + 3 = 5
    [InlineData(0, 0, 0)]   // 0 + 0 = 0
    [InlineData(-1, 1, 0)]  // -1 + 1 = 0
    public void Add_TwoNumbers_GivesCorrectResult(int x, int y, int expectedResult)
    {
        // Arrange
        var calculator = new Calculator();
        // system under test
        // var sut = new Calculator();

        // manual calculation
        // var x = 5;
        // var y = 10;
        // var expectedResult = 15;

        // Act
        var actualResult = calculator.Add(x, y);

        // Assert
        Assert.Equal(expected: expectedResult, actual: actualResult);
    }

    [Theory]
    [InlineData(2, 3, -1)]
    [InlineData(0, 0, 0)]
    [InlineData(-1, 1, -2)]
    public void Subtract_TwoNumbers_GivesCorrectResult(int x, int y, int expectedResult)
    {
        // Arrange
        var calculator = new Calculator();
        // system under test
        // var sut = new Calculator();

        // manual calculation
        // var x = 5;
        // var y = 10;
        // var expectedResult = -5;

        // Act
        var actualResult = calculator.Subtract(x, y);

        // Assert
        Assert.Equal(expected: expectedResult, actual: actualResult);
    }

    [Theory]
    [InlineData(2, 3, 6)]
    [InlineData(0, 10, 0)]
    [InlineData(-4, 2, -8)]
    public void Multiply_TwoNumbers_GivesCorrectResult(int x, int y, int expectedResult)
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var actualResult = calculator.Multiply(x, y);

        // Assert
        Assert.Equal(expected: expectedResult, actual: actualResult);
    }

    [Theory]
    [InlineData(10, 2, 5)]
    [InlineData(0, 5, 0)]
    [InlineData(-12, 3, -4)]
    public void Divide_TwoNumbers_GivesCorrectResult(int x, int y, int expectedResult)
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var actualResult = calculator.Divide(x, y);

        // Assert
        Assert.Equal(expected: expectedResult, actual: actualResult);
    }

    [Fact]
    public void Divide_ByZero_ThrowsException()
    {
        // Arrange
        var calculator = new Calculator();

        // Act + Assert
        Assert.Throws<DivideByZeroException>(() => calculator.Divide(10, 0));
    }
}
