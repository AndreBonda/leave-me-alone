using Velaptor;
using Velaptor.UI;

namespace App;

public class Game : Window
{
    private readonly Controller _controller;

    public Game(Controller controller)
    {
        _controller = controller;
    }

    protected override void OnLoad()
    {
        _controller.InitWindowSize(Width, Height);
        base.OnLoad();
    }

    protected override void OnUpdate(FrameTime frameTime)
    {
        _controller.UpdateGame(frameTime);
        base.OnUpdate(frameTime);
    }

    protected override void OnDraw(FrameTime frameTime)
    {
        _controller.RenderGame();
        base.OnDraw(frameTime);
    }

    protected override void OnUnload()
    {
        base.OnUnload();
    }
}