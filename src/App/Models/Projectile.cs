using System.Numerics;
using Velaptor.Graphics;

namespace App.Models;

public class Projectile : MovingBody
{
    private readonly byte _trailMaxLength;
    private readonly Queue<(float X, float Y)> _trail;
    public (float X, float Y) EndOfTrail => _trail.Peek();

    public Projectile(CircleShape shape, Vector2 vector, byte trailMaxLength) : base(shape, vector)
    {
        _trailMaxLength = trailMaxLength;
        _trail = new Queue<(float X, float Y)>(_trailMaxLength);
    }

    public override void Update()
    {
        EnqueueCurrentPositionToTrail(_shape.Position.X, _shape.Position.Y);
        base.Update();
    }

    private void EnqueueCurrentPositionToTrail(float x, float y)
    {
        _trail.Enqueue((x, y));

        if (_trail.Count > _trailMaxLength)
            _trail.Dequeue();
    }
}