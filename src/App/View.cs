using System.Drawing;
using System.Numerics;
using App.Models;
using Velaptor.Batching;
using Velaptor.Content;
using Velaptor.Content.Fonts;
using Velaptor.ExtensionMethods;
using Velaptor.Graphics;
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
    private IFont _font;
    private ITexture _mascotTexture;
    private ITexture _smallMeteoriteTexture;
    private ITexture _mediumMeteoriteTexture;
    private ITexture _largeMeteoriteTexture;

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
        _font = _fontLoader.Load(GameResources.FontName, 11);
        _mascotTexture = _textureLoader.Load(GameResources.MascotImageName);
        _smallMeteoriteTexture = _textureLoader.Load(GameResources.SmallMeteoriteImageName);
        _mediumMeteoriteTexture = _textureLoader.Load(GameResources.MediumMeteoriteImageName);
        _largeMeteoriteTexture = _textureLoader.Load(GameResources.LargeMeteoriteImageName);
    }

    public void UnloadView()
    {
        _fontLoader.Unload(GameResources.FontName);
        _textureLoader.Unload(GameResources.MascotImageName);
        _textureLoader.Unload(GameResources.SmallMeteoriteImageName);
        _textureLoader.Unload(GameResources.MediumMeteoriteImageName);
        _textureLoader.Unload(GameResources.LargeMeteoriteImageName);
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
        _textureRenderer.Render(_mascotTexture, x, y);
    }

    private void RenderMeteorites()
    {
        foreach (var m in _model.GetMeteorites())
        {
            _textureRenderer.Render(GetMeteoriteTexture(m.Size), (int)m.X, (int)m.Y, m.Angle);
        }

    }

    private void RenderProjectiles()
    {
        foreach (var p in _model.GetProjectiles())
        {
            _renderer.Render(new CircleShape()
            {
                Diameter = p.Radius * 2,
                Position = new Vector2(p.X, p.Y)
            });
        }
    }

    private void RenderScore()
    {
        uint score = _model.Score;
        _fontRenderer.Render(_font, $"Score: {score}", 70, 10, Color.White);
    }

    private ITexture GetMeteoriteTexture(BodySize size) => size switch
    {
        BodySize.Small => _smallMeteoriteTexture,
        BodySize.Medium => _mediumMeteoriteTexture,
        BodySize.Large => _largeMeteoriteTexture,
        _ => throw new ArgumentOutOfRangeException(nameof(size), "Size not valid")
    };
}