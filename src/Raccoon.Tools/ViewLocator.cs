using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Raccoon.Tools.ViewModels;

namespace Raccoon.Tools;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? data)
    {
        if (data is null)
            return null;

        var name = data.GetType().Name.Replace("ViewModel", "");
        var type = Type.GetType("Raccoon.Tools.Pages." + name);

        if (type != null)
        {
            var control = (Control)Activator.CreateInstance(type)!;
            control.DataContext = data;
            return control;
        }

        return new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}