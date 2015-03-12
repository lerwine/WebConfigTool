using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAppData
{
    public class ProcessValueEventArgs<TValue> : EventArgs
    {
        public TValue Value { get; set; }

        public int Index { get; set; }

        public ProcessValueEventArgs() : this(default(TValue)) { }

        public ProcessValueEventArgs(TValue initialValue) : this(initialValue, -1) { }

        public ProcessValueEventArgs(TValue initialValue, int index)
            : base()
        {
            this.Value = initialValue;
            this.Index = index;
        }
    }

    public class ProcessValueEventArgs<TValue, TItem> : ProcessValueEventArgs<TValue>
    {
        public TItem Item { get; set; }

        public ProcessValueEventArgs() : this(default(TValue)) { }

        public ProcessValueEventArgs(TValue initialValue) : this(initialValue, default(TItem)) { }

        public ProcessValueEventArgs(TValue initialValue, TItem item)
            : base(initialValue)
        {
            this.Item = item;
        }

        public ProcessValueEventArgs(TValue initialValue, TItem item, int index)
            : base(initialValue, index)
        {
            this.Item = item;
        }
    }

    /// <summary>
    /// Represents the method that will handle a ProcessValue event.
    /// </summary>
    /// <typeparam name="TValue">Type of value to process.</typeparam>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Arguments that contains the event data.</param>
    [Serializable]
    public delegate void ProcessValueEventHandler<TValue>(object sender, ProcessValueEventArgs<TValue> e);

    /// <summary>
    /// Represents the method that will handle a ProcessValue event.
    /// </summary>
    /// <typeparam name="T">Type of value to process.</typeparam>
    /// <typeparam name="TItem">Type of item</typeparam>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Arguments that contains the event data.</param>
    [Serializable]
    public delegate void ProcessValueEventHandler<TValue, TItem>(object sender, ProcessValueEventArgs<TValue, TItem> e);
}
