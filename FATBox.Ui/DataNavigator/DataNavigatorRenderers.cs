using System;
using System.Collections.Generic;
using System.Linq;

namespace FATBox.Ui.DataNavigator
{
    public static class DataNavigatorRenderers
    {
        private static Dictionary<Type,List<Type>> Types = new Dictionary<Type, List<Type>>();
 
        public static void Register(Type rendererType)
        {
            if (!typeof(BaseRenderer).IsAssignableFrom(rendererType))
                throw new Exception();

            var renderer = (BaseRenderer) Activator.CreateInstance(rendererType);
            foreach (var t in renderer.SupportedTypes())
            {
                if (!Types.ContainsKey(t))
                {
                    Types.Add(t, new List<Type>());
                }

                Types[t].Add(rendererType);
            }
        }

        public static BaseRenderer Get(string propertyName, object o)
        {
            var objType = o.GetType();
            foreach (var kvp in Types)
            {
                if (kvp.Key.IsAssignableFrom(objType))
                {
                    var rendererTypes = kvp.Value;

                    foreach (var rendererType in rendererTypes)
                    {
                        var renderer = (BaseRenderer) Activator.CreateInstance(rendererType);
                        var ok = renderer.SetObject(propertyName, o);
                        if (ok)
                        {
                            return renderer;
                        }

                    }

                }
            }
            
            return null;
        }
    }
}