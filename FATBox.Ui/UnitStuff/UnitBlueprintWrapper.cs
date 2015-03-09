using System.Drawing;
using System.Linq;
using FATBox.Core;
using FATBox.Core.ModCatalog;

namespace FATBox.Ui.UnitStuff
{
    public class UnitBlueprintWrapper
    {
        public Blueprint Blueprint { get; set; }

        public UnitBlueprintWrapper(Blueprint blueprint)
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
        //public string UnitDescription
        //{
        //    get
        //    {
        //        return "T" + Tech + " " + Description;
        //    }
        //}

        public string FactionName
        {
            get
            {
                return Blueprint.General == null ? "" : Blueprint.General.FactionName;
            }
        }

        public int Tech
        {
            get
            {
                if (Blueprint.Categories == null) return 0;
                if (Blueprint.Categories.Contains("TECH1")) return 1;
                if (Blueprint.Categories.Contains("TECH2")) return 2;
                if (Blueprint.Categories.Contains("TECH3")) return 3;
                if (Blueprint.Categories.Contains("EXPERIMENTAL")) return 4;
                return 0;
            }
        }

        public string Description
        {
            get
            {
                return Localizer.Localize(Blueprint.Description);
            }
        }

        public string CleanDescription
        {
            get
            {
                var desc = Description ?? "";
                desc = desc.Replace("Support Armored Command Unit", "SACU");
                var rewritesString = @"xsb5202,Air Staging Facility
xsb2304,Anti-Air SAM Launcher
xsb2104,Anti-Air Turret
xas0204,Submarine
xrs0204,Submarine
ura0303,Air-Superiority Fighter
xaa0305,Heavy Gunship
dslk004,Mobile Missile Anti-Air
delk002,Mobile Missile Anti-Air
xsl0303,Heavy Assault Bot
url0303,Heavy Assault Bot
ual0201,Medium Tank
url0107,Medium Tank
xsl0101,Land Scout
xal0203,Amphibious Tank
drl0204,Range Bot
del0204,Range Bot
xsl0202,Heavy Tank
xsl0203,Amphibious Tank
xsl0205,Mobile AA Flak Artillery
url0402,Experimental Assault Bot
xrl0403,Experimental Ranged Fire
uel0401,Experimental Ranged Fire
uas0304,Submarine
urs0304,Submarine
ues0304,Submarine
xss0304,Submarine
xsb3104,Omni Sensor Array";

                var rewrites = rewritesString
                    .Replace("\n", "").Split('\r')
                    .Select(x => x.Split(','))
                    .Select(x => new
                    {
                        blueprintId = x[0],
                        Replacement = x[1]
                    });

                foreach (var rewrite in rewrites)
                    if (BlueprintId == rewrite.blueprintId)
                        desc = rewrite.Replacement;

                return desc;
            }
        }
        public string CleanName
        {
            get
            {
                var name = UnitName ?? "";
                if (name == CleanDescription) name = null;
                return name;
            }
        }

        public string Source { get { return Blueprint.Source; } }

        private Image _strategicIcon;
        public Image StrategicIcon
        {
            get
            {
                if (_strategicIcon == null)
                {
                    _strategicIcon = UiData.Cache.GetCachedStrategicIconPng(Blueprint.StrategicIconName);
                    _strategicIcon = UiData.StrategicIconFactionifier.ModifyIcon((Bitmap) _strategicIcon, FactionName);
                }
                return _strategicIcon;
            }
        }

    }
}