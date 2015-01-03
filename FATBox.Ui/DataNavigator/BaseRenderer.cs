using System;
using System.Windows.Forms;

namespace FATBox.Ui.DataNavigator
{
    public class BaseRenderer : UserControl
    {
        public virtual Type[] SupportedTypes()
        {
            return new Type[0];
        }

        public virtual bool SetObject(string propertyName, object o)
        {
            return false;
        }
    }

    public class BaseRenderer<T> : BaseRenderer
    {
        public override Type[] SupportedTypes()
        {
            return new[] { typeof(T) };
        }

        public override bool SetObject(string propertyName, object o)
        {
            return SetObject(propertyName, (T)o);
        }

        public virtual bool SetObject(string propertyName, T value)
        {
            return false;
        }
    }
}