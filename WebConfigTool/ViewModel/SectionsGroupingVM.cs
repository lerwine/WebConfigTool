using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WebConfigTool.ViewModel
{
    public class SectionsGroupingVM : IItemCategory<IConfigSection>
    {
        private ObservableCollection<IConfigSection> _items = new ObservableCollection<IConfigSection>();

        public string Name { get { return "Sections"; } }

        public ObservableCollection<IConfigSection> Items { get { return this._items; } }
    }
}
