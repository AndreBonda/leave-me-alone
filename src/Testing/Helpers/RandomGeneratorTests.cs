using App.Helpers;
using App.Models;
using FluentAssertions;
using NSubstitute;

namespace Testing.Helpers;

[TestFixture]
public class RandomGeneratorTests
{
    private Random _rnd;
    private RandomGenerator _sut;

    [SetUp]
    protected void SetUp()
    {
        _rnd = Substitute.For<Random>();
        _sut = new RandomGenerator(_rnd);
    }

    [TestCase(0, BodySize.Small)]
    [TestCase(1, BodySize.Medium)]
    [TestCase(2, BodySize.Large)]
    public void GetRandomBodySize_ReturnsExpectedBodySize(int randomValue, BodySize expectedBodySize)
    {
        // Arrange
        _rnd.Next(default).ReturnsForAnyArgs(randomValue);

        // Act
        BodySize actualBodySize = _sut.GetRandomBodySize();

        // Assert
        actualBodySize.Should().Be(expectedBodySize);
    }

    [TestCase(0, Sides.TOP)]
    [TestCase(1, Sides.RIGHT)]
    [TestCase(2, Sides.BOTTOM)]
    [TestCase(3, Sides.LEFT)]
    public void GetRandomSide_ReturnsExpectedSide(int randomValue, Sides expectedSide)
    {
        // Arrange
        _rnd.Next(default).ReturnsForAnyArgs(randomValue);

        // Act
        Sides actualSide = _sut.GetRandomSide();

        // Assert
        actualSide.Should().Be(expectedSide);
    }

    [Test]
    public void RandomFloat_ThrowsArgumentException_WhenMinValueIsGreaterThanMaxValue()
    {
        // Arrange
        float min = 5;
        float max = 4;

        // Act
        var act = () => _sut.RandomFloat(min, max);

        // Arrange
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage($"Min value {min} must be less than or equal to max value {max}");
    }

    [TestCase(0f, 5f, 0.5d, 2.5f)]
    [TestCase(-5f, 5f, 0.5d, 0f)]
    [TestCase(-5f, 5f, 1d, 5f)]
    [TestCase(-5f, 5f, 0d, -5f)]
    public void RandomFloat_ReturnsExpectedResult_WhenInvoked(float min, float max, double randomValue, float expected)
    {
        // Arrange
        _rnd.NextDouble().Returns(randomValue);

        // Act
        var actual = _sut.RandomFloat(min, max);

        // Assert
        actual.Should().BeApproximately(expected, 0.1f);
    }
}
