using BindingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpiTestSample001
{
    public class WindowPositonViewModel : NotifyPropertyBase
    {
        private int _screenIndex;
        public int ScreenIndex
        {
            get => _screenIndex;
            set => SetProperty(ref _screenIndex, value);
        }

        private long _screenPtr;
        public long ScreenPtr
        {
            get => _screenPtr;
            set => SetProperty(ref _screenPtr, value);
        }

        private double _scaleFactor;
        public double ScaleFactor
        {
            get => _scaleFactor; 
            set => SetProperty(ref _scaleFactor, value);
        }

        private double _scaledWidth;
        public double ScaledWidth
        {
            get => _scaledWidth; 
            set => SetProperty(ref _scaledWidth, value);
        }

        private double _scaledHeight;
        public double ScaledHeight
        {
            get => _scaledHeight; 
            set => SetProperty(ref _scaledHeight, value);
        }

        private double _top;

        /// <summary>
        /// 相對於螢幕零點的 Y 座標
        /// </summary>
        public double Top
        {
            get => _top; 
            set => SetProperty(ref _top, value);
        }


        private double _left;
        /// <summary>
        /// 相對於螢幕零點的 X 座標
        /// </summary>
        public double Left
        {
            get => _left; 
            set => SetProperty(ref _left, value);
        }

    }
}
