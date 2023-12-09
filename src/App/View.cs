using App.Models;
using Velaptor.Batching;
using Velaptor.Graphics.Renderers;

namespace App;

public class View
{
    private readonly Model _model;
    private readonly IShapeRenderer _renderer;
    private readonly IBatcher _batcher;

    public View(Model model, IShapeRenderer renderer, IBatcher batcher)
    {
        _model = model;
        _renderer = renderer;
        _batcher = batcher;
    }

    public void RenderBodies()
    {
        _batcher.Begin();

        foreach (var s in _model.GetMeteoriteShapes())
        {
            _renderer.Render(s);
        }

        _batcher.End();
    }
}