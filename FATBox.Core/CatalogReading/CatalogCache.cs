using System;
using System.Drawing;
using System.IO;
using System.Linq;
using FATBox.Core.ModCatalog;
using FATBox.Initialization;
using FATBox.Util;

namespace FATBox.Core.CatalogReading
{
    public class CatalogCache
    {
        private CatalogReader _loader;

        public CatalogCache(Catalog catalog)
        {
            _loader = new CatalogReader(catalog.Mounted.ToArray());
        }
        

        private void CreateIfNotExists(string cachedFilename, Action toCreate)
        {
            if (!File.Exists(cachedFilename))
            {
                toCreate();
            }
        }

        public string GetCachedFilename(string modFilename)
        {

            if (string.IsNullOrEmpty(modFilename)) return null;
            var cacheFilename = Path.Combine(Initializer.WorkingFolder + @"\FACache", modFilename.Replace("/", "\\").TrimStart('\\'));
            var cacheFolder = Path.GetDirectoryName(cacheFilename);
            if (!Directory.Exists(cacheFolder))
                Directory.CreateDirectory(cacheFolder);
                
            CreateIfNotExists(cacheFilename, () =>
            {
                var ok = _loader.GetFile(modFilename, cacheFilename);
                if (!ok)
                {
                    // .... ?
                }
            });

            return cacheFilename;
        }


        //public byte[] GetCachedFileBytes(string modFilename)
        //{
        //    var cacheFilename = GetCachedFilename(modFilename);
        //    return File.ReadAllBytes(cacheFilename);
        //}


        //public byte[] GetCachedStrategicIconBytes(string iconName)
        //{
        //    var modFilename = _loader.GetStrategicIconFilename(iconName);
        //    return GetCachedFileBytes(modFilename);
        //}        
        
        public Image GetCachedStrategicIconPng(string iconName)
        {
            if (string.IsNullOrEmpty(iconName)) return null;
            var modDdsFilename = _loader.GetStrategicIconFilename(iconName);
            return GetCachedPng(modDdsFilename);
        }

        public Image GetFactionIconPngSmall(string factionName)
        {
            if (string.IsNullOrEmpty(factionName)) return null;
            var modDdsFilename = _loader.GetFactionIconSmallFilename(factionName);
            return GetCachedPng(modDdsFilename);
        }

        public Image GetCachedPng(string modDdsFilename)
        {
            if (string.IsNullOrEmpty(modDdsFilename)) return null;
            var cacheDdsFilename = GetCachedFilename(modDdsFilename);
            var cachePngFilename = 
                Path.GetDirectoryName(cacheDdsFilename) + "\\" +
                Path.GetFileNameWithoutExtension(cacheDdsFilename) + ".png";

            CreateIfNotExists(cachePngFilename, () =>
            {
                DirectXHelper.ConvertDdsToPng(cacheDdsFilename, cachePngFilename);
            });

            return Image.FromFile(cachePngFilename);
        }



    }
}