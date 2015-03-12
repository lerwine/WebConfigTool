using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace WebConfigTool.ViewModel
{
    public class EnumOptionCollection<TOption, TValue> : OptionCollection<TOption, TValue>
        where TValue : struct, IComparable, IFormattable, IConvertible
        where TOption : EnumOptionItemVM<TValue, TOption>, new()
    {
        public EnumOptionCollection() : this(EnumOptionItemVM<TValue, TOption>.GetAllOptions()) { }
        public EnumOptionCollection(ObservableCollection<TOption> collection) : base(collection) { }
        public EnumOptionCollection(IEnumerable<TOption> collection) : this(new ObservableCollection<TOption>(collection)) { }
        public EnumOptionCollection(IEnumerable<TValue> collection)
            : this(collection.Select(v =>
            {
                TOption opt = new TOption();
                opt.Value = v;
                return opt;
            })) { }

        protected override bool ValuesAreEqual(TValue value1, TValue value2)
        {
            return value1.CompareTo(value2) == 0;
        }
    }
}
