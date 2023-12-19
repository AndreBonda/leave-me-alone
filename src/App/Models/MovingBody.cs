using System.Numerics;
using Velaptor.Graphics;

namespace App.Models;

public class MovingBody : Body
{
    private Vector2 _vector;

    public bool Despawn { get; private set; }
    public float X => _shape.Position.X;
    public float Y => _shape.Position.Y;

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

    private void HandleDespawn(uint windowWidth, uint windowHeight)
    {
        if (IsOutOfWindowTop() || IsOutOfWindowRight(windowWidth) 
            || IsOutOfWindowBottom(windowHeight) || IsOutOfWindowLeft())
        {
            Console.WriteLine("Despawn!");
            Despawn = true;
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