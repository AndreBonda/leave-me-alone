using Velaptor.Graphics;

namespace App.Models;

public class Body {
    protected CircleShape _shape;

    public Guid Id { get; }
    public CircleShape Shape => _shape;
    public float X => _shape.Position.X;
    public float Y => _shape.Position.Y;
    public float Radius => _shape.Radius;

    public Body(CircleShape shape)
    {
        Id = Guid.NewGuid();
        _shape = shape;
    }

    public bool HasCollided(Body body)
    {
        var distance = GetDistance(body);
        return distance <= (Radius + body.Radius);
    }

    public override bool Equals(object obj)
    {
        if (obj is null || obj.GetType() != GetType())
            return false;

        var other = (Body)obj;

        return Id.Equals(other.Id);
    }

    public override int GetHashCode() => Id.GetHashCode();

    private double GetDistance(Body body)
        => Math.Sqrt(Math.Pow(X - body.X, 2) + Math.Pow(Y - body.Y, 2));
}