using System;
using System.IO;
using System.Linq;
using Ionic.Zip;

namespace FATBox.Util
{
	public class ZipReader : IDisposable
	{
		private ZipFile _zip;

		public ZipReader(string filename)
		{
			_zip = ZipFile.Read(filename);
			_zip.FlattenFoldersOnExtract = true;
		}

        //public string[] Filenames
        //{
        //    get
        //    {
        //        return _zip
        //            .Select(p => p.FileName)
        //            .ToArray();
        //    }
        //}

        //public string GetText(string filename)
        //{
        //    return System.Text.Encoding.UTF8.GetString(GetBytes(filename));
        //}

        public void Dispose()
        {
            _zip.Dispose();
        }


        //public bool Exists(string filename)
        //{
        //    var file = _zip.FirstOrDefault(p => p.FileName.Equals(filename, StringComparison.InvariantCultureIgnoreCase));
        //    return file != null;
        //}

        //public byte[] GetBytes(string filename)
        //{
        //    var file = _zip.FirstOrDefault(p => p.FileName.Equals(filename, StringComparison.InvariantCultureIgnoreCase));
        //    if (file == null) return null;
        //    using (var sw = new MemoryStream())
        //    {
        //        file.Extract(sw);
        //        return sw.GetBuffer();
        //    }
        //}

		public bool ExtractTo(string sourceFilename, string folder)
		{
			var file = _zip[sourceFilename];
			if (file == null) return false;
			file.Extract(folder, ExtractExistingFileAction.OverwriteSilently);
			return true;
		}

		
	}
}