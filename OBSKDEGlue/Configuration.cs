using System.IO;
using System.Text;

namespace OBSKDEGlue
{
    internal class Configuration
    {
        public ConnectionConfig Connection { get; set; }
        public ActivitySwitcherConfig ActivitySwitcher { get; set; }
    }

    internal class ConnectionConfig
    {
        public string URL { get; set; }
        public string Password { get; set; }
    }

    internal class ActivitySwitcherConfig
    {
        public string TargetActivity { get; set; }
        public string TargetScene { get; set; }
        public string HideScene { get; set; }
    }
}