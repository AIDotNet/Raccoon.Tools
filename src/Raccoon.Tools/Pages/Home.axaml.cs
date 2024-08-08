using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Raccoon.Tools.Pages;

public partial class Home : UserControl
{
    public Home()
    {
        InitializeComponent();
    }

    private void OpenGitHub_OnClick(object? sender, RoutedEventArgs e)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "https://github.com/AIDotNet",
            UseShellExecute = true
        };
        Process.Start(psi);
    }
}