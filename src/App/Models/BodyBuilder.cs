using System.Numerics;
using App.Helpers;

namespace App.Models;

public class BodyBuilder
{
    private readonly RandomGenerator _rnd;

    public BodyBuilder(RandomGenerator rnd)
    {
        _rnd = rnd;
    }

    public Meteorite BuildNewMeteorite(uint windowWidth, uint windowHeight)
    {
        var side = _rnd.GetRandomSide();
        var size = _rnd.GetRandomBodySize();
        var position = GetRandomInitialPosition(side, windowWidth, windowHeight);
        var vector = GetRandomVector(side);
        return new Meteorite(size, position, vector);
    }

    public MovingBody BuildNewProjectile(uint windowWidth, uint windowHeight, (int X, int Y) userClickCC)
    {
        (uint X, uint Y) originCC = new(windowWidth / 2, windowHeight / 2);
        int vxSign = userClickCC.X < originCC.X ? -1 : 1;
        int vySign = userClickCC.Y < originCC.Y ? -1 : 1;
        var x = Math.Abs(originCC.X - userClickCC.X) * vxSign;
        var y = Math.Abs(originCC.Y - userClickCC.Y) * vySign;
        var alpha = Math.Atan2(y, x);
        var vx = (float)Math.Cos(alpha) * GameParameters.ProjectileMagnitude;
        var vy = (float)Math.Sin(alpha) * GameParameters.ProjectileMagnitude;

        return new MovingBody(
            radius: GameParameters.ProjectileRadius,
            position: new(originCC.X, originCC.Y),
            vector: new Vector2(vx, vy)
        );
    }

    private Vector2 GetRandomVector(Sides side) =>
        side switch
        {
            Sides.TOP => new(_rnd.RandomFloat(-GameParameters.MaxV, GameParameters.MaxV), _rnd.RandomFloat(0, GameParameters.MaxV)),
            Sides.RIGHT => new(_rnd.RandomFloat(-GameParameters.MaxV, 0), _rnd.RandomFloat(-GameParameters.MaxV, GameParameters.MaxV)),
            Sides.BOTTOM => new(_rnd.RandomFloat(-GameParameters.MaxV, GameParameters.MaxV), _rnd.RandomFloat(-GameParameters.MaxV, 0)),
            Sides.LEFT => new(_rnd.RandomFloat(0, GameParameters.MaxV), _rnd.RandomFloat(-GameParameters.MaxV, GameParameters.MaxV)),
            _ => throw new ArgumentException($"Invalid side value {side}")
        };

    private (float X, float Y) GetRandomInitialPosition(Sides side, uint windowWidth, uint windowHeight) =>
        side switch
        {
            Sides.TOP => new(_rnd.RandomFloat(0, windowWidth), 0),
            Sides.RIGHT => new(windowWidth, _rnd.RandomFloat(0, windowHeight)),
            Sides.BOTTOM => new(_rnd.RandomFloat(0, windowWidth), windowHeight),
            Sides.LEFT => new(0, _rnd.RandomFloat(0, windowHeight)),
            _ => throw new ArgumentException($"Invalid side value {side}")
        };
}