using System.Collections.ObjectModel;
using Raccoon.Tools.Dto;

namespace Raccoon.Tools.ViewModels;

public class FilesViewModel : ViewModelBase
{
    private ObservableCollection<FilesDto> _files = new();

    public ObservableCollection<FilesDto> Files
    {
        get => _files;
        set => SetProperty(ref _files, value);
    }
}