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

namespace DpiTestSample001
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            LoadPosition();
            this.Closing += MainWindow_Closing;

        }

        private void LoadPosition()
        {
            var viewModel = this.DataContext as MainViewModel;
            var screens = viewModel.ScreenViewModels;
            PositionOperationHelper.LoadWindowPosition(this, screens);
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var viewModel = this.DataContext as MainViewModel;
            var screens = viewModel.ScreenViewModels;
            PositionOperationHelper.SaveWindowPosition(this, screens);
        }
    }
}