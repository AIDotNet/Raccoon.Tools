using System.Collections.ObjectModel;
using Raccoon.Tools.Dto;

namespace Raccoon.Tools.ViewModels;

public class AIToolsViewModel : ViewModelBase
{
    private ObservableCollection<AIToolsItemsViewModel> _aiToolsItems = new();

    public ObservableCollection<AIToolsItemsViewModel> AIToolsItems
    {
        get => _aiToolsItems;
        set => SetProperty(ref _aiToolsItems, value);
    }

    public AIToolsViewModel()
    {
        _aiToolsItems =
        [
            new AIToolsItemsViewModel
            {
                Title = "AI生成Logo",
                Key = MenuKeys.MenuKeyCreateLogo,
            }
        ];
    }
}