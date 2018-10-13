using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using Xunit;

namespace ApkNet.ApkReader.Test
{
    public class ApkReaderTest
    {
        #region Tests

        private const string apkFilePath = @"SampleAPK/ApkReaderSample.apk";
        private readonly ApkInfo _apkInfo;
        public ApkReaderTest()
        {
            _apkInfo = ReadApkFromPath(apkFilePath);
        }

        [Fact]
        public void Should_Success_When_PackageNameIsCorrect()
        {
            Assert.NotNull(_apkInfo);
            Assert.Equal("com.Iteedee.ApkReaderSample", _apkInfo.packageName);
        }

        [Fact]
        public void Should_Success_When_PackageNameIsIncorrect()
        {
            Assert.NotNull(_apkInfo);
            Assert.NotEqual("apk", _apkInfo.packageName);
        }

        [Fact]
        public void Should_Success_When_VersionNameIsCorrect()
        {
            Assert.NotNull(_apkInfo);
            Assert.Equal("1.0", _apkInfo.versionName);
        }

        [Fact]
        public void Should_Success_When_VersionNameIsIncorrect()
        {
            Assert.NotNull(_apkInfo);
            Assert.NotEqual("2.0", _apkInfo.versionName);
        }

        [Fact]
        public void Should_Success_When_VersionCodeIsCorrect()
        {
            Assert.NotNull(_apkInfo);
            Assert.Equal("1", _apkInfo.versionCode);
        }

        [Fact]
        public void Should_Success_When_VersionCodeIsIncorrect()
        {
            Assert.NotNull(_apkInfo);
            Assert.NotEqual("2", _apkInfo.versionCode);
        }

        [Fact]
        public void Should_Success_When_IconCountGreaterThanZero()
        {
            Assert.NotNull(_apkInfo);
            Assert.True(_apkInfo.hasIcon);
            Assert.True(_apkInfo.iconFileName.Count > 0);
        }

        [Fact]
        public void Should_Success_When_MinSdkVersionIsCorrect()
        {
            Assert.NotNull(_apkInfo);
            Assert.Equal("8", _apkInfo.minSdkVersion);
        }

        [Fact]
        public void Should_Success_When_MinSdkVersionIsIncorrect()
        {
            Assert.NotNull(_apkInfo);
            Assert.NotEqual("2", _apkInfo.minSdkVersion);
        }

        [Fact]
        public void Should_Success_When_TargetSdkVersionIsCorrect()
        {
            Assert.NotNull(_apkInfo);
            Assert.Equal("15", _apkInfo.targetSdkVersion);
        }

        [Fact]
        public void Should_Success_When_TargetSdkVersionIsIncorrect()
        {
            Assert.NotNull(_apkInfo);
            Assert.NotEqual("20", _apkInfo.targetSdkVersion);
        }

        [Fact]
        public void Should_Success_When_PermissonCountGreaterThanZero()
        {
            Assert.NotNull(_apkInfo);
            Assert.NotNull(_apkInfo.Permissions);
            Assert.True(_apkInfo.Permissions.Count > 0);
        }

        [Fact]
        public void Should_Success_When_ApkSupportAnyDensity()
        {
            Assert.NotNull(_apkInfo);
            Assert.True(_apkInfo.supportAnyDensity);
        }

        [Fact]
        public void Should_Success_When_ApkDoesNotSupportLargeScreens()
        {
            Assert.NotNull(_apkInfo);
            Assert.False(_apkInfo.supportLargeScreens);
        }

        [Fact]
        public void Should_Success_When_ApkDoesNotSupportNormalScreens()
        {
            Assert.NotNull(_apkInfo);
            Assert.False(_apkInfo.supportNormalScreens);
        }

        [Fact]
        public void Should_Success_When_ApkDoesNotSupportSmallScreens()
        {
            Assert.NotNull(_apkInfo);
            Assert.False(_apkInfo.supportNormalScreens);
        }

        #endregion

        #region Helpers

        private ApkInfo ReadApkFromPath(string path)
        {
            byte[] manifestData = null;
            byte[] resourcesData = null;
            using (var zip = new ZipInputStream(File.OpenRead(path)))
            {
                using (var filestream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    var zipfile = new ICSharpCode.SharpZipLib.Zip.ZipFile(filestream);
                    ICSharpCode.SharpZipLib.Zip.ZipEntry item;
                    while ((item = zip.GetNextEntry()) != null)
                    {
                        if (item.Name.ToLower() == "androidmanifest.xml")
                        {
                            manifestData = new byte[50 * 1024];
                            using (var strm = zipfile.GetInputStream(item))
                            {
                                strm.Read(manifestData, 0, manifestData.Length);
                            }

                        }
                        if (item.Name.ToLower() == "resources.arsc")
                        {
                            using (var strm = zipfile.GetInputStream(item))
                            {
                                using (var s = new BinaryReader(strm))
                                {
                                    resourcesData = s.ReadBytes((int)s.BaseStream.Length);
                                }
                            }
                        }
                    }
                }
            }

            var apkReader = new ApkReader();
            var info = apkReader.extractInfo(manifestData, resourcesData);

            return info;
        }

        #endregion
    }
}
