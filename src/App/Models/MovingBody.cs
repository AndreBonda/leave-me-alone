using System.Numerics;
using Velaptor.Graphics;

namespace App.Models;

public class MovingBody : Body
{
    private Vector2 _vector;

    public bool Despawned { get; private set; }

    public MovingBody(CircleShape shape, Vector2 vector) : base(shape)
    {
        _vector = vector;
    }

    public virtual void Update(uint windowWidth, uint windowHeight)
    {
        _shape.Position = new Vector2(
            x: _shape.Position.X + _vector.X,
            y: _shape.Position.Y + _vector.Y);

        HandleDespawn(windowWidth, windowHeight);
    }

    public void Despawn() => Despawned = true;

    private void HandleDespawn(uint windowWidth, uint windowHeight)
    {
        if (IsOutOfWindowTop() || IsOutOfWindowRight(windowWidth)
            || IsOutOfWindowBottom(windowHeight) || IsOutOfWindowLeft())
        {
            Despawn();
        }
    }

    private bool IsOutOfWindowLeft()
        => X < 0 && Math.Abs(X) > _shape.Radius;

    private bool IsOutOfWindowRight(uint windowWidth)
        => X > windowWidth && (X - windowWidth) > _shape.Radius;

    private bool IsOutOfWindowTop()
        => Y < 0 && Math.Abs(Y) > _shape.Radius;

    private bool IsOutOfWindowBottom(uint windowHeight)
        => Y > windowHeight && (Y - windowHeight) > _shape.Radius;
}