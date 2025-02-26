using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using NGEL.Data.Helpers;
using NGEL.Data.DB;
using NGEL.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace NGEL.Data.Helpers
{

    public static class AddDBHelpersExtensions
    {
        public static void AddDBHelpers(this IServiceCollection Services)
        {
            Services.AddSingleton<DBHelper>();
            Services.AddSingleton<DBHelperAdmin>();
            Services.AddSingleton<DBHelperAdminWriteOnly>();
            Services.AddSingleton<DBHelperChatLog>();
            Services.AddSingleton<DBHelperGame>();
            Services.AddSingleton<DBHelperGameManager>();
            Services.AddSingleton<DBHelperGameManagerWriteOnly>();
            Services.AddSingleton<DBHelperGameWriteOnly>();
            Services.AddSingleton<DBHelperGMLog>();
            Services.AddSingleton<DBHelperGMLogWriteOnly>();
            Services.AddSingleton<DBHelperLog>();
            Services.AddSingleton<DBHelperVisitWriteOnly>();
        }
    }
}
