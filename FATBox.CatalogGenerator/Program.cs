using System;
using FATBox.Initialization;
using Xamasoft.JsonClassGenerator;

namespace FATBox.CatalogGenerator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var gen = new JsonClassGenerator();
            var f = CatalogInitializer.WorkingFolder + @"\blueprints.json";
            gen.Example = System.IO.File.ReadAllText(f);
            gen.ExplicitDeserialization = false;
            gen.Namespace = "FATBox.Core.ModCatalog";
            //gen.SecondaryNamespace = "FATBox.Core.ModCatalog";
            gen.NoHelperClass = false;
            gen.TargetFolder = @"..\..\..\FatBox.Core\ModCatalog";
            gen.UseProperties = true;
            gen.MainClass = "Catalog";
            gen.UsePascalCase = true;
            gen.UseNestedClasses = false;
            gen.ApplyObfuscationAttributes = false;
            gen.SingleFile = false;
            gen.ExamplesInDocumentation = false;            
            gen.GenerateClasses();

        }


    }
}
