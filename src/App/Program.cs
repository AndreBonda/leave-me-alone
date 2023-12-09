using App;
using App.Helpers;
using App.Models;
using Velaptor.Factories;

// Initialize game components
var model = new Model(new BodyBuilder(
        new RandomGenerator(
            new Random()
        )
    ));
var view = new View(model, RendererFactory.CreateShapeRenderer(), RendererFactory.CreateBatcher());
var controller = new Controller(model,view);
var game = new Game(controller);
game.Show();