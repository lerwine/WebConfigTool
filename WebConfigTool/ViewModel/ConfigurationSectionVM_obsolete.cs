using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;

namespace WebConfigTool.ViewModel
{
    public class ConfigurationSectionVM_obsolete : ConfigurationSectionVM<ConfigurationSection>
    {
        private RelayCommand _openCommand = null;
        private ConfigurationSectionWindow _window = null;
        private object _syncRoot = new object();

        public RelayCommand OpenCommand
        {
            get
            {
                if (this._openCommand == null)
                {
                    this._openCommand = new RelayCommand((object o) =>
                    {
                        ConfigurationSectionWindow window;
                        lock (this._syncRoot)
                        {
                            window = this._window;
                            if (window == null)
                            {
                                this._window = new ConfigurationSectionWindow();
                                this._window.DataContext = this;
                                this._window.Closed += this.ConfigurationSectionWindow_Closed;
                            }
                            this._openCommand.IsEnabled = false;
                            this.WindowIsOpen = true;
                        }

                        if (window == null)
                            this._window.Show();
                        else
                            window.Activate();
                    });
                }

                return this._openCommand;
            }
        }

        public static readonly DependencyProperty WindowIsOpenProperty =
            DependencyProperty.Register("WindowIsOpen", typeof(bool), typeof(ConfigurationSectionVM_obsolete), new PropertyMetadata(false));

        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(ConfigurationSectionVM_obsolete), new PropertyMetadata(""));

        public static readonly DependencyProperty SectionNameProperty =
            DependencyProperty.Register("SectionName", typeof(string), typeof(ConfigurationSectionVM_obsolete), new PropertyMetadata(""));

        public static readonly DependencyProperty SectionTypeProperty =
            DependencyProperty.Register("SectionType", typeof(string), typeof(ConfigurationSectionVM_obsolete), new PropertyMetadata(""));

        public static readonly DependencyProperty RawXmlProperty =
            DependencyProperty.Register("RawXml", typeof(string), typeof(ConfigurationSectionVM_obsolete),
                new PropertyMetadata(""/*, ConfigurationSectionVM.RawXml_PropertyChanged*/));

        public bool WindowIsOpen
        {
            get { return (bool)(this.GetValue(ConfigurationSectionVM_obsolete.WindowIsOpenProperty)); }
            private set { this.SetValue(ConfigurationSectionVM_obsolete.WindowIsOpenProperty, value); }
        }

        public string Name
        {
            get { return (string)(this.GetValue(ConfigurationSectionVM_obsolete.NameProperty)); }
            set { this.SetValue(ConfigurationSectionVM_obsolete.NameProperty, (value == null) ? "" : value); }
        }

        public string SectionName
        {
            get { return (string)(this.GetValue(ConfigurationSectionVM_obsolete.SectionNameProperty)); }
            set { this.SetValue(ConfigurationSectionVM_obsolete.SectionNameProperty, (value == null) ? "" : value); }
        }

        public string SectionType
        {
            get { return (string)(this.GetValue(ConfigurationSectionVM_obsolete.SectionTypeProperty)); }
            set { this.SetValue(ConfigurationSectionVM_obsolete.SectionTypeProperty, (value == null) ? "" : value); }
        }

        public string RawXml
        {
            get { return (string)(this.GetValue(ConfigurationSectionVM_obsolete.RawXmlProperty)); }
            private set { this.SetValue(ConfigurationSectionVM_obsolete.RawXmlProperty, (value == null) ? "" : value); }
        }

        void ConfigurationSectionWindow_Closed(object sender, EventArgs e)
        {
            lock (this._syncRoot)
            {
                this._window = null;
                this._openCommand.IsEnabled = true;
                this.WindowIsOpen = false;
            }
        }

        public void FocusWindow()
        {
            lock (this._syncRoot)
            {
                if (this._window != null)
                    this._window.Activate();
            }
        }

        //private static void RawXml_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    ConfigurationSectionVM vm = d as ConfigurationSectionVM;

        //    if (vm.Section != null)
        //        vm.Section.SectionInformation.SetRawXml(vm.RawXml);
        //}

