using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Versioning;

namespace SitefinityWebApp.RevisionHistory
{
    /// <summary>
    /// Helper class that truncates reduntant revision history versions.
    /// </summary>
    public static class RevisionHistoryHelper
    {
        /// <summary>
        /// Truncates the revision history versions by number.
        /// </summary>
        /// <param name="revisionsNumber">The revisions number.</param>
        /// <param name="pageData">The page data.</param>
        public static void TruncateVersionsByNumber(int revisionsNumber, PageData pageData)
        {
            var versionManager = VersionManager.GetManager();

            // Gets the changes for the current page
            var changes = versionManager.GetItemVersionHistory(pageData.Id);

            var changesToRemove = changes
                .OrderByDescending(c => c.Version)
                .Skip(revisionsNumber)
                .AsEnumerable();

            if (changesToRemove.Count() > 0)
            {
                foreach (var change in changesToRemove)
                {
                    // Deletes all changes with version number smaller or equal to the specified number
                    versionManager.TruncateVersions(pageData.Id, change.Version);
                }

                versionManager.SaveChanges();
            }
        }

        /// <summary>
        /// Truncates the revision history versions by days to keep.
        /// </summary>
        /// <param name="daysToKeep">The days to keep.</param>
        /// <param name="pageData">The page data.</param>
        public static void TruncateVersionsByDaysToKeep(int daysToKeep, PageData pageData)
        {
            var versionManager = VersionManager.GetManager();

            var changes = versionManager.GetItemVersionHistory(pageData.Id);

            if (changes.Count > 0)
            {
                var lastChange = changes.OrderByDescending(c => c.Version).FirstOrDefault();
                if (lastChange != null)
                {
                    var earliestDate = lastChange.LastModified.AddDays(daysToKeep * (-1));

                    //Delete all the changes with dates older or equal to the specified date
                    versionManager.TruncateVersions(pageData.Id, earliestDate);

                    versionManager.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Truncates the revision history versions by days to keep and count.
        /// </summary>
        /// <param name="daysToKeep">The days to keep.</param>
        /// <param name="revisionsCount">The revisions count.</param>
        /// <param name="pageData">The page data.</param>
        public static void TruncateVersionsByDaysToKeepAndCount(int daysToKeep, int revisionsCount, PageData pageData)
        {
            var versionManager = VersionManager.GetManager();

            var changes = versionManager.GetItemVersionHistory(pageData.Id);
            if (changes.Count > 0)
            {
                var lastChange = changes.OrderByDescending(c => c.Version).FirstOrDefault();
                if (lastChange != null)
                {
                    var earliestDate = lastChange.LastModified.AddDays(daysToKeep * (-1));

                    versionManager.TruncateVersions(pageData.Id, earliestDate);

                    versionManager.SaveChanges();

                    var leftChanges = versionManager.GetItemVersionHistory(pageData.Id);

                    if (leftChanges.Count() > 0)
                    {
                        var changesToRemove = leftChanges
                        .OrderByDescending(c => c.Version)
                        .Skip(revisionsCount)
                        .AsEnumerable();

                        if (changesToRemove.Count() > 0)
                        {
                            foreach (var change in changesToRemove)
                            {
                                // Deletes all changes with version number smaller or equal to the specified number
                                versionManager.TruncateVersions(pageData.Id, change.Version);
                            }

                            versionManager.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}