using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Raccoon.Tools.DataAccess;
using Raccoon.Tools.Dto;
using Raccoon.Tools.Entities;
using Raccoon.Tools.ViewModels;

namespace Raccoon.Tools.Pages;

public partial class Files : UserControl
{
    public Files()
    {
        InitializeComponent();
    }

    private FilesViewModel ViewModel => (FilesViewModel)DataContext;

    private async void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        // 打开文件选择器
        var file = TopLevel.GetTopLevel(this).StorageProvider;
        var picker = await file.OpenFilePickerAsync(new FilePickerOpenOptions()
        {
            AllowMultiple = true,
            Title = "选择文件",
        });

        await using var scope = RaccoonContext.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetService<RaccoonDbContext>();

        // 选择文件后的操作
        if (picker != null)
        {
            foreach (var item in picker)
            {
                var fileInfo = new FileInfo(item.Path.AbsolutePath);

                var fileEntity = RaccoonContext.LiteDatabase();

                var entity = await dbContext.Files.AddAsync(new FileStorage()
                {
                    CreateAt = DateTime.Now,
                    Name = fileInfo.Name,
                    Path = string.Empty,
                    UpdatedAt = DateTime.Now,
                    SessionId = string.Empty,
                    Size = fileInfo.Length,
                    State = FileState.None,
                    FileType = "file",
                });

                await using var stream = fileInfo.OpenRead();

                fileEntity.FileStorage.Upload(entity.Entity.Id.ToString(), entity.Entity.Id + fileInfo.Name, stream);

                ViewModel.Files.Add(new FilesDto()
                {
                    CreateAt = DateTime.Now,
                    Name = fileInfo.Name,
                    Path = fileInfo.FullName,
                    Size = fileInfo.Length,
                    State = FileState.None,
                    FileType = "file",
                    Id = entity.Entity.Id,
                });
            }

            await dbContext.SaveChangesAsync();
        }
    }

    private async void Delete_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is long id)
        {
            await using var scope = RaccoonContext.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetService<RaccoonDbContext>();

            var fileEntity = RaccoonContext.LiteDatabase();

            fileEntity.FileStorage.Delete(id.ToString());

            await dbContext.Files.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
    }

    private async void Quantize_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: long id })
        {
            await using var scope = RaccoonContext.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetService<RaccoonDbContext>();

            await dbContext.Files.Where(x => x.Id == id)
                .ExecuteUpdateAsync(x => x.SetProperty(i => i.State, FileState.Success));
        }
    }
}