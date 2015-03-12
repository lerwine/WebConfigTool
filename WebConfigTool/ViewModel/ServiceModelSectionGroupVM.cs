using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;

namespace WebConfigTool.ViewModel
{
    public class ServiceModelSectionGroupVM : SectionGroupVM<ServiceModelSectionGroup>
    {
        public ServiceModelSectionGroupVM() : base() { }
        public ServiceModelSectionGroupVM(ServiceModelSectionGroup settingsGroup) : base(settingsGroup) { }

        internal void Load(ServiceModelSectionGroup serviceModelSectionGroup)
        {
            throw new NotImplementedException();
        }
    }
}