        protected override void OnSectionChanged()
        {
            this.RawXml = this.Section.SectionInformation.GetRawXml();
            this.Name = this.Section.SectionInformation.Name;
            this.SectionName = this.Section.SectionInformation.SectionName;
            this.SectionType = this.Section.SectionInformation.Type;
            base.OnSectionChanged();
        }
    }

    public class ConfigurationSectionVM<TSection> : DependencyObject
        where TSection : ConfigurationSection
    {
        private TSection _section = null;
        private ObservableCollection<ConfigAttributeVM_obsolete> _attributes = new ObservableCollection<ConfigAttributeVM_obsolete>();
        private ObservableCollection<ConfigElementVM_obsolete> _elements = new ObservableCollection<ConfigElementVM_obsolete>();

        public static readonly DependencyProperty HasSectionProperty =
            DependencyProperty.Register("HasSection", typeof(bool), typeof(ConfigurationSectionVM<TSection>), new PropertyMetadata(false));

        public static readonly DependencyProperty AllowOverrideProperty =
            DependencyProperty.Register("AllowOverride", typeof(bool), typeof(ConfigurationSectionVM<TSection>),
                new PropertyMetadata(false, ConfigurationSectionVM<TSection>.AllowOverride_PropertyChanged));

        public static readonly DependencyProperty IsDeclaredProperty =
            DependencyProperty.Register("IsDeclared", typeof(bool), typeof(ConfigurationSectionVM<TSection>), new PropertyMetadata(false));

        public static readonly DependencyProperty LineNumberProperty =
            DependencyProperty.Register("LineNumber", typeof(int), typeof(ConfigurationSectionVM<TSection>), new PropertyMetadata(0));

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(string), typeof(ConfigurationSectionVM<TSection>), new PropertyMetadata(""));

        public static readonly DependencyProperty ConfigSourceProperty =
            DependencyProperty.Register("ConfigSource", typeof(string), typeof(ConfigurationSectionVM<TSection>), new PropertyMetadata(""));

        public static readonly DependencyProperty IsLockedProperty =
            DependencyProperty.Register("IsLocked", typeof(bool), typeof(ConfigurationSectionVM<TSection>), new PropertyMetadata(false));

        public static readonly DependencyProperty IsProtectedProperty =
            DependencyProperty.Register("IsProtected", typeof(bool), typeof(ConfigurationSectionVM<TSection>), new PropertyMetadata(false));

        public static readonly DependencyProperty ProtectionProviderNameProperty =
            DependencyProperty.Register("ProtectionProviderName", typeof(string), typeof(ConfigurationSectionVM<TSection>), new PropertyMetadata(""));

        public static readonly DependencyProperty ProtectionProviderDescriptionProperty =
            DependencyProperty.Register("ProtectionProviderDescription", typeof(string), typeof(ConfigurationSectionVM<TSection>), new PropertyMetadata(""));

        public static readonly DependencyProperty ShowPropertiesColumnProperty =
            DependencyProperty.Register("ShowPropertiesColumn", typeof(bool), typeof(ConfigurationSectionVM<TSection>), new PropertyMetadata(true));

        public static readonly DependencyProperty ShowElementsColumnProperty =
            DependencyProperty.Register("ShowElementsColumn", typeof(bool), typeof(ConfigurationSectionVM<TSection>), new PropertyMetadata(false));

        protected TSection Section { get { return this._section; } }

        public ObservableCollection<ConfigAttributeVM_obsolete> Properties { get { return this._attributes; } }

        public ObservableCollection<ConfigElementVM_obsolete> Elements { get { return this._elements; } }

        public bool HasSection
        {
            get { return (bool)(this.GetValue(ConfigurationSectionVM<TSection>.HasSectionProperty)); }
            private set { this.SetValue(ConfigurationSectionVM<TSection>.HasSectionProperty, value); }
        }

        public bool AllowOverride
        {
            get { return (bool)(this.GetValue(ConfigurationSectionVM<TSection>.AllowOverrideProperty)); }
            set { this.SetValue(ConfigurationSectionVM<TSection>.AllowOverrideProperty, value); }
        }

        public bool IsDeclared
        {
            get { return (bool)(this.GetValue(ConfigurationSectionVM<TSection>.IsDeclaredProperty)); }
            private set { this.SetValue(ConfigurationSectionVM<TSection>.IsDeclaredProperty, value); }
        }

        public int LineNumber
        {
            get { return (int)(this.GetValue(ConfigurationSectionVM<TSection>.LineNumberProperty)); }
            set { this.SetValue(ConfigurationSectionVM<TSection>.LineNumberProperty, value); }
        }

        public string Source
        {
            get { return (string)(this.GetValue(ConfigurationSectionVM<TSection>.SourceProperty)); }
            set { this.SetValue(ConfigurationSectionVM<TSection>.SourceProperty, (value == null) ? "" : value); }
        }

        public string ConfigSource
        {
            get { return (string)(this.GetValue(ConfigurationSectionVM<TSection>.ConfigSourceProperty)); }
            private set { this.SetValue(ConfigurationSectionVM<TSection>.ConfigSourceProperty, (value == null) ? "" : value); }
        }

        public bool IsLocked
        {
            get { return (bool)(this.GetValue(ConfigurationSectionVM<TSection>.IsLockedProperty)); }
            private set { this.SetValue(ConfigurationSectionVM<TSection>.IsLockedProperty, value); }
        }

        public bool IsProtected
        {
            get { return (bool)(this.GetValue(ConfigurationSectionVM<TSection>.IsProtectedProperty)); }
            private set { this.SetValue(ConfigurationSectionVM<TSection>.IsProtectedProperty, value); }
        }

        public string ProtectionProviderName
        {
            get { return (string)(this.GetValue(ConfigurationSectionVM<TSection>.ProtectionProviderNameProperty)); }
            private set { this.SetValue(ConfigurationSectionVM<TSection>.ProtectionProviderNameProperty, (value == null) ? "" : value); }
        }

        public string ProtectionProviderDescription
        {
            get { return (string)(this.GetValue(ConfigurationSectionVM<TSection>.ProtectionProviderDescriptionProperty)); }
            private set { this.SetValue(ConfigurationSectionVM<TSection>.ProtectionProviderDescriptionProperty, (value == null) ? "" : value); }
        }

        public bool ShowPropertiesColumn
        {
            get { return (bool)(this.GetValue(ConfigurationSectionVM<TSection>.ShowPropertiesColumnProperty)); }
            private set { this.SetValue(ConfigurationSectionVM<TSection>.ShowPropertiesColumnProperty, value); }
        }

        public bool ShowElementsColumn
        {
            get { return (bool)(this.GetValue(ConfigurationSectionVM<TSection>.ShowElementsColumnProperty)); }
            private set { this.SetValue(ConfigurationSectionVM<TSection>.ShowElementsColumnProperty, value); }
        }

        private static void AllowOverride_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ConfigurationSectionVM_obsolete<TSection> vm = d as ConfigurationSectionVM<TSection>;

            if (vm._section != null && vm._section.SectionInformation.AllowOverride != vm.AllowOverride)
                vm._section.SectionInformation.AllowOverride = vm.AllowOverride;
        }

        internal void SetSection(TSection configurationSection)
        {
            if ((this._section == null) ==  (configurationSection == null) && (this._section == null || Object.ReferenceEquals(this._section, configurationSection)))
                return;
            
            this._section = configurationSection;

            this.OnSectionChanged();
        }

        protected virtual void OnSectionChanged()
        {
            if (this._section == null)
            {
                this.HasSection = false;
                this.ShowPropertiesColumn = true;
                this.ShowElementsColumn = false;
                return;
            }

            this.HasSection = true;
            this.AllowOverride = this._section.SectionInformation.AllowOverride;
            this.ConfigSource = this._section.SectionInformation.ConfigSource;
            this.IsDeclared = this._section.SectionInformation.IsDeclared;
            this.IsLocked = this._section.SectionInformation.IsLocked;
            this.IsProtected = this._section.SectionInformation.IsProtected;
            if (this.IsProtected)
            {
                this.ProtectionProviderName = (this._section.SectionInformation.ProtectionProvider == null) ? "" : this._section.SectionInformation.ProtectionProvider.Name;
                this.ProtectionProviderDescription = (this._section.SectionInformation.ProtectionProvider == null) ? "" : this._section.SectionInformation.ProtectionProvider.Description;
            }
            else
            {
                this.ProtectionProviderName = "";
                this.ProtectionProviderDescription = "";
            }
            this.LineNumber = this._section.ElementInformation.LineNumber;
            this.Source = this._section.ElementInformation.Source;
            this._attributes.Clear();

            foreach (PropertyInformation property in this._section.ElementInformation.Properties.OfType<PropertyInformation>().OrderBy(p => p.Name))
                    this.OnAddingProperty(property);

            if (this.Elements.Count == 0)
            {
                if (this.Properties.Count > 0 || !this._section.ElementInformation.IsCollection)
                {
                    this.ShowPropertiesColumn = true;
                    this.ShowElementsColumn = false;
                }
                else
                {
                    this.ShowPropertiesColumn = false;
                    this.ShowElementsColumn = true;
                }
            }
            else
            {
                this.ShowElementsColumn = true;
                this.ShowPropertiesColumn = (this.Properties.Count > 0);
            }
        }

        protected virtual void OnAddingProperty(PropertyInformation property)
        {
            if (property.Value != null && property.Value is ConfigurationElement)
                this.OnAddingElement(property.Value as ConfigurationElement, property);
            else
                this.OnAddingAttribute(property);
        }

        private void OnAddingElement(ConfigurationElement configurationElement, PropertyInformation property)
        {
            ConfigElementVM_obsolete vm = new ConfigElementVM_obsolete();
            vm.SetElement(configurationElement, property, this._section);
            this._elements.Add(vm);
        }

        private void OnAddingAttribute(PropertyInformation property)
        {
            ConfigAttributeVM_obsolete vm = new ConfigAttributeVM_obsolete();
            vm.SetAttribute(property, this._section);
            this._attributes.Add(vm);
        }
    }
}
