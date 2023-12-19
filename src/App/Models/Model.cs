using Velaptor.Graphics;

namespace App.Models;

public class Model
{
    private uint? _windowWidth;
    private uint? _windowHeight;
    private readonly HashSet<MovingBody> _meteorites = new();
    private readonly HashSet<Projectile> _projectiles = new();
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
    public virtual void GenerateProjectile((int X, int Y) userClickCC) => _projectiles.Add(_bodyBuilder.BuildNewProjectile(_windowWidth.Value, _windowHeight.Value, userClickCC));

    public virtual void UpdateBodies()
    {
        ArgumentNullException.ThrowIfNull(_windowWidth);
        ArgumentNullException.ThrowIfNull(_windowHeight);

        foreach (var m in _meteorites)
            m.Update(_windowWidth.Value, _windowHeight.Value);

        foreach (var p in _projectiles)
            p.Update(_windowWidth.Value, _windowHeight.Value);

        HandleBodyDespawn();
    }

    public IEnumerable<MovingBody> GetMeteorites() => _meteorites;
    public IEnumerable<MovingBody> GetProjectiles() => _projectiles;

    private void HandleBodyDespawn()
    {
        _meteorites.RemoveWhere(m => m.Despawn);
        _projectiles.RemoveWhere(p => p.Despawn);
    }
}