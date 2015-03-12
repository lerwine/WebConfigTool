using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace WebConfigTool.ViewModel
{
    public class OptionCollection<TOption> : ReadOnlyObservableCollection<TOption>
        where TOption : OptionItemVM, IEquatable<TOption>, new()
    {
        public event SelectedIndexChangedEventHandler SelectedIndexChanged;

        private int _selectedIndex = -1;
        private TOption _selectedItem = null;

        public int SelectedIndex
        {
            get
            {
                if (this._selectedIndex < 0 && this.Count > 0)
                    this._selectedIndex = 0;
                return this._selectedIndex;
            }
            set
            {
                if (this._selectedIndex == value)
                    return;

                TOption selectedItem = this[value];
                this._selectedIndex = value;

                this.RaiseSelectedIndexChanged();

                this.SelectedItem = selectedItem;
            }
        }

        public TOption SelectedItem
        {
            get
            {
                if (this._selectedItem == null && this.SelectedIndex > -1)
                    this._selectedItem = this[this.SelectedIndex];

                return this._selectedItem;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();

                if (this._selectedItem != null && Object.ReferenceEquals(value, this._selectedItem))
                    return;

                TOption matchingItem;

                int selectedIndex = this.TakeWhile(i => !Object.ReferenceEquals(i, value)).Count();

                if (selectedIndex == this.Count)
                {
                    if ((selectedIndex = this.TakeWhile(i => !i.Equals(value)).Count()) == this.Count)
                        throw new ArgumentOutOfRangeException();
                    matchingItem = this[selectedIndex];
                }
                else
                    matchingItem = value;

                if (this._selectedItem != null && Object.ReferenceEquals(this._selectedItem, matchingItem))
                    return;

                this._selectedItem = matchingItem;

                this.RaisePropertyChanged("SelectedItem");
                this.SelectedIndex = selectedIndex;
            }
        }

        public OptionCollection(ObservableCollection<TOption> list) : base(list) { }

        public OptionCollection(IList<TOption> list) : this(new ObservableCollection<TOption>(list)) { }

        private void RaiseSelectedIndexChanged()
        {
            this.OnSelectedIndexChanged(new SelectedIndexChangedEventArgs(this.SelectedIndex));
        }

        protected virtual void OnSelectedIndexChanged(SelectedIndexChangedEventArgs args)
        {
            if (this.SelectedIndexChanged != null)
                this.SelectedIndexChanged(this, args);

            this.RaisePropertyChanged("SelectedIndex");
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }

    public abstract class OptionCollection<TOption, TValue> : OptionCollection<TOption>
        where TOption : OptionItemVM<TValue, TOption>, IEquatable<TOption>, new()
    {
        public event ValueChangedEventHandler<TValue> ValueChanged;

        private TValue _selectedValue;

        public TValue SelectedValue
        {
            get { return this._selectedValue; }
            set
            {
                if (this.ValuesAreEqual(this._selectedValue, value))
                    return;

                int index = this.TakeWhile(o => !o.ValueEquals(value)).Count();

                if (index == this.Count)
                    index = -1;

                this._selectedValue = value;

                this.RaiseSelectedValueChanged();

                this.SelectedIndex = index;
            }
        }

        protected override void OnSelectedIndexChanged(SelectedIndexChangedEventArgs args)
        {
            base.OnSelectedIndexChanged(args);

            if (args.SelectedIndex > -1 && args.SelectedIndex < this.Count)
                this.SelectedValue = this[args.SelectedIndex].Value;
        }

        private void RaiseSelectedValueChanged()
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, new ValueChangedEventArgs<TValue>(this.SelectedValue));

            this.RaisePropertyChanged("SelectedValue");
        }

        protected virtual bool ValuesAreEqual(TValue value1, TValue value2)
        {
            return ((object)value1 == null) == ((object)value2 == null) && ((object)value1 == null || EqualityComparer<TValue>.Default.Equals(value1, value2));
        }

        protected OptionCollection(ObservableCollection<TOption> list) : base(list) { }
    }
}
