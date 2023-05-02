using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TMV.Core;
using TMV.Web.Core.AjaxServer;
using TMV.Web.Core.Components;

namespace TMV.Web.Core
{
    public class Startup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // 允许跨域
            services.AddCorsAccessor();
            services.AddConsoleFormatter();
            services.AddControllers().AddInjectWithUnifyResult();
            //认证组件
            services.AddComponent<AuthComponent>();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSqlsugarSetup(App.Configuration);
            services.AddMasaBlazor(builder =>
            {
                builder.ConfigureTheme(theme =>
                {
                    theme.Themes.Light.Primary = "#4318FF";
                    theme.Themes.Light.Accent = "#4318FF";
                });
            });
            services.AddHttpContextAccessor();
            services.AddGlobalForServer();
            services.AddRemoteRequest();
            services.AddScoped<AjaxService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseInject();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}