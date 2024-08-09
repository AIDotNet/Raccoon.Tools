using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Raccoon.Tools.DataAccess;
using Raccoon.Tools.ViewModels;
using Raccoon.Tools.Views;
using Serilog;

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

        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Warning()
#else
            .MinimumLevel.Warning()
#endif
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
            .CreateLogger();

        builder.AddSingleton<MainWindow>(provider => new MainWindow()
        {
            DataContext = new MainWindowViewModel()
        });

        builder.Service.AddHttpClient();

        builder.Service.AddDbContext<RaccoonDbContext>(options =>
        {
            options.UseSqlite("Data Source=raccoon.db")
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .LogTo(Log.Logger.Information);
        });

        builder.Service.AddMapster();

        var serviceProvider = RaccoonContext.Build(builder);

        // 迁移数据库
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<RaccoonDbContext>();
            context.Database.EnsureCreated();
        }


        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            BindingPlugins.DataValidators.RemoveAt(0);
            var main = serviceProvider.GetRequiredService<MainWindow>();
            desktop.MainWindow = main;
        }

        base.OnFrameworkInitializationCompleted();
    }
}