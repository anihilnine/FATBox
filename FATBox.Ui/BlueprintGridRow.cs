using System.Drawing;
using FATBox.Core;
using FATBox.Core.ModCatalog;

namespace FATBox.Ui
{
    internal class BlueprintGridRow
    {
        public BlueprintGridRow(Blueprint blueprint)
        {
            Blueprint = blueprint;
        }

        public string BlueprintId { get { return Blueprint.BlueprintId; } }
        public string Type { get { return Blueprint.Type; } }

        public string UnitName
        {
            get
            {
                return Blueprint.General == null ? "" : Localizer.Localize(Blueprint.General.UnitName);
            }
        }

        public string FactionName
        {
            get
            {
                return Blueprint.General == null ? "" : Blueprint.General.FactionName;
            }
        }

        public string Tech
        {
            get
            {
                if (Blueprint.Categories == null) return null;
                if (Blueprint.Categories.Contains("TECH1")) return "1";
                if (Blueprint.Categories.Contains("TECH2")) return "2";
                if (Blueprint.Categories.Contains("TECH3")) return "3";
                if (Blueprint.Categories.Contains("EXPERIMENTAL")) return "4";
                return "";
            }
        }

        public string Description { get { return Localizer.Localize(Blueprint.Description); } }
        public string Source { get { return Blueprint.Source; } }

        public Image StrategicIcon
        {
            get
            {
                var icon = UiData.Cache.GetCachedStrategicIconPng(Blueprint.StrategicIconName);
                icon = UiData.StrategicIconFactionifier.ModifyIcon((Bitmap)icon, FactionName);
                return icon;
            }
        }

        public Blueprint Blueprint { get; set; }
    }
}