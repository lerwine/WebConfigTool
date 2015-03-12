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
    public class ConfigElementVM_obsolete : DependencyObject
    {
        private ConfigurationElement _configurationElement = null;
        private ObservableCollection<ConfigAttributeVM_obsolete> _properties = new ObservableCollection<ConfigAttributeVM_obsolete>();
        private ObservableCollection<ConfigElementVM_obsolete> _elements = new ObservableCollection<ConfigElementVM_obsolete>();

        private RelayCommand _openCommand = null;
        private ConfigElementWindow _window = null;
        private object _syncRoot = new object();

        public RelayCommand OpenCommand
        {
            get
            {
                if (this._openCommand == null)
                {
                    this._openCommand = new RelayCommand((object o) =>
                    {
                        ConfigElementWindow window;
                        lock (this._syncRoot)
                        {
                            window = this._window;
                            if (window == null)
                            {
                                this._window = new ConfigElementWindow();
                                this._window.DataContext = this;
                                this._window.Closed += this.ConfigElementWindow_Closed;
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

        public static readonly DependencyProperty HasElementProperty =
            DependencyProperty.Register("HasElement", typeof(bool), typeof(ConfigElementVM_obsolete), new PropertyMetadata(false));

        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(ConfigElementVM_obsolete), new PropertyMetadata(""));

        public static readonly DependencyProperty LineNumberProperty =
            DependencyProperty.Register("LineNumber", typeof(int), typeof(ConfigElementVM_obsolete), new PropertyMetadata(0));

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(string), typeof(ConfigElementVM_obsolete), new PropertyMetadata(""));

        public static readonly DependencyProperty ShowPropertiesColumnProperty =
            DependencyProperty.Register("ShowPropertiesColumn", typeof(bool), typeof(ConfigElementVM_obsolete), new PropertyMetadata(true));

        public static readonly DependencyProperty ShowElementsColumnProperty =
            DependencyProperty.Register("ShowElementsColumn", typeof(bool), typeof(ConfigElementVM_obsolete), new PropertyMetadata(false));

        public static readonly DependencyProperty WindowIsOpenProperty =
            DependencyProperty.Register("WindowIsOpen", typeof(bool), typeof(ConfigElementVM_obsolete), new PropertyMetadata(false));

        public bool WindowIsOpen
        {
            get { return (bool)(this.GetValue(ConfigElementVM_obsolete.WindowIsOpenProperty)); }
            private set { this.SetValue(ConfigElementVM_obsolete.WindowIsOpenProperty, value); }
        }

        public bool HasElement
        {
            get { return (bool)(this.GetValue(ConfigElementVM_obsolete.HasElementProperty)); }
            private set { this.SetValue(ConfigElementVM_obsolete.HasElementProperty, value); }
        }

        public string Name
        {
            get { return (string)(this.GetValue(ConfigElementVM_obsolete.NameProperty)); }
            set { this.SetValue(ConfigElementVM_obsolete.NameProperty, (value == null) ? "" : value); }
        }

        public int LineNumber
        {
            get { return (int)(this.GetValue(ConfigElementVM_obsolete.LineNumberProperty)); }
            private set { this.SetValue(ConfigElementVM_obsolete.LineNumberProperty, value); }
        }

        public string Source
        {
            get { return (string)(this.GetValue(ConfigElementVM_obsolete.SourceProperty)); }
            private set { this.SetValue(ConfigElementVM_obsolete.SourceProperty, (value == null) ? "" : value); }
        }

        public bool ShowPropertiesColumn
        {
            get { return (bool)(this.GetValue(ConfigElementVM_obsolete.ShowPropertiesColumnProperty)); }
            private set { this.SetValue(ConfigElementVM_obsolete.ShowPropertiesColumnProperty, value); }
        }

        public bool ShowElementsColumn
        {
            get { return (bool)(this.GetValue(ConfigElementVM_obsolete.ShowElementsColumnProperty)); }
            private set { this.SetValue(ConfigElementVM_obsolete.ShowElementsColumnProperty, value); }
        }

        public ObservableCollection<ConfigAttributeVM_obsolete> Properties { get { return this._properties; } }

        public ObservableCollection<ConfigElementVM_obsolete> Elements { get { return this._elements; } }

        protected ConfigurationElement ConfigElement { get { return this._configurationElement; } }

        void ConfigElementWindow_Closed(object sender, EventArgs e)
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

        protected virtual void OnElementChanged()
        {
            if (this._configurationElement == null)
            {
                this.HasElement = false;
                this.ShowPropertiesColumn = true;
                this.ShowElementsColumn = false;
                return;
            }

            this.HasElement = true;
            this.LineNumber = this._configurationElement.ElementInformation.LineNumber;
            this.Source = this._configurationElement.ElementInformation.Source;
            
            this._properties.Clear();

            foreach (PropertyInformation property in this._configurationElement.ElementInformation.Properties.OfType<PropertyInformation>().OrderBy(p => p.Name))
            {
                Trace.TraceInformation(String.Format("Element {0}/@{1} is {2} ({3})", this.Name, property.Name, (property.Value == null) ? "(null)" : property.Value.GetType().FullName, (property.Value == null) ? "()" : property.Value.GetType().BaseType .FullName));

                if (property.Value != null)
                {
                    if (property.Value is ConfigurationElementCollection)
                    {
                        int index = 0;
                        if (String.IsNullOrEmpty(property.Name))
                        {
                            foreach (ConfigurationElement element in (property.Value as ConfigurationElementCollection))
                            {
                                Trace.TraceInformation(String.Format("Child of {0}/@{1} is {2} ({3})", this.Name, property.Name,
                                    element.GetType().FullName, element.GetType().BaseType.FullName));

                                this.OnAddingElement(String.Format("(element {0})", index++), element);
                            }
                        }
                        else
                        {
                            foreach (ConfigurationElement element in (property.Value as ConfigurationElementCollection))
                            {
                                Trace.TraceInformation(String.Format("Child of {0}/@{1} is {2} ({3})", this.Name, property.Name,
                                    element.GetType().FullName, element.GetType().BaseType.FullName));
                                this.OnAddingElement(String.Format("{0}[{1}]", property.Name, index++), element);
                            }
                        }
                        continue;
                    }
                    else if (property.Value is ConfigurationElement)
                    {
                        this.OnAddingElement(property.Name, property.Value as ConfigurationElement);
                        continue;
                    }
                }

                this.OnAddingProperty(property);
            }

            if (this.Elements.Count == 0)
            {
                if (this.Properties.Count > 0 || !this._configurationElement.ElementInformation.IsCollection)
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

        private void OnAddingElement(string name, ConfigurationElement configurationElement)
        {
            ConfigElementVM_obsolete vm = new ConfigElementVM_obsolete();
            vm.SetElement(name, configurationElement);
            this._elements.Add(vm);
        }

        protected virtual void OnAddingProperty(PropertyInformation property)
        {
            ConfigAttributeVM_obsolete vm = new ConfigAttributeVM_obsolete();
            vm.SetAttribute(property);
            this._properties.Add(vm);
        }

        private ConfigurationSection _section = null;
        PropertyInformation _property = null;

        internal void SetElement(ConfigurationElement configurationElement, PropertyInformation property, ConfigurationSection section)
        {
            if ((this._configurationElement == null) == (configurationElement == null) && (this._configurationElement == null || Object.ReferenceEquals(this._configurationElement, configurationElement)))
                return;

            this._section = section;
            this._configurationElement = configurationElement;
            this._property = property;

            this.OnElementChanged();
        }
    }
}
