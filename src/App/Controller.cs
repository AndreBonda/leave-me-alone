using App.Models;
using Velaptor;

namespace App;

public class Controller
{
    /// <summary>
    /// Meteorite generation frequency in milliseconds
    /// </summary>
    private const float _METEORITE_FREQUENCY_GENERATION = 500;
    private readonly Model _model;
    private readonly View _view;
    private float _elapsedMs = 0;

    public Controller(Model model, View view)
    {
        _model = model;
        _view = view;
    }

    public void InitWindowSize(uint windowWidth, uint windowHeight) =>
        _model.InitWindowSize(windowWidth, windowHeight);

    public void UpdateGame(FrameTime frameTime)
    {
        _elapsedMs += frameTime.ElapsedTime.Milliseconds;

        if (_elapsedMs > _METEORITE_FREQUENCY_GENERATION)
        {
            _model.GenerateMeteorite();
            _elapsedMs = 0;
        }

        _model.UpdateBodies();
    }

    public void RenderGame()
    {
        _view.RenderBodies();
    }
}