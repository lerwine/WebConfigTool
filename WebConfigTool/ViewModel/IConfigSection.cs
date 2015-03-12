using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace WebConfigTool.ViewModel
{
    public interface IConfigSection : IConfigElement
    {
    }

    public interface IConfigSection<TSection> : IConfigSection, IConfigElement<TSection>
        where TSection : ConfigurationSection
    {
    }
}
