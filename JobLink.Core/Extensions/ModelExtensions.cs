using JobLink.Core.Contracts;
using System.Text.RegularExpressions;

namespace JobLink.Core.Extensions
{
    public static class ModelExtensions
    {
        public static string GetInformation(this IJobModel job)
        {
            string info = job.Title.Replace(" ", "-") + "-" + GetLocation(job.Location);
            info = Regex.Replace(info, @"[^a-zA-Z0-9\-]", string.Empty);

            return info;
        }

        private static string GetLocation(string location)
        {
            location = string.Join("-", location.Split(" ").Take(3));

            return location;
        }
    }
}
