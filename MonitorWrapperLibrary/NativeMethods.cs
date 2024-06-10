using System.Runtime.InteropServices;

namespace MonitorWrapperLibrary
{
    public delegate bool EnumMonitorsDelegate(IntPtr hMonitor, IntPtr hdcMonitor, RECT rect, IntPtr dwData);

   
    public static class NativeMethods
    {
        public const Int32 MONITOR_DEFAULTTOPRIMARY = 0x00000001;
        public const Int32 MONITOR_DEFAULTTONEAREST = 0x00000002;

        /// <summary>
        /// 列舉目前所有的螢幕
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="lprcClip"></param>
        /// <param name="lpfnEnum"></param>
        /// <param name="dwData"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, EnumMonitorsDelegate lpfnEnum, IntPtr dwData);

        /// <summary>
        /// 取得螢幕的資訊
        /// </summary>
        /// <param name="hmonitor"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMonitorInfo(IntPtr hmonitor, [In, Out] MONITORINFO info);

        /// <summary>
        /// 取得螢幕的 DPI
        /// </summary>
        /// <param name="hmonitor"></param>
        /// <param name="dpiType"></param>
        /// <param name="dpiX"></param>
        /// <param name="dpiY"></param>
        /// <returns></returns>
        [DllImport("Shcore.dll")]
        public static extern int GetDpiForMonitor(IntPtr hmonitor, int dpiType, out uint dpiX, out uint dpiY);

        /// <summary>
        /// 設定 Process 的 DPI 感知度，這個函式必須在應用程式初始化時呼叫並且設定 [assembly:DisableDpiAwareness] 屬性
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [DllImport("Shcore.dll")]
        public static extern int SetProcessDpiAwareness(PROCESS_DPI_AWARENESS value);

        /// <summary>
        /// 取得 Window 所在的螢幕
        /// </summary>
        /// <param name="handle">handle of window</param>
        /// <param name="flags"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr handle, Int32 flags);

        /// <summary>
        /// 取得 Process 的 DPI 感知度
        /// </summary>
        /// <param name="hWnd">handle of process</param>
        /// <param name="value"></param>
        /// <returns></returns>
        [DllImport("Shcore.dll")]
        public static extern uint GetProcessDpiAwareness(IntPtr hWnd, out PROCESS_DPI_AWARENESS value);



        //[DllImport("user32.dll")]
        //public static extern uint GetDpiFromDpiAwarenessContext(DPI_AWARENESS_CONTEXT value);

        //[DllImport("user32.dll")]
        //public static extern DPI_AWARENESS_CONTEXT GetThreadDpiAwarenessContext();

        //[DllImport("user32.dll")]
        //public static extern DPI_AWARENESS_CONTEXT GetDpiAwarenessContextForProcess(IntPtr handle);

       

    }   

    /// <summary>
    /// The RECT structure defines the coordinates of the upper-left and lower-right corners of a rectangle.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    /// The MONITORINFO structure contains information about a display monitor.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
    public class MONITORINFO
    {
        public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));
        public RECT rcMonitor = new RECT();
        public RECT rcWork = new RECT();
        public int dwFlags;
    }
    /// <summary>
    /// 
    /// </summary>
    public enum PROCESS_DPI_AWARENESS
    {
        PROCESS_DPI_UNAWARE = 0,
        PROCESS_SYSTEM_DPI_AWARE = 1,
        PROCESS_PER_MONITOR_DPI_AWARE = 2
    }

    public enum DPI_AWARENESS_CONTEXT
    {
        DPI_AWARENESS_CONTEXT_UNAWARE = -1,
        DPI_AWARENESS_CONTEXT_SYSTEM_AWARE = -2,
        DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE = -3,
        DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2 = -4,
        DPI_AWARENESS_CONTEXT_UNAWARE_GDISCALED = -5,
    }
}
