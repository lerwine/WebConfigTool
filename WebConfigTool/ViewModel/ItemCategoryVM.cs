using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace WebConfigTool.ViewModel
{
    public class ItemCategoryVM<TItem> : DependencyObject, IItemCategory<TItem>
        where TItem : IConfigListItem
    {
        private string _name;
        private ObservableCollection<TItem> _items = new ObservableCollection<TItem>();

        public string Name { get { return this._name; } }

        public ObservableCollection<TItem> Items { get { return this._items; } }

        public ItemCategoryVM(string name)
        {
            this._name = name;
        }
    }
}
