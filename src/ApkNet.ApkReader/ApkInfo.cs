﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApkNet.ApkReader
{
    public class ApkInfo
    {
        public static int FINE = 0;
        public static int NULL_VERSION_CODE = 1;
        public static int NULL_VERSION_NAME = 2;
        public static int NULL_PERMISSION = 3;
        public static int NULL_ICON = 4;
        public static int NULL_CERT_FILE = 5;
        public static int BAD_CERT = 6;
        public static int NULL_SF_FILE = 7;
        public static int BAD_SF = 8;
        public static int NULL_MANIFEST = 9;
        public static int NULL_RESOURCES = 10;
        public static int NULL_DEX = 13;
        public static int NULL_METAINFO = 14;
        public static int BAD_JAR = 11;
        public static int BAD_READ_INFO = 12;
        public static int NULL_FILE = 15;
        public static int HAS_REF = 16;

    

        public String label;
        public String versionName;
        public String versionCode;
        public String minSdkVersion;
        public String targetSdkVersion;
        public String packageName;
        public List<String> Permissions;
        public List<String> iconFileName;
        public List<String> iconFileNameToGet;
        public List<String> iconHash;
        public String resourcesFileName;
        public byte[] resourcesFileBytes;
        public bool hasIcon;
        public bool supportSmallScreens;
        public bool supportNormalScreens;
        public bool supportLargeScreens;
        public bool supportAnyDensity;
        public Dictionary<string, List<string>> ResStrings;
        public Dictionary<string, string> LayoutStrings;

        public static bool SupportSmallScreen(byte[] dpi)
        {
            return dpi[0] == 1;
        }

        public static bool SupportNormalScreen(byte[] dpi)
        {
            return dpi[1] == 1;
        }

        public static bool SupportLargeScreen(byte[] dpi)
        {
            return dpi[2] == 1;
        }
        
        //public byte[] getDPI()
        //{
        //    byte[] dpi = new byte[3];
        //    if (this.supportAnyDensity)
        //    {
        //        dpi[0] = 1;
        //        dpi[1] = 1;
        //        dpi[2] = 1;
        //    }
        //    else
        //    {
        //        if (this.supportSmallScreens)
        //            dpi[0] = 1;
        //        if (this.supportNormalScreens)
        //            dpi[1] = 1;
        //        if (this.supportLargeScreens)
        //            dpi[2] = 1;
        //    }
        //    return dpi;
        //}

        public ApkInfo()
        {
            hasIcon = false;
            supportSmallScreens = false;
            supportNormalScreens = false;
            supportLargeScreens = false;
            supportAnyDensity = true;
            versionCode = null;
            versionName = null;
            iconFileName = null;
            iconFileNameToGet = null;

            Permissions = new List<String>();
        }

        private bool IsReference(IEnumerable<string> strs)
        {
            try
            {
                if (strs.Any(IsReference))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return false;
        }

        private static bool IsReference(string str)
        {
            try
            {
                if (str != null && str.StartsWith("@"))
                {
                    int.Parse(str, System.Globalization.NumberStyles.HexNumber);
                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return false;
        }
    }
}
