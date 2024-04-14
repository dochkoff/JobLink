using JobLink.Core.Contracts;
using System.Text.RegularExpressions;

namespace JobLink.Core.Extensions
{
    public static class ModelExtensions
    {
        public static string GetJobInformation(this IJobModel job)
        {
            string info = (job.Title + "-" + job.Location).Replace(" ", "-");
            info = Regex.Replace(info, @"[^a-zA-Z0-9\-]", string.Empty);

            return info;
        }

        public static string GetCompanyInformation(this ICompanyModel job)
        {
            string info = (job.Name + "-" + job.Address).Replace(" ", "-");
            info = Regex.Replace(info, @"[^a-zA-Z0-9\-]", string.Empty);

            return info;
        }
    }
}