using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.Messaging;
using FluentAvalonia.UI.Controls;
using Raccoon.Tools.ViewModels;

namespace Raccoon.Tools.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        AdjustWindowSize();
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);
    }

    /// <summary>
    /// Avalonia根据当前屏幕的 高宽，调整窗口大小
    /// </summary>
    private void AdjustWindowSize()
    {
        var screen = Screens.Primary;
        if (screen == null)
        {
            return;
        }

        var width = screen.WorkingArea.Width;
        var height = screen.WorkingArea.Height;

        switch (width)
        {
            // 根据屏幕大小设置指定的窗口大小
            case <= 1920 when height <= 1080:
                Width = 800;
                Height = 580;
                break;
            case <= 2560 when height <= 1440:
                Width = 1180;
                Height = 720;
                break;
            case <= 3840 when height <= 2160:
                Width = 1300;
                Height = 900;
                break;
            default:
                Width = 1520;
                Height = 1080;
                break;
        }
    }

    private MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

    private void NvSample_OnSelectionChanged(object? sender, NavigationViewSelectionChangedEventArgs e)
    {
        if (e.SelectedItem is NavigationViewItem item)
        {
            ViewModel.OnNavigation(ViewModel, item.Tag.ToString());
        }
    }
}