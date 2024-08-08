using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Raccoon.Tools.ViewModels;

public class MenuItemViewModel : ViewModelBase
{
    public ICommand ActivateCommand { get; set; }

    public string? MenuHeader { get; set; }

    public string? Key { get; set; }

    public string Icon { get; set; }
    
    public bool IsSeparator { get; set; }

    public MenuItemViewModel()
    {
        ActivateCommand = new RelayCommand(OnActivate);
    }

    private void OnActivate()
    {
        if (IsSeparator || Key is null) return;
        WeakReferenceMessenger.Default.Send(Key);
    }
}