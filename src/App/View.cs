using System.Drawing;
using App.Models;
using Velaptor.Batching;
using Velaptor.Content;
using Velaptor.Content.Fonts;
using Velaptor.ExtensionMethods;
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
    private readonly ILoader<IFont> _fontLoader;
    private readonly ILoader<ITexture> _textureLoader;
    public IFont Font { get; set; }
    public ITexture MascotTexture { get; set; }

    public View(
        Model model,
        IFontRenderer fontRenderer,
        IShapeRenderer renderer,
        ITextureRenderer textureRenderer,
        IBatcher batcher,
        ILoader<IFont> fontLoader, ILoader<ITexture> textureLoader)
    {
        _model = model;
        _fontRenderer = fontRenderer;
        _renderer = renderer;
        _textureRenderer = textureRenderer;
        _batcher = batcher;
        _fontLoader = fontLoader;
        _textureLoader = textureLoader;
    }

    public void InitView(uint windowWidth, uint windowHeight)
    {
        _windowWidth = windowWidth;
        _windowHeight = windowHeight;
        Font = _fontLoader.Load("TimesNewRoman-Regular", 11);
        MascotTexture = _textureLoader.Load("velaptor_mascot");
    }

    public void UnloadView()
    {
        _fontLoader.Unload("TimesNewRoman-Regular");
        _textureLoader.Unload("velaptor-mascot");
    }

    public void RenderGame()
    {
        _batcher.Begin();
        RenderMascot();
        RenderMeteorites();
        RenderProjectiles();
        RenderScore();
        _batcher.End();
    }

    private void RenderMascot()
    {
        var x = (int)(_windowWidth / 2);
        var y = (int)(_windowHeight / 2);
        _textureRenderer.Render(MascotTexture, x, y);
    }

    private void RenderMeteorites()
    {
        foreach (var m in _model.GetMeteorites())
        {
            //_renderer.Render(m.Shape);
        }

    }

    private void RenderProjectiles()
    {
        foreach (var p in _model.GetProjectiles())
        {
            //_renderer.Render(p.Shape);
        }
    }

    private void RenderScore()
    {
        uint score = _model.Score;
        _fontRenderer.Render(Font, $"Score: {score}", 70, 10, Color.White);
    }
}