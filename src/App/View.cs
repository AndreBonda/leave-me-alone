using App.Models;
using Velaptor.Batching;
using Velaptor.Graphics.Renderers;

namespace App;

public class View
{
    private readonly Model _model;
    private IShapeRenderer _renderer;
    private IBatcher _batcher;

    public View(Model model, IShapeRenderer renderer, IBatcher batcher)
    {
        _model = model;
        _renderer = renderer;
        _batcher = batcher;
    }

    public void RenderBodies()
    {
        _batcher.Begin();

        foreach (var m in _model.GetMeteorites())
        {
            _renderer.Render(m.Shape);
        }

        foreach(var p in _model.GetProjectiles())
        {
            _renderer.Render(p.Shape);
        }

        _batcher.End();
    }
}