using System.Collections.ObjectModel;
using Raccoon.Tools.ViewModels.AIDto;

namespace Raccoon.Tools.ViewModels;

public class CreateLogoViewModel : ViewModelBase
{
    public CreateLogoViewModel()
    {
    }

    private ObservableCollection<CreateLogoItemViewModel> _createLogoItems = new();

    public ObservableCollection<CreateLogoItemViewModel> CreateLogoItems
    {
        get => _createLogoItems;
        set => SetProperty(ref _createLogoItems, value);
    }
    
    private string _prompt;
    
    public string Prompt
    {
        get => _prompt;
        set => SetProperty(ref _prompt, value);
    }
    
    
}