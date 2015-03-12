using System;

namespace WebConfigTool.ViewModel
{
    public class SelectedIndexChangedEventArgs : EventArgs
    {
        public int SelectedIndex { get; private set; }

        public SelectedIndexChangedEventArgs(int selectedIndex)
            : base()
        {
            this.SelectedIndex = selectedIndex;
        }
    }

    public delegate void SelectedIndexChangedEventHandler(object sender, SelectedIndexChangedEventArgs e);
}
