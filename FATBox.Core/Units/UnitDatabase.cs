using System.Collections.Generic;
using System.Linq;
using FATBox.Core.CatalogReading;
using FATBox.Core.ModCatalog;
using FATBox.Core.Units.Model;

namespace FATBox.Core.Units
{
    public class UnitDatabase
    {
        private readonly Catalog _catalog;
        private CatalogCache catalogCache;
        private StrategicIconFactionifier strategicIconFactionifier;

        private List<UnitBlueprintWrapper> _units;
        private List<UnitGroup> _groups = new List<UnitGroup>();

        public UnitDatabase(Catalog catalog, CatalogCache catalogCache, StrategicIconFactionifier strategicIconFactionifier)
        {
            _catalog = catalog;
            this.catalogCache = catalogCache;
            this.strategicIconFactionifier = strategicIconFactionifier;
        }

        public List<UnitGroup> Load()
        {
            //var expr = new CategoryExpression("BUILTBYTIER3FACTORY,TRANSPORTBUILTBYTIER3FACTORY,BUILTBYTIER3ENGINEER,COMMAND,BUILTBYQUANTUMGATE,ENGINEER");
            //var expr = new CategoryExpression("TECH1,TECH2,TECH3,EXPERIMENTAL");
            var includeExpression = new CategoryExpression("AEON,UEF,CYBRAN,SERAPHIM");
            var excludeExpression = new CategoryExpression("CIVILIAN,TELEPORTBEACON,PROJECTILE,FERRYBEACON,OPERATION,INVULNERABLE");
            var excludeIds = @"uxl0021,urb3103,uab5204,urb5204,ueb5204,ueb5208,ura0001,urb5206".Split(',');

            _units = _catalog.Blueprints
                .Where(includeExpression.Matches)
                .Where(x => !excludeExpression.Matches(x))
                .Where(x => !excludeIds.Contains(x.BlueprintId))
                .Select(x => new UnitBlueprintWrapper(x, catalogCache, strategicIconFactionifier))
                .OrderByDescending(x => x.CleanDescription)
                .ThenBy(x => x.CleanName)
                .ToList();


            SetupGroup("Land", "MOBILE LAND -SUBCOMMANDER -ENGINEER -COMMANDER");
            SetupGroup("Air", "MOBILE AIR -ENGINEER");
            SetupGroup("Navy", "MOBILE NAVAL -MOBILESONAR");

            SetupGroup("Commander", "COMMAND,SUBCOMMANDER");
            SetupGroup("Engineer", "ENGINEER -SUBCOMMANDER, ENGINEERSTATION");

            SetupGroup("(Structures) Military", "STRUCTURE DEFENSE,STRUCTURE STRATEGIC,STRUCTURE ORBITALSYSTEM");
            SetupGroup("Factory", "STRUCTURE FACTORY");
            SetupGroup("Economy", "STRUCTURE ECONOMIC");
            SetupGroup("Intel", "STRUCTURE INTELLIGENCE,MOBILESONAR");
            SetupOrphans();

            return _groups;
        }

        private void SetupGroup(string name, string expression)
        {
            var expr = new CategoryExpression(expression);

            var units = _units
                .Where(x => expr.Matches(x.Blueprint))
                .ToArray();

            _units.RemoveAll(units.Contains);

            var group = new UnitGroup(name, units);
            _groups.Add(group);
        }

        private void SetupOrphans()
        {
            if (_units.Any())
            {
                var group = new UnitGroup("Uncategorized", _units.ToArray());
                _groups.Add(group);
            }
        }
    }
}