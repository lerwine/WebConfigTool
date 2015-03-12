using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;

namespace WebConfigTool.ViewModel
{
    public class NetSectionGroupVM : SectionGroupVM<NetSectionGroup>
    {
        public NetSectionGroupVM() : base() { }
        public NetSectionGroupVM(NetSectionGroup settingsGroup) : base(settingsGroup) { }

        internal void Load(NetSectionGroup netSectionGroup)
        {
            throw new NotImplementedException();
        }
    }
}
