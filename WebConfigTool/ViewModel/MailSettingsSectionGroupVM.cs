using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;

namespace WebConfigTool.ViewModel
{
    public class MailSettingsSectionGroupVM : SectionGroupVM<MailSettingsSectionGroup>
    {
        public MailSettingsSectionGroupVM() : base() { }
        public MailSettingsSectionGroupVM(MailSettingsSectionGroup settingsGroup) : base(settingsGroup) { }

        internal void Load(MailSettingsSectionGroup mailSettingsSectionGroup)
        {
            throw new NotImplementedException();
        }
    }
}
