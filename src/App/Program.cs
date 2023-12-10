using App;
using App.Helpers;
using App.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Velaptor.Factories;
using Velaptor.Graphics.Renderers;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<Random>();
        services.AddSingleton<RandomGenerator>();
        services.AddSingleton<BodyBuilder>();
        services.AddSingleton<Model>();
        services.AddSingleton(RendererFactory.CreateShapeRenderer());
        services.AddSingleton(RendererFactory.CreateBatcher());
        services.AddSingleton<View>();
        services.AddSingleton<Controller>();
    })
    .Build();

var game = new Game(host.Services.GetService<Controller>());
game.Show();