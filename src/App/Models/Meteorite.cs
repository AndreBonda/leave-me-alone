using System.Numerics;

namespace App.Models;

public class Meteorite : MovingBody
{
    public Meteorite(float radius, (float X, float Y) position, Vector2 vector, float angle = 0)
        : base(radius, position, vector, angle)
    {
    }

    public override void Update(uint windowWidth, uint windowHeight)
    {
        Angle += GameParameters.MeteoriteRotationAngleDegrees;

        if (Angle >= 360)
        {
            Angle = 0;
        }

        base.Update(windowWidth, windowHeight);
    }
}