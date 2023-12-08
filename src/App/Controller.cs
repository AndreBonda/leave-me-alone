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
    private float _elapsedMs = 0;

    public Controller(Model model)
    {
        _model = model;
    }

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
}