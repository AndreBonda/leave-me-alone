using System.Numerics;
using Velaptor.Graphics;

namespace App.Models;

public class MovingBody : Body
{
    private Vector2 _vector;

    public bool Despawned { get; private set; }

    public MovingBody(float radius, (float X, float Y) position, Vector2 vector) : base(radius, position)
    {
        _vector = vector;
    }

    public virtual void Update(uint windowWidth, uint windowHeight)
    {
        X += _vector.X;
        Y += _vector.Y;

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
        => X < 0 && Math.Abs(X) > Radius;

    private bool IsOutOfWindowRight(uint windowWidth)
        => X > windowWidth && (X - windowWidth) > Radius;

    private bool IsOutOfWindowTop()
        => Y < 0 && Math.Abs(Y) > Radius;

    private bool IsOutOfWindowBottom(uint windowHeight)
        => Y > windowHeight && (Y - windowHeight) > Radius;
}