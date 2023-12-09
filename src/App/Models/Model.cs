using Velaptor.Graphics;

namespace App.Models;

public class Model
{
    private readonly uint _windowWidth;
    private readonly uint _windowHeight;
    private readonly HashSet<MovingBody> _meteorites = new();
    private readonly BodyBuilder _bodyBuilder;

    public Model(uint windowWidth, uint windowHeight, BodyBuilder bodyBuilder)
    {
        _windowWidth = windowWidth;
        _windowHeight = windowHeight;
        _bodyBuilder = bodyBuilder;
    }

    public virtual void GenerateMeteorite() => _meteorites.Add(_bodyBuilder.BuildNewMeteorite(_windowWidth, _windowHeight));

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