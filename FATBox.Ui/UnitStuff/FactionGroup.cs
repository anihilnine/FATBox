namespace FATBox.Ui.UnitStuff
{
    public class FactionGroup
    {
        public string Faction { get; set; }
        public UnitBlueprintWrapper[] Units { get; set; }
        public bool ShowDescriptions { get; set; }

        public FactionGroup(string faction, UnitBlueprintWrapper[] units, bool showDescriptions)
        {
            Faction = faction;
            Units = units;
            ShowDescriptions = showDescriptions;
        }
    }
}