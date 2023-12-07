using App.Helpers;
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

    [Test]
    public void Next_WhenInvoked_InvokeRandomMethodCorrectly()
    {
        // Arrange
        int expected = 5;

        // Act
        _sut.Next(expected);

        // Assert
        _rnd.Received().Next(expected);
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
