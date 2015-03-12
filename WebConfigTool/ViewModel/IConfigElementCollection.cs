using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebConfigTool.ViewModel
{
    public interface IConfigElementCollection : IConfigElement
    {
        int Count();
        IConfigElement Get(int index);
    }

    public interface IConfigElementCollection<TElement> : IConfigElementCollection, IConfigElement<TElement>
        where TElement : ConfigurationElementCollection
    {
    }

    public interface IConfigElementCollection<TElement, TItem> : IConfigElementCollection<TElement>
        where TElement : ConfigurationElementCollection
        where TItem : IConfigElement<TElement>
    {
    }
}
