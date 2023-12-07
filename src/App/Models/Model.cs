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

    public void SpawnMeteorite() => _meteorites.Add(_bodyBuilder.BuildNewMeteorite());

    public void UpdateMeteorites()
    {
        foreach(var m in _meteorites)
        {
            m.Update();
        }
    }
}