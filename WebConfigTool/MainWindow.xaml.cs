using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebConfigTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ViewModel.MainWindowVM vm = this.DataContext as ViewModel.MainWindowVM;

            if (vm == null)
                return;

            ViewModel.ConfigurationSectionVM_obsolete section;
            ViewModel.ConfigurationSectionGroupVM_obsolete sg;
            if ((section = vm.WebConfig.OtherConfigSections.FirstOrDefault(el => el.WindowIsOpen)) != null)
                section.FocusWindow();
            else if ((sg = vm.WebConfig.OtherConfigGroups.FirstOrDefault(g => g.WindowIsOpen)) == null)
                return;
            else
                sg.FocusWindow();
            e.Cancel = true;
        }
    }
}
