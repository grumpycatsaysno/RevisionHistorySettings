using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace SitefinityWebApp.RevisionHistory
{
    /// <summary>
    /// Configuration class for revision history settings.
    /// </summary>
    public class RevisionHistoryConfig : ConfigSection
    {
        [ConfigurationProperty("NumberOfRevisionsToKeep")]
        [ObjectInfo(typeof(RevisionHistoryResources), Title = "NumberOfRevisionsToKeepTitle", Description = "NumberOfRevisionsToKeepDescription")]
        public string NumberOfRevisionsToKeep
        {
            get
            {
                return (string)this["NumberOfRevisionsToKeep"];
            }

            set
            {
                this["NumberOfRevisionsToKeep"] = value;
            }
        }

        [ConfigurationProperty("NumberOfDaysToKeepHistory")]
        [ObjectInfo(typeof(RevisionHistoryResources), Title = "NumberOfDaysToKeepHistoryTitle", Description = "NumberOfDaysToKeepHistoryDescription")]
        public string NumberOfDaysToKeepHistory
        {
            get
            {
                return (string)this["NumberOfDaysToKeepHistory"];
            }

            set
            {
                this["NumberOfDaysToKeepHistory"] = value;
            }
        }
    }
}