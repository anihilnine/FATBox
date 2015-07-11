using System;
using System.Drawing;
using System.Windows.Forms;
using FATBox.Core;
using FATBox.Core.CatalogReading;
using FATBox.Core.Lua;
using FATBox.Core.MapScenarioLua;
using FATBox.Core.ModCatalog;
using FATBox.Initialization;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using SlimDX.Direct3D9;

namespace FATBox.Ui
{    

    public static class UiData
    {
        private static Form _thinkingForm;

        static UiData()
        {
            var f = CatalogInitializer.WorkingFolder + @"\blueprints.json";
            var str = System.IO.File.ReadAllText(f);
            Catalog = JsonConvert.DeserializeObject<Catalog>(str); // todo: wrap
            Cache = new CatalogCache(Catalog);
            Lore = new Lore();
            StrategicIconFactionifier = new StrategicIconFactionifier(Lore);
            MapScenarioLuaParser = new MapScenarioLuaParser(Cache);
            DirectX9Device = CreateDevice();

        }

        public static MapScenarioLuaParser MapScenarioLuaParser { get; set; }

        public static void Initialize(MainForm mainForm)
        {
            MainForm = mainForm;
        }

        public static MainForm MainForm { get; set; }


        public static Device DirectX9Device { get; set; }

        public static CatalogCache Cache { get; set; }
        public static Catalog Catalog { get; set; }
        public static StrategicIconFactionifier StrategicIconFactionifier { get; set; }
        public static Lore Lore { get; set; }

        private static Device CreateDevice()
        {
            PresentParameters PP = new PresentParameters();
            PP.BackBufferFormat = Format.X8R8G8B8;
            return new Device(new Direct3D(), 0, DeviceType.Hardware, new IntPtr(0), CreateFlags.HardwareVertexProcessing, PP);
        }

        public static event EventHandler Tick;

        public static void MainLoop()
        {
            if (Tick != null)
                Tick(null, null);
        }


    }
}