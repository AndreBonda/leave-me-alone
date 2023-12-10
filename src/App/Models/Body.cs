using Velaptor.Graphics;

namespace App.Models;

public abstract class Body {
    protected CircleShape _shape;

    public Guid Id { get; }
    public CircleShape Shape => _shape;

    public Body(CircleShape shape)
    {
        Id = Guid.NewGuid();
        _shape = shape;
    }

    public override bool Equals(object obj)
    {
        if (obj is null || obj.GetType() != GetType())
            return false;

        var other = (MovingBody)obj;

        return Id.Equals(other.Id);
    }

    public override int GetHashCode() => Id.GetHashCode();
}