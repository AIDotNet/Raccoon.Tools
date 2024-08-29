using Avalonia.Controls;
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
                Width = 850;
                Height = 580;
                break;
            case <= 2560 when height <= 1440:
                Width = 1280;
                Height = 720;
                break;
            case <= 3840 when height <= 2160:
                Width = 1350;
                Height = 900;
                break;
            default:
                Width = 1580;
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