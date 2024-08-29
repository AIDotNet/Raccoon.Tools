using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Microsoft.SemanticKernel.ChatCompletion;
using Raccoon.Tools.ViewModels;
using Raccoon.Tools.ViewModels.AIDto;

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
            var aiChatLogView = new AIChatLogViewModel()
            {
                Role = "assistant",
                Content = "",
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                FromModel = "gpt-4o",
            };

            ViewModel.ChatLogs.Add(new AIChatLogViewModel()
            {
                Role = "user",
                Content = Input.Text,
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                FromModel = "gpt-4o",
            });

            Input.Text = string.Empty;

            AiChatScrollViewer.ScrollToEnd();

            var kernel = KernelFactory.Create();

            var chat = kernel.GetRequiredService<IChatCompletionService>();

            var history = new ChatHistory();
            foreach (var chatLog in ViewModel.ChatLogs)
            {
                history.AddMessage(new AuthorRole(chatLog.Role), chatLog.Content);
            }

            ViewModel.ChatLogs.Add(aiChatLogView);

            await foreach (var item in chat.GetStreamingChatMessageContentsAsync(history))
            {
                Dispatcher.UIThread.InvokeAsync((() =>
                {
                    aiChatLogView.Content += item.Content;
                    AiChatScrollViewer.ScrollToEnd();
                }));
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }
}