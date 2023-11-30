using System.Drawing;
using System.Numerics;
using App.Model;
using FluentAssertions;
using NUnit.Framework;
using Velaptor.Graphics;

namespace Tests.Model;

[TestFixture]
public class BodyTests
{
    private class FakeBody : Body
    {
        public FakeBody(CircleShape shape, Vector2 velocity) : base(shape, velocity)
        {
        }
    }

    private FakeBody _sut;

    [SetUp]
    public void SetUp()
    {
        _sut = new FakeBody(
                new CircleShape()
                {
                    Radius = 20,
                    Position = new Vector2(0, 2),
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
        var actualHashCode = _sut.GetHashCode();

        // Assert
        actualHashCode.Should().Be(expectedHashCode);
    }
}