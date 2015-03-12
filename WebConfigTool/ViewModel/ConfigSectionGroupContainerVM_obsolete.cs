using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Windows;

namespace WebConfigTool.ViewModel
{
    public class ConfigSectionGroupContainerVM_obsolete : DependencyObject
    {
        private ErrorMessageVM _error = new ErrorMessageVM();
        private ObservableCollection<ConfigurationSectionGroupVM_obsolete> _otherConfigGroups = new ObservableCollection<ConfigurationSectionGroupVM_obsolete>();
        private ObservableCollection<ConfigurationSectionVM_obsolete> _otherConfigSections = new ObservableCollection<ConfigurationSectionVM_obsolete>();

        public ErrorMessageVM Error { get { return this._error; } }

        public ObservableCollection<ConfigurationSectionGroupVM_obsolete> OtherConfigGroups { get { return this._otherConfigGroups; } }

        public ObservableCollection<ConfigurationSectionVM_obsolete> OtherConfigSections { get { return this._otherConfigSections; } }

        internal void LoadSectionGroups(ConfigurationSectionGroupCollection configurationSectionGroupCollection)
        {
            this._otherConfigGroups.Clear();

            if (configurationSectionGroupCollection == null)
                return;

            foreach (ConfigurationSectionGroup grp in configurationSectionGroupCollection.OfType<ConfigurationSectionGroup>().OrderBy(g => g.Name))
            {
                ConfigurationSectionGroupVM_obsolete vm = new ConfigurationSectionGroupVM_obsolete();
                vm.SetSectionGroup(grp);
                this._otherConfigGroups.Add(vm);
            }
        }

        internal void LoadSections(ConfigurationSectionCollection configurationSectionCollection)
        {
            this._otherConfigSections.Clear();

            if (configurationSectionCollection == null)
                return;

            foreach (ConfigurationSection section in configurationSectionCollection.OfType<ConfigurationSection>().OrderBy(s => s.SectionInformation.Name))
            {
                ConfigurationSectionVM_obsolete vm = new ConfigurationSectionVM_obsolete();
                vm.SetSection(section);
                this._otherConfigSections.Add(vm);
            }
        }
    }
}
