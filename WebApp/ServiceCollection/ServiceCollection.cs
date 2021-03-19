using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Helper;
using WebApp.IServiceManager;
using WebApp.ServiceManager;

namespace WebApp.ServiceCollection
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddServiceManager(this IServiceCollection services)
        {
            services.AddTransient<IUserServiceManager, UserServiceManager>();
            services.AddTransient<IUserRoleServiceManager, UserRoleServiceManager>();
            services.AddTransient<INewsServiceManager, NewsServiceManager>();
            services.AddTransient<IUploaderHelper, UploadHelper>();
            services.AddTransient<INewsFrontService, NewsFrontServiceManager>();
            services.AddTransient<IUserProfileService, UserProfileService>();
            services.AddTransient<ICommentsServiceManager, CommentsServiceManager>();
            services.AddTransient<INewsSettingsService, NewsSettingsServiceManager>();
            return services;
        }
    }
}
