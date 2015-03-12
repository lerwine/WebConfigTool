using System;

namespace WebConfigTool.ViewModel
{
    public class ValueChangedEventArgs<TValue> : EventArgs
    {
        public TValue Value { get; private set; }

        public ValueChangedEventArgs(TValue value)
            : base()
        {
            this.Value = value;
        }
    }

    public delegate void ValueChangedEventHandler<TValue>(object sender, ValueChangedEventArgs<TValue> e);

    public delegate void ValueChangedEventHandler<TEventArgs, TValue>(object sender, TEventArgs e)
        where TEventArgs : ValueChangedEventArgs<TValue>;
}
