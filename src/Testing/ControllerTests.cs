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

namespace Testing.App;

[TestFixture]
public class ControllerTests
{
    private Controller _sut;
    private Model _model;
    private View _view;

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
            Substitute.For<IBatcher>()
        );

        _sut = new Controller(_model, _view, Substitute.For<IAppInput<MouseState>>(), Substitute.For<ILoader<IFont>>(), Substitute.For<ILoader<ITexture>>());
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
        _model.Received().UpdateGameModel();
    }
}