﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NBST.Properties {
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
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NBST.Properties.Resources", typeof(Resources).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ];
        ///
        ///line=a(:,1);
        ///rsrp=a(:,2);
        ///rsrq=a(:,3);
        ///rssi=a(:,4);
        ///Ts=datetime(startTime,&apos;ConvertFrom&apos;,&apos;posixtime&apos;, &apos;Format&apos;, &apos;yyyy-MM-dd HH:mm:ss&apos;);
        ///
        ///for i=2:1:length(a)
        ///    Ts=[Ts, datetime(startTime+i,&apos;ConvertFrom&apos;,&apos;posixtime&apos;, &apos;Format&apos;, &apos;yyyy-MM-dd HH:mm:ss&apos;)];
        ///end
        ///
        ///RP_Idx=1;
        ///RP_Sum=rsrp(1);
        ///rpAve=[rsrp(1)];
        ///
        ///for i=2:1:length(rsrp)
        ///    RP_Idx=RP_Idx+1;
        ///    RP_Sum=RP_Sum+rsrp(i);
        ///    rpAve=[rpAve, RP_Sum/RP_Idx];
        ///end
        ///
        ///RQ_Idx=1;
        ///RQ_Sum=rsrq(1);
        ///rqAve=[rsrq(1)];
        ///
        ///for i=2:1:length(rsrq)
        ///     [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string footer {
            get {
                return ResourceManager.GetString("footer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to clc;
        ///clear all;
        ///clf;
        ///close all;.
        /// </summary>
        internal static string header {
            get {
                return ResourceManager.GetString("header", resourceCulture);
            }
        }
    }
}