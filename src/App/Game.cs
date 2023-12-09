using App.Helpers;
using App.Models;
using Velaptor;
using Velaptor.Batching;
using Velaptor.Factories;
using Velaptor.Graphics;
using Velaptor.Graphics.Renderers;
using Velaptor.UI;

namespace App;

public class Game : Window
{
    private readonly Controller _controller;
    private IShapeRenderer _renderer = RendererFactory.CreateShapeRenderer();
    private IBatcher _batcher = RendererFactory.CreateBatcher();

    public Game()
    {
        var model = new Model(
                Width,
                Height,
                new BodyBuilder(new RandomGenerator(new Random()))
            );

        _controller = new(model, new View(model, _renderer, _batcher));
    }

    protected override void OnLoad()
    {
        base.OnLoad();
    }

    protected override void OnUpdate(FrameTime frameTime)
    {
        _controller.UpdateGame(frameTime);
        base.OnUpdate(frameTime);
    }

    protected override void OnDraw(FrameTime frameTime)
    {
        _batcher.Begin();
        _controller.RenderGame();
        base.OnDraw(frameTime);
    }

    protected override void OnUnload()
    {
        base.OnUnload();
    }
}