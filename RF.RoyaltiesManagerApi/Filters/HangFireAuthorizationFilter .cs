using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace RF.RoyaltiesManagerApi.Filters
{
    /// <summary>
    /// Filter for Hangfire Authorization
    /// </summary>
    /// <seealso cref="Hangfire.Dashboard.IDashboardAuthorizationFilter" />
    public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}