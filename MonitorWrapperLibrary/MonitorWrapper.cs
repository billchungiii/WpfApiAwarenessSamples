using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitorWrapperLibrary
{
    public static class MonitorWrapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>true version >= 8.1</returns>
        public static bool OSVersionCheck()
        {
            Microsoft.VisualBasic.Devices.Computer computer = new Microsoft.VisualBasic.Devices.Computer();
            var versions = computer.Info.OSVersion.Split('.');
            double verson = double.Parse(versions[0] + "." + versions[1]);
            return verson >= 8.1;
        }

        public static bool SetProcessDpiAwareness()
        {

            if (OSVersionCheck())
            {
                int result = NativeMethods.SetProcessDpiAwareness(PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE);
                if (result == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static List<ScreenInfo> GetScreens()
        {

            var enumMonitors = new EnumMonitors();
            NativeMethods.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, enumMonitors.MonitorEnumProc, IntPtr.Zero);
            return enumMonitors.screens;
        }

        private sealed class EnumMonitors
        {
            public List<ScreenInfo> screens = new List<ScreenInfo>();
            public bool MonitorEnumProc(IntPtr hMonitor, IntPtr hdcMonitor, RECT rect, IntPtr dwData)
            {
                MONITORINFO mi = new MONITORINFO();

                if (NativeMethods.GetMonitorInfo(hMonitor, mi))
                {
                    var monitor = new AreaInfo(mi.rcMonitor.Left, mi.rcMonitor.Top, mi.rcMonitor.Right, mi.rcMonitor.Bottom);
                    var working = new AreaInfo(mi.rcWork.Left, mi.rcWork.Top, mi.rcWork.Right, mi.rcWork.Bottom);
                    uint dpiX, dpiY;
                    double scaleFactor = 1.0;

                    if (OSVersionCheck())
                    {
                        NativeMethods.GetDpiForMonitor(hMonitor, 0, out dpiX, out dpiY); // 0 = MDT_EFFECTIVE_DPI
                        scaleFactor = dpiX / 96.0; // Windows standard DPI is 96
                    }
                   else
                    {
                       scaleFactor = GetScaleFactorWhenOldOS();
                    }

                    // ==1 is PRIMARY
                    screens.Add(new ScreenInfo((mi.dwFlags & 1) == 1, monitor, working, scaleFactor));
                }

                return true;
            }

            /// <summary>
            /// when the OS is old, we can't use GetDpiForMonitor
            /// </summary>
            /// <returns></returns>
            private static double GetScaleFactorWhenOldOS()
            {
                float dpiX, dpiY;
                using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromHwnd(IntPtr.Zero))
                {
                    dpiX = graphics.DpiX;
                    dpiY = graphics.DpiY;
                }
                double scaleFactor = dpiX / 96.0; // windows 96, mac 72
                return scaleFactor;
            }
        }
    }

    public sealed class ScreenInfo
    {
        /// <summary>
        /// Initializes a new instance of the Screen class.
        /// </summary>
        /// <param name="primary">A value indicating whether the display is the primary screen.</param>
        /// <param name="x">The display's top corner X value.</param>
        /// <param name="y">The display's top corner Y value.</param>
        /// <param name="w">The width of the display.</param>
        /// <param name="h">The height of the display.</param>
        public ScreenInfo(bool primary, AreaInfo monitoArea, AreaInfo workingArea, double scaleFactor)
        {
            this.IsPrimary = primary;
            MonitorArea = monitoArea;
            WorkingArea = workingArea;
            ScaleFactor = scaleFactor;
        }

        public double ScaleFactor { get; }
        /// <summary>
        /// Gets a value indicating whether the display device is the primary monitor.
        /// </summary>
        public bool IsPrimary { get; }

        public AreaInfo MonitorArea { get; }

        public AreaInfo WorkingArea { get; }
    }

    public struct AreaInfo
    {
        public AreaInfo(int left, int top, int right, int bottom)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
            this.Width = Math.Abs(right - left);
            this.Height = Math.Abs(bottom - top);
        }
        public int Left { get; }
        public int Top { get; }
        public int Right { get; }
        public int Bottom { get; }
        public int Width { get; }
        public int Height { get; }
    }
}
