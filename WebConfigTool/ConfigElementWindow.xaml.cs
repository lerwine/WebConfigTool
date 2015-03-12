﻿using System;
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
using System.Windows.Shapes;

namespace WebConfigTool
{
    /// <summary>
    /// Interaction logic for ConfigElementWindow.xaml
    /// </summary>
    public partial class ConfigElementWindow : Window
    {
        public ConfigElementWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ViewModel.ConfigElementVM_obsolete vm = this.DataContext as ViewModel.ConfigElementVM_obsolete;

            ViewModel.ConfigElementVM_obsolete shown;
            if (vm == null || (shown = vm.Elements.FirstOrDefault(el => el.WindowIsOpen)) == null)
                return;

            shown.FocusWindow();
            e.Cancel = true;
        }
    }
}
