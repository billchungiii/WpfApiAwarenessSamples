using MonitorWrapperLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DpiTestSample001
{
    public class PositionOperationHelper
    {
        public static WindowPositonViewModel CreateWindowPositionViewModel(Window window, ObservableCollection<ScreenViewModel> screens)
        {
            var result = new WindowPositonViewModel();
            IntPtr screenPtr = MonitorWrapperLibrary.MonitorWrapper.GetScreenOfWindow(window);
            var screenInfo = screens.FirstOrDefault (x => x.ScreenPtr == screenPtr);
            var screenIndex = screens.IndexOf(screenInfo);
            var pistionViewModel = new WindowPositonViewModel
            {
                ScreenIndex = screenIndex,
                ScreenPtr = screenPtr.ToInt64(),
                ScaleFactor = screenInfo.ScaleFactor,
                ScaledWidth = screenInfo.ScaledWidth,
                ScaledHeight = screenInfo.ScaledHeight,
                Top = window.Top - screenInfo.ScaledTop,
                Left = window.Left - screenInfo.ScaledLeft
            };          
            return pistionViewModel;
        }

        public static void SaveWindowPosition(Window window,ObservableCollection<ScreenViewModel> screens)
        {
           var model = CreateWindowPositionViewModel(window, screens);
           var json =  Newtonsoft.Json.JsonConvert.SerializeObject(model);
           File.WriteAllText ("position.json", json);
        }

        public static void LoadWindowPosition(Window window, ObservableCollection<ScreenViewModel> screens)
        {
            if (File.Exists("position.json"))
            {
                var json = File.ReadAllText("position.json");
                var model = Newtonsoft.Json.JsonConvert.DeserializeObject<WindowPositonViewModel>(json);
                var screenIndex = model.ScreenIndex;
                ScreenViewModel screenInfo;
                if (screenIndex <  screens.Count)
                {
                    screenInfo = screens[model.ScreenIndex];

                    if (model.ScaledHeight == screenInfo.ScaledHeight)
                    {
                        window.Top = model.Top + screenInfo.ScaledTop;
                    }
                    else
                    {
                        // 若現有螢幕的高度不等於儲存的螢幕高度，則依比例計算
                        window.Top =  model.Top * (screenInfo.ScaledHeight / model.ScaledHeight)+screenInfo.ScaledTop;
                    }

                    if (model.ScaledWidth == screenInfo.ScaledWidth)
                    {
                        window.Left = model.Left + screenInfo.ScaledLeft;
                    }
                    else
                    {
                        // 若現有螢幕的寬度不等於儲存的螢幕寬度，則依比例計算
                        window.Left = model.Left * (screenInfo.ScaledWidth / model.ScaledWidth)+screenInfo.ScaledLeft;
                    }
                   
                }
                else
                {
                    // 如果螢幕被拔掉了, 就回到主要螢幕, (0,0)
                    window.Top = 0;
                    window.Left = 0;
                } 
            }
            else
            {
                window.Top = 0;
                window.Left = 0;
            }
        }
    }
}
