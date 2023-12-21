using System.Drawing;
using App.Models;
using Velaptor.Batching;
using Velaptor.Content;
using Velaptor.Content.Fonts;
using Velaptor.Graphics.Renderers;

namespace App;

public class View
{
    private uint? _windowWidth;
    private uint? _windowHeight;
    private readonly Model _model;
    private readonly IFontRenderer _fontRenderer;
    private readonly IShapeRenderer _renderer;
    private readonly ITextureRenderer _textureRenderer;
    private readonly IBatcher _batcher;
    public IFont Font { get; set; }
    public ITexture MascotTexture { get; set; }

    public View(Model model,
        IFontRenderer fontRenderer,
        IShapeRenderer renderer,
        ITextureRenderer textureRenderer,
        IBatcher batcher)
    {
        _model = model;
        _fontRenderer = fontRenderer;
        _renderer = renderer;
        _textureRenderer = textureRenderer;
        _batcher = batcher;
    }

    public void InitWindowSize(uint windowWidth, uint windowHeight)
    {
        _windowWidth = windowWidth;
        _windowHeight = windowHeight;
    }

    public void RenderGame()
    {
        _batcher.Begin();
        RenderMascot();
        RenderBodies();
        RenderScore();
        _batcher.End();
    }

    private void RenderMascot()
    {
        var x = (int)(_windowWidth / 2);
        var y = (int)(_windowHeight / 2);
        _textureRenderer.Render(MascotTexture, x, y);
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