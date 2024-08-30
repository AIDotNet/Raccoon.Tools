using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Threading;
using Microsoft.SemanticKernel.ChatCompletion;
using Raccoon.Tools.Dto;
using Raccoon.Tools.ViewModels;
using Raccoon.Tools.ViewModels.AIDto;
using Serilog;

namespace Raccoon.Tools.Pages;

public partial class AIChat : UserControl
{
    private WindowNotificationManager? _manager;
    private AIChatViewModel ViewModel => DataContext as AIChatViewModel ?? throw new NullReferenceException();

    public AIChat()
    {
        InitializeComponent();
    }

    private async void ChatSend_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(Input.Text))
            {
                return;
            }

            var aiChatLogView = new AIChatLogViewModel()
            {
                Role = "assistant",
                Content = "",
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                FromModel = ViewModel.SelectedChatModels.Id,
            };

            ViewModel.ChatLogs.Add(new AIChatLogViewModel()
            {
                Role = "user",
                Content = Input.Text,
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                FromModel = ViewModel.SelectedChatModels.Id,
            });

            Input.Text = string.Empty;

            AiChatScrollViewer.ScrollToEnd();

            var kernel = KernelFactory.Create(ViewModel.SelectedChatModels.Id);

            var chat = kernel.GetRequiredService<IChatCompletionService>();

            var history = new ChatHistory();
            foreach (var chatLog in ViewModel.ChatLogs)
            {
                history.AddMessage(new AuthorRole(chatLog.Role), chatLog.Content);
            }

            ViewModel.ChatLogs.Add(aiChatLogView);

            await foreach (var item in chat.GetStreamingChatMessageContentsAsync(history))
            {
                await Dispatcher.UIThread.InvokeAsync((() =>
                {
                    aiChatLogView.Content += item.Content;
                    AiChatScrollViewer.ScrollToEnd();
                }));
            }
        }
        catch (Exception exception)
        {
            _manager?.Show(new Notification("错误", exception.Message, NotificationType.Error));
            Log.Logger.Error(exception, "ChatSend_Click");
        }
    }

    private void ChatInput_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter && !e.KeyModifiers.HasFlag(KeyModifiers.Shift))
        {
            ChatSend_Click(sender, e);
        }

        Console.WriteLine(e.Key + " " + e.KeyModifiers + " " + e.KeyModifiers.HasFlag(KeyModifiers.Shift));
    }

    private void SelectingItemsControl_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is ListBox { SelectedItem: ChatModelsDto chatModelsDto })
        {
            ViewModel.SelectedChatModels = ViewModel.ModelDto.Where(x => x.Id == chatModelsDto.Id).FirstOrDefault();
        }
    }
}