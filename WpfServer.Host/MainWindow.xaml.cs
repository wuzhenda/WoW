using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfServer.Host
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                var srv = ElementServer.Create<WindowFactory>("main", "http://localhost:5444");
                SetupTrayIcon();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to start the server. " + ex.Message);
                this.Close();
            }
        }

        private void SetupTrayIcon()
        {
            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon("Srv.ico");
            ni.Visible = true;
            ni.BalloonTipTitle = "Wpf Html5 Server";
            ni.BalloonTipText = "Server is listening..";
            ni.ShowBalloonTip(3000);
            ni.Text = "Wpf Html5 Server";

            ni.DoubleClick += (s, a) =>
                                  {
                                      this.Show();
                                      this.WindowState = WindowState.Normal;
                                  };
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }
    }

    public class WindowFactory : IStreamableElementFactory
    {
        public IStreamableElement Create(string key)
        {
            var obj = App.Current.Dispatcher.Invoke(new Func<IStreamableElement>(() => new WpfServer.Screens.MainWindow()));
            return obj as IStreamableElement;
        }
    }
}
