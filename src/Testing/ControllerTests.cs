using App;
using App.Helpers;
using App.Models;
using NSubstitute;
using Velaptor;
using Velaptor.Batching;
using Velaptor.Content;
using Velaptor.Content.Fonts;
using Velaptor.Graphics.Renderers;
using Velaptor.Input;

namespace Testing;

[TestFixture]
public class ControllerTests
{
    private Controller _sut;
    private Model _model;
    private View _view;
    private const uint _windowWidth = 0, _windowHeight = 0;

    [SetUp]
    protected void SetUp()
    {
        _model = Substitute.For<Model>(
            Substitute.For<BodyBuilder>(
                Substitute.For<RandomGenerator>(
                    Substitute.For<Random>()
                )
            )
        );

        _view = Substitute.For<View>(
            _model,
            Substitute.For<IFontRenderer>(),
            Substitute.For<IShapeRenderer>(),
            Substitute.For<ITextureRenderer>(),
            Substitute.For<IBatcher>(),
            Substitute.For<ILoader<IFont>>(),
            Substitute.For<ILoader<ITexture>>()
        );

        _sut = new Controller(_model, _view, Substitute.For<IAppInput<MouseState>>());
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
        _sut.UpdateGame(frameTime, _windowWidth, _windowHeight);

        // Assert
        _model.DidNotReceive().GenerateMeteorite(_windowWidth, _windowHeight);
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
        _sut.UpdateGame(frameTime,_windowWidth, _windowHeight);

        // Assert
        _model.Received().UpdateGameModel(_windowWidth, _windowHeight);
    }
}