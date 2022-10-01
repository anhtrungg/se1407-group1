using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Helpers;


namespace VoiceAPI.DI
{
    public static class ServiceDI
    {
        public static void ConfigServiceDI(this IServiceCollection services)
        {
          //  services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            //Tools
            services.AddScoped<ITools, Tools>();

            // Auth
          /*  services.AddScoped<IAuthService, AuthService>();

            // Account
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();

            // Candidate
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<ICandidateService, CandidateService>();

            // Category
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            // District
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IDistrictService, DistrictService>();

            // Enterprise
            services.AddScoped<IEnterpriseRepository, EnterpriseRepository>();
            services.AddScoped<IEnterpriseService, EnterpriseService>();

            // Province
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<IProvinceService, ProvinceService>();

            // SubCategory
            services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();

            // Wallet
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IWalletService, WalletService>();

            // Ward
            services.AddScoped<IWardRepository, WardRepository>();
            services.AddScoped<IWardService, WardService>();*/
        }
    }
}