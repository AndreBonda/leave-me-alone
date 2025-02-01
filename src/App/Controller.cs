using App.Models;
using Velaptor;
using Velaptor.Content;
using Velaptor.Content.Fonts;
using Velaptor.ExtensionMethods;
using Velaptor.Input;

namespace App;

public class Controller
{
    /// <summary>
    /// Meteorite generation frequency in milliseconds
    /// </summary>
    private const float MeteoriteFrequencyGeneration = 500;
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

    public void LoadGame() => _view.InitView();

    public void UpdateGame(FrameTime frameTime, uint windowWidth, uint windowHeight)
    {
        _elapsedMs += frameTime.ElapsedTime.Milliseconds;

        if (IsMouseLeftButtonClicked())
        {
            var mouseCoordinates = (_mouse.GetState().GetX(), _mouse.GetState().GetY());
            _model.GenerateProjectile(mouseCoordinates, windowWidth, windowHeight);

        }

        if (_elapsedMs > MeteoriteFrequencyGeneration)
        {
            _model.GenerateMeteorite(windowWidth, windowHeight);
            _elapsedMs = 0;
        }

        _model.UpdateGameModel(windowWidth, windowHeight);
        _prevMouseState = _mouse.GetState();
    }

    public void RenderGame(uint windowWidth, uint windowHeight)
    {
        _view.RenderGame(windowWidth, windowHeight);
    }

    public void UnloadGame()
    {
        _view.UnloadView();
    }

    private bool IsMouseLeftButtonClicked() => _mouse.GetState().IsLeftButtonDown() && _prevMouseState.IsLeftButtonUp();
}