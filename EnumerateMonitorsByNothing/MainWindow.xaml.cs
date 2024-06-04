using MonitorWrapperLibrary;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EnumerateMonitorsByNothing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Do nothing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MonitorWrapper.GetScreens().ForEach(screen =>
            {
                System.Diagnostics.Debug.WriteLine($"Primary: {screen.IsPrimary} , Scale: {screen.ScaleFactor}, Width: {screen.MonitorArea.Width}");
            });
        }
    }
}