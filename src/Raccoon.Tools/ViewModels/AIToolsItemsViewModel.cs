using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Raccoon.Tools.ViewModels;

public class AIToolsItemsViewModel
{
    public string Title { get; set; }

    public string Key { get; set; }

    public string Icon { get; set; }

    public object ViewModel { get; set; }
    
    
    public ICommand ActivateCommand { get; set; }
    
    public AIToolsItemsViewModel()
    {
        ActivateCommand = new RelayCommand(OnActivate);
    }

    private void OnActivate()
    {
        WeakReferenceMessenger.Default.Send(Key);
    }
}