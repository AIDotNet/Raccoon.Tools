using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using LiteDB;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Raccoon.Tools.DataAccess;
using Raccoon.Tools.Dto;
using Raccoon.Tools.Services;
using Raccoon.Tools.ViewModels;
using Raccoon.Tools.Views;
using Serilog;
using JsonSerializer = System.Text.Json.JsonSerializer;

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

        builder.Service.AddSingleton<ILiteDatabase>((provider => new LiteDatabase("raccoon-file.db")));

        builder.Service.AddHttpClient();

        builder.Service.AddDbContext<RaccoonDbContext>(options =>
        {
            options.UseSqlite("Data Source=raccoon.db")
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .LogTo(Log.Logger.Information);
        });
        builder.Service.AddSingleton<ModelsDto>(option =>
        {
            // 获取avaloniaResource内的资源
            using var json = AssetLoader.Open(new Uri("avares://Raccoon.Tools/Assets/models.json"));
            var reader = new StreamReader(json);
            var jsonStr = reader.ReadToEnd();
            var models = JsonSerializer.Deserialize<ModelsDto>(jsonStr, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
            return models;
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