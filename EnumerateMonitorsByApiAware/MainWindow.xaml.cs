using MonitorWrapperLibrary;
using System.Diagnostics;
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

namespace EnumerateMonitorsByApiAware
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
        /// Disable DpiAwareness and reset DpiAwareness, see app.xaml.cs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($" {SystemParameters.FullPrimaryScreenWidth}, {SystemParameters.PrimaryScreenWidth}");
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


        private int _index = 0;

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
            Top = MonitorWrapper.GetScreens()[0].MonitorArea.Top;
            Left = MonitorWrapper.GetScreens()[0].MonitorArea.Left;
            Debug.WriteLine ($"Top: {Top}, Left: {Left}");
            _index = 0;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
            if(_index == 1) { return; }
            var scale = MonitorWrapper.GetScreens()[_index].ScaleFactor;
            Top = MonitorWrapper.GetScreens()[1].MonitorArea.Top / scale;
            Left = MonitorWrapper.GetScreens()[1].MonitorArea.Left / scale;
            Debug.WriteLine($"Top: {Top}, Left: {Left}");
            _index = 1;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (_index == 2) { return; }
            var scale = MonitorWrapper.GetScreens()[_index].ScaleFactor;
            Top = MonitorWrapper.GetScreens()[2].MonitorArea.Top / scale;
            Left = MonitorWrapper.GetScreens()[2].MonitorArea.Left / scale;
            Debug.WriteLine($"Top: {Top}, Left: {Left}");
            _index = 2;
        }
    }
}