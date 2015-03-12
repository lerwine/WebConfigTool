using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebConfigTool.ViewModel
{
    public class SectionGroupVM<TSectionGroup> : DependencyObject, ISectionGroup<TSectionGroup>
        where TSectionGroup : ConfigurationSectionGroup
    {
        private TSectionGroup _sectionGroup = null;
        private ObservableCollection<IItemCategory> _items = new ObservableCollection<IItemCategory>();

        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(SectionGroupVM<TSectionGroup>), new PropertyMetadata("Section Group"));

        public static readonly DependencyProperty DisplayTextProperty =
            DependencyProperty.Register("DisplayText", typeof(string), typeof(SectionGroupVM<TSectionGroup>), new PropertyMetadata("Section Group"));

        public ObservableCollection<IItemCategory> Items { get { return this._items; } }

        public string Name
        {
            get { return (string)(this.GetValue(SectionGroupVM<TSectionGroup>.NameProperty)); }
            private set { this.SetValue(SectionGroupVM<TSectionGroup>.NameProperty, value); }
        }

        public string DisplayText
        {
            get { return (string)(this.GetValue(SectionGroupVM<TSectionGroup>.DisplayTextProperty)); }
            private set { this.SetValue(SectionGroupVM<TSectionGroup>.DisplayTextProperty, value); }
        }

        protected TSectionGroup SectionGroup { get { return this._sectionGroup; } }

        public SectionGroupVM() { }

        public SectionGroupVM(TSectionGroup sectionGroup)
        {
            this.Load(this._sectionGroup);
        }

        public void Load(TSectionGroup sectionGroup)
        {
            this.OnSectionGroupChanging(sectionGroup);
            this.OnLoadSectionGroup(sectionGroup);
            this.Name = this.OnChangeName();
            this.DisplayText = this.OnChangeDisplayText();
            this.OnSectionGroupLoaded();
        }

        protected virtual void OnSectionGroupChanging(TSectionGroup newSectionGroup)
        {
            this.Items.Clear();
        }

        protected virtual void OnLoadSectionGroup(TSectionGroup sectionGroup)
        {
            this._sectionGroup = sectionGroup;
            if (this._sectionGroup != null)
            {
                if (this._sectionGroup.SectionGroups != null)
                {
                    foreach (ConfigurationSectionGroup grp in this._sectionGroup.SectionGroups)
                        this.OnAddSectionGroup(grp);
                }

                if (this._sectionGroup.Sections != null)
                {
                    foreach (ConfigurationSection section in this._sectionGroup.Sections)
                        this.OnAddSection(section);
                }
            }

        }

        protected virtual void OnAddSectionGroup(ConfigurationSectionGroup grp)
        {
            this.Items.Add(new SectionGroupVM(grp));
        }

        protected virtual void OnAddSection(ConfigurationSection section)
        {
            throw new NotImplementedException();
        }

        protected virtual string OnChangeName()
        {
            return (this._sectionGroup == null) ? "Section Group" : ((String.IsNullOrWhiteSpace(this._sectionGroup.Name)) ? this._sectionGroup.GetType().Name : this._sectionGroup.Name);
        }

        protected virtual string OnChangeDisplayText()
        {
            return (this._sectionGroup == null) ? "Section Group" : ((String.IsNullOrWhiteSpace(this._sectionGroup.Name)) ? this._sectionGroup.GetType().Name : this._sectionGroup.Name);
        }

        protected virtual void OnSectionGroupLoaded()
        {
        }
    }

    public class SectionGroupVM : SectionGroupVM<ConfigurationSectionGroup>
    {
        public SectionGroupVM() : base() { }

        public SectionGroupVM(ConfigurationSectionGroup sectionGroup) : base(sectionGroup) { }
    }
}
