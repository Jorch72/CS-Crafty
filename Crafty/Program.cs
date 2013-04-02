using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Craft.Net.Client;

namespace Crafty
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //var lastLogin = LastLogin.GetLastLogin();
            var session = new Session("Crafty");//Session.DoLogin(lastLogin.Username, lastLogin.Password);
            var client = new MinecraftClient(session);

            using (var window = new MainWindow(client))
            {
                window.Run(new IPEndPoint(IPAddress.Loopback, 25565));
            }
        }
    }
}
