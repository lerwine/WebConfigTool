using System;
using System.Windows;

namespace WebConfigTool.ViewModel
{
    public abstract class OptionItemVM : DependencyObject, IEquatable<OptionItemVM>
    {
        public string DisplayText
        {
            get { return (string)(this.GetValue(OptionItemVM.DisplayTextProperty)); }
            set { this.SetValue(OptionItemVM.DisplayTextProperty, (value == null) ? "" : value); }
        }

        public static readonly DependencyProperty DisplayTextProperty =
            DependencyProperty.Register("DisplayText", typeof(string), typeof(OptionItemVM), new PropertyMetadata(OptionItemVM.DisplayText_PropertyChanged));

        public OptionItemVM() : base() { }

        public OptionItemVM(string displayText)
        {
            this.DisplayText = displayText;
        }

        public static void DisplayText_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as OptionItemVM).OnDisplayTextChanged(e.OldValue as string, e.NewValue as string);
        }

        protected virtual void OnDisplayTextChanged(string oldValue, string newValue) { }

        public abstract bool Equals(OptionItemVM other);

        public override string ToString()
        {
            return this.DisplayText;
        }
    }

    public abstract class OptionItemVM<TValue> : OptionItemVM, IEquatable<OptionItemVM<TValue>>
    {
        public TValue Value
        {
            get { return (TValue)(this.GetValue(OptionItemVM<TValue>.ValueProperty)); }
            set { this.SetValue(OptionItemVM<TValue>.ValueProperty, this.NormalizeValue(value)); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(TValue), typeof(OptionItemVM<TValue>), new PropertyMetadata(OptionItemVM<TValue>.Value_PropertyChanged));

        protected OptionItemVM() : this(default(TValue)) { }

        protected OptionItemVM(TValue value) : this(value, null) { }

        protected OptionItemVM(TValue value, string displayText)
            : base(displayText)
        {
            this.Value = value;
            if (!String.IsNullOrEmpty(displayText))
                return;

            TValue v = this.NormalizeValue(default(TValue));
            if (this.ValuesAreEqual(v, this.Value))
                this.OnValueChanged(v, this.Value);
        }

        public static void Value_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as OptionItemVM<TValue>).OnValueChanged((TValue)(e.OldValue), (TValue)(e.NewValue));
        }

        protected virtual TValue NormalizeValue(TValue value) { return value; }

        protected virtual void OnValueChanged(TValue oldValue, TValue newValue)
        {
            if ((object)newValue == null)
                this.DisplayText = "";
            else
                this.DisplayText = newValue.ToString();
        }

        public virtual bool Equals(OptionItemVM<TValue> other)
        {
            return other != null && (Object.ReferenceEquals(this, other) || this.ValuesAreEqual(this.Value, other.Value));
        }

        public override bool Equals(OptionItemVM other)
        {
            return this.Equals(other as OptionItemVM<TValue>);
        }

        protected abstract bool ValuesAreEqual(TValue value1, TValue value2);

        public bool ValueEquals(TValue value) { return this.ValuesAreEqual(this.Value, value); }
    }

    public abstract class OptionItemVM<TValue, TOption> : OptionItemVM<TValue>, IEquatable<TOption>
        where TOption : OptionItemVM<TValue, TOption>, new()
    {
        protected OptionItemVM() : base() { }

        protected OptionItemVM(TValue value) : base(value) { }

        protected OptionItemVM(TValue value, string displayText) : base(value, displayText) { }

        protected abstract override bool ValuesAreEqual(TValue tValue1, TValue tValue2);

        public bool Equals(TOption other)
        {
            return other != null && (Object.ReferenceEquals(this, other) || this.ValuesAreEqual(this.Value, other.Value));
        }

        public override bool Equals(OptionItemVM<TValue> other)
        {
            return this.Equals(other as TOption);
        }
    }
}
