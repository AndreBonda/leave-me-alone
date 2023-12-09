using App;
using App.Helpers;
using App.Models;
using NSubstitute;
using Velaptor;
using Velaptor.Batching;
using Velaptor.Graphics.Renderers;

namespace Testing.App;

[TestFixture]
public class ControllerTests
{
    private Controller _sut;
    private Model _model;

    [SetUp]
    protected void SetUp()
    {
        _model = Substitute.For<Model>(
            (uint)0,
            (uint)0,
            new BodyBuilder(new RandomGenerator(new Random()))
        );
        _sut = new(_model, new View(_model, Substitute.For<IShapeRenderer>(), Substitute.For<IBatcher>()));
    }

    [Test]
    public void UpdateGame_WhenElapsedTimeIsLowerThanMeteoriteFrequencyGeneration_DoesNotCallGenerateMeteorite()
    {
        // Arrange
        FrameTime frameTime = new()
        {
            ElapsedTime = TimeSpan.Zero
        };

        // Act
        _sut.UpdateGame(frameTime);

        // Assert
        _model.DidNotReceive().GenerateMeteorite();
    }

    [Test]
    public void UpdateGame_WhenInvoked_CallsUpdateBodies()
    {
        // Arrange
        FrameTime frameTime = new()
        {
            ElapsedTime = TimeSpan.Zero
        };

        // Act
        _sut.UpdateGame(frameTime);

        // Assert
        _model.Received().UpdateBodies();
    }
}