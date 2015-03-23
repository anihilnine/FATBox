using System;

namespace FATBox.Initialization
{
    public static class Initializer
    {
     
        static Initializer()
        {
            EnsureInitialized();
        }

        public static void EnsureInitialized()
        {
            if (!CatalogInitializer.IsInitialized())
                Initialize();
        }

        private static void Initialize()
        {
            var f = new InitializationForm();
            f.ShowDialog();
            if (!CatalogInitializer.IsInitialized())
            {
                throw new Exception("FATBox initialization failed");
            }
        }
    }

}