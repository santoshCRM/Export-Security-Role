using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleDocumenter
{
    public partial class RoleDocumenterCtl
    {
        private List<string> PrvExclusionList = new List<string> { "prvAppendActivity", "prvAppendToActivity", "prvDeleteActivity", "prvAssignActivity", "prvShareActivity" };

        private List<string> TableExlusionList = new List<string> { "task", "appointment", "fax", "letter","customeraddress","canvasappextendedmetadata" };

        private List<string> CustomisationList = new List<string> { "displaystring", "isvconfig", "ribboncommand", "ribboncontectgroup", "ribboncustomization",
            "ribbondiff","ribbonrule","ribbontabtocommandmap","sitemap","teamtemplate"};

        private List<string> TablePrefixes = new List<string> { "prvAppend", "prvAppendTo", "prvDelete", "prvAssign", "prvShare", "prvRead", "prvCreate", "prvWrite" };

        private List<MiscPriv> MiscPrivs = new List<MiscPriv>{ new MiscPriv("prvPublishRSReport","Add Reporting Services Reports"), //Core
            new MiscPriv("prvBulkDelete","Bulk Delete"), new MiscPriv("prvRestoreSqlEncryptionKey","Manage Data Encryption key - Activate"),
            new MiscPriv("prvChangeSqlEncryptionKey","Manage Data Encryption key - Change"), new MiscPriv("prvReadSqlEncryptionKey", "Manage Data Encryption key - Read"),
            new MiscPriv("prvDeleteAuditPartitions", "Delete Audit Partitions"), new MiscPriv("prvAdminFilter","Manage User Synchronization Filters"),
            new MiscPriv("prvPromoteToAdmin", "Promote User to Microsoft Dynamics 365 Administrator Role"),
            new MiscPriv("prvPublishDuplicateRule","Publish Duplicate Detection Rules" ), new MiscPriv("prvCreateOrgEmailTemplates","Publish E-mail Templates"),
            new MiscPriv("prvPublishOrgMailMergeTemplate", "Publish Mail Merge Templates to Organization"),new MiscPriv("prvPublishOrgReport","Publish Reports"),
            new MiscPriv("prvReadRecordAuditHistory","View Audit History"), new MiscPriv("prvDeleteRecordChangeHistory","Delete Audit Record Change History"),
            new MiscPriv("prvReadAuditPartitions","View Audit Partitions"), new MiscPriv("prvReadAuditSummary","View Audit Summary" ), new MiscPriv("prvConfigureSharePoint", "Run SharePoint Integration Wizard"),
            new MiscPriv("prvTurnDevErrorsOnOff","Turn On Tracing"),
            new MiscPriv("prvConfigureInternetMarketing","Configure Internet Marketing module"),new MiscPriv("prvAllowQuickCampaign","Create Quick Campaign"),new MiscPriv("prvUseInternetMarketing","Use internet marketing module"), //Marketing
            new MiscPriv("prvOverridePriceEngineInvoice", "Override Invoice Pricing"), new MiscPriv("prvOverridePriceEngineOrder","Override Order Pricing"),//Sales
            new MiscPriv("prvOverridePriceEngineQuote", "Override Quote Pricing"), new MiscPriv("prvOverridePriceEngineOpportunity","Override Opportunity Pricing"),new MiscPriv("prvQOIOverrideDelete","Override Quote Order Invoice Delete"),
            new MiscPriv("prvPublishArticle","Publish Articles"),new MiscPriv("prvApproveKnowledgeArticle","Approve Knowledge Articles"),new MiscPriv("prvPublishKnowledgeArticle","Publish Knowledge Articles"), // service
            new MiscPriv("prvGoMobile","Dynamics 365 for mobile"), new MiscPriv("prvExportToExcel","Export to Excel"), new MiscPriv("prvDocumentGeneration", "Document Generation"),
            new MiscPriv("prvGoOffline", "Go Offline in Outlook"),new MiscPriv("prvMailMerge", "Merge"), new MiscPriv("prvPrint", "Print"),
            new MiscPriv("prvSyncToOutlook","Sync To Outlook"), new MiscPriv("prvUseOfficeApps", "Use Dynamics 365 App for Outlook"), new MiscPriv("prvActOnBehalfOfAnotherUser","Act on Behalf of Another User"),
            new MiscPriv("prvApproveRejectEmailAddress", "Approve Email Addresses for Users or Queues"), new MiscPriv("prvAssignManager", "Assign manager for a user"),
            new MiscPriv("prvAssignPosition", "Assign position for a user"), new MiscPriv("prvAssignTerritory", "Assign Territory to User"), new MiscPriv("prvBulkEdit","Bulk Edit"),
            new MiscPriv("prvWriteHierarchicalSecurityConfiguration", "Change Hierarchy Security Settings"), new MiscPriv("prvDisableBusinessUnit", "Enable or Disable Business Unit"),
            new MiscPriv("prvDisableUser", "Enable or Disable User"), new MiscPriv("prvMerge", "Merge"), new MiscPriv("prvOverrideCreatedOnCreatedBy","Override Created on or Created by for Records during Data Import"),
            new MiscPriv("prvReparentBusinessUnit", "Reparent Business unit"), new MiscPriv("prvReparentTeam", "Reparent Team"),new MiscPriv("prvReparentUser","Reparent user"),
            new MiscPriv("prvSendAsUser", "Send Email as Another User"), new MiscPriv("prvSendInviteForLive", "Send Invitation"), new MiscPriv("prvWriteBusinessClosureCalendar","Update Business Closures"),
            new MiscPriv("prvAddressBook", "Dynamics 365 Address Book"),new MiscPriv("prvLanguageSettings", "Language Settings"), new MiscPriv("prvRollupGoal","Perform in sync rollups on goals"), new MiscPriv("prvReadLicense","Read License info"),
            new MiscPriv("prvWebMailMerge","Web Mail Merge"),
            new MiscPriv("prvBrowseAvailability", "Browse Availability"), new MiscPriv("prvControlDecrementTerm","Control Decrement Terms"), new MiscPriv("prvCreateOwnCalendar", "Create own calendar"),
            new MiscPriv("prvDeleteOwnCalendar", "Delete own calendar"),new MiscPriv("prvReadOwnCalendar", "Read own Calendar"), new MiscPriv("prvSearchAvailability", "Search Availability"),
            new MiscPriv("prvWriteHolidayScheduleCalendar", "Update Holiday Schedules"), new MiscPriv("prvWriteOwnCalendar","Write own calendar"),
            new MiscPriv("prvActivateBusinessProcessFlow", "Activate Business Process Flows"    ), new MiscPriv("prvActivateBusinessRule","Activate Business Rules" ), // Customisation
            new MiscPriv("prvActivateSynchronousWorkflow","Activate Real-time Processes" ), new MiscPriv("prvConfigureYammer", "Configure Yammer"), new MiscPriv("prvWorkflowExecution", "Execute Workflow Job"),
            new MiscPriv("prvExportCustomization","Export Customisations"), new MiscPriv("prvImportCustomization", "Import Customizations"), new MiscPriv("prvISVExtensions", "ISV Extensions"),
            new MiscPriv("prvLearningPath", "Learning Path Authoring"), new MiscPriv("prvAppendConstraint","Modify Customization constraints" ), new MiscPriv("prvPublishCustomization", "Publish Customisations"),
            new MiscPriv("prvRetrieveMultipleSocialInsights", "Retrieve Multiple Social Insights"), new MiscPriv("prvFlow", "Run Flows")
        };

        private readonly List<SecurityTab> D365Tabs = new List<SecurityTab> { CoreTab, MarketingTab, SalesTab, ServiceTab, BusManTab, ServManTab, MisEntTab, CustomisationTab };
        private static SecurityTab CoreTab = new SecurityTab
        {
            tables = new List<string> { "account", "aciviewmapper", "actioncard", "actioncardusersettings", "activitypointer","advancedsimilarityrule",
            "businessunitnewsarticle","applicationfile", "category", "connection","connectionrole","contact", "customerrelationship",
            "import", "importmap","dataperformance","documentindex","emailsignature","template","feedback","postfollow","importfile",
            "interactionforemail","languagelocale","lead","mailmergetemplate","mobileofflineprofile","annotation","opportunity",
            "post","queue","relationshiprole","report","savedorginsightsconfiguration","sharepointsite","socialprofile","subject",
            "suggestioncardtemplate","syncerror","textanalyticsentitymapping","tracelog","userform","userqueryvisualization","userentityinstancedata","userentityuisettings",
            "usermapping","webwizard", "wizardaccessprivilege","wizardpage", "sharepointdocumentlocation","recommendeddocument", "customeropportunityrole","userquery"},
            TabName = "Core",
            miscPrivs = new List<string> { "prvPublishRSReport","prvBulkDelete","prvDeleteAuditPartitions","prvDeleteRecordChangeHistory",
                "prvRestoreSqlEncryptionKey","prvChangeSqlEncryptionKey","prvReadSqlEncryptionKey","prvAdminFilter","prvPromoteToAdmin",
                "prvPublishDuplicateRule","prvCreateOrgEmailTemplates","prvPublishOrgMailMergeTemplate","prvPublishOrgReport",
            "prvReadRecordAuditHistory", "prvReadAuditPartitions","prvReadAuditSummary", "prvConfigureSharePoint", "prvTurnDevErrorsOnOff" }
        };

        private static SecurityTab MarketingTab = new SecurityTab
        {
            tables = new List<string> { "campaign", "list" },
            TabName = "Marketing",
            miscPrivs = new List<string> { "prvDocumentGeneration", "prvPublishKnowledgeArticle", "prvPublishArticle" }



        };

        private static SecurityTab SalesTab = new SecurityTab
        {
            tables = new List<string> {"competitor", "invoice", "salesorder", "product", "dynamicpropertyassociation","dynamicpropertyinstance",
                "dynamicpropertyoptionsetitem", "quote", "salesliterature", "territory" },
            TabName = "Sales",
            miscPrivs = new List<string>{ "prvOverridePriceEngineInvoice", "prvOverridePriceEngineOrder", "prvOverridePriceEngineQuote",
                "prvOverridePriceEngineOpportunity","prvQOIOverrideDelete" }

        };

        private static SecurityTab ServiceTab = new SecurityTab
        {
            tables = new List<string> { "kbarticle","kbarticletemplate", "bookableresource", "bookableresourcebooking",
            "bookableresourcebookingheader", "bookableresourcecategory", "bookableresourcecategoryassn", "bookableresourcecharacteristic", "bookableresourcegroup",
            "bookingstatus", "incident","characteristic","knowledgearticle", "msdyn_knowledgearticleimage", "knowledgearticleviews","msdyn_knowledgeinteractioninsight",
            "msdyn_knowledgesearchinsight", "ratingmodel", "ratingvalue"
            },
            TabName = "Service",
            miscPrivs = new List<string> { "prvApproveKnowledgeArticle", "prvPublishKnowledgeArticle", "prvPublishArticle" }
        };

        private static SecurityTab BusManTab = new SecurityTab
        {
            tables = new List<string> { "businessunit","channelpropertygroup", "documenttemplate", "emailserverprofile",
            "fieldsecurityprofile", "principalobjectattributeaccess", "goal", "mailbox", "mailboxtrackingfolder",
            "organization", "personaldocumenttemplate","position","principalsyncattributemap", "goalrollupquery", "role","syncattributemappingprofile",
            "team", "systemuser", "usersettings"
            },
            TabName = "Business Mangement",
            miscPrivs = new List<string> { "prvConfigureInternetMarketing", "prvUseOfficeApps", "prvGoMobile","prvExportToExcel","prvGoOffline",
            "prvMailMerge", "prvSyncToOutlook", "prvUseOfficeApps","prvActOnBehalfOfAnotherUser","prvApproveRejectEmailAddress","prvAssignManager",
            "prvAssignPosition", "prvAssignTerritory", "prvBulkEdit", "prvWriteHierarchicalSecurityConfiguration","prvDisableBusinessUnit","prvDisableUser",
            "prvLanguageSettings", "prvMailMerge","prvOverrideCreatedOnCreatedBy","prvRollupGoal","prvReadLicense","prvReparentBusinessUnit","prvReparentTeam","prvReparentUser"

            }
        };

        private static SecurityTab ServManTab = new SecurityTab
        {
            tables = new List<string> { "calendar","entitlement", "entitlementtemplate", "equipment",
            "msdyn_knowledgearticletemplate", "knowledgebaserecord", "convertrule", "routingrule", "service",
            "site", "sla","slakpiinstance"
            },
            TabName = "Service Mangement",
            miscPrivs = new List<string> { "prvBrowseAvailability", "prvControlDecrementTerm", "prvCreateOwnCalendar","prvDeleteOwnCalendar","prvReadOwnCalendar",
            "prvSearchAvailability", "prvWriteHolidayScheduleCalendar", "prvWriteOwnCalendar"
            }
        };
        private static readonly SecurityTab CustomisationTab = new SecurityTab
        {
            tables = new List<string> { "appconfigmaster","attributemap", "callbackregistration", "canvasapp",
            "customcontrol", "customcontroldefaultconfig", "customcontrolresource", "Customizations", "entity",
            "entitykey", "entitymap","expanderevent","attribute","hierarchyrule","importjob","appmodule","optionset",
            "pluginassembly", "plugintracelog","plugintype","workflow","complexcontrol", "processsession","publisher",
            "relationship", "sdkmessage", "sdkmessageprocessingstep", "sdkmessageprocessingstepimage", "sdkmessageprocessingstepsecureconfig",
            "serviceendpoint","solution","systemapplicationmetadata", "savedqueryvisualization", "systemform",
            "asyncoperation","theme","userapplicationmetadata","savedquery","entitydatasource","webresource"

            },
            TabName = "Customisation",
            miscPrivs = new List<string> { "prvActivateBusinessProcessFlow", "prvActivateBusinessRule", "prvActivateSynchronousWorkflow","prvConfigureYammer","prvWorkflowExecution",
            "prvExportCustomization", "prvImportCustomization", "prvISVExtensions","prvLearningPath","prvAppendConstraint","prvPublishCustomization",
            "prvRetrieveMultipleSocialInsights", "prvFlow"
            }
        };

        private static SecurityTab MisEntTab = new SecurityTab
        {
            tables = new List<string> { "bookableresourcebookingexchangesyncidmapping","delveactionhub", "entityanalyticsconfig", "offlinecommanddefinition",
            "entitydataprovider"
            },
            TabName = "Missing Entities",
            miscPrivs = new List<string> { }
        };
    }
}
