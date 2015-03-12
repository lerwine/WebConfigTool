using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;

namespace WebConfigTool.ViewModel
{
    public interface ISectionGroup : IConfigListItem
    {
        ObservableCollection<IItemCategory> Items { get; }
    }

    public interface ISectionGroup<TSectionGroup> : ISectionGroup
        where TSectionGroup : ConfigurationSectionGroup
    {
    }
}
