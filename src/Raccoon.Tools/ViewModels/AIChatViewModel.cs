using System.Collections.ObjectModel;
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
}