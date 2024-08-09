using System.Collections.ObjectModel;

namespace Raccoon.Tools.ViewModels;

public class MenuViewModel : ViewModelBase
{
    public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }

    public MenuViewModel()
    {
        MenuItems = new ObservableCollection<MenuItemViewModel>()
        {
            new()
            {
                MenuHeader = "首页",
                Key = MenuKeys.MenuKeyHome,
                IsSeparator = false,
                Icon = "home"
            },
            new()
            {
                MenuHeader = "AI工具",
                Key = MenuKeys.MenuKeyAI,
                IsSeparator = false,
                Icon = "ai-tool"
            },
            new()
            {
                MenuHeader = "AI Chat",
                Key = MenuKeys.MenuKeyAIChat,
                Icon = "ai-chat"
            },
            new()
            {
                MenuHeader = "登录账号",
                Key = MenuKeys.MenuKeyLogin,
                Icon = "login"
            }
        };
    }
}

public static class MenuKeys
{
    public const string MenuKeyHome = "Home";

    public const string MenuKeyAI = "AI";

    public const string MenuKeyAIChat = "AIChat";

    public const string MenuKeyLogin = "Login";
    
    public const string MenuKeyCreateLogo = "CreateLogo";
}