using System;
using System.Collections.Generic;

namespace FATBox.Ui.DataNavigator
{
    public static class DataNavigatorRenderers
    {
        private static Dictionary<Type, Type> Types = new Dictionary<Type, Type>();
 
        public static void Register(Type rendererType)
        {
            if (!typeof(BaseRenderer).IsAssignableFrom(rendererType))
                throw new Exception();

            var renderer = (BaseRenderer) Activator.CreateInstance(rendererType);
            foreach (var t in renderer.SupportedTypes())
            {
                Types[t] = rendererType;
            }
        }

        public static BaseRenderer Get(string propertyName, object o)
        {
            var objType = o.GetType();
            foreach (var kvp in Types)
            {
                if (kvp.Key.IsAssignableFrom(objType))
                {
                    var rendererType = kvp.Value;

                    if (rendererType != null)
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