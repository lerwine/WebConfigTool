using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebConfigTool.ViewModel
{
    public interface IConfigListItem
    {
        string Name { get; }
        string DisplayText { get; }
    }
}
