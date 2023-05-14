using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TMV.PrintServer
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Serve.RunNative(includeWeb: false);
			Serve.RunNative(services =>
			{
				services.AddFileLogging("Log/InformLog-{0:yyyy}-{0:MM}-{0:dd}.log", options =>
				{
					options.FileNameRule = fileName =>
					{
						return string.Format(fileName, DateTime.UtcNow);
					};
					options.WriteFilter = (logMsg) =>
					{
						return logMsg.LogLevel == LogLevel.Information;
					};
				});
				services.AddFileLogging("Log/errorLog-{0:yyyy}-{0:MM}-{0:dd}.log", options =>
				{
					options.FileNameRule = fileName =>
					{
						return string.Format(fileName, DateTime.UtcNow);
					};
					options.WriteFilter = (logMsg) =>
					{
						return logMsg.LogLevel == LogLevel.Error;
					};
				});
			});
			ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}