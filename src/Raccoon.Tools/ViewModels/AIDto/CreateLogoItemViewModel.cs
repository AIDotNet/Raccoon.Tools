using Avalonia.Media.Imaging;

namespace Raccoon.Tools.ViewModels.AIDto;

public class CreateLogoItemViewModel : ViewModelBase
{
    private string? _prompt;

    public string? Prompt
    {
        get => _prompt;
        set => base.SetProperty(ref _prompt, value);
    }

    private string? _uri;

    public string? Uri
    {
        get => _uri;
        set => base.SetProperty(ref _uri, value);
    }

    private DateTime _createdTime;

    public DateTime CreatedTime
    {
        get => _createdTime;
        set => base.SetProperty(ref _createdTime, value);
    }

    private DateTime _lastModifiedTime;

    public DateTime LastModifiedTime
    {
        get => _lastModifiedTime;
        set => base.SetProperty(ref _lastModifiedTime, value);
    }

    private Bitmap _image;

    public Bitmap Image
    {
        get => _image;
        set => base.SetProperty(ref _image, value);
    }

    private bool _isBusy = false;

    public bool IsBusy
    {
        get => _isBusy;
        set => base.SetProperty(ref _isBusy, value);
    }

    public async Task LoadImageAsync()
    {
        if (Uri is null)
        {
            return;
        }

        var response = await RaccoonContext.GetHttpClient().GetAsync(Uri);
        if (response.IsSuccessStatusCode)
        {
            var stream = await response.Content.ReadAsStreamAsync();
            Image = new Bitmap(stream);
        }

        IsBusy = true;
    }
}