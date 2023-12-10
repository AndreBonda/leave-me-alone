using System.Drawing;
using System.Numerics;
using App;
using App.Models;
using FluentAssertions;
using Velaptor.Graphics;

namespace Testing.Models;

[TestFixture]
public class ProjectileTests
{
    [Test]
    public void Update_WhenInvoked_UpdatesTrail()
    {
        // Arrange
        float vx = 5;
        byte trailMaxLength = 3;

        var sut = new Projectile(
            new CircleShape()
            {
                Radius = 20,
                Position = new Vector2(0, 0),
                Color = Color.Blue,
            },
            new Vector2(vx, 0),
            trailMaxLength
        );

        // Act & Assert
        sut.Update();
        sut.EndOfTrail.Should().Be((0, 0));
        sut.Update();
        sut.EndOfTrail.Should().Be((0, 0));
        sut.Update();
        sut.EndOfTrail.Should().Be((0, 0));
        // the trail is full now
        sut.Update();
        sut.EndOfTrail.Should().Be((5, 0));
    }
}