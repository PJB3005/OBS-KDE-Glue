using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ActivityManager.DBus;
using Nett;
using OBSWebsocketDotNet;
using Tmds.DBus;

namespace OBSKDEGlue
{
    class Program
    {
        private Configuration config;
        private OBSWebsocket obs;
        private IActivities _activityManager;

        private static async Task Main(string[] args)
        {
            var program = new Program();
            await program.Run();
        }

        private async Task Run()
        {
            // Read config.
            var execDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var configPath = Path.Combine(execDir, "config.toml");
            config = Toml.ReadFile<Configuration>(configPath);

            obs = new OBSWebsocket();
            obs.Connect(config.Connection.URL, config.Connection.Password);

            _activityManager =
                Connection.Session.CreateProxy<IActivities>("org.kde.ActivityManager", "/ActivityManager/Activities");

            try
            {
                await Everything();
            }
            finally
            {
                obs.Disconnect();
            }
        }

        private async Task Everything()
        {
            using (var handle = await _activityManager.WatchCurrentActivityChangedAsync(newCurrent =>
            {
                if (newCurrent == config.ActivitySwitcher.TargetActivity)
                {
                    obs.SetCurrentScene(config.ActivitySwitcher.TargetScene);
                }
                else
                {
                    obs.SetCurrentScene(config.ActivitySwitcher.HideScene);
                }
            }))
            {
                while (true)
                {
                    await Task.Delay(5);
                }
            }
        }
    }
}