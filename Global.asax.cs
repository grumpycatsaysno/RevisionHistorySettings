using SitefinityWebApp.RevisionHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Versioning;

namespace SitefinityWebApp
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            SystemManager.ApplicationStart += SystemManager_ApplicationStart;
        }

        protected void SystemManager_ApplicationStart(object sender, EventArgs e)
        {
            Res.RegisterResource<RevisionHistoryResources>();

            Config.RegisterSection<RevisionHistoryConfig>();

            ConfigManager.Executed += new EventHandler<ExecutedEventArgs>(ConfigManager_Executed);

            PageManager.Executing += new EventHandler<Telerik.Sitefinity.Data.ExecutingEventArgs>(PageManager_Executing);
        }

        private void ConfigManager_Executed(object sender, ExecutedEventArgs args)
        {
            if (args != null)
            {
                var commandArgs = args.CommandArguments as RevisionHistoryConfig;
                if (commandArgs != null)
                {
                    int revisionsNumber = 0;
                    int.TryParse(commandArgs.NumberOfRevisionsToKeep, out revisionsNumber);

                    int daysToKeepHistory = 1;
                    int.TryParse(commandArgs.NumberOfDaysToKeepHistory, out daysToKeepHistory);

                    var manager = PageManager.GetManager();
                    var pageDataList = manager.GetPageDataList();

                    foreach (var data in pageDataList)
                    {
                        if ((revisionsNumber > 0) && (daysToKeepHistory <= 1))
                        {
                            RevisionHistoryHelper.TruncateVersionsByNumber(revisionsNumber, data);
                        }
                        else if ((revisionsNumber == 0) && (daysToKeepHistory > 1))
                        {
                            RevisionHistoryHelper.TruncateVersionsByDaysToKeep(daysToKeepHistory, data);
                        }
                        else if ((revisionsNumber > 0) && (daysToKeepHistory > 1))
                        {
                            RevisionHistoryHelper.TruncateVersionsByDaysToKeepAndCount(daysToKeepHistory, revisionsNumber, data);
                        }
                    }
                }
            }
        }

        private void PageManager_Executing(object sender, ExecutingEventArgs e)
        {
            if (e.CommandName == "CommitTransaction" || e.CommandName == "FlushTransaction")
            {
                var configManager = ConfigManager.GetManager();
                var revisionHistoryConfig = configManager.GetSection<RevisionHistoryConfig>();

                if (revisionHistoryConfig != null)
                {
                    int revisionsNumber = 0;
                    int.TryParse(revisionHistoryConfig.NumberOfRevisionsToKeep, out revisionsNumber);

                    int daysToKeepHistory = 1;
                    int.TryParse(revisionHistoryConfig.NumberOfDaysToKeepHistory, out daysToKeepHistory);

                    var provider = sender as PageDataProvider;
                    var dirtyItems = provider.GetDirtyItems();
                    var pageManager = PageManager.GetManager();
                    if (dirtyItems.Count != 0)
                    {
                        foreach (var item in dirtyItems)
                        {
                            SecurityConstants.TransactionActionType itemStatus = provider.GetDirtyItemStatus(item);
                            var pageData = item as PageData;
                            if (pageData != null)
                            {
                                //Versions are truncated only when updating a page
                                if (itemStatus == SecurityConstants.TransactionActionType.Updated)
                                {
                                    if ((revisionsNumber > 0) && (daysToKeepHistory <= 1))
                                    {
                                        RevisionHistoryHelper.TruncateVersionsByNumber(revisionsNumber, pageData);
                                    }
                                    else if ((revisionsNumber == 0) && (daysToKeepHistory > 1))
                                    {
                                        RevisionHistoryHelper.TruncateVersionsByDaysToKeep(daysToKeepHistory, pageData);
                                    }
                                    else if ((revisionsNumber > 0) && (daysToKeepHistory > 1))
                                    {
                                        RevisionHistoryHelper.TruncateVersionsByDaysToKeepAndCount(daysToKeepHistory, revisionsNumber, pageData);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}