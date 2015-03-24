using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Nugget;
using Nugget.Framework;
using Nugget.Server;

namespace WpfServer
{
    class ElementSocket<T> : WebSocket where T: IStreamableElementFactory, new()
    {
        
        IStreamableElementFactory factory;
        IStreamableElement element = null;
        Timer t;

        public override void Incoming(string data)
        {
            try
            {
                ProcessCommand(data);
            }
            catch (Exception ex)
            {
                Log.Warn("Invalid incoming message: " + data);
            }
        }

        private void ProcessCommand(string data)
        {
            var msgAndParams = data.Split(';');

            if (data.StartsWith("stop;"))
            {
                t.Stop();
            }
            else if (data.StartsWith("start;"))
            {
                t.Start();
            }
            else if (data.StartsWith("destroy;"))
            {
                t.Close();
                element.Dispose();
            }
            else if (data.StartsWith("create;"))
            {
                try
                {
                    if (factory == null)
                        factory = new T();
                    element = factory.Create(msgAndParams[1]);
                    element.Initialize();
                    if (msgAndParams.Length > 2)
                        SetupTimer(int.Parse(msgAndParams[2]));
                    else
                        SetupTimer(100);
                }
                catch (Exception ex)
                {
                    Log.Error("Unable to create object instance. " + ex.Message);
                }
            }

            else if (data.StartsWith("mousedown;"))
            {
                element.OnMouseAction(MouseAction.LeftButtonDown, int.Parse(msgAndParams[1]),
                        int.Parse(msgAndParams[2]));
            }
            else if (data.StartsWith("mouseup;"))
            {
                element.OnMouseAction(MouseAction.LeftButtonUp, int.Parse(msgAndParams[1]),
                        int.Parse(msgAndParams[2]));
            }
            else if (data.StartsWith("mousemove;"))
            {
                element.OnMouseAction(MouseAction.Move, int.Parse(msgAndParams[1]),
                        int.Parse(msgAndParams[2]));
            }

        }

        public override void Disconnected()
        {
            element.Dispose();
        }

       
        public override void Connected(ClientHandshake handshake)
        {

        }


        void SetupTimer(int interval)
        {
            t = new Timer { Interval = interval };
            t.Elapsed += (e, s) =>
            {
                try
                {
                    Send(element.GetContextAsBase64());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            };

            t.Start();

        }

        
    }
}
