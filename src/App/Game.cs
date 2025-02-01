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
        _controller.LoadGame();
        base.OnLoad();
    }

    protected override void OnUpdate(FrameTime frameTime)
    {
        _controller.UpdateGame(frameTime, Width, Height);
        base.OnUpdate(frameTime);
    }

    protected override void OnDraw(FrameTime frameTime)
    {
        _controller.RenderGame(Width, Height);
        base.OnDraw(frameTime);
    }

    protected override void OnUnload()
    {
        _controller.UnloadGame();
        base.OnUnload();
    }
}