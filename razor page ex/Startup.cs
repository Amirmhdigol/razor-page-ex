using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RazorEx.DAL.Context;
using RazorEx.DAL.Entities;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.Services;
using RazorEX.BAL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace razor_page_ex
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RXContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

            services.AddRazorPages();
            services.AddScoped<ISignup, Signup>();
            services.AddScoped<ISIgnIn, SignIn>();
            services.AddScoped<ICategory, CategoryS>();
            services.AddScoped<IPost, PostSs>();
            services.AddTransient<IFileManager, FileManager>();
            services.AddTransient<IPostComment, PostComments>();
            services.AddTransient<IMainPage, MainPage>();
            services.AddTransient<IUser, UserService>();
            services.AddTransient<IViewRenderService, RenderViewToString>();
            services.AddTransient<IProduct, ProductService>();
            services.AddTransient<IProductComment, ProductCommentService>();
            services.AddAuthorization(option =>
            {
                option.AddPolicy("AdminPolicy",
                builder => builder.RequireRole("Admin"));
            });
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(option =>
            {
                option.LoginPath = "/Register/SignIn";
                option.LogoutPath = "/Resgister/SignOut";
                option.ExpireTimeSpan = TimeSpan.FromDays(30);
                option.AccessDeniedPath = "/";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseExceptionHandler("/ErrorHandler/500");
            //app.UseStatusCodePagesWithReExecute("/ErrorHandler/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapRazorPages();
                    endpoints.MapControllerRoute(
                      name: "Adminstration",
                      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );
                    endpoints.MapControllerRoute(
                      name: "UserPanel",
                      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );
                });

            });
        }
    }
}
