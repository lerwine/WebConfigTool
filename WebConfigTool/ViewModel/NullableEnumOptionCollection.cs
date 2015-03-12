using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace WebConfigTool.ViewModel
{
    public class NullableEnumOptionCollection<TOption, TValue> : OptionCollection<TOption, TValue?>
        where TValue : struct, IComparable, IFormattable, IConvertible
        where TOption : NullableEnumOptionItemVM<TValue, TOption>, new()
    {
        public NullableEnumOptionCollection() : this(NullableEnumOptionItemVM<TValue, TOption>.GetAllOptions()) { }
        public NullableEnumOptionCollection(ObservableCollection<TOption> collection) : base(collection) { }
        public NullableEnumOptionCollection(IEnumerable<TOption> collection) : this(new ObservableCollection<TOption>(collection)) { }
        public NullableEnumOptionCollection(IEnumerable<TValue?> collection)
            : this(collection.Select(v =>
            {
                TOption opt = new TOption();
                opt.Value = v;
                return opt;
            })) { }

        protected override bool ValuesAreEqual(TValue? value1, TValue? value2)
        {
            return value1.HasValue == value2.HasValue && (!value1.HasValue || value1.Value.CompareTo(value2.Value) == 0);
        }
    }
}
