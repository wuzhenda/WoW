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
    public class StreamableWindow : Window, IStreamableElement
    {
        public string GetContextAsBase64()
        {
            return (string)Dispatcher.Invoke(new Func<string>(() => Helpers.FrameworkElementToBase64(this)));
        }

        public string GetContextAsBase64(string name)
        {
            return (string)Dispatcher.Invoke(new Func<string>(() => GetContextAsBase64()));

        }

        public void Initialize()
        {
            Dispatcher.Invoke(new Action(() => Show()));
        }

        public void Dispose()
        {
            Dispatcher.Invoke(new Action(() => { Close(); }));
        }

        public virtual void OnMouseAction(MouseAction action, int x, int y)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                var pnt=this.TranslatePoint(new Point(x, y), this);
                var res=VisualTreeHelper.HitTest(this, pnt);
            
                if (res != null && res.VisualHit != null && res.VisualHit is UIElement)
                {
                    SimulateEvent(res, action, pnt);
                }
            }));
        }


        public virtual void OnKeyboardAction(KeyBoardAction action, Key k)
        {
            Dispatcher.Invoke(new Action(() =>
            {
            }));

            //throw new NotImplementedException();
        }



        void SimulateEvent(HitTestResult res, MouseAction action, Point point)
        {
            InputEventArgs marg = null;

            if (action == MouseAction.LeftButtonDown || action == MouseAction.RightButtonDown)
            {
                MouseButtonEventArgs args = action == MouseAction.LeftButtonDown ?
                    new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left) : 
                    new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Right);

                args.RoutedEvent = Mouse.MouseDownEvent;
                marg = args;

                (res.VisualHit as UIElement).RaiseEvent(marg);
            }
            else if (action == MouseAction.LeftButtonUp || action == MouseAction.RightButtonUp)
            {
                MouseButtonEventArgs args = action == MouseAction.LeftButtonUp ? 
                    new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left) : 
                    new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Right);

                args.RoutedEvent = Mouse.MouseUpEvent;
                marg = args;

                (res.VisualHit as UIElement).RaiseEvent(marg);
            }
            else if (action == MouseAction.Move)
            {  
                //var wih = new WindowInteropHelper(this);
                //SendMessage(wih.Handle, WM_SETCURSOR, wih.Handle, MakeLParam((int)HTCLIENT, (int)WM_MOUSEMOVE));
                //PostMessage(wih.Handle, WM_MOUSEMOVE, IntPtr.Zero, MakeLParam((int)point.X, (int)point.Y));
                //MouseEventArgs args = new MouseEventArgs(Mouse.PrimaryDevice, 0);
                
                //args.RoutedEvent = Mouse.MouseMoveEvent;
                //marg = args;
            }


        }

        
        static T GetVisualParent<T>(DependencyObject element) where T : DependencyObject
        {
            while (element != null && !(element is T))
                element = VisualTreeHelper.GetParent(element);

            return (T)element;
        }
    }
}
