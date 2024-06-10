using MonitorWrapperLibrary;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EnumerateMonitorsByDisableAwareness
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
        /// only Disable DpiAwareness, see app.xaml.cs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            foreach (var sc in System.Windows.Forms.Screen.AllScreens)
            {
                System.Diagnostics.Debug.WriteLine($"{i}, top:{sc.Bounds.Top} , left:{sc.Bounds.Left}, bottom:{sc.Bounds.Bottom} , right:{sc.Bounds.Right}, x:{sc.Bounds.X} , y:{sc.Bounds.Y},  width:{sc.Bounds.Width} ,height:{sc.Bounds.Height}, ");
                i++;
            }
            MonitorWrapper.GetScreens().ForEach(screen =>
            {
                System.Diagnostics.Debug.WriteLine($"Primary: {screen.IsPrimary} , Scale: {screen.ScaleFactor}, Width: {screen.MonitorArea.Width}, Top : {screen.MonitorArea.Top}, Left: {screen.MonitorArea.Left} ");
            });
        }
    }
}