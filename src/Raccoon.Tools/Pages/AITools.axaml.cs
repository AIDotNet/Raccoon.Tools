using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Input;

namespace Raccoon.Tools.Pages;

public partial class AITools : UserControl
{
    public AITools()
    {
        InitializeComponent();
    }

    private void InputElement_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (sender is WrapPanel { Tag: ICommand command })
        {
            command.Execute(null);
        }
        
        
    }
}