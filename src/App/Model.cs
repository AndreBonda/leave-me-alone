using App.Models;

namespace App;

public class Model
{
    private uint? _windowWidth;
    private uint? _windowHeight;
    private readonly HashSet<Meteorite> _meteorites = new();
    private readonly HashSet<MovingBody> _projectiles = new();
    private readonly BodyBuilder _bodyBuilder;
    public uint Score { get; private set; } = 0;

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

    public virtual void UpdateGameModel()
    {
        ArgumentNullException.ThrowIfNull(_windowWidth);
        ArgumentNullException.ThrowIfNull(_windowHeight);

        UpdateBodies();
        HandleBodyCollisions();
        HandleBodyDespawns();
    }

    public IEnumerable<MovingBody> GetMeteorites() => _meteorites;
    public IEnumerable<MovingBody> GetProjectiles() => _projectiles;

    private void UpdateBodies()
    {
        foreach (var m in _meteorites)
            m.Update(_windowWidth.Value, _windowHeight.Value);

        foreach (var p in _projectiles)
            p.Update(_windowWidth.Value, _windowHeight.Value);
    }

    private void HandleBodyDespawns()
    {
        _meteorites.RemoveWhere(m => m.Despawned);
        _projectiles.RemoveWhere(p => p.Despawned);
    }

    private void HandleBodyCollisions()
    {
        foreach (var projectile in _projectiles)
            foreach (var meteorite in _meteorites)
                if (projectile.HasCollided(meteorite))
                {
                    projectile.Despawn();
                    meteorite.Despawn();
                    IncreaseScore();
                }
    }

    private void IncreaseScore() => Score += 1;
}