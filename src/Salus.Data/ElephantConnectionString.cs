﻿using System;
using System.Configuration;

namespace Salus.Data
{
    public class ElephantConnectionString
    {
        public static string GetConnectionString()
        {
            var uri = new Uri(ConfigurationManager.AppSettings["ELEPHANTSQL_URL"]);
            var db = uri.AbsolutePath.Trim('/');
            var user = uri.UserInfo.Split(':')[0];
            var passwd = uri.UserInfo.Split(':')[1];
            var port = uri.Port > 0 ? uri.Port : 5432;

            return string.Format("Server={0};Database={1};User Id={2};Password={3};Port={4}",
                uri.Host,
                db,
                user,
                passwd,
                port);
        }
    }
}