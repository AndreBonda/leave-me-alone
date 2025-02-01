using App.Models;

namespace App;

public class Model
{
    private readonly HashSet<Meteorite> _meteorites = new();
    private readonly HashSet<MovingBody> _projectiles = new();
    private readonly BodyBuilder _bodyBuilder;
    public uint Score { get; private set; } = 0;

    public Model(BodyBuilder bodyBuilder)
    {
        _bodyBuilder = bodyBuilder;
    }

    public virtual void GenerateMeteorite(uint windowWidth, uint windowHeight) =>
        _meteorites.Add(_bodyBuilder.BuildNewMeteorite(windowWidth, windowHeight));
    public virtual void GenerateProjectile((int X, int Y) userClickCC, uint windowWidth, uint windowHeight)
        => _projectiles.Add(_bodyBuilder.BuildNewProjectile(windowWidth, windowHeight, userClickCC));

    public virtual void UpdateGameModel(uint windowWidth, uint windowHeight)
    {
        UpdateBodies(windowWidth, windowHeight);
        HandleBodyCollisions();
        HandleBodyDespawns();
    }

    public IEnumerable<Meteorite> GetMeteorites() => _meteorites;
    public IEnumerable<MovingBody> GetProjectiles() => _projectiles;

    private void UpdateBodies(uint windowWidth, uint windowHeight)
    {
        foreach (var m in _meteorites)
            m.Update(windowWidth, windowHeight);

        foreach (var p in _projectiles)
            p.Update(windowWidth, windowHeight);
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