using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebAppData
{
    public class WebConfigVM : DependencyObject, IWebConfigItem
    {
        private WebConfigItemCategory<IWebConfigSectionGroup> _allSectionGroups = null;
        private WebConfigItemCategory<IWebConfigSectionGroup> _otherSectionGroups = null;
        private WebConfigItemCategory<IWebConfigSection> _allSections = null;
        private ObservableCollection<IWebConfigItemCategory> _categories = new ObservableCollection<IWebConfigItemCategory>();

        public WebConfigVM() : this(null) { }

        public WebConfigVM(Configuration configuration)
        {
            this.Load(configuration);
        }

        public void Load(Configuration configuration)
        {
            throw new NotImplementedException();
        }

        private void OnLoad(Configuration configuration, Collection<IWebConfigSectionGroup> sectionGroups, Collection<IWebConfigSection> sections)
        {
            throw new NotImplementedException();
        }

        public Guid Id
        {
            get { throw new NotImplementedException(); }
        }

        public string DisplayText
        {
            get { throw new NotImplementedException(); }
        }
    }
}
