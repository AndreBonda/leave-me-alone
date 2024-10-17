using System.Drawing;
using System.Numerics;
using App;
using App.Models;
using FluentAssertions;
using Velaptor.Graphics;

namespace Testing.Models;

[TestFixture]
public class BodyTests
{
    [Test]
    public void Contructor_BuildsBodyWithExpectedValues()
    {
        // Arrange & Act
        Body body = new(radius: 50f, position: new (10f, 10f), angle: 15f);

        // Assert
        body.Radius.Should().Be(50f);
        body.X.Should().Be(10f);
        body.Y.Should().Be(10f);
        body.Angle.Should().Be(15f);
    }

    [Test]
    public void GetHashCode_ReturnsCorrectValue()
    {
        // Arrange
        Body body = new(radius: 50f, position: (10f, 10f), angle: 15f);
        var expectedHashCode = body.Id.GetHashCode();

        // Act
        var actual = body.GetHashCode();

        // Assert
        actual.Should().Be(expectedHashCode);
    }

    [Test]
    public void Equals_WithNullParameter_ReturnsFalse()
    {
        // Arrange & Act
        Body body = new(radius: 50f, position: (10f, 10f), angle: 15f);
        var actual = body.Equals(null);

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    public void Equals_WithDifferentObjectTypeAsParameter_ReturnsFalse()
    {
        // Arrange & Act
        Body body = new(radius: 50f, position: (10f, 10f), angle: 15f);
        var actual = body.Equals(new object());

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    public void Equals_WithDifferentBodyAsParameter_ReturnsFalse()
    {
        // Arrange
        Body body = new(radius: 50f, position: (10f, 10f), angle: 15f);
        var anotherBody = new Body(10, position: new(0, 2));

        // Act
        var actual = body.Equals(anotherBody);

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    public void Equals_WithSameBodyAsParameter_ReturnsTrue()
    {
        // Arrange
        Body body = new(radius: 50f, position: (10f, 10f), angle: 15f);
        var sameBody = body;

        // Act
        var actual = body.Equals(sameBody);

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    public void HasCollided_WhenThereIsNotACollision_ReturnsFalse()
    {
        // Arrange
        var b1 = new Body(10, new(10, 10));
        var b2 = new Body(10, new(50, 50));

        // Act
        var result = b1.HasCollided(b2);

        // Assert
        result.Should().BeFalse();
    }

    [TestCase(10, 10, 15, 15)]
    [TestCase(0, 0, 20, 0)]
    public void HasCollided_WhenThereIsACollision_ReturnsTrue(
        float b1X, float b1Y, float b2X, float b2Y
    )
    {
        // Arrange
        var b1 = new Body(10, new(b1X, b1Y));
        var b2 = new Body(10, new(b2X, b2Y));

        // Act
        var result = b1.HasCollided(b2);

        // Assert
        result.Should().BeTrue();
    }
}