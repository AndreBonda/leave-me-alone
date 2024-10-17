using System.Numerics;

namespace App.Models;

public class Meteorite : MovingBody
{
    private int _updateNumber = 1;
    public BodySize Size { get; set; }

    public Meteorite(BodySize size, (float X, float Y) position, Vector2 vector, float angle = 0)
        : base(SetRadius(size), position, vector, angle)
    {
        Size = size;
    }

    public override void Update(uint windowWidth, uint windowHeight)
    {
        if (_updateNumber == GameParameters.MeteoriteRotationSpeed)
        {
            Angle += GameParameters.MeteoriteRotationAngleDegrees;
            _updateNumber = 0;
        }
        _updateNumber++;

        if (Angle >= 360)
        {
            Angle = 0;
        }

        base.Update(windowWidth, windowHeight);
    }

    private static float SetRadius(BodySize size) => size switch
    {
        BodySize.Small => GameParameters.SmallBodySizeRadius,
        BodySize.Medium => GameParameters.MediumBodySizeRadius,
        BodySize.Large => GameParameters.LargeBodySizeRadius,
        _ => throw new ArgumentOutOfRangeException(nameof(size), "Body size not valid")
    };
}