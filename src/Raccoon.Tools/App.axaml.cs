using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Raccoon.Tools.ViewModels;
using Raccoon.Tools.Views;

namespace Raccoon.Tools;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var builder = RaccoonContext.Create();

        builder.AddSingleton<MainWindow>(provider => new MainWindow()
        {
            DataContext = new MainWindowViewModel()
        });

        builder.Service.AddHttpClient();
        
        builder.Service.AddMapster();

        var serviceProvider = RaccoonContext.Build(builder);


        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            BindingPlugins.DataValidators.RemoveAt(0);
            var main = serviceProvider.GetRequiredService<MainWindow>();
            desktop.MainWindow = main;
        }

        base.OnFrameworkInitializationCompleted();
    }
}