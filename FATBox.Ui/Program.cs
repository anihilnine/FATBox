using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FATBox.Core.ModCatalog;
using FATBox.Ui.DataNavigator;
using FATBox.Ui.Renderers;
using SlimDX.Windows;

namespace FATBox.Ui
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

            // TODO: move
            DataNavigatorRenderers.Register(typeof(UnitRenderer));
            DataNavigatorRenderers.Register(typeof(StrategicIconRenderer));
            DataNavigatorRenderers.Register(typeof(ImageRenderer));
            DataNavigatorRenderers.Register(typeof(DdsRenderer));
            DataNavigatorRenderers.Register(typeof(MapFolderRenderer));


            Application.Run(new MapExplorer());
            //Application.Run(new BlueprintExplorer());
            
            //var mv = new MapViewer();
            //MessagePump.Run(mv, mv.MainLoop);
        }
    }
}
