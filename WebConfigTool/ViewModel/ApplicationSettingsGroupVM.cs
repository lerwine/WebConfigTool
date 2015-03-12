using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace WebConfigTool.ViewModel
{
    public class ApplicationSettingsGroupVM : SectionGroupVM<ApplicationSettingsGroup>
    {
        public ApplicationSettingsGroupVM() : base() { }
        public ApplicationSettingsGroupVM(ApplicationSettingsGroup applicationSettingsGroup) : base(applicationSettingsGroup) { }

        internal void Load(ApplicationSettingsGroup applicationSettingsGroup)
        {
            throw new NotImplementedException();
        }
    }
}
