namespace App.Helpers;

public class RandomGenerator
{
    private readonly Random _rnd;

    public RandomGenerator(Random rnd)
    {
        _rnd = rnd;
    }

    public virtual int Next(int maxValue) => _rnd.Next(maxValue);

    public virtual float RandomFloat(float min, float max)
    {
        if (min > max) throw new ArgumentException($"Min value {min} must be less than or equal to max value {max}");

        var absoluteRange = Math.Abs(max - min);
        var random = (float)_rnd.NextDouble() * absoluteRange;
        return random + min;
    }
}