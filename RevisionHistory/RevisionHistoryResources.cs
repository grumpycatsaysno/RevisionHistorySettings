using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Localization;

namespace SitefinityWebApp.RevisionHistory
{
    [ObjectInfo(typeof(RevisionHistoryResources), Title = "RevisionHistoryResourcesTitle", Description = "RevisionHistoryResourcesDescription")]
    public class RevisionHistoryResources : Resource
    {
        [ResourceEntry("RevisionHistoryResourcesTitle",
                       Value = "Revision history resources",
                       Description = "The title of this class.",
                       LastModified = "2015/02/27")]
        public string RevisionHistoryResourcesTitle
        {
            get
            {
                return this["RevisionHistoryResourcesTitle"];
            }
        }

        [ResourceEntry("RevisionHistoryResourcesDescription",
                       Value = "Contains localizable resources for revision history settings",
                       Description = "The description of this class.",
                       LastModified = "2015/02/27")]
        public string RevisionHistoryResourcesDescription
        {
            get
            {
                return this["RevisionHistoryResourcesDescription"];
            }
        }

        [ResourceEntry("NumberOfRevisionsToKeepTitle",
                       Value = "Enter number of versions to keep",
                       Description = "MailChimp Api Key Title",
                       LastModified = "2015/02/27")]
        public string NumberOfRevisionsToKeepTitle
        {
            get
            {
                return this["NumberOfRevisionsToKeepTitle"];
            }
        }

        [ResourceEntry("NumberOfRevisionsToKeepDescription",
                      Value = "Each change of the page node creates new version in the Revision history. By this setting you can control the number of versions to keep.",
                      Description = "Number of revisions to keep description",
                      LastModified = "2015/02/27")]
        public string NumberOfRevisionsToKeepDescription
        {
            get
            {
                return this["NumberOfRevisionsToKeepDescription"];
            }
        }

        [ResourceEntry("NumberOfDaysToKeepHistoryTitle",
                       Value = "Enter number of days to keep history",
                       Description = "Number of days to keep history title",
                       LastModified = "2015/02/27")]
        public string NumberOfDaysToKeepHistoryTitle
        {
            get
            {
                return this["NumberOfDaysToKeepHistoryTitle"];
            }
        }

        [ResourceEntry("NumberOfDaysToKeepHistoryDescription",
                      Value = "By default revision history is kept forever. With this setting you can control the number of days to keep the revision history",
                      Description = "Number of days to keep history description",
                      LastModified = "2015/02/27")]
        public string NumberOfDaysToKeepHistoryDescription
        {
            get
            {
                return this["NumberOfDaysToKeepHistoryDescription"];
            }
        }
    }
}