using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ApkNet.ApkReader.Run
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string apkFilePath = @"SampleAPK\ApkReaderSample.apk";
            const string outputLocation = @"SampleAPK\icons\";

            var info = ReadApk.ReadApkFromPath(apkFilePath);

            Directory.CreateDirectory(outputLocation);
            for (var i = 0; i < info.iconFileName.Count; i++)
            {

                ExtractApkFile.ExtractFileAndSave(apkFilePath, info.iconFileName[i], @"SampleAPK\icons\", i);
            }

            Console.ReadKey();
        }
    }
}
