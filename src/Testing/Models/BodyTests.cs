using System.Drawing;
using System.Numerics;
using App.Models;
using FluentAssertions;
using Velaptor.Graphics;

namespace Testing.Models;

[TestFixture]
public class BodyTests
{
    private Body _sut;

    [SetUp]
    public void SetUp()
    {
        _sut = new Body(radius: 20, position: new (0, 0));
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
        var anotherBody = new Body(radius: 20, position: new (0, 2));

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
    public void HasCollided_WhenThereIsNotACollision_ReturnsFalse()
    {
        // Arrange
        var b1 = new Body(10, new (10, 10));
        var b2 = new Body(10, new (50, 50));

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
        var b1 = new Body(10, new (b1X, b1Y));
        var b2 = new Body(10,new (b2X, b2Y));

        // Act
        var result = b1.HasCollided(b2);

        // Assert
        result.Should().BeTrue();
    }
}