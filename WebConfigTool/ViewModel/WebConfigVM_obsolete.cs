using System;
using System.Configuration;
using System.Web.Configuration;
using System.Windows;

namespace WebConfigTool.ViewModel
{
    public class WebConfigVM_obsolete : ConfigSectionGroupContainerVM_obsolete
    {
        private Configuration _webConfig = null;

        public static readonly DependencyProperty WebConfigLoadedProperty =
            DependencyProperty.Register("WebConfigLoaded", typeof(bool), typeof(WebConfigVM_obsolete), new PropertyMetadata(false));

        public static readonly DependencyProperty FilePathProperty =
            DependencyProperty.Register("FilePath", typeof(string), typeof(WebConfigVM_obsolete), new PropertyMetadata(""));

        public bool WebConfigLoaded
        {
            get { return (bool)(this.GetValue(WebConfigVM_obsolete.WebConfigLoadedProperty)); }
            private set { this.SetValue(WebConfigVM_obsolete.WebConfigLoadedProperty, value); }
        }

        public string FilePath
        {
            get { return (string)(this.GetValue(WebConfigVM_obsolete.FilePathProperty)); }
            private set { this.SetValue(WebConfigVM_obsolete.FilePathProperty, (value == null) ? "" : value); }
        }

        internal void OpenMappedWebConfiguration(WebConfigurationFileMap webConfigurationFileMap, string virtualPath)
        {
            try
            {
                this._webConfig = System.Web.Configuration.WebConfigurationManager.OpenMappedWebConfiguration(webConfigurationFileMap, virtualPath);
                this.Error.ClearError();
            }
            catch (Exception exc)
            {
                this.Error.SetError("Error loading web.config", exc);
            }

            if (this.Error.HasError)
            {
                base.LoadSectionGroups(null);
                base.LoadSections(null);
                this.WebConfigLoaded = (this._webConfig != null);
                return;
            }

            this.FilePath = this._webConfig.FilePath;

            base.LoadSectionGroups(this._webConfig.SectionGroups);
            base.LoadSections(this._webConfig.Sections);

            this.WebConfigLoaded = true;
        }
    }
}
