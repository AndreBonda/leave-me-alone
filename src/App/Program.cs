using App;
using App.Helpers;
using App.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Velaptor.Factories;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<Random>();
        services.AddSingleton<RandomGenerator>();
        services.AddSingleton<BodyBuilder>();
        services.AddSingleton<Model>();
        services.AddSingleton(RendererFactory.CreateShapeRenderer());
        services.AddSingleton(RendererFactory.CreateBatcher());
        services.AddSingleton(RendererFactory.CreateTextureRenderer());
        services.AddSingleton(ContentLoaderFactory.CreateFontLoader());
        services.AddSingleton(ContentLoaderFactory.CreateTextureLoader());
        services.AddSingleton(RendererFactory.CreateFontRenderer());
        services.AddSingleton<View>();
        services.AddSingleton(HardwareFactory.GetMouse());
        services.AddSingleton<Controller>();
    })
    .Build();

var game = new Game(host.Services.GetService<Controller>());
game.Show();