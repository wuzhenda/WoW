using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Nugget;
using Nugget.Framework;
using Nugget.Server;

namespace WpfServer
{
    public static class ElementServer
    {

        public static WebSocketServer Create<T>(string name, string origin, int interval = 20, string server = "localhost", int port = 8181) where T : IStreamableElementFactory, new()
        {
            // create the server
            var srv = new WebSocketServer("ws://" + server + ":" + port, origin);
            //srv.RegisterHandler<ElementSocket<T>>("/" + name);

            var wsf = new WebSocketFactory(srv);
            wsf.Register<ElementSocket<T>>("/" + name);
            
            srv.Start();
            return srv;
        }

       
    }
}
