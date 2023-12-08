using System.Numerics;
using Velaptor.Graphics;

namespace App.Models;

public sealed class MovingBody : Body
{
    private Vector2 _vector;

    public MovingBody(CircleShape shape, Vector2 vector) : base(shape)
    {
        _vector = vector;
    }

    public void Update()
    {
        var oldPosition = _shape.Position;
        _shape.Position = new Vector2(
            x: oldPosition.X + _vector.X,
            y: oldPosition.Y + _vector.Y);
    }
}