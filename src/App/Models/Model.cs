using Velaptor.Graphics;

namespace App.Models;

public class Model
{
    private uint? _windowWidth;
    private uint? _windowHeight;
    private readonly HashSet<MovingBody> _meteorites = new();
    private readonly BodyBuilder _bodyBuilder;

    public Model(BodyBuilder bodyBuilder)
    {
        _bodyBuilder = bodyBuilder;
    }

    public void InitWindowSize(uint windowWidth, uint windowHeight)
    {
        _windowWidth = windowWidth;
        _windowHeight = windowHeight;
    }

    public virtual void GenerateMeteorite() => _meteorites.Add(_bodyBuilder.BuildNewMeteorite(_windowWidth.Value, _windowHeight.Value));

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