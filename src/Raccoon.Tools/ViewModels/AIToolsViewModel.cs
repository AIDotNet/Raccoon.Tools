﻿using System.Collections.ObjectModel;

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
                Title = "Logo生成",
                Key = MenuKeys.MenuKeyCreateLogo,
                Icon = "home"
            }
        ];
    }
}