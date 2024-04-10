using JobLink.Core.Contracts;
using System.Text.RegularExpressions;

namespace JobLink.Core.Extensions
{
    public static class ModelExtensions
    {
        public static string GetInformation(this IJobModel job)
        {
            string info = (job.Title + "-" + job.Location).Replace(" ", "-");
            info = Regex.Replace(info, @"[^a-zA-Z0-9\-]", string.Empty);

            return info;
        }
    }
}
 