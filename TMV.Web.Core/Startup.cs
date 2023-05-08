using Blazored.SessionStorage;
using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TMV.Core;
using TMV.Web.Core.AjaxServer;
using TMV.Web.Core.Components;
using TMV.Web.Core.Handle;
using TMV.Web.Core.SocketServer;

namespace TMV.Web.Core
{
    public class Startup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // 允许跨域
            services.AddCorsAccessor();
            services.AddConsoleFormatter(options =>
            {
                options.WithTraceId = true;
            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            //services.AddWebSockets();
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
            services.AddScoped<WebsiteAuthenticator>();
            services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<WebsiteAuthenticator>());
            services.AddBlazoredSessionStorage();
            // 日志配置信息 begin
            services.AddFileLogging("logs/SysLog-{0:yyyy}-{0:MM}-{0:dd}.log", options =>
            {
                options.FileNameRule = fileName =>
                {
                    return string.Format(fileName, DateTime.UtcNow);
                };
            });
            // end
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseInject();
            app.UseSession();
            app.SocketServereMildd();
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