using System.Drawing;
using System.Numerics;
using App.Helpers;
using Velaptor.Graphics;

namespace App.Models;

public class BodyBuilder
{
    private readonly RandomGenerator _rnd;

    public BodyBuilder(RandomGenerator rnd)
    {
        _rnd = rnd;
    }

    public MovingBody BuildNewMeteorite(uint windowWidth, uint windowHeight)
    {
        var side = RandomSide();
        return new MovingBody(
            radius: RandomMeteoriteRadius(),
            position: RandomInitialPosition(
                side,
                windowWidth,
                windowHeight),
            GenerateRandomVector(side)
        );
    }

    public MovingBody BuildNewProjectile(uint windowWidth, uint windowHeight, (int X, int Y) userClickCC)
    {
        (uint X, uint Y) originCC = new(windowWidth / 2, windowHeight / 2);
        int vxSign = userClickCC.X < originCC.X ? -1 : 1;
        int vySign = userClickCC.Y < originCC.Y ? -1 : 1;
        var x = Math.Abs(originCC.X - userClickCC.X) * vxSign;
        var y = Math.Abs(originCC.Y - userClickCC.Y) * vySign;
        var alpha = Math.Atan2(y, x);
        var vx = (float)Math.Cos(alpha) * GameParameters.PROJECTILE_MAGNITUDE;
        var vy = (float)Math.Sin(alpha) * GameParameters.PROJECTILE_MAGNITUDE;

        return new MovingBody(
            radius: GameParameters.PROJECTILE_RADIUS,
            position: new(originCC.X, originCC.Y),
            vector: new Vector2(vx, vy)
        );
    }

    private Sides RandomSide()
    {
        var random = _rnd.Next(GameParameters.NUMBER_OF_SIDE);
        return (Sides)random;
    }

    private Vector2 GenerateRandomVector(Sides side) =>
        side switch
        {
            Sides.TOP => new(_rnd.RandomFloat(-GameParameters.MAX_V, GameParameters.MAX_V), _rnd.RandomFloat(0, GameParameters.MAX_V)),
            Sides.RIGHT => new(_rnd.RandomFloat(-GameParameters.MAX_V, 0), _rnd.RandomFloat(-GameParameters.MAX_V, GameParameters.MAX_V)),
            Sides.BOTTOM => new(_rnd.RandomFloat(-GameParameters.MAX_V, GameParameters.MAX_V), _rnd.RandomFloat(-GameParameters.MAX_V, 0)),
            Sides.LEFT => new(_rnd.RandomFloat(0, GameParameters.MAX_V), _rnd.RandomFloat(-GameParameters.MAX_V, GameParameters.MAX_V)),
            _ => throw new ArgumentException($"Invalid side value {side}")
        };

    private float RandomMeteoriteRadius() =>
        _rnd.RandomFloat(GameParameters.MIN_METEORITE_RADIUS, GameParameters.MAX_METEORITE_RADIUS);

    private (float X, float Y) RandomInitialPosition(Sides side, uint windowWidth, uint windowHeight) =>
        side switch
        {
            Sides.TOP => new(_rnd.RandomFloat(0, windowWidth), 0),
            Sides.RIGHT => new(windowWidth, _rnd.RandomFloat(0, windowHeight)),
            Sides.BOTTOM => new(_rnd.RandomFloat(0, windowWidth), windowHeight),
            Sides.LEFT => new(0, _rnd.RandomFloat(0, windowHeight)),
            _ => throw new ArgumentException($"Invalid side value {side}")
        };
}