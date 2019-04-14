using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;

namespace StudentMajorLeague.Web.Infrastructure
{
    public class StudentMajorLeagueConfiguration
    {
        public string ConnectionString { get; set; }
        public string SiteUrl { get; set; }
        public string BasePath { get; set; }
        public string StorageUrl { get; set; }

        public static StudentMajorLeagueConfiguration FromWebConfig(NameValueCollection nameValueCollection)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;

                //Path mapped from URL
                var configuration = new StudentMajorLeagueConfiguration
                {
                    ConnectionString = connectionString,
                    SiteUrl = nameValueCollection["SiteUrl"],
                    BasePath = ConfigurationManager.AppSettings["BasePath"],
                    StorageUrl = ConfigurationManager.AppSettings["StorageUrl"],
                };

                return configuration;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Invalid settings in web.config", ex);
            }
        }

    }
}