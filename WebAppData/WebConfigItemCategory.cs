using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WebAppData
{
    public class WebConfigItemCategory<TItem> : GenericAccessObservableCollection<TItem, IWebConfigItem>, IWebConfigItemCategory<TItem>
        where TItem : IWebConfigItem
    {
        string _name;

        public string Name { get { return this._name; } }

        public WebConfigItemCategory() : base() { }

        public WebConfigItemCategory(List<TItem> list) : base(list) { this.OnInitializeItemCollection(list); }

        public WebConfigItemCategory(IEnumerable<TItem> collection) : base(collection) { }

        public WebConfigItemCategory(string name) : base() { this.InitializeName(name); }

        public WebConfigItemCategory(string name, List<TItem> list) : base(list) { this.InitializeName(name); }

        public WebConfigItemCategory(string name, IEnumerable<TItem> collection) : base(collection) { this.InitializeName(name); }

        protected virtual void InitializeName(string name)
        {
            this._name = (String.IsNullOrWhiteSpace(name)) ? this.GetDefaultName() : name;
        }

        protected virtual string GetDefaultName()
        {
            return typeof(TItem).Name;
        }
    }
}
