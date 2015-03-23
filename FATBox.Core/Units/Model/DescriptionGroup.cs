using System.Linq;

namespace FATBox.Core.Units.Model
{
    public class DescriptionGroup
    {
        public FactionGroup[] FactionGroups { get; set; }

        public string Description { get; set; }

        public DescriptionGroup(string description, UnitBlueprintWrapper[] units, bool showDescriptions)
        {
            FactionGroups = units
                .GroupBy(x => x.FactionName)
                .OrderBy(x => x.Key)
                .Select(x => new FactionGroup(x.Key, x.ToArray(), showDescriptions))
                .ToArray();

            Description = description;
        }
    }
}