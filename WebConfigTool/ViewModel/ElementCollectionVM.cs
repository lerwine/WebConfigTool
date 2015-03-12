using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebConfigTool.ViewModel
{
    public class ElementCollectionVM<TElement, TItem> : ElementVM<TElement>, IConfigElementCollection<TElement>
        where TElement : ConfigurationElementCollection
        where TItem : IConfigElement<TElement>
    {
        public int Count()
        {
            throw new NotImplementedException();
        }

        public IConfigElement Get(int index)
        {
            throw new NotImplementedException();
        }
    }

    public class ElementCollectionVM<TElement> : ElementCollectionVM<IConfigElement<TElement>, TElement>
        where TElement : ConfigurationElementCollection
    {
    }

    public class ElementCollectionVM : ElementCollectionVM<ConfigurationElementCollection>
    {
    }
}
