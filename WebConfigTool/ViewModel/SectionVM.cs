using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebConfigTool.ViewModel
{
    public class SectionVM<TSection> : ElementVM<TSection>, IConfigSection<TSection>
        where TSection : ConfigurationSection
    {
        public SectionVM() : base() { }

        public SectionVM(TSection configurationSection) : base(configurationSection) { }
    }

    public class SectionVM : SectionVM<ConfigurationSection>
    {
        public SectionVM() : base() { }

        public SectionVM(ConfigurationSection configurationSection) : base(configurationSection) { }
    }
}
