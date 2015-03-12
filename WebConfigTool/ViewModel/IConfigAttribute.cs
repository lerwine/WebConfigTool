using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebConfigTool.ViewModel
{
    public interface IConfigAttribute : IConfigListItem
    {
    }

    public interface IConfigAttribute<TValue> : IConfigAttribute, IConfigListItem
    {
    }
}
