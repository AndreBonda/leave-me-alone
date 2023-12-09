using Velaptor.Graphics;

namespace App.Models;

public class Model
{
    private readonly uint _width;
    private readonly uint _height;
    private readonly HashSet<MovingBody> _meteorites = new();
    private readonly BodyBuilder _bodyBuilder;

    public Model(uint width, uint height, BodyBuilder bodyBuilder)
    {
        _width = width;
        _height = height;
        _bodyBuilder = bodyBuilder;
    }

    public virtual void GenerateMeteorite() => _meteorites.Add(_bodyBuilder.BuildNewMeteorite());

    public virtual void UpdateBodies()
    {
        foreach (var m in _meteorites)
            m.Update();
    }

    public IEnumerable<CircleShape> GetMeteoriteShapes()
    {
        foreach (var m in _meteorites)
        {
            yield return m.Shape;
        }
    }
}