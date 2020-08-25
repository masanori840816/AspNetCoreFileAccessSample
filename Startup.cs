using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileAccesses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace FileAccessSample
{
    public class Startup
    {
        
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<IFileAccessService, FileAccessService>();
            services.AddControllers();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // 属性ベースのルーティングを有効にする.
                endpoints.MapControllers();
            });
        }
    }
}
