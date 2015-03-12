using System;
using System.Configuration;
using System.Windows;

namespace WebConfigTool.ViewModel
{
    public class ConfigurationSectionGroupVM_obsolete : ConfigurationSectionGroupVM<ConfigurationSectionGroup>
    {
        private RelayCommand _openCommand = null;
        private ConfigurationSectionGroupWindow _window = null;
        private object _syncRoot = new object();

        public RelayCommand OpenCommand
        {
            get
            {
                if (this._openCommand == null)
                {
                    this._openCommand = new RelayCommand((object o) =>
                    {
                        ConfigurationSectionGroupWindow window;
                        lock (this._syncRoot)
                        {
                            window = this._window;
                            if (window == null)
                            {
                                this._window = new ConfigurationSectionGroupWindow();
                                this._window.DataContext = this;
                                this._window.Closed += this.ConfigurationSectionGroupWindow_Closed;
                                this.WindowIsOpen = true;
                            }
                            this._openCommand.IsEnabled = false;
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
            DependencyProperty.Register("WindowIsOpen", typeof(bool), typeof(ConfigurationSectionGroupVM_obsolete), new PropertyMetadata(false));

        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(ConfigurationSectionGroupVM_obsolete), new PropertyMetadata(""));

        public static readonly DependencyProperty SectionGroupNameProperty =
            DependencyProperty.Register("SectionGroupName", typeof(string), typeof(ConfigurationSectionGroupVM_obsolete), new PropertyMetadata(""));

        public bool WindowIsOpen
        {
            get { return (bool)(this.GetValue(ConfigurationSectionGroupVM_obsolete.WindowIsOpenProperty)); }
            set { this.SetValue(ConfigurationSectionGroupVM_obsolete.WindowIsOpenProperty, value); }
        }

        public string Name
        {
            get { return (string)(this.GetValue(ConfigurationSectionGroupVM_obsolete.NameProperty)); }
            set { this.SetValue(ConfigurationSectionGroupVM_obsolete.NameProperty, (value == null) ? "" : value); }
        }

        public string SectionGroupName
        {
            get { return (string)(this.GetValue(ConfigurationSectionGroupVM_obsolete.SectionGroupNameProperty)); }
            set { this.SetValue(ConfigurationSectionGroupVM_obsolete.SectionGroupNameProperty, (value == null) ? "" : value); }
        }

        public void FocusWindow()
        {
            lock (this._syncRoot)
            {
                if (this._window != null)
                    this._window.Activate();
            }
        }

        void ConfigurationSectionGroupWindow_Closed(object sender, EventArgs e)
        {
            lock (this._syncRoot)
            {
                this._window = null;
                this._openCommand.IsEnabled = true;
                this.WindowIsOpen = false;
            }
        }

        protected override void OnSectionGroupChanged()
        {
            base.OnSectionGroupChanged();

            this.Name = this.SectionGroup.Name;
            this.SectionGroupName = this.SectionGroup.SectionGroupName;
        }
    }

    public class ConfigurationSectionGroupVM<TSectionGroup> : ConfigSectionGroupContainerVM_obsolete
        where TSectionGroup : ConfigurationSectionGroup
    {
        private TSectionGroup _sectionGroup = null;

        protected TSectionGroup SectionGroup { get { return this._sectionGroup; } }

        public static readonly DependencyProperty HasSectionGroupProperty =
            DependencyProperty.Register("HasSectionGroup", typeof(bool), typeof(ConfigurationSectionGroupVM<TSectionGroup>), new PropertyMetadata(false));

        public static readonly DependencyProperty IsDeclaredProperty =
            DependencyProperty.Register("IsDeclared", typeof(bool), typeof(ConfigurationSectionGroupVM<TSectionGroup>), new PropertyMetadata(false));

        public bool HasSectionGroup
        {
            get { return (bool)(this.GetValue(ConfigurationSectionGroupVM<TSectionGroup>.HasSectionGroupProperty)); }
            private set { this.SetValue(ConfigurationSectionGroupVM<TSectionGroup>.HasSectionGroupProperty, value); }
        }

        public bool IsDeclared
        {
            get { return (bool)(this.GetValue(ConfigurationSectionGroupVM<TSectionGroup>.IsDeclaredProperty)); }
            private set { this.SetValue(ConfigurationSectionGroupVM<TSectionGroup>.IsDeclaredProperty, value); }
        }

        internal void SetSectionGroup(TSectionGroup configurationSectionGroup)
        {
            if ((this._sectionGroup == null) == (configurationSectionGroup == null) && (this._sectionGroup == null || Object.ReferenceEquals(this._sectionGroup, configurationSectionGroup)))
                return;

            this._sectionGroup = configurationSectionGroup;

            this.OnSectionGroupChanged();
        }

        protected virtual void OnSectionGroupChanged()
        {
            if (this._sectionGroup == null)
            {
                base.LoadSectionGroups(null);
                base.LoadSections(null);
                this.HasSectionGroup = false;
                return;
            }

            this.HasSectionGroup = true;
            this.IsDeclared = this._sectionGroup.IsDeclared;
            base.LoadSectionGroups(this._sectionGroup.SectionGroups);
            base.LoadSections(this._sectionGroup.Sections);
        }
    }
}
