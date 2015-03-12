using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebConfigTool.ViewModel
{
    public interface IConfigProperty
    {
        string Name { get; set; }
        string DisplayText { get; }
    }

    public interface IConfigProperty<TValue> : IConfigProperty
    {
    }
}
