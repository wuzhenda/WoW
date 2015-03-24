using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;


namespace WpfServer
{
    public class EventSimulator
    {
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(
              IntPtr hWnd,     // handle to destination window
              uint Msg,     // message
              IntPtr wParam,  // first message parameter
              IntPtr lParam   // second message parameter
              );

        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(
              IntPtr hWnd,     // handle to destination window
              uint Msg,     // message
              IntPtr wParam,  // first message parameter
              IntPtr lParam   // second message parameter
              );

        private uint WM_MOUSEMOVE = 0x200;
        private uint WM_SETCURSOR = 0x020;
        private uint WM_LBUTTONDOWN = 0x201;
        private uint WM_LBUTTONUP = 0x202;
        private uint HTCLIENT = 0x1;

        public EventSimulator(UIElement uiElement)
        {

        }

        public IntPtr MakeLParam(int LoWord, int HiWord)
        {
            return new IntPtr((HiWord << 16) | (LoWord & 0xFFFF));
        }
    }
}
