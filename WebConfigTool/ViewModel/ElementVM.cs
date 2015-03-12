using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebConfigTool.ViewModel
{
    public class ElementVM<TElement> : IConfigElement<TElement>
        where TElement : ConfigurationElement
    {
        protected TElement Element { get; private set; }

        public string Name { get; private set; }

        public string DisplayText { get; private set; }

        public bool LockItem { get; set; }

        public bool IsLocked { get; private set; }

        public bool IsPresent { get; private set; }

        public int LineNumber { get; private set; }

        public string Source { get; private set; }

        public System.Collections.ObjectModel.ObservableCollection<ElementErrorVM> Errors { get; private set; }

        public System.Collections.ObjectModel.ObservableCollection<IConfigProperty> Properties { get; private set; }

        public ElementVM() { }

        public ElementVM(TElement element)
        {
            this.Element = element;
        }

        public bool IsReadOnly()
        {
            throw new NotImplementedException();
        }

        public ConfigurationElement GetConfigurationElement()
        {
            throw new NotImplementedException();
        }

        public Type GetElementType()
        {
            throw new NotImplementedException();
        }
    }

    public class ElementVM : ElementVM<ConfigurationElement>
    {
        public ElementVM() : base() { }

        public ElementVM(ConfigurationElement element) : base(element) { }
    }
}
