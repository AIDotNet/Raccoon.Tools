using CommunityToolkit.Mvvm.Messaging;

namespace Raccoon.Tools.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        WeakReferenceMessenger.Default.Register<MainWindowViewModel, string>(this, OnNavigation);

        OnNavigation(this, MenuKeys.MenuKeyHome);
    }

    private string _windowTitle = "浣熊工具";

    public string WindowTitle
    {
        get => _windowTitle;
        set => SetProperty(ref _windowTitle, value);
    }

    private object? _content;

    public object? Content
    {
        get => _content;
        set => SetProperty(ref _content, value);
    }

    public MenuViewModel Menus { get; set; } = new();


    private void OnNavigation(MainWindowViewModel vm, string s)
    {
        Content = s switch
        {
            MenuKeys.MenuKeyHome => new HomeViewModel(),
            MenuKeys.MenuKeyAI => new AIToolsViewModel(),
            MenuKeys.MenuKeyAIChat => new AIChatViewModel(),
            MenuKeys.MenuKeyLogin => new LoginViewModel(),
            MenuKeys.MenuKeyCreateLogo => new CreateLogoViewModel(),
            _ => throw new ArgumentOutOfRangeException(nameof(s), s, null)
        };
    }
}