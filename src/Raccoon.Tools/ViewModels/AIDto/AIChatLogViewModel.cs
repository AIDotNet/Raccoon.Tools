namespace Raccoon.Tools.ViewModels.AIDto;

public sealed class AIChatLogViewModel : ViewModelBase
{
    public long Id { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    private string _content;

    public string Content
    {
        get => _content;
        set => SetProperty(ref _content, value);
    }

    public string Role { get; set; }

    public string UserName { get; set; }

    public string FromModel { get; set; }

    public string ParentId { get; set; }

    public Dictionary<string, string> Extend { get; set; }

    public AIChatLogViewModel()
    {
        Extend = new Dictionary<string, string>();
    }
}