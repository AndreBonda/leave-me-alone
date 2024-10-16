using System.Numerics;
using App;
using App.Models;
using FluentAssertions;

namespace Testing.Models;

[TestFixture]
public class MeteoriteTests
{
    private const uint WindowWidth = 100;
    private const uint WindowEight = 100;

    [TestCase(BodySize.Small, GameParameters.SmallBodySizeRadius)]
    [TestCase(BodySize.Medium, GameParameters.MediumBodySizeRadius)]
    [TestCase(BodySize.Large, GameParameters.LargeBodySizeRadius)]
    public void Constructor_WhenInvoked_CreatesMeteorite(BodySize size, float expectedRadius)
    {
        // Arrange & Act
        Meteorite meteorite = new(
            size,
            (10,10),
            new Vector2(10, 10));

        // Assert
        meteorite.Size.Should().Be(size);
        meteorite.Radius.Should().Be(expectedRadius);
    }

    [Test]
    public void Update_WhenInvoked_UpdatesAngle()
    {
        // Arrange
        Meteorite sut = new(
            BodySize.Small,
            position: new(10, 10),
            vector: new Vector2(10, 10));

        // Act & Assert
        sut.Angle.Should().Be(0);

        UpdateMeteorite(GameParameters.MeteoriteRotationSpeed, sut);
        sut.Angle.Should().BeApproximately(30f, 0.1f);

        UpdateMeteorite(GameParameters.MeteoriteRotationSpeed, sut);
        sut.Angle.Should().BeApproximately(60f, 0.1f);

        UpdateMeteorite(GameParameters.MeteoriteRotationSpeed, sut);
        sut.Angle.Should().BeApproximately(90f, 0.1f);

        UpdateMeteorite(GameParameters.MeteoriteRotationSpeed, sut);
        sut.Angle.Should().BeApproximately(120f, 0.1f);

        UpdateMeteorite(GameParameters.MeteoriteRotationSpeed, sut);
        sut.Angle.Should().BeApproximately(150f, 0.1f);

        UpdateMeteorite(GameParameters.MeteoriteRotationSpeed, sut);
        sut.Angle.Should().BeApproximately(180f, 0.1f);
    }

    [Test]
    public void Update_WhenFullRotationCompleted_ResetsAngle()
    {
        // Arrange
        Meteorite sut = new(
            BodySize.Small,
            position: new(10, 10),
            vector: new Vector2(10, 10),
            angle: 330);

        // Act & Assert
        UpdateMeteorite(GameParameters.MeteoriteRotationSpeed, sut);
        sut.Angle.Should().BeApproximately(0f, 0.1f);
    }

    private void UpdateMeteorite(int numberOfUpdates, Meteorite meteorite)
    {
        for (int i = 0; i < numberOfUpdates; i++)
        {
            meteorite.Update(WindowWidth, WindowEight);
        }
    }
}