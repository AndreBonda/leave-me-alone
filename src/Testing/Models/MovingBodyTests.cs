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
    public void GetHashCode_ReturnsCorrectValue()
    {
        // Arrange
        var expectedHashCode = _sut.Id.GetHashCode();

        // Act
        var actual = _sut.GetHashCode();

        // Assert
        actual.Should().Be(expectedHashCode);
    }

    [Test]
    public void Equals_WithNullParameter_ReturnsFalse()
    {
        // Arrange & Act
        var actual = _sut.Equals(null);

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    public void Equals_WithDifferentObjectTypeAsParameter_ReturnsFalse()
    {
        // Arrange & Act
        var actual = _sut.Equals(new object());

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    public void Equals_WithDifferentBodyAsParameter_ReturnsFalse()
    {
        // Arrange
        var anotherBody = new MovingBody(
            new CircleShape()
            {
                Radius = 20,
                Position = new Vector2(0, 2),
                Color = Color.Blue,
                IsSolid = true
            },
            new Vector2(5, 5));

        // Act
        var actual = _sut.Equals(anotherBody);

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    public void Equals_WithSameBodyAsParameter_ReturnsTrue()
    {
        // Arrange
        var sameBody = _sut;

        // Act
        var actual = _sut.Equals(sameBody);

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    public void Update_WhenInvoked_UpdatesPosition()
    {
        // Arrange
        _sut = new MovingBody(
            new CircleShape()
            {
                Radius = 20,
                Position = new Vector2(0, 0),
                Color = Color.Blue,
                IsSolid = true
            },
            new Vector2(5, 5));

        var updatedPositionExpected = new Vector2(5, 5);

        // Act
        _sut.Update();

        // Assert
        _sut.Shape.Position.Should().Be(updatedPositionExpected);
    }
}