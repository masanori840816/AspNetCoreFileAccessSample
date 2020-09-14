using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace FileAccessSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var nlogFile = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Nlog.config");
            
            // nlog.configの読み込み.
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(nlogFile)
                .GetCurrentClassLogger();
            try 
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex) {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally {
                NLog.LogManager.Shutdown();
            }            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://0.0.0.0:5000");
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    // NLog 以外で設定された Provider の無効化.
                    logging.ClearProviders();
                    // 最小ログレベルの設定.
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog();
    }
}
