using System.Drawing;
using App.Models;
using Velaptor.Batching;
using Velaptor.Content;
using Velaptor.Content.Fonts;
using Velaptor.Graphics.Renderers;

namespace App;

public class View
{
    private readonly Model _model;
    private readonly IFontRenderer _fontRenderer;
    private IShapeRenderer _renderer;
    private IBatcher _batcher;
    public IFont Font { get; set; }

    public View(Model model,
        IFontRenderer fontRenderer,
        IShapeRenderer renderer,
        IBatcher batcher)
    {
        _model = model;
        _fontRenderer = fontRenderer;
        _renderer = renderer;
        _batcher = batcher;
    }

    public void RenderGame()
    {
        _batcher.Begin();
        RenderBodies();
        RenderScore();
        _batcher.End();
    }

    private void RenderBodies()
    {
        foreach (var m in _model.GetMeteorites())
            _renderer.Render(m.Shape);

        foreach (var p in _model.GetProjectiles())
            _renderer.Render(p.Shape);
    }

    private void RenderScore()
    {
        uint score = _model.Score;
        _fontRenderer.Render(Font, $"Score: {score}", 70, 10, Color.White);
    }
}