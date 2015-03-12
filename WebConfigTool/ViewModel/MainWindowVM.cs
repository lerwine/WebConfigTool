using Microsoft.Win32;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using System.Windows;

namespace WebConfigTool.ViewModel
{
    public class MainWindowVM : DependencyObject
    {
        private RelayCommand _openConfigFileCommand = null;
        private VirtualDirectoryMapping _virtualDirectoryMapping = null;
        private WebConfigurationFileMap _webConfigurationFileMap = null;
        private WebConfigVM_obsolete _webConfig = new WebConfigVM_obsolete();

        public static readonly DependencyProperty WebAppRootPathProperty =
            DependencyProperty.Register("WebAppRootPath", typeof(string), typeof(MainWindowVM),
                new PropertyMetadata(""));


        public RelayCommand OpenConfigFileCommand
        {
            get
            {
                if (this._openConfigFileCommand == null) this._openConfigFileCommand = new RelayCommand((object o) =>
                {
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.AddExtension = true;
                    dlg.Filter = "Web Config Files (web.config)|web.config|Config files (*.config)|*.config|All Files (*.*)|*.*";
                    dlg.CheckFileExists = false;
                    dlg.CheckPathExists = true;
                    dlg.DefaultExt = ".config";
                    dlg.AddExtension = true;
                    dlg.Multiselect = false;
                    dlg.Title = "Open Root Web Config";
                    dlg.FileName = "web.config";
                    bool? dr = dlg.ShowDialog();
                    if (!dr.HasValue || !dr.Value)
                        return;

                    this.WebAppRootPath = Path.GetDirectoryName(dlg.FileName);
                    this._virtualDirectoryMapping = new VirtualDirectoryMapping(this.WebAppRootPath, true);
                    this._webConfigurationFileMap = new WebConfigurationFileMap();
                    this._webConfigurationFileMap.VirtualDirectories.Add("/", this._virtualDirectoryMapping);
                    this.WebConfig.OpenMappedWebConfiguration(this._webConfigurationFileMap, "/");
                });

                return this._openConfigFileCommand;
            }
        }

        public string WebAppRootPath
        {
            get { return (string)(this.GetValue(MainWindowVM.WebAppRootPathProperty)); }
            private set { this.SetValue(MainWindowVM.WebAppRootPathProperty, value); }
        }

        public WebConfigVM_obsolete WebConfig
        {
            get { return this._webConfig; }
        }
    }
}