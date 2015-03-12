using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebConfigTool.ViewModel
{
    public class EnumOptionItemVM<TValue> : EnumOptionItemVM<TValue, EnumOptionItemVM<TValue>>
        where TValue : struct, IComparable, IFormattable, IConvertible
    {
        public EnumOptionItemVM() : base() { }

        public EnumOptionItemVM(TValue value) : base(value) { }

        public EnumOptionItemVM(TValue value, string displayText) : base(value, displayText) { }
    }

    public abstract class EnumOptionItemVM<TValue, TOption> : OptionItemVM<TValue, TOption>
        where TValue : struct, IComparable, IFormattable, IConvertible
        where TOption : EnumOptionItemVM<TValue, TOption>, new()
    {
        protected EnumOptionItemVM() : base() { }

        protected EnumOptionItemVM(TValue value) : base(value) { }

        protected EnumOptionItemVM(TValue value, string displayText) : base(value, displayText) { }

        protected override bool ValuesAreEqual(TValue tValue1, TValue tValue2)
        {
            return tValue1.CompareTo(tValue2) == 0;
        }

        public static IEnumerable<TOption> GetAllOptions()
        {
            return Enum.GetValues(typeof(TValue)).OfType<TValue>().Select(v =>
            {
                TOption option = new TOption();
                option.Value = v;
                return option;
            });
        }
    }
}
