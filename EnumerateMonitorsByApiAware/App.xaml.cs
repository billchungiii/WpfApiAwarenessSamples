using MonitorWrapperLibrary;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Media;
[assembly:DisableDpiAwareness]
namespace EnumerateMonitorsByApiAware
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
           MonitorWrapper.SetProcessDpiAwareness();
           base.OnStartup(e);
        }
    }

}
