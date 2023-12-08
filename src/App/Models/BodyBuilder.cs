using System.Drawing;
using System.Numerics;
using App.Helpers;
using Velaptor.Graphics;

namespace App.Models;

public class BodyBuilder
{
    private readonly uint _windowWidth;
    private readonly uint _windowHeight;
    private readonly RandomGenerator _rnd;

    public BodyBuilder(uint width, uint height, RandomGenerator rnd)
    {
        _windowWidth = width;
        _windowHeight = height;
        _rnd = rnd;
    }

    public MovingBody BuildNewMeteorite()
    {
        var side = RandomSide();
        return new MovingBody(GenerateRandomCircleShape(side), GenerateRandomVector(side));
    }

    private Sides RandomSide()
    {
        var random = _rnd.Next(GameConsts.NUMBER_OF_SIDE);
        return (Sides)random;
    }

    private CircleShape GenerateRandomCircleShape(Sides side) =>
        new()
        {
            Radius = RandomMeteoriteRadius(),
            Position = RandomInitialPosition(side),
            Color = RandomColor(),
        };

    private Vector2 GenerateRandomVector(Sides side) =>
        side switch
        {
            Sides.TOP => new(_rnd.RandomFloat(-GameConsts.MAX_V, GameConsts.MAX_V), _rnd.RandomFloat(0, GameConsts.MAX_V)),
            Sides.RIGHT => new(_rnd.RandomFloat(-GameConsts.MAX_V, 0), _rnd.RandomFloat(-GameConsts.MAX_V, GameConsts.MAX_V)),
            Sides.BOTTOM => new(_rnd.RandomFloat(-GameConsts.MAX_V, GameConsts.MAX_V), _rnd.RandomFloat(-GameConsts.MAX_V, 0)),
            Sides.LEFT => new(_rnd.RandomFloat(0, GameConsts.MAX_V), _rnd.RandomFloat(-GameConsts.MAX_V, GameConsts.MAX_V)),
            _ => throw new ArgumentException($"Invalid side value {side}")
        };

    private Color RandomColor() => Color.FromArgb(red: _rnd.Next(256), green: _rnd.Next(256), blue: _rnd.Next(256));

    private float RandomMeteoriteRadius() => 
        _rnd.RandomFloat(GameConsts.MIN_METEORITE_RADIUS, GameConsts.MAX_METEORITE_RADIUS);

    private Vector2 RandomInitialPosition(Sides side) =>
        side switch
        {
            Sides.TOP => new(_rnd.RandomFloat(0, _windowWidth), 0),
            Sides.RIGHT => new(_windowWidth, _rnd.RandomFloat(0, _windowHeight)),
            Sides.BOTTOM => new(_rnd.RandomFloat(0, _windowWidth), _windowHeight),
            Sides.LEFT => new(0, _rnd.RandomFloat(0, _windowHeight)),
            _ => throw new ArgumentException($"Invalid side value {side}")
        };
}