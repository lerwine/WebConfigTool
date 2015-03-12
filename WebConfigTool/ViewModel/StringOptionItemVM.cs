using System;

namespace WebConfigTool.ViewModel
{
    public class StringOptionItemVM : OptionItemVM<string>, IEquatable<StringOptionItemVM>
    {
        public StringOptionItemVM() : base() { }

        public StringOptionItemVM(string value) : base(value) { }

        public StringOptionItemVM(string value, string displayText) : base(value, displayText) { }

        protected override string NormalizeValue(string value)
        {
            return (value == null) ? "" : value;
        }

        protected override bool ValuesAreEqual(string value1, string value2)
        {
            return (value1 == null) == (value2 == null) && (value1 == null || value1 == value2);
        }

        public bool Equals(StringOptionItemVM other)
        {
            return other != null && (Object.ReferenceEquals(this, other) || this.Value == other.Value);
        }

        public override bool Equals(OptionItemVM<string> other)
        {
            return this.Equals(other as StringOptionItemVM);
        }
    }
}
