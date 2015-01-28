using SlimDX;
using SlimDX.Direct3D10;
using SlimDX.Direct3D9;

namespace SCMAPTools
{
    public class MapUnitDisplay
    {
        public string StrategicIconName { get; set; }
        public Vector3 WorldPosition { get; set; }
        public System.Drawing.Color Color { get; set; }
        public float Scale { get; set; }

        public MapUnitDisplay()
        {
            Scale = 1;
        }

        //public void Load(MergedModDdsLoader mergedModDdsLoader)
        //{
        //    Texture = mergedModDdsLoader.LoadTexture("/textures/ui/common/game/strategicicons/" + StrategicIconName + ".dds");
        //}
    }
}