using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ApkNet.ApkReader.Run
{
    public static class ExtractApkFile
    {
        public static void ExtractFileAndSave(string apkFilePath, string fileResourceLocation, string filePathToSave, int index)
        {
            using (var zip = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(File.OpenRead(apkFilePath)))
            {
                using (var fileStream = new FileStream(apkFilePath, FileMode.Open, FileAccess.Read))
                {
                    var zipFile = new ICSharpCode.SharpZipLib.Zip.ZipFile(fileStream);
                    ICSharpCode.SharpZipLib.Zip.ZipEntry item;
                    while ((item = zip.GetNextEntry()) != null)
                    {
                        if (item.Name.ToLower() != fileResourceLocation) continue;
                        var fileLocation = Path.Combine(filePathToSave, $"{index}-{fileResourceLocation.Split(Convert.ToChar(@"/")).Last()}");
                        using (var stream = zipFile.GetInputStream(item))
                        using (var output = File.Create(fileLocation))
                        {
                            stream.CopyTo(output);
                        }
                    }
                }
            }
        }
    }
}
