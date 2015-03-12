using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebConfigTool.ViewModel
{
    public interface IPropertyDerrivedElement : IConfigProperty
    {
    }

    public interface IPropertyDerrivedElement<TElement> : IPropertyDerrivedElement, IConfigProperty<TElement>
        where TElement : IConfigElement
    {
    }
}
