﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tql.Plugins.Azure {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Labels {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Labels() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Tql.Plugins.Azure.Labels", typeof(Labels).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Azure - {0}.
        /// </summary>
        public static string AzureApi_AuthenticationResourceName {
            get {
                return ResourceManager.GetString("AzureApi_AuthenticationResourceName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to connect to Azure - {0}. Click here to reconnect..
        /// </summary>
        public static string AzureApi_UnableToConnect {
            get {
                return ResourceManager.GetString("AzureApi_UnableToConnect", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Azure Portal.
        /// </summary>
        public static string AzurePlugin_Title {
            get {
                return ResourceManager.GetString("AzurePlugin_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Add.
        /// </summary>
        public static string Button_Add {
            get {
                return ResourceManager.GetString("Button_Add", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Delete.
        /// </summary>
        public static string Button_Delete {
            get {
                return ResourceManager.GetString("Button_Delete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Update.
        /// </summary>
        public static string Button_Update {
            get {
                return ResourceManager.GetString("Button_Update", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Connections.
        /// </summary>
        public static string ConfigurationControl_Connections {
            get {
                return ResourceManager.GetString("ConfigurationControl_Connections", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to General.
        /// </summary>
        public static string ConfigurationControl_General {
            get {
                return ResourceManager.GetString("ConfigurationControl_General", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Name.
        /// </summary>
        public static string ConfigurationControl_Name {
            get {
                return ResourceManager.GetString("ConfigurationControl_Name", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Name:.
        /// </summary>
        public static string ConfigurationControl_NameLabel {
            get {
                return ResourceManager.GetString("ConfigurationControl_NameLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tenant ID.
        /// </summary>
        public static string ConfigurationControl_TenantID {
            get {
                return ResourceManager.GetString("ConfigurationControl_TenantID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You can retrieve the Azure Portal Tenant ID by searching for the Microsoft Entra ID in Azure Portal, or click on your account, click Switch directory and copy the Directory ID. The Directory ID is your Tenant ID..
        /// </summary>
        public static string ConfigurationControl_TenantIDHelpText {
            get {
                return ResourceManager.GetString("ConfigurationControl_TenantIDHelpText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tenant ID:.
        /// </summary>
        public static string ConfigurationControl_TenantIDLabel {
            get {
                return ResourceManager.GetString("ConfigurationControl_TenantIDLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Azure Portal.
        /// </summary>
        public static string PortalsType_Label {
            get {
                return ResourceManager.GetString("PortalsType_Label", resourceCulture);
            }
        }
    }
}
