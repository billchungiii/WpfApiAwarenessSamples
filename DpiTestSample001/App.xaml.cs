using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Media;
//[assembly:DisableDpiAwareness]
namespace DpiTestSample001
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        override protected void OnStartup(StartupEventArgs e)
        {
           // MonitorWrapperLibrary.MonitorWrapper.SetProcessDpiAwareness();
            base.OnStartup(e);
           
        }
    }

}
