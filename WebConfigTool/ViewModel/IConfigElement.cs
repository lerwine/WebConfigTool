using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;

namespace WebConfigTool.ViewModel
{
    public interface IConfigElement : IConfigListItem
    {
        bool LockItem { get; set; }
        bool IsLocked { get; }
        bool IsPresent { get; }
        int LineNumber { get; }
        string Source { get; }
        bool IsReadOnly();
        ObservableCollection<ElementErrorVM> Errors { get; }
        ConfigurationElement GetConfigurationElement();
        ObservableCollection<IConfigProperty> Properties { get; }
        Type GetElementType();
    }

    public interface IConfigElement<TElement> : IConfigElement
        where TElement : ConfigurationElement
    {
    }
}
