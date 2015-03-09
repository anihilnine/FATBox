using System.Linq;

namespace FATBox.Ui.UnitStuff
{
    public class UnitGroup
    {
        public TechGroup[] TechGroups { get; set; }
        public string Name { get; set; }

        public UnitGroup(string name, UnitBlueprintWrapper[] units)
        {
            Name = name;
            TechGroups = units
                .GroupBy(x => x.Tech)
                .OrderBy(x => x.Key)
                .Select(x => new TechGroup(x.Key, x.ToArray()))
                .ToArray();
        }
    }
}