using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.TextToImage;
using Raccoon.Tools.ViewModels;
using Raccoon.Tools.ViewModels.AIDto;
using Raccoon.Tools.Views;

#pragma warning disable SKEXP0001

namespace Raccoon.Tools.Pages;

public partial class CreateLogo : UserControl
{
    public CreateLogo()
    {
        InitializeComponent();
    }

    private CreateLogoViewModel ViewModel => DataContext as CreateLogoViewModel;

    private async void GenerateLogo_OnClick(object? sender, RoutedEventArgs e)
    {
        var value = new CreateLogoItemViewModel()
        {
            CreatedTime = DateTime.Now,
            LastModifiedTime = DateTime.Now,
            Prompt = "",
            Uri = "",
        };

        ViewModel?.CreateLogoItems.Add(value);

        var kernel = KernelFactory.Create();

        var plugin = kernel.Plugins["Tools"];

        value.Prompt = string.Empty;

        await foreach (var item in kernel.InvokeStreamingAsync(plugin["Logo"], new KernelArguments()
                       {
                           ["input"] = ViewModel.Prompt
                       }))
        {
            await Dispatcher.UIThread.InvokeAsync((() => { value.Prompt += item.ToString(); }));
        }

        var image = kernel.GetRequiredService<ITextToImageService>();

        var result = await image.GenerateImageAsync(value.Prompt, 1024, 1024);
        value.Uri = result;

        await value.LoadImageAsync();
    }

    private async void SaveItem_OnClick(object? sender, RoutedEventArgs e)
    {
        // 选择目录保存
        var dialog = new SaveFileDialog();
        dialog.Title = "Save Image";
        dialog.Filters.Add(new FileDialogFilter() { Name = "Image", Extensions = { "png" } });

        var main = RaccoonContext.GetService<MainWindow>();
        var result = await dialog.ShowAsync(main);

        if (result is not null && sender is MenuItem item && item.Tag is Bitmap bitmap)
        {
            bitmap.Save(result);
        }
    }

    private void DeleteItem_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
}