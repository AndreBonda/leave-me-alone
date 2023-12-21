using App.Models;
using Velaptor;
using Velaptor.Input;

namespace App;

public class Controller
{
    /// <summary>
    /// Meteorite generation frequency in milliseconds
    /// </summary>
    private const float _METEORITE_FREQUENCY_GENERATION = 500;
    private readonly Model _model;
    private readonly View _view;
    private readonly IAppInput<MouseState> _mouse;
    private float _elapsedMs = 0;
    private MouseState _prevMouseState;

    public Controller(Model model, View view, IAppInput<MouseState> mouse)
    {
        _model = model;
        _view = view;
        _mouse = mouse;
    }

    public void InitWindowSize(uint windowWidth, uint windowHeight) =>
        _model.InitWindowSize(windowWidth, windowHeight);

    public void UpdateGame(FrameTime frameTime)
    {
        _elapsedMs += frameTime.ElapsedTime.Milliseconds;

        if (IsMouseLeftButtonClicked())
            _model.GenerateProjectile((_mouse.GetState().GetX(), _mouse.GetState().GetY()));

        if (_elapsedMs > _METEORITE_FREQUENCY_GENERATION)
        {
            _model.GenerateMeteorite();
            _elapsedMs = 0;
        }

        _model.UpdateGameModel();
        _prevMouseState = _mouse.GetState();
    }

    public void RenderGame()
    {
        _view.RenderBodies();
    }

    private bool IsMouseLeftButtonClicked() => _mouse.GetState().IsLeftButtonDown() && _prevMouseState.IsLeftButtonUp();
}