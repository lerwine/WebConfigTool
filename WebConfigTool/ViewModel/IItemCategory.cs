using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WebConfigTool.ViewModel
{
    public interface IItemCategory
    {
        string Name { get; }
        ObservableCollection<IConfigListItem> Items { get; }
    }

    public interface IItemCategory<TItem> : IItemCategory
        where TItem : IConfigListItem
    {
    }
}
