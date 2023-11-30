using System.Numerics;
using Velaptor.Graphics;

namespace App.Model;

public abstract class MovingBody
{
    private readonly Vector2 _velocity;
    private CircleShape _circleShape;

    protected MovingBody(CircleShape circleShape, Vector2 velocity)
    {
        _circleShape = circleShape;
        _velocity = velocity;
    }

    public void Update()
    {
        var oldPosition = _circleShape.Position;
        _circleShape.Position = new Vector2(
            x: oldPosition.X + _velocity.X,
            y: oldPosition.Y + _velocity.Y);
    }


}