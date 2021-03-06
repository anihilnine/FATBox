﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FATBox.Core.Lua;
using FATBox.Core.ModCatalog;
using FATBox.Initialization;
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

            Initializer.EnsureInitialized();


            //// TODO: move
            DataNavigatorRenderers.Register(typeof(UnitRenderer));
            DataNavigatorRenderers.Register(typeof(StrategicIconRenderer));
            DataNavigatorRenderers.Register(typeof(ImageRenderer));
            DataNavigatorRenderers.Register(typeof(DdsRenderer));
            DataNavigatorRenderers.Register(typeof(ScmRenderer));
            DataNavigatorRenderers.Register(typeof(MapFolderRenderer));
            DataNavigatorRenderers.Register(typeof(ByteArrayRenderer));


            //new Icons().ShowDialog();

            MessagePump.Run(new MainForm(), UiData.MainLoop);
        }
    }
}
