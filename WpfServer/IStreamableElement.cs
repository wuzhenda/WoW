using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace WpfServer
{
    public interface IStreamableElement
    {
        string GetContextAsBase64();
        string GetContextAsBase64(string name);
        void Initialize();
        void Dispose();
        void OnMouseAction(MouseAction action, int x, int y);
        void OnKeyboardAction(KeyBoardAction action, Key k);
    }

   
}
