using System;

namespace FATBox.Initialization
{
    public static class Wait
    {
        public static void Until(Func<bool> a)
        {
            while (!a())
            {
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}