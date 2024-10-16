namespace App.Models;

public class Body {
    public Guid Id { get; }
    public float X { get; protected set; }
    public float Y { get; protected set; }
    public float Angle { get; protected set; }
    public float Radius { get; }

    public Body(float radius, (float X, float Y) position, float angle = 0)
    {
        Id = Guid.NewGuid();
        Radius = radius;
        X = position.X;
        Y = position.Y;
        Angle = angle;
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