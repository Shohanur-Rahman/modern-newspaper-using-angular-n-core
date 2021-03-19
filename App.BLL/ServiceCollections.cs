using App.BLL.BLLManager;
using App.BLL.IBLLManager;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.BLL
{
    public static class ServiceCollections
    {
        public static IServiceCollection AddBLLManager(this IServiceCollection services) {

            services.AddTransient<IUserBLLManager, UserBLLManager>();
            services.AddTransient<IUserRoleBLLManager, UserRoleBLLManager>();
            services.AddTransient<INewsBLLManager, NewsBLLManager>();
            services.AddTransient<IFrontNewsBLLManager, FrontNewsBLLManager>();
            services.AddTransient<IUserProfileBLLManager, UserProfileBLLManager>();
            services.AddTransient<ICommentsBLLManager, CommentsBLLManager>();
            services.AddTransient<INewsPaperSettingsBLL, NewsPaperSettingsBLL>();
            return services;
        }
    }
}
