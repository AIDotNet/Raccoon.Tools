using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Raccoon.Tools.DataAccess;
using Raccoon.Tools.Services;
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


        builder.AddSingleton<MainWindow>(provider => new MainWindow()
        {
            DataContext = new MainWindowViewModel()
        });
        builder.AddSingleton<Login>(provider => new Login()
        {
            DataContext = new LoginViewModel()
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

            var token = TokenService.GetToken();
            if (string.IsNullOrEmpty(token))
            {
                var login = serviceProvider.GetRequiredService<Login>();
                login.Show();
                desktop.MainWindow = login;
                login.OnSuccess = () =>
                {
                    desktop.MainWindow = serviceProvider.GetRequiredService<MainWindow>();
                    desktop.MainWindow.Show();
                    
                    // 隐藏
                    login.Hide();
                    login.Close();
                };
                return;
            }
            else
            {
                var main = serviceProvider.GetRequiredService<MainWindow>();
                desktop.MainWindow = main;
            }
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void Exit_Click(object? sender, EventArgs e)
    {
        Environment.Exit(0);
    }

    private void Open_Click(object? sender, EventArgs e)
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow?.Show();
        }
    }
}