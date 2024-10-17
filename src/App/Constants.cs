using System.Drawing;

namespace App;

public static class GameParameters
{
    public const int MaxV = 5;
    public const float SmallBodySizeRadius = 20f;
    public const float MediumBodySizeRadius = 40f;
    public const float LargeBodySizeRadius = 60f;
    public const int ProjectileMagnitude = 10;
    public const float ProjectileRadius = 5;
    public const float MeteoriteRotationAngleDegrees = 30;
    // Number of frames after updating the angle
    public const int MeteoriteRotationSpeed = 3;
}

public static class GameResources
{
    public const string MascotImageName = "velaptor_mascot";
    public const string FontName = "TimesNewRoman-Regular";
    public const string SmallMeteoriteImageName = "meteorite-small";
    public const string MediumMeteoriteImageName = "meteorite-medium";
    public const string LargeMeteoriteImageName = "meteorite-large";
}