using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Raccoon.Tools.ViewModels;

namespace Raccoon.Tools;

public class ViewLocator : IDataTemplate
{
    private static Dictionary<string,object> _cache = new();
    
    public Control? Build(object? data)
    {
        if (data is null)
            return null;

        var name = data.GetType().Name.Replace("ViewModel", "");
        var type = Type.GetType("Raccoon.Tools.Pages." + name);

        if (type != null)
        {
            if(_cache.TryGetValue(name, out var value))
                return (Control)value;
            
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