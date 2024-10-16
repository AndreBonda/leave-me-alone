using System.Numerics;
using App.Models;
using FluentAssertions;

namespace Testing.Models;

[TestFixture]
public class MeteoriteTests
{
    private const uint WindowWidth = 100;
    private const uint WindowEight = 100;
    [Test]
    public void Update_WhenInvoked_UpdatesAngle()
    {
        // Arrange
        Meteorite sut = new(
            radius: 10,
            position: new(10, 10),
            vector: new Vector2(10, 10));

        // Act & Assert
        sut.Angle.Should().Be(0);

        sut.Update(WindowWidth, WindowEight);
        sut.Angle.Should().BeApproximately(30f, 0.1f);

        sut.Update(WindowWidth, WindowEight);
        sut.Angle.Should().BeApproximately(60f, 0.1f);

        sut.Update(WindowWidth, WindowEight);
        sut.Angle.Should().BeApproximately(90f, 0.1f);

        sut.Update(WindowWidth, WindowEight);
        sut.Angle.Should().BeApproximately(120f, 0.1f);

        sut.Update(WindowWidth, WindowEight);
        sut.Angle.Should().BeApproximately(150f, 0.1f);

        sut.Update(WindowWidth, WindowEight);
        sut.Angle.Should().BeApproximately(180f, 0.1f);

        sut.Update(WindowWidth, WindowEight);
        sut.Angle.Should().BeApproximately(210f, 0.1f);

        sut.Update(WindowWidth, WindowEight);
        sut.Angle.Should().BeApproximately(240f, 0.1f);

        sut.Update(WindowWidth, WindowEight);
        sut.Angle.Should().BeApproximately(270f, 0.1f);

        sut.Update(WindowWidth, WindowEight);
        sut.Angle.Should().BeApproximately(300f, 0.1f);

        sut.Update(WindowWidth, WindowEight);
        sut.Angle.Should().BeApproximately(330f, 0.1f);

        sut.Update(WindowWidth, WindowEight);
        sut.Angle.Should().BeApproximately(0f, 0.1f);

    }
}