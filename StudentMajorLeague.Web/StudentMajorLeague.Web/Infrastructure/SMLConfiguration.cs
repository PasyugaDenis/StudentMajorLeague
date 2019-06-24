﻿using System;
using System.Collections.Specialized;
using System.Configuration;

namespace StudentMajorLeague.Web.Infrastructure
{
    public class SMLConfiguration
    {
        public string ConnectionString { get; set; }
        public string SiteUrl { get; set; }
        public string BasePath { get; set; }
        public string StorageUrl { get; set; }

        public static SMLConfiguration FromWebConfig(NameValueCollection nameValueCollection)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;

                //Path mapped from URL
                var configuration = new SMLConfiguration
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