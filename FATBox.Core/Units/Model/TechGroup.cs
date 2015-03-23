using System.Linq;

namespace FATBox.Core.Units.Model
{
    public class TechGroup
    {
        public DescriptionGroup[] DescriptionGroups { get; set; }

        public int Tech { get; set; }

        public TechGroup(int tech, UnitBlueprintWrapper[] units)
        {
            DescriptionGroups = units
                .GroupBy(x => x.CleanDescription)
                .OrderBy(x => x.Key)
                .Select(x => new DescriptionGroup(x.Key, x.ToArray(), false))
                .ToArray();

            var solos = DescriptionGroups.Where(x => x.FactionGroups.SelectMany(y => y.Units).Count() == 1).ToArray();

            DescriptionGroups = DescriptionGroups.Where(x => x.FactionGroups.SelectMany(y => y.Units).Count() != 1).ToArray();

            var soloUnits = solos.SelectMany(x => x.FactionGroups).SelectMany(y => y.Units).ToArray();
            if (soloUnits.Any())
            {
                var soloGroup = new DescriptionGroup("Others", soloUnits, true);
                DescriptionGroups = DescriptionGroups.Union(new[] {soloGroup}).ToArray();
            }

            Tech = tech;
        }
    }
}