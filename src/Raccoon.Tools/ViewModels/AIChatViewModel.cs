using System.Collections.ObjectModel;
using Raccoon.Tools.Dto;
using Raccoon.Tools.ViewModels.AIDto;

namespace Raccoon.Tools.ViewModels;

public class AIChatViewModel : ViewModelBase
{
    private ObservableCollection<AIChatLogViewModel> _chatLogs = new();

    public ObservableCollection<AIChatLogViewModel> ChatLogs
    {
        get => _chatLogs;
        set => SetProperty(ref _chatLogs, value);
    }

    private bool _isToolbarVisible;

    public bool IsToolbarVisible
    {
        get => _isToolbarVisible;
        set => SetProperty(ref _isToolbarVisible, value);
    }

    private ObservableCollection<ChatModelsDto> _models = new();

    public ObservableCollection<ChatModelsDto> ModelDto
    {
        get => _models;
        set => SetProperty(ref _models, value);
    }

    private ChatModelsDto _selectedChatModels;

    public ChatModelsDto SelectedChatModels
    {
        get => _selectedChatModels;
        set => SetProperty(ref _selectedChatModels, value);
    }

    public AIChatViewModel()
    {
        var models = RaccoonContext.GetService<ModelsDto>();

        foreach (var chatModelsDto in models.ChatModels.Where(x => x.Enabled))
        {
            _models.Add(chatModelsDto);
        }

        SelectedChatModels = _models.Where(x => x.Id == models.CheckModel).FirstOrDefault();
    }
}