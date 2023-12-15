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
        _rnd = Substitute.For<RandomGenerator>(
            Substitute.For<Random>()
        );
        _sut = new BodyBuilder(rnd: _rnd);
    }

    [Test]
    public void BuildNewMeteorite_WhenInvoked_ReturnsABodyWithExpectedRadius()
    {
        // Arrange
        float expectedRadius = 20f;
        _rnd.RandomFloat(default, default).ReturnsForAnyArgs(expectedRadius);

        // Act
        var actual = _sut.BuildNewMeteorite(windowWidth, windowHeight);

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
        var actual = _sut.BuildNewMeteorite(windowWidth, windowHeight);

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
        var actual = _sut.BuildNewMeteorite(windowWidth, windowHeight);

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
        var actual = _sut.BuildNewMeteorite(windowWidth, windowHeight);

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
        var actual = _sut.BuildNewMeteorite(windowWidth, windowHeight);

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
        var actual = _sut.BuildNewMeteorite(windowWidth, windowHeight);

        // Assert
        _rnd.Received().RandomFloat(0, windowHeight);
        actual.Shape.Position.X.Should().Be(expectedX);
        actual.Shape.Position.Y.Should().Be(expectedY);
    }

    [TestCase(Sides.TOP, -GameParameters.MAX_V, GameParameters.MAX_V, 0, GameParameters.MAX_V)]
    [TestCase(Sides.RIGHT, -GameParameters.MAX_V, 0, -GameParameters.MAX_V, GameParameters.MAX_V)]
    [TestCase(Sides.BOTTOM, -GameParameters.MAX_V, GameParameters.MAX_V, -GameParameters.MAX_V, 0)]
    [TestCase(Sides.LEFT, 0, GameParameters.MAX_V, -GameParameters.MAX_V, GameParameters.MAX_V)]
    public void BuildNewMeteorite_WhenInvoked_CallsRandomFloatWithCorrectParameters(
        Sides windowSide, int minValueX, int maxValueX, int minValueY, int maxValueY)
    {
        // Arrange
        _rnd.Next(default).ReturnsForAnyArgs((int)windowSide);

        // Act
        _sut.BuildNewMeteorite(windowWidth, windowHeight);

        // Assert
        _rnd.Received().RandomFloat(minValueX, maxValueX);
        _rnd.Received().RandomFloat(minValueY, maxValueY);
    }

    [TestCase(100u, 80u, GameParameters.PROJECTILE_RADIUS, 50F, 40F)]
    public void BuildNewProjectile_WhenInvoked_ReturnsAProjectileWithExpectedProperties(
        uint windowWidth,
        uint windowHeight,
        float expectedRadius,
        float expectedProjectileX,
        float expectedProjectileY
    )
    {
        // Act
        var actual = _sut.BuildNewProjectile(windowWidth, windowHeight, new(20, 20));

        // Assert
        actual.Shape.Radius.Should().Be(expectedRadius);
        actual.Shape.Position.X.Should().BeApproximately(expectedProjectileX, 0.1F);
        actual.Shape.Position.Y.Should().BeApproximately(expectedProjectileY, 0.1F);
    }

    [TestCase(75, 40, 60, 40)] // projectile direction: right
    [TestCase(25, 40, 40, 40)] // projectile direction: left
    [TestCase(50, 10, 50, 30)] // projectile direction: top
    [TestCase(50, 60, 50, 50)] // projectile direction: bottom
    [TestCase(70, 20, 57.07F, 32.93F)] // projectile direction: top-left
    [TestCase(30, 60, 42.93F, 47.07F)] // projectile direction: bottom-right
    public void BuildNewProjectile_WhenInvoked_ReturnsAProjectileWithExpectedTrajectory(
    int userClickX,
    int userClickY,
    float expectedProjectileXAfterUpdate,
    float expectedProjectileYAfterUpdate
    )
    {
        // Arrange
        var windowWidth = 100u;
        var windowHeight = 80u;

        // Act
        var actual = _sut.BuildNewProjectile(windowWidth, windowHeight, new(userClickX, userClickY));
        actual.Update();

        // Assert
        actual.Shape.Position.X.Should().BeApproximately(expectedProjectileXAfterUpdate, 0.1F);
        actual.Shape.Position.Y.Should().BeApproximately(expectedProjectileYAfterUpdate, 0.1F);
    }
}