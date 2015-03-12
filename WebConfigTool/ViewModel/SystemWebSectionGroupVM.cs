using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration;

namespace WebConfigTool.ViewModel
{
    public class SystemWebSectionGroupVM : SectionGroupVM<SystemWebSectionGroup>
    {
        public SystemWebSectionGroupVM() : base() { }
        public SystemWebSectionGroupVM(SystemWebSectionGroup settingsGroup) : base(settingsGroup) { }

        internal void Load(SystemWebSectionGroup systemWebSectionGroup)
        {
            throw new NotImplementedException();
        }
    }
}
