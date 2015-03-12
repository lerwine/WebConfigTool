using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAppData
{
    public interface IWebConfigItem
    {
        Guid Id { get; }
        string DisplayText { get; }
    }
}
