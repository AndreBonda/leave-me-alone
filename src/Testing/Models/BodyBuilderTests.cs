using System.Drawing;
using App;
using App.Helpers;
using App.Models;
using FluentAssertions;
using NSubstitute;

namespace Testing.Models;

public class BodyBuilderTests
{
    private RandomGenerator _rnd;
    private BodyBuilder _sut;
    private uint windowWidth = 150;
    private uint windowHeight = 100;

    [SetUp]
    protected void SetUp()
    {
        _rnd = Substitute.For<RandomGenerator>(new Random());
        _sut = new BodyBuilder(width: windowWidth, height: windowHeight, rnd: _rnd);
    }

    [Test]
    public void BuildNewMeteorite_WhenInvoked_ReturnsABodyWithExpectedRadius()
    {
        // Arrange
        float expectedRadius = 20f;
        _rnd.RandomFloat(default, default).ReturnsForAnyArgs(expectedRadius);

        // Act
        var actual = _sut.BuildNewMeteorite();

        // Assert
        actual.Shape.Radius.Should()
            .BeApproximately(expectedRadius, 0.1f);
    }

    [Test]
    public void BuildNewMeteorite_WhenInvoked_ReturnsABodyWithExpectedColor()
    {
        // Arrange
        var expectedColor = Color.FromArgb(100, 100, 100);
        _rnd.Next(256).Returns(100);
        _rnd.Next(4).Returns(0);

        // Act
        var actual = _sut.BuildNewMeteorite();

        // Assert
        actual.Shape.Color.Should().Be(expectedColor);
    }

    [Test]
    public void BuildNewMeteorite_WhenInvoked_ReturnsABodyWithExpectedPositionAtTopWindowSide()
    {
        // Arrange
        float expectedX = 75;
        float expectedY = 0;
        _rnd.Next(default).ReturnsForAnyArgs((int)Sides.TOP);
        _rnd.RandomFloat(default, default).ReturnsForAnyArgs(expectedX);

        // Act
        var actual = _sut.BuildNewMeteorite();

        // Assert
        _rnd.Received().RandomFloat(0, windowWidth);
        actual.Shape.Position.X.Should().Be(expectedX);
        actual.Shape.Position.Y.Should().Be(expectedY);
    }

    [Test]
    public void BuildNewMeteorite_WhenInvoked_ReturnsABodyWithExpectedPositionAtRightWindowSide()
    {
        // Arrange
        float expectedX = windowWidth;
        float expectedY = 50;
        _rnd.Next(default).ReturnsForAnyArgs((int)Sides.RIGHT);
        _rnd.RandomFloat(default, default).ReturnsForAnyArgs(expectedY);

        // Act
        var actual = _sut.BuildNewMeteorite();

        // Assert
        _rnd.Received().RandomFloat(0, windowHeight);
        actual.Shape.Position.X.Should().Be(expectedX);
        actual.Shape.Position.Y.Should().Be(expectedY);
    }

    [Test]
    public void BuildNewMeteorite_WhenInvoked_ReturnsABodyWithExpectedPositionAtBottomWindowSide()
    {
        // Arrange
        float expectedX = 75;
        float expectedY = windowHeight;
        _rnd.Next(default).ReturnsForAnyArgs((int)Sides.BOTTOM);
        _rnd.RandomFloat(default, default).ReturnsForAnyArgs(expectedX);

        // Act
        var actual = _sut.BuildNewMeteorite();

        // Assert
        _rnd.Received().RandomFloat(0, windowWidth);
        actual.Shape.Position.X.Should().Be(expectedX);
        actual.Shape.Position.Y.Should().Be(expectedY);
    }

    [Test]
    public void BuildNewMeteorite_WhenInvoked_ReturnsABodyWithExpectedPositionAtLeftWindowSide()
    {
        // Arrange
        float expectedX = 0;
        float expectedY = 50;
        _rnd.Next(default).ReturnsForAnyArgs((int)Sides.LEFT);
        _rnd.RandomFloat(default, default).ReturnsForAnyArgs(expectedY);

        // Act
        var actual = _sut.BuildNewMeteorite();

        // Assert
        _rnd.Received().RandomFloat(0, windowHeight);
        actual.Shape.Position.X.Should().Be(expectedX);
        actual.Shape.Position.Y.Should().Be(expectedY);
    }

    [TestCase(Sides.TOP, -GameConsts.MAX_V, GameConsts.MAX_V, 0, GameConsts.MAX_V)]
    [TestCase(Sides.RIGHT, -GameConsts.MAX_V, 0, -GameConsts.MAX_V, GameConsts.MAX_V)]
    [TestCase(Sides.BOTTOM, -GameConsts.MAX_V, GameConsts.MAX_V, -GameConsts.MAX_V, 0)]
    [TestCase(Sides.LEFT, 0, GameConsts.MAX_V, -GameConsts.MAX_V, GameConsts.MAX_V)]
    public void BuildNewMeteorite_WhenBodyInvoked_CallsRandomFloatWithCorrectParameters(Sides windowSide, int minValueX, int maxValueX, int minValueY, int maxValueY)
    {
        // Arrange
        _rnd.Next(default).ReturnsForAnyArgs((int)windowSide);

        // Act
        _sut.BuildNewMeteorite();

        // Assert
        _rnd.Received().RandomFloat(minValueX, maxValueX);
        _rnd.Received().RandomFloat(minValueY, maxValueY);
    }
}