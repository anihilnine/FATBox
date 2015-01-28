using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FATBox.Mapping.Save
{
    class SaveLuaParser
    {
        public void Parse()
        {
            var filename = @"E:\Documents\My Games\Gas Powered Games\Supreme Commander Forged Alliance\Maps\Balvery Mountains V2.v0001\Balvery Mountains V2_save.lua";
            var content = System.IO.File.ReadAllText(filename);
            
        }
    }
}
