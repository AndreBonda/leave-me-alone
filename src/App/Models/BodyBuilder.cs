using System.Drawing;
using System.Numerics;
using App.Helpers;
using Velaptor.Graphics;

namespace App.Models;

public class BodyBuilder
{
    private const int _NUMBER_OF_SIDE = 4;
    private const int _MAX_V = 5;
    private const int _MAX_METEORITE_RADIUS = 30;
    private const int _MIN_METEORITE_RADIUS = 5;

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

    private Color RandomColor() => Color.FromArgb(red: _rnd.Next(256), green: _rnd.Next(256), blue: _rnd.Next(256));

    private Sides RandomSide()
    {
        var random = _rnd.Next(_NUMBER_OF_SIDE);

        return (Sides)random;
    }

    private Vector2 GenerateRandomVector(Sides side) =>
        side switch
        {
            Sides.TOP => new(_rnd.RandomFloat(-_MAX_V, _MAX_V), _rnd.RandomFloat(0, _MAX_V)),
            Sides.RIGHT => new(_rnd.RandomFloat(-_MAX_V, 0), _rnd.RandomFloat(-_MAX_V, _MAX_V)),
            Sides.BOTTOM => new(_rnd.RandomFloat(-_MAX_V, _MAX_V), _rnd.RandomFloat(-_MAX_V, 0)),
            Sides.LEFT => new(_rnd.RandomFloat(0, _MAX_V), _rnd.RandomFloat(-_MAX_V, _MAX_V)),
            _ => throw new ArgumentException($"Invalid side value {side}")
        };

    private Vector2 RandomInitialPosition(Sides side) =>
        side switch
        {
            Sides.TOP => new(_rnd.RandomFloat(0, _windowWidth), 0),
            Sides.RIGHT => new(_windowWidth, _rnd.RandomFloat(0, _windowHeight)),
            Sides.BOTTOM => new(_rnd.RandomFloat(0, _windowWidth), _windowHeight),
            Sides.LEFT => new(0, _rnd.RandomFloat(0, _windowHeight)),
            _ => throw new ArgumentException($"Invalid side value {side}")
        };

    private float RandomMeteoriteRadius() => _rnd.RandomFloat(_MIN_METEORITE_RADIUS, _MAX_METEORITE_RADIUS);

    private CircleShape GenerateRandomCircleShape(Sides side) =>
        new()
        {
            Radius = RandomMeteoriteRadius(),
            Position = RandomInitialPosition(side),
            Color = RandomColor(),
        };
}