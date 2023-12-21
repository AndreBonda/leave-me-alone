using System.Drawing;
using System.Numerics;
using App.Models;
using FluentAssertions;
using Velaptor.Graphics;

namespace Testing.Models;

[TestFixture]
public class MovingBodyTests
{
    private MovingBody _sut;

    [SetUp]
    public void SetUp()
    {
        _sut = new MovingBody(
                new CircleShape()
                {
                    Radius = 20,
                    Position = new Vector2(0, 0),
                    Color = Color.Blue,
                    IsSolid = true
                },
                new Vector2(5, 5));
    }

    [Test]
    public void Update_WhenInvoked_UpdatesPosition()
    {
        // Arrange
        uint windowWidth = 100;
        uint windowHeight = 100;

        _sut = new MovingBody(
            new CircleShape()
            {
                Radius = 20,
                Position = new Vector2(30, 30),
            },
            new Vector2(5, 5));

        var updatedPositionExpected = new Vector2(35, 35);

        // Act
        _sut.Update(windowWidth, windowHeight);

        // Assert
        _sut.Shape.Position.Should().Be(updatedPositionExpected);
        _sut.Despawned.Should().BeFalse();
    }

    [TestCase(50F, 0F)]
    [TestCase(-50F, 0F)]
    [TestCase(0F, 50F)]
    [TestCase(0F, -50F)]
    public void Update_WhenBodyIsOutOfTheWindow_SetsDespawnPropertyTrue(float bodyPositionX, float bodyPositionY)
    {
        // Arrange
        uint windowWidth = 10;
        uint windowHeight = 10;
        _sut = new MovingBody(
            new CircleShape()
            {
                Radius = 20,
                Position = new Vector2(bodyPositionX, bodyPositionY),
            },
            new Vector2(5, 5));

        // Act
        _sut.Update(windowWidth, windowHeight);

        // Assert
        _sut.Despawned.Should().BeTrue();
    }
}