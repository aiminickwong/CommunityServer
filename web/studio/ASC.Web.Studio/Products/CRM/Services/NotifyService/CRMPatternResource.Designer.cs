﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASC.Web.CRM.Services.NotifyService {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class CRMPatternResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CRMPatternResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ASC.Web.CRM.Services.NotifyService.CRMPatternResource", typeof(CRMPatternResource).Assembly);
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
        ///   Looks up a localized string similar to h1. New Event Added to &quot;$EntityTitle&quot;:&quot;${__VirtualRootPath}/${EntityRelativeURL}&quot;            
        ///$__DateTime &quot;$__AuthorName&quot;:&quot;$__AuthorUrl&quot; has added a new event to &quot;$EntityTitle&quot;:&quot;${__VirtualRootPath}/${EntityRelativeURL}&quot;:                                                               
        ///          $AdditionalData.get_item(&quot;EventContent&quot;)
        ///
        ///          #foreach($fileInfo in $AdditionalData.get_item(&quot;Files&quot;).Keys)
        ///
        ///          #beforeall
        ///
        ///          ----------------------------------------
        ///
        ///          #each
        ///        /// [rest of string was truncated]&quot;;.
        /// </summary>
        public static string pattern_AddRelationshipEvent {
            get {
                return ResourceManager.GetString("pattern_AddRelationshipEvent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to h1. CRM. New Contact Created Using the &apos;Website Contact Form&apos; &quot;$EntityTitle&quot;:&quot;${__VirtualRootPath}/products/crm/default.aspx?id=$EntityID&quot;
        ///
        ///$__DateTime A new contact has been created using the &apos;Website Contact Form&apos; &quot;$EntityTitle&quot;:&quot;${__VirtualRootPath}/products/crm/default.aspx?id=$EntityID&quot;
        ///
        ///Contact information:
        ///
        ///#foreach($contactInfo in $AdditionalData.Keys)
        ///#each
        ///
        ///$contactInfo: $AdditionalData.get_item($contactInfo)
        ///
        ///#end
        ///
        ///
        ///^You receive this email because you are a registered user of the &quot;$ [rest of string was truncated]&quot;;.
        /// </summary>
        public static string pattern_CreateNewContact {
            get {
                return ResourceManager.GetString("pattern_CreateNewContact", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to h1. CRM. Data Export Successfully Completed
        ///
        ///Please, follow this link to download the archive: &quot;exportdata.zip&quot;:&quot;${EntityRelativeURL}&quot;
        ///
        ///*Note*: this link is valid for 24 hours only.
        ///
        ///If you have any questions or need assistance please feel free to contact us at &quot;support@teamlab.com&quot;:&quot;mailto:support@teamlab.com&quot;
        ///
        ///Best regards,
        ///TeamLab Support Team
        ///&quot;www.teamlab.com&quot;:&quot;http://teamlab.com/&quot;
        ///
        ///^You receive this email because you are a registered user of the &quot;${__VirtualRootPath}&quot;:&quot;${__VirtualRootPath}&quot; [rest of string was truncated]&quot;;.
        /// </summary>
        public static string pattern_ExportCompleted {
            get {
                return ResourceManager.GetString("pattern_ExportCompleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to h1. CRM. Data Import Successfully Completed
        ///
        ///Go to the &quot;${EntityListTitle}&quot;:&quot;$__VirtualRootPath/${EntityListRelativeURL}&quot; list.
        ///
        ///If you have any questions or need assistance please feel free to contact us at &quot;support@teamlab.com&quot;:&quot;mailto:support@teamlab.com&quot;
        ///
        ///Best regards,
        ///TeamLab Support Team
        ///&quot;www.teamlab.com&quot;:&quot;http://teamlab.com/&quot;
        ///
        ///^You receive this email because you are a registered user of the &quot;${__VirtualRootPath}&quot;:&quot;${__VirtualRootPath}&quot; portal. To change the notification type, please manage  [rest of string was truncated]&quot;;.
        /// </summary>
        public static string pattern_ImportCompleted {
            get {
                return ResourceManager.GetString("pattern_ImportCompleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to h1.You were Appointed Responsible for the Opportunity: &quot;$EntityTitle&quot;:&quot;${__VirtualRootPath}/products/crm/deals.aspx?id=$EntityID&quot;
        ///
        ///$__DateTime &quot;$__AuthorName&quot;:&quot;$__AuthorUrl&quot; has appointed you responsible for the opportunity: $EntityTitle .
        ///
        ///#if($AdditionalData.get_item(&quot;OpportunityDescription&quot;)&amp;&amp;$AdditionalData.get_item(&quot;OpportunityDescription&quot;)!=&quot;&quot;)
        ///
        ///Opportunity description:
        ///$AdditionalData.get_item(&quot;OpportunityDescription&quot;)
        ///#end
        ///
        ///^You receive this email because you are a registered user of the &quot; [rest of string was truncated]&quot;;.
        /// </summary>
        public static string pattern_ResponsibleForOpportunity {
            get {
                return ResourceManager.GetString("pattern_ResponsibleForOpportunity", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to h1.Task Assigned to You: $EntityTitle
        ///
        ///$__DateTime &quot;$__AuthorName&quot;:&quot;$__AuthorUrl&quot; has appointed you responsible for the task: $EntityTitle.
        ///#if($AdditionalData.get_item(&quot;TaskCategory&quot;))
        ///
        ///Task category:  $AdditionalData.get_item(&quot;TaskCategory&quot;)
        ///#end
        ///#if($AdditionalData.get_item(&quot;ContactRelativeUrl&quot;))
        ///
        ///Link with contact: &quot;$AdditionalData.get_item(&quot;ContactTitle&quot;)&quot;:&quot;${__VirtualRootPath}/$AdditionalData.get_item(&quot;ContactRelativeUrl&quot;)&quot;
        ///#end
        ///#if($AdditionalData.get_item(&quot;CaseRelativeUrl&quot;))
        ///
        ///Link with  [rest of string was truncated]&quot;;.
        /// </summary>
        public static string pattern_ResponsibleForTask {
            get {
                return ResourceManager.GetString("pattern_ResponsibleForTask", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to h1.Access Granted to &quot;$EntityTitle&quot;:&quot;${__VirtualRootPath}/${EntityRelativeURL}&quot;
        ///
        ///$__DateTime &quot;$__AuthorName&quot;:&quot;$__AuthorUrl&quot; has granted you the access to &quot;$EntityTitle&quot;:&quot;${__VirtualRootPath}/${EntityRelativeURL}&quot;.
        ///
        ///
        ///^You receive this email because you are a registered user of the &quot;${__VirtualRootPath}&quot;:&quot;${__VirtualRootPath}&quot; portal. If you don&apos;t want to receive the notifications about the access granted to the CRM items, please manage your &quot;subscription settings&quot;:&quot;$RecipientSubscriptionConfigURL&quot;.^.
        /// </summary>
        public static string pattern_SetAccess {
            get {
                return ResourceManager.GetString("pattern_SetAccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to h1. Оповещение о наступление задачи $EntityTitle
        ///#if($AdditionalData.get_item(&quot;TaskCategory&quot;))
        ///
        ///Task category:  $AdditionalData.get_item(&quot;TaskCategory&quot;)
        ///#end
        ///#if($AdditionalData.get_item(&quot;ContactRelativeUrl&quot;))
        ///
        ///Link with contact: &quot;$AdditionalData.get_item(&quot;ContactTitle&quot;)&quot;:&quot;${__VirtualRootPath}/$AdditionalData.get_item(&quot;ContactRelativeUrl&quot;)&quot;
        ///#end
        ///#if($AdditionalData.get_item(&quot;CaseRelativeUrl&quot;))
        ///
        ///Link with case: &quot;$AdditionalData.get_item(&quot;CaseTitle&quot;)&quot;:&quot;${__VirtualRootPath}/$AdditionalData.get_item( [rest of string was truncated]&quot;;.
        /// </summary>
        public static string pattern_TaskReminder {
            get {
                return ResourceManager.GetString("pattern_TaskReminder", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;patterns&gt;
        ///  &lt;formatter type=&quot;ASC.Notify.Patterns.NVelocityPatternFormatter, ASC.Common&quot; /&gt;
        ///
        ///  &lt;!--Export is completed--&gt;
        ///  &lt;pattern id=&quot;ExportCompleted&quot; sender=&quot;email.sender&quot;&gt;
        ///    &lt;subject resource=&quot;|subject_ExportCompleted|ASC.Web.CRM.Services.NotifyService.CRMPatternResource,ASC.Web.CRM&quot; /&gt;
        ///    &lt;body styler=&quot;ASC.Notify.Textile.TextileStyler,ASC.Notify.Textile&quot; resource=&quot;|pattern_ExportCompleted|ASC.Web.CRM.Services.NotifyService.CRMPatternResource,ASC.Web.CRM&quot; /&gt;
        ///  &lt;/pattern&gt;
        ///  &lt;pattern id=&quot;Expor [rest of string was truncated]&quot;;.
        /// </summary>
        public static string patterns {
            get {
                return ResourceManager.GetString("patterns", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CRM. New Event Added to $EntityTitle.
        /// </summary>
        public static string subject_AddRelationshipEvent {
            get {
                return ResourceManager.GetString("subject_AddRelationshipEvent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CRM. New Contact Created Using &apos;Website Contact Form&apos;.
        /// </summary>
        public static string subject_CreateNewContact {
            get {
                return ResourceManager.GetString("subject_CreateNewContact", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CRM. Data Export Successfully Completed.
        /// </summary>
        public static string subject_ExportCompleted {
            get {
                return ResourceManager.GetString("subject_ExportCompleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CRM. Data Import Successfully Completed.
        /// </summary>
        public static string subject_ImportCompleted {
            get {
                return ResourceManager.GetString("subject_ImportCompleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CRM. You were Appointed as a Responsible Person for the Opportunity: $EntityTitle.
        /// </summary>
        public static string subject_ResponsibleForOpportunity {
            get {
                return ResourceManager.GetString("subject_ResponsibleForOpportunity", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CRM. Task Assigned to You: $EntityTitle.
        /// </summary>
        public static string subject_ResponsibleForTask {
            get {
                return ResourceManager.GetString("subject_ResponsibleForTask", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CRM. Access Granted to $EntityTitle.
        /// </summary>
        public static string subject_SetAccess {
            get {
                return ResourceManager.GetString("subject_SetAccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CRM. Напоминание о задаче: $EntityTitle.
        /// </summary>
        public static string subject_TaskReminder {
            get {
                return ResourceManager.GetString("subject_TaskReminder", resourceCulture);
            }
        }
    }
}
