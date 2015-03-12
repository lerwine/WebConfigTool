using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WebConfigTool.ViewModel
{
    public class SectionGroupCategoryVM : IItemCategory<ISectionGroup>
    {
        private ObservableCollection<IConfigListItem> _items = new ObservableCollection<IConfigListItem>();

        public string Name { get { return "Section Groups"; } }

        public ObservableCollection<IConfigListItem> Items { get { return this._items; } }
    }
}
