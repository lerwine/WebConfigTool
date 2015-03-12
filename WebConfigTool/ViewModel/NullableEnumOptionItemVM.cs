using System;
using System.Collections.Generic;
using System.Linq;

namespace WebConfigTool.ViewModel
{
    public class NullableEnumOptionItemVM<TValue> : NullableEnumOptionItemVM<TValue, NullableEnumOptionItemVM<TValue>>
        where TValue : struct, IComparable, IFormattable, IConvertible
    {
        public NullableEnumOptionItemVM() : base() { }

        public NullableEnumOptionItemVM(TValue? value) : base(value) { }

        public NullableEnumOptionItemVM(TValue? value, string displayText) : base(value, displayText) { }
    }

    public abstract class NullableEnumOptionItemVM<TValue, TOption> : OptionItemVM<TValue?, TOption>
        where TValue : struct, IComparable, IFormattable, IConvertible
        where TOption : OptionItemVM<TValue?, TOption>, new()
    {
        protected NullableEnumOptionItemVM() : base() { }

        protected NullableEnumOptionItemVM(TValue? value) : base(value) { }

        protected NullableEnumOptionItemVM(TValue? value, string displayText) : base(value, displayText) { }

        protected override bool ValuesAreEqual(TValue? value1, TValue? value2)
        {
            return value1.HasValue == value2.HasValue && (!value1.HasValue || value1.Value.CompareTo(value2.Value) == 0);
        }

        protected override void OnValueChanged(TValue? oldValue, TValue? newValue)
        {
            if (newValue.HasValue)
                this.DisplayText = this.GetDisplayText(newValue.Value);
            else
                this.DisplayText = this.GetNullValueDisplayText();
        }

        protected virtual string GetDisplayText(TValue value) { return value.ToString(null); }

        protected virtual string GetNullValueDisplayText() { return "(none)"; }

        public static IEnumerable<TOption> GetAllOptions()
        {
            TOption nullOption = new TOption();
            nullOption.Value = null;

            return Enum.GetValues(typeof(TValue)).OfType<TValue>().Select(v =>
            {
                TOption option = new TOption();
                option.Value = v;
                return option;
            }).Concat(new TOption[] { nullOption });
        }
    }
}
