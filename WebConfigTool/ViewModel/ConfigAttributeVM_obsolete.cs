using System;
using System.Configuration;
using System.Windows;

namespace WebConfigTool.ViewModel
{
    public class ConfigAttributeVM_obsolete : DependencyObject
    {
        private PropertyInformation _property = null;

        public static readonly DependencyProperty HasPropertyProperty =
            DependencyProperty.Register("HasProperty", typeof(bool), typeof(ConfigAttributeVM_obsolete), new PropertyMetadata(false));

        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(ConfigAttributeVM_obsolete), new PropertyMetadata(""));

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(ConfigAttributeVM_obsolete), new PropertyMetadata(""));

        public static readonly DependencyProperty ValueIsNullProperty =
            DependencyProperty.Register("ValueIsNull", typeof(bool), typeof(ConfigAttributeVM_obsolete), new PropertyMetadata(true));

        public static readonly DependencyProperty ValueAsTextProperty =
            DependencyProperty.Register("ValueAsText", typeof(string), typeof(ConfigAttributeVM_obsolete), new PropertyMetadata(""));

        public static readonly DependencyProperty ValueOriginProperty =
            DependencyProperty.Register("ValueOrigin", typeof(string), typeof(ConfigAttributeVM_obsolete), new PropertyMetadata(""));

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(string), typeof(ConfigAttributeVM_obsolete), new PropertyMetadata(""));

        public static readonly DependencyProperty LineNumberProperty =
            DependencyProperty.Register("LineNumber", typeof(int), typeof(ConfigAttributeVM_obsolete), new PropertyMetadata(0));

        public static readonly DependencyProperty IsKeyProperty =
            DependencyProperty.Register("IsKey", typeof(bool), typeof(ConfigAttributeVM_obsolete), new PropertyMetadata(false));

        public static readonly DependencyProperty IsLockedProperty =
            DependencyProperty.Register("IsLocked", typeof(bool), typeof(ConfigAttributeVM_obsolete), new PropertyMetadata(false));

        public static readonly DependencyProperty IsModifiedProperty =
            DependencyProperty.Register("IsModified", typeof(bool), typeof(ConfigAttributeVM_obsolete), new PropertyMetadata(false));

        public static readonly DependencyProperty IsRequiredProperty =
            DependencyProperty.Register("IsRequired", typeof(bool), typeof(ConfigAttributeVM_obsolete), new PropertyMetadata(false));

        public bool HasProperty
        {
            get { return (bool)(this.GetValue(ConfigAttributeVM_obsolete.HasPropertyProperty)); }
            private set { this.SetValue(ConfigAttributeVM_obsolete.HasPropertyProperty, value); }
        }

        public string Name
        {
            get { return (string)(this.GetValue(ConfigAttributeVM_obsolete.NameProperty)); }
            private set { this.SetValue(ConfigAttributeVM_obsolete.NameProperty, (value == null) ? "" : value); }
        }

        public string Description
        {
            get { return (string)(this.GetValue(ConfigAttributeVM_obsolete.DescriptionProperty)); }
            private set { this.SetValue(ConfigAttributeVM_obsolete.DescriptionProperty, (value == null) ? "" : value); }
        }

        public bool ValueIsNull
        {
            get { return (bool)(this.GetValue(ConfigAttributeVM_obsolete.ValueIsNullProperty)); }
            private set { this.SetValue(ConfigAttributeVM_obsolete.ValueIsNullProperty, value); }
        }

        public string ValueAsText
        {
            get { return (string)(this.GetValue(ConfigAttributeVM_obsolete.ValueAsTextProperty)); }
            private set { this.SetValue(ConfigAttributeVM_obsolete.ValueAsTextProperty, (value == null) ? "" : value); }
        }

        public string ValueOrigin
        {
            get { return (string)(this.GetValue(ConfigAttributeVM_obsolete.ValueOriginProperty)); }
            private set { this.SetValue(ConfigAttributeVM_obsolete.ValueOriginProperty, (value == null) ? "" : value); }
        }

        public string Source
        {
            get { return (string)(this.GetValue(ConfigAttributeVM_obsolete.SourceProperty)); }
            private set { this.SetValue(ConfigAttributeVM_obsolete.SourceProperty, (value == null) ? "" : value); }
        }

        public int LineNumber
        {
            get { return (int)(this.GetValue(ConfigAttributeVM_obsolete.LineNumberProperty)); }
            private set { this.SetValue(ConfigAttributeVM_obsolete.LineNumberProperty, value); }
        }

        public bool IsKey
        {
            get { return (bool)(this.GetValue(ConfigAttributeVM_obsolete.IsKeyProperty)); }
            private set { this.SetValue(ConfigAttributeVM_obsolete.IsKeyProperty, value); }
        }

        public bool IsLocked
        {
            get { return (bool)(this.GetValue(ConfigAttributeVM_obsolete.IsLockedProperty)); }
            private set { this.SetValue(ConfigAttributeVM_obsolete.IsLockedProperty, value); }
        }

        public bool IsModified
        {
            get { return (bool)(this.GetValue(ConfigAttributeVM_obsolete.IsModifiedProperty)); }
            private set { this.SetValue(ConfigAttributeVM_obsolete.IsModifiedProperty, value); }
        }

        public bool IsRequired
        {
            get { return (bool)(this.GetValue(ConfigAttributeVM_obsolete.IsRequiredProperty)); }
            private set { this.SetValue(ConfigAttributeVM_obsolete.IsRequiredProperty, value); }
        }

        protected virtual void OnPropertyChanged()
        {
            if (this._property == null)
            {
                this.HasProperty = false;
                this.ValueOrigin = "";
                return;
            }

            this.HasProperty = true;

            this.Name = this._property.Name;
            this.Description = this._property.Description;
            if (this._property.Value == null)
            {
                this.ValueIsNull = true;
                this.ValueAsText = "(null)";
            }
            else
            {
                this.ValueIsNull = false;
                if (this._property.Converter == null)
                    this.ValueAsText = this._property.Value.ToString();
                else if (this._property.Value is string)
                    this.ValueAsText = this._property.Value as string;
                else
                {
                    try
                    {
                        this.ValueAsText = this._property.Converter.ConvertToString(this._property.Value);
                    }
                    catch
                    {
                        this.ValueAsText = this._property.Value.ToString();
                    }
                }
            }

            switch (this._property.ValueOrigin)
            {
                case PropertyValueOrigin.Default:
                    this.ValueOrigin = "Default value";
                    break;
                case PropertyValueOrigin.SetHere:
                    this.ValueOrigin = "Set here";
                    break;
                default:
                    this.ValueOrigin = this._property.ValueOrigin.ToString("F");
                    break;
            }

            this.Source = this._property.Source;
            this.LineNumber = this._property.LineNumber;
            this.IsKey = this._property.IsKey;
            this.IsLocked = this._property.IsLocked;
            this.IsModified = this._property.IsModified;
            this.IsRequired = this._property.IsRequired;
        }

        private ConfigurationSection _section = null;
        internal void SetAttribute(PropertyInformation property, ConfigurationSection section)
        {
            if ((this._property == null) == (property == null) && (property == null || Object.ReferenceEquals(property, this._property)))
                return;

            this._property = property;
            this._section = section;
            this.OnPropertyChanged();
        }
    }
}
