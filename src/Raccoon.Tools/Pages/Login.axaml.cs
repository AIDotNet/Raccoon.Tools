﻿using System.Diagnostics;
using System.Net.Http.Json;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Raccoon.Tools.Dto;
using Raccoon.Tools.Services;
using Raccoon.Tools.Views;

namespace Raccoon.Tools.Pages;

public partial class Login : UserControl
{
    private WindowNotificationManager? _manager;

    public Login()
    {
        InitializeComponent();
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        var main = RaccoonContext.GetService<MainWindow>();
        _manager = new WindowNotificationManager(main)
        {
            MaxItems = 3
        };
    }

    private async void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var client = RaccoonContext.GetHttpClient();

        var responseMessage = await client.PostAsJsonAsync("https://api.token-ai.cn/api/v1/authorize/token", new
        {
            account = UserName.Text,
            pass = Password.Text
        });

        var result = await responseMessage.Content.ReadFromJsonAsync<ResultDto<TokenDto>>();

        if (result.Success)
        {
            _manager?.Show(new Notification("成功", "Token已更新", NotificationType.Success));
            TokenService.SaveToken(result.Data.Token);   
        }
        else
        {
            _manager?.Show(new Notification("错误", result.Message, NotificationType.Error));
        }
    }

    private void RegisterButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "https://api.token-ai.cn/register",
            UseShellExecute = true
        };
        Process.Start(psi);
    }
}