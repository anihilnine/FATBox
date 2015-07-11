using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FATBox.Core.Units.Model;

namespace FATBox.Ui.Controls.UnitExplorerControls
{
    public partial class UnitDescriptionControl : UserControl
    {
        private UnitBlueprintWrapper _unit;

        public UnitDescriptionControl()
        {
            InitializeComponent();
        }

        public void SetUnit(UnitBlueprintWrapper unit)
        {
            _unit = unit;

            dataNavigator1.SetObject(null, unit, true);

            try
            {
                label1.Text = unit.CleanName;
                label2.Text = unit.FactionName;
                label3.Text = unit.CleanDescription;
                label4.Text = unit.BlueprintId;
                label5.Text = "TECH " + unit.Tech;
                pictureBox1.Image = unit.StrategicIcon;
                pictureBox2.Image = UiData.Cache.GetFactionIconPngSmall(unit.Blueprint.General.FactionName);

                textBox1.Text = "";
                var blueprint = unit.Blueprint;
                // health
                Report("MaxHealth", () => blueprint.Defense.MaxHealth);
                Report("RegenRate", () => blueprint.Defense.RegenRate);

                // cost
                Report("BuildCostMass", () => blueprint.Economy.BuildCostMass);
                Report("BuildCostEnergy", () => blueprint.Economy.BuildCostEnergy);

                // storage
                Report("StorageMass", () => blueprint.Economy.StorageMass);
                Report("StorageEnergy", () => blueprint.Economy.StorageEnergy);

                // usage
                Report("MaintenanceConsumptionPerSecondEnergy",
                    () => blueprint.Economy.MaintenanceConsumptionPerSecondEnergy);

                // build
                Report("BuildRate", () => blueprint.Economy.BuildRate);
                Report("BuildTime", () => blueprint.Economy.BuildTime);
                Report("BuildRadius", () => blueprint.Economy.BuildRadius);
                Report("BuildableCategory", () => String.Join(", ", blueprint.Economy.BuildableCategory));
                Report("MaxBuildDistance", () => blueprint.Economy.MaxBuildDistance);

                //intel
                Report("Intel.Cloak", () => blueprint.Intel.Cloak);
                Report("Intel.CloakFieldRadius", () => blueprint.Intel.CloakFieldRadius);
                Report("Intel.IntelDurationOnDeath", () => blueprint.Intel.IntelDurationOnDeath);
                Report("Intel.JamRadius.Min", () => blueprint.Intel.JamRadius.Min);
                Report("Intel.JamRadius.Max", () => blueprint.Intel.JamRadius.Max);
                Report("Intel.JammerBlips", () => blueprint.Intel.JammerBlips);
                Report("Intel.MaxVisionRadius", () => blueprint.Intel.MaxVisionRadius);
                Report("Intel.MinVisionRadius", () => blueprint.Intel.MinVisionRadius);
                Report("Intel.OmniRadius", () => blueprint.Intel.OmniRadius);
                Report("Intel.RadarRadius", () => blueprint.Intel.RadarRadius);
                Report("Intel.RadarStealth", () => blueprint.Intel.RadarStealth);
                Report("Intel.RadarStealthField", () => blueprint.Intel.RadarStealthField);
                Report("Intel.RadarStealthFieldRadius", () => blueprint.Intel.RadarStealthFieldRadius);
                Report("Intel.ReactivateTime", () => blueprint.Intel.ReactivateTime);
                Report("Intel.RemoteViewingRadius", () => blueprint.Intel.RemoteViewingRadius);
                Report("Intel.SonarRadius", () => blueprint.Intel.SonarRadius);
                Report("Intel.SonarStealth", () => blueprint.Intel.SonarStealth);
                Report("Intel.SonarStealthFieldRadius", () => blueprint.Intel.SonarStealthFieldRadius);
                Report("Intel.SpoofRadius.Min", () => blueprint.Intel.SpoofRadius.Min);
                Report("Intel.SpoofRadius.Max", () => blueprint.Intel.SpoofRadius.Max);
                Report("Intel.StealthWaitTime", () => blueprint.Intel.StealthWaitTime);
                Report("Intel.VisionRadius", () => blueprint.Intel.VisionRadius);
                Report("Intel.VisionRadiusOnDeath", () => blueprint.Intel.VisionRadiusOnDeath);
                Report("Intel.WaterVisionRadius", () => blueprint.Intel.WaterVisionRadius);
                Report("Intel.WaterVisionradius", () => blueprint.Intel.WaterVisionradius);

                // physics
                Report("Physics.MaxSpeed", () => blueprint.Physics.MaxSpeed);
                Report("Physics.TurnRate", () => blueprint.Physics.TurnRate);

                // wreckage
                Report("Wreckage.HealthMult", () => blueprint.Wreckage.HealthMult);
                Report("Wreckage.MassMult", () => blueprint.Wreckage.MassMult);
                Report("Wreckage.EnergyMult", () => blueprint.Wreckage.EnergyMult);
                Report("Wreckage.ReclaimTimeMultiplier", () => blueprint.Wreckage.ReclaimTimeMultiplier);

                if (blueprint.Weapon != null)
                {

                    for (int i = 0; i < blueprint.Weapon.Count; i++)
                    {
                        Report("\r\nWeapon." + i, () => "...");
                        Report("\tDisplayName", () => blueprint.Weapon[0].DisplayName);
                        Report("\tProjectileId", () => blueprint.Weapon[0].ProjectileId);
                        Report("\tProjectilesPerOnFire", () => blueprint.Weapon[0].ProjectilesPerOnFire);
                        Report("\tRackSalvoSize", () => blueprint.Weapon[0].RackSalvoSize);
                        Report("\tRequiresEnergy", () => blueprint.Weapon[0].RequiresEnergy);
                        Report("\tRequiresMass", () => blueprint.Weapon[0].RequiresMass);
                        Report("\tWeaponCategory", () => blueprint.Weapon[0].WeaponCategory);
                    }

                }   
                
                //if (blueprint.UpgradeDesc != null)
                //{

                //    for (int i = 0; i < blueprint.UpgradeDesc.Count; i++)
                //    {
                //        Report("\r\nWeapon." + i, () => "...");
                //        Report("\tDisplayName", () => blueprint.Weapon[0].DisplayName);
                //        Report("\tProjectileId", () => blueprint.Weapon[0].ProjectileId);
                //        Report("\tProjectilesPerOnFire", () => blueprint.Weapon[0].ProjectilesPerOnFire);
                //        Report("\tRackSalvoSize", () => blueprint.Weapon[0].RackSalvoSize);
                //        Report("\tRequiresEnergy", () => blueprint.Weapon[0].RequiresEnergy);
                //        Report("\tRequiresMass", () => blueprint.Weapon[0].RequiresMass);
                //        Report("\tWeaponCategory", () => blueprint.Weapon[0].WeaponCategory);
                //    }

                //}

            }
            catch (Exception ex)
            {
                {
                    textBox1.Text = ex.ToString();
                }

            }
        }

        private void Report(string name, Func<object> a)
        {
            try
            {
                var v = a();
                if (v == null)
                {

                }
                else if (v is int && (int)v == 0)
                {

                }
                else if (v is double && (double)v == 0)
                {

                }
                else if (v is float && (float)v == 0)
                {

                }
                else
                {
                    textBox1.AppendText(name + ": " + v + "\r\n");
                }
            }
            catch (Exception)
            {

            }
        }

		private void pictureBox2_Click(object sender, EventArgs e)
		{
			Clipboard.SetText("\"" + _unit.BlueprintId + "\", -- " + _unit.UnitName);
		}
    }
}
