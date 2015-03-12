using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows;

namespace WebConfigTool.ViewModel
{
    public class WebConfigVM : DependencyObject
    {
        private Configuration _configuration = null;

        public static readonly DependencyProperty FilePathProperty =
            DependencyProperty.Register("FilePath", typeof(string), typeof(WebConfigVM), new PropertyMetadata(""));

        public string FilePath
        {
            get { return (string)(this.GetValue(WebConfigVM.FilePathProperty)); }
            private set { this.SetValue(WebConfigVM.FilePathProperty, value); }
        }

        public static readonly DependencyProperty HasFileProperty =
            DependencyProperty.Register("HasFile", typeof(bool), typeof(WebConfigVM), new PropertyMetadata(false));

        public bool HasFile
        {
            get { return (bool)(this.GetValue(WebConfigVM.HasFileProperty)); }
             private set { this.SetValue(WebConfigVM.HasFileProperty, value); }
        }

        public static readonly DependencyProperty NamespaceDeclaredProperty =
            DependencyProperty.Register("NamespaceDeclared", typeof(bool), typeof(WebConfigVM),
                new PropertyMetadata(false, WebConfigVM.NamespaceDeclared_PropertyChanged));

        public bool NamespaceDeclared
        {
            get { return (bool)(this.GetValue(WebConfigVM.NamespaceDeclaredProperty)); }
            set { this.SetValue(WebConfigVM.NamespaceDeclaredProperty, value); }
        }

        private static void NamespaceDeclared_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WebConfigVM vm = d as WebConfigVM;

            if (vm._configuration != null)
                vm._configuration.NamespaceDeclared = (bool)(e.NewValue);
        }

        private ApplicationSettingsGroupVM _applicationSettingsGroup = new ApplicationSettingsGroupVM();
        private MailSettingsSectionGroupVM _mailSettingsSectionGroup = new MailSettingsSectionGroupVM();
        private NetSectionGroupVM _netSectionGroup = new NetSectionGroupVM();
        private ServiceModelSectionGroupVM _serviceModelSectionGroup = new ServiceModelSectionGroupVM();
        private SystemWebSectionGroupVM _systemWebSectionGroup = new SystemWebSectionGroupVM();
        private ObservableCollection<IItemCategory> _categories = new ObservableCollection<IItemCategory>();
        
        public ApplicationSettingsGroupVM ApplicationSettingsGroup { get { return this._applicationSettingsGroup; } }

        public MailSettingsSectionGroupVM MailSettingsSectionGroup { get { return this._mailSettingsSectionGroup; } }

        public NetSectionGroupVM NetSectionGroup { get { return this._netSectionGroup; } }

        public ServiceModelSectionGroupVM ServiceModelSectionGroup { get { return this._serviceModelSectionGroup; } }

        public SystemWebSectionGroupVM SystemWebSectionGroup { get { return this._systemWebSectionGroup; } }

        public ObservableCollection<IItemCategory> Categories { get { return this._categories; } }

        public WebConfigVM() { }

        public void Load(Configuration configuration)
        {
            this.Categories.Clear();
            this.ApplicationSettingsGroup.Load(null);
            this.MailSettingsSectionGroup.Load(null);
            this.NetSectionGroup.Load(null);
            this.ServiceModelSectionGroup.Load(null);
            this.SystemWebSectionGroup.Load(null);

            this._configuration = configuration;

            if (configuration == null)
                return;

            Collection<ISectionGroup> sectionGroups = new Collection<ISectionGroup>();
            Collection<IConfigSection> sections = new Collection<IConfigSection>();

            foreach (ConfigurationSectionGroup sectionGroup in configuration.SectionGroups)
                this.AddSectionGroup(sectionGroup, sectionGroups);

            foreach (ConfigurationSection section in configuration.Sections)
                this.AddSection(section, sections);

            this.FilePath = configuration.FilePath;
            this.HasFile = configuration.HasFile;
            this.NamespaceDeclared = configuration.NamespaceDeclared;
        }

        private void AddSectionGroup(ConfigurationSectionGroup sectionGroup, Collection<ISectionGroup> sectionGroups)
        {
            if (sectionGroup == null)
                return;

            if (sectionGroup is ApplicationSettingsGroup)
            {
                this.ApplicationSettingsGroup.Load(sectionGroup as ApplicationSettingsGroup);
                return;
            }

            if (sectionGroup is MailSettingsSectionGroup)
            {
                this.MailSettingsSectionGroup.Load(sectionGroup as MailSettingsSectionGroup);
                return;
            }

            if (sectionGroup is NetSectionGroup)
            {
                this.NetSectionGroup.Load(sectionGroup as NetSectionGroup);
                return;
            }

            if (sectionGroup is ServiceModelSectionGroup)
            {
                this.ServiceModelSectionGroup.Load(sectionGroup as ServiceModelSectionGroup);
                return;
            }

            if (sectionGroup is SystemWebSectionGroup)
            {
                this.SystemWebSectionGroup.Load(sectionGroup as SystemWebSectionGroup);
                return;
            }

             //System.Configuration.UserSettingsGroup
             //System.Runtime.Caching.Configuration.CachingSectionGroup
             //System.Runtime.Serialization.Configuration.SerializationSectionGroup
             //System.ServiceModel.Activation.Configuration.ServiceModelActivationSectionGroup
             //System.Transactions.Configuration.TransactionsSectionGroup
             //System.Web.Configuration.ScriptingSectionGroup
             //System.Web.Configuration.ScriptingWebServicesSectionGroup
             //System.Web.Configuration.SystemWebCachingSectionGroup
             //System.Web.Configuration.SystemWebExtensionsSectionGroup
             //System.Xaml.Hosting.Configuration.XamlHostingSectionGroup
             //System.Xml.Serialization.Configuration.SerializationSectionGroup

            sectionGroups.Add(new SectionGroupVM(sectionGroup));
        }

        private void AddSection(ConfigurationSection section, Collection<IConfigSection> sections)
        {
            if (section == null)
                return;

            sections.Add(new SectionVM(section));

            //System.Configuration.AppSettingsSection
            //System.Configuration.ClientSettingsSection
            //System.Configuration.ConnectionStringsSection
            //System.Configuration.DefaultSection
            //System.Configuration.IgnoreSection
            //System.Configuration.ProtectedConfigurationSection
            //System.Configuration.UriSection
            //System.Net.Configuration.AuthenticationModulesSection
            //System.Net.Configuration.ConnectionManagementSection
            //System.Net.Configuration.DefaultProxySection
            //System.Net.Configuration.RequestCachingSection
            //System.Net.Configuration.SettingsSection
            //System.Net.Configuration.SmtpSection
            //System.Net.Configuration.WebRequestModulesSection
            //System.Runtime.Caching.Configuration.MemoryCacheSection
            //System.Runtime.Serialization.Configuration.DataContractSerializerSection
            //System.ServiceModel.Activation.Configuration.DiagnosticSection
            //System.ServiceModel.Activation.Configuration.NetPipeSection
            //System.ServiceModel.Activation.Configuration.NetTcpSection
            //System.ServiceModel.Activities.Tracking.Configuration.TrackingSection
            //System.ServiceModel.Configuration.BehaviorsSection
            //System.ServiceModel.Configuration.BindingsSection
            //System.ServiceModel.Configuration.ClientSection
            //System.ServiceModel.Configuration.ComContractsSection
            //System.ServiceModel.Configuration.CommonBehaviorsSection
            //System.ServiceModel.Configuration.DiagnosticSection
            //System.ServiceModel.Configuration.ExtensionsSection
            //System.ServiceModel.Configuration.ProtocolMappingSection
            //System.ServiceModel.Configuration.ServiceHostingEnvironmentSection
            //System.ServiceModel.Configuration.ServicesSection
            //System.ServiceModel.Configuration.StandardEndpointsSection
            //System.ServiceModel.Routing.Configuration.RoutingSection
            //System.Transactions.Configuration.DefaultSettingsSection
            //System.Transactions.Configuration.MachineSettingsSection
            //System.Web.Configuration.AnonymousIdentificationSection
            //System.Web.Configuration.AuthenticationSection
            //System.Web.Configuration.AuthorizationSection
            //System.Web.Configuration.CacheSection
            //System.Web.Configuration.ClientTargetSection
            //System.Web.Configuration.CompilationSection
            //System.Web.Configuration.CustomErrorsSection
            //System.Web.Configuration.DeploymentSection
            //System.Web.Configuration.FullTrustAssembliesSection
            //System.Web.Configuration.GlobalizationSection
            //System.Web.Configuration.HealthMonitoringSection
            //System.Web.Configuration.HostingEnvironmentSection
            //System.Web.Configuration.HttpCookiesSection
            //System.Web.Configuration.HttpHandlersSection
            //System.Web.Configuration.HttpModulesSection
            //System.Web.Configuration.HttpRuntimeSection
            //System.Web.Configuration.IdentitySection
            //System.Web.Configuration.MachineKeySection
            //System.Web.Configuration.MembershipSection
            //System.Web.Configuration.OutputCacheSection
            //System.Web.Configuration.OutputCacheSettingsSection
            //System.Web.Configuration.PagesSection
            //System.Web.Configuration.PartialTrustVisibleAssembliesSection
            //System.Web.Configuration.ProcessModelSection
            //System.Web.Configuration.ProfileSection
            //System.Web.Configuration.ProtocolsSection
            //System.Web.Configuration.RoleManagerSection
            //System.Web.Configuration.ScriptingAuthenticationServiceSection
            //System.Web.Configuration.ScriptingJsonSerializationSection
            //System.Web.Configuration.ScriptingProfileServiceSection
            //System.Web.Configuration.ScriptingRoleServiceSection
            //System.Web.Configuration.ScriptingScriptResourceHandlerSection
            //System.Web.Configuration.SecurityPolicySection
            //System.Web.Configuration.SessionPageStateSection
            //System.Web.Configuration.SessionStateSection
            //System.Web.Configuration.SiteMapSection
            //System.Web.Configuration.SqlCacheDependencySection
            //System.Web.Configuration.TraceSection
            //System.Web.Configuration.TrustSection
            //System.Web.Configuration.UrlMappingsSection
            //System.Web.Configuration.WebControlsSection
            //System.Web.Configuration.WebPartsSection
            //System.Web.Configuration.XhtmlConformanceSection
            //System.Web.Mobile.DeviceFiltersSection
            //System.Web.Services.Configuration.WebServicesSection
            //System.Web.UI.MobileControls.MobileControlsSection
            //System.Windows.Forms.WindowsFormsSection
            //System.Workflow.Activities.Configuration.ActiveDirectoryRoleFactoryConfiguration
            //System.Workflow.Activities.ExternalDataExchangeServiceSection
            //System.Workflow.Runtime.Configuration.WorkflowRuntimeSection
            //System.Xaml.Hosting.Configuration.XamlHostingSection
            //System.Xml.Serialization.Configuration.DateTimeSerializationSection
            //System.Xml.Serialization.Configuration.SchemaImporterExtensionsSection
            //System.Xml.Serialization.Configuration.XmlSerializerSection
        }
    }
}
