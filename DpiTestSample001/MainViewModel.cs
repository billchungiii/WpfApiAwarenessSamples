using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using BindingLibrary;
using Microsoft.Win32;
using MonitorWrapperLibrary;

namespace DpiTestSample001
{
    public class MainViewModel : NotifyPropertyBase
    {

        public MainViewModel()
        {
            SystemEvents.DisplaySettingsChanged -= this.OnDisplaySettingsChanged;
            SystemEvents.DisplaySettingsChanged += this.OnDisplaySettingsChanged;
            CreateScreens();
        }
        private  void OnDisplaySettingsChanged(object sender, EventArgs e)
        {
            CreateScreens();
        }


        private void CreateScreens()
        {
            ScreenViewModels = new ObservableCollection<ScreenViewModel>();
            var screens = MonitorWrapper.GetScreens();
            int index = 0;
            foreach (var screen in screens)
            {
                ScreenViewModels.Add(new ScreenViewModel
                {
                    ScreenPtr = screen.ScreenPtr,
                    DeviceName = $"Screen {index}",
                    IsPrimary = screen.IsPrimary,
                    Left = screen.MonitorArea.Left,
                    Top = screen.MonitorArea.Top,
                    Right = screen.MonitorArea.Right,
                    Bottom = screen.MonitorArea.Bottom,
                    Width = screen.MonitorArea.Width,
                    Height = screen.MonitorArea.Height,
                    ScaleFactor = screen.ScaleFactor,
                    ScaleFactorType = screen.ScaleFactorType,
                    ScaledLeft = screen.ScaledMonitorArea.Left,
                    ScaledTop = screen.ScaledMonitorArea.Top,
                    ScaledRight = screen.ScaledMonitorArea.Right,
                    ScaledBottom = screen.ScaledMonitorArea.Bottom,
                    ScaledWidth = screen.ScaledMonitorArea.Width,
                    ScaledHeight = screen.ScaledMonitorArea.Height
                });
                index++;
            }
        }

        private ObservableCollection<ScreenViewModel> _screenViewModels;
        public ObservableCollection<ScreenViewModel> ScreenViewModels
        {
            get { return _screenViewModels; }
            set { SetProperty(ref _screenViewModels, value); }
        }

        private int _screenIndex;
        public int ScreenIndex
        {
            get { return _screenIndex; }
            set { SetProperty(ref _screenIndex, value); }
        }

        private ICommand _moveCommand;
        public ICommand MoveCommand
        {
            get
            {
                if (_moveCommand == null)
                {
                    _moveCommand = new RelayCommand((x) =>
                    {
                        if (x is object[] values)
                        {
                            var name = values[0] as string;
                            var window = values[1] as Window;
                            var next = ScreenViewModels.FirstOrDefault(s => s.DeviceName == name);
                            window.Top = next.ScaledTop;
                            window.Left = next.ScaledLeft;
                            ScreenIndex = ScreenViewModels.IndexOf(next);
                        }
                    });
                }
                return _moveCommand;
            }
        }

        public ICommand GetLeftCommand
        {
            get
            {
                return new RelayCommand((x) =>
                {
                    var window = x as Window;
                    MessageBox.Show(window.Left.ToString());
                });
            }

        }


        public ICommand GetWindowCommand
        {

            get
            {
                return new RelayCommand((x) =>
                {
                    var window = x as Window;
                    IntPtr s = MonitorWrapper.GetScreenOfWindow(window);
                    MessageBox.Show(s.ToString());
                });
            }

        }
    }


    public class ScreenViewModel : NotifyPropertyBase
    {
        private IntPtr _screenPtr;
        public IntPtr ScreenPtr
        {
            get { return _screenPtr; }
            set { SetProperty(ref _screenPtr, value); }
        }


        private string _deviceName;
        public string DeviceName
        {
            get { return _deviceName; }
            set { SetProperty(ref _deviceName, value); }
        }

        private bool _isPrimary;
        public bool IsPrimary
        {
            get { return _isPrimary; }
            set { SetProperty(ref _isPrimary, value); }
        }

        private double _left;
        public double Left
        {
            get { return _left; }
            set { SetProperty(ref _left, value); }
        }

        private double _top;
        public double Top
        {
            get { return _top; }
            set { SetProperty(ref _top, value); }
        }

        private double _right;
        public double Right
        {
            get { return _right; }
            set { SetProperty(ref _right, value); }
        }

        private double _bottom;
        public double Bottom
        {
            get { return _bottom; }
            set { SetProperty(ref _bottom, value); }
        }

        private double _width;
        public double Width
        {
            get { return _width; }
            set { SetProperty(ref _width, value); }
        }

        private double _height;
        public double Height
        {
            get { return _height; }
            set { SetProperty(ref _height, value); }
        }

        private double _scaleFactor;
        public double ScaleFactor
        {
            get { return _scaleFactor; }
            set { SetProperty(ref _scaleFactor, value); }
        }

        private ScaleFactorType _scaleFactorType;
        public ScaleFactorType ScaleFactorType
        {
            get => _scaleFactorType;
            set => SetProperty(ref _scaleFactorType, value);
        }

        private double _scaledLeft;
        public double ScaledLeft
        {
            get { return _scaledLeft; }
            set { SetProperty(ref _scaledLeft, value); }
        }

        private double _scaledtop;
        public double ScaledTop
        {
            get { return _scaledtop; }
            set { SetProperty(ref _scaledtop, value); }
        }

        private double _scaledright;
        public double ScaledRight
        {
            get { return _scaledright; }
            set { SetProperty(ref _scaledright, value); }
        }

        private double _scaledbottom;
        public double ScaledBottom
        {
            get { return _scaledbottom; }
            set { SetProperty(ref _scaledbottom, value); }
        }

        private double _scaledwidth;
        public double ScaledWidth
        {
            get { return _scaledwidth; }
            set { SetProperty(ref _scaledwidth, value); }
        }

        private double _scaledheight;
        public double ScaledHeight
        {
            get { return _scaledheight; }
            set { SetProperty(ref _scaledheight, value); }
        }

    }


    public class MoveConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            return values.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

