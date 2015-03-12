using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WebAppData
{
    public interface IWebConfigItemCategory : IGenericAccessObservableCollection<IWebConfigItem>
    {
        string Name { get; }
    }

    public interface IWebConfigItemCategory<TItem> : IWebConfigItemCategory, IGenericAccessObservableCollection<TItem, IWebConfigItem>
        where TItem : IWebConfigItem
    {
    }
}
