using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ComTypes = System.Runtime.InteropServices.ComTypes;
using System.Linq;

namespace DVB
{
    public static class GCGGetIECahcedFile
    {
        [StructLayout(LayoutKind.Explicit, Size = 80)]
        public struct INTERNET_CACHE_ENTRY_INFOA
        {
            [FieldOffset(0)]
            public UInt32 dwStructSize;
            [FieldOffset(4)]
            public IntPtr lpszSourceUrlName;
            [FieldOffset(8)]
            public IntPtr lpszLocalFileName;
            [FieldOffset(12)]
            public UInt32 CacheEntryType;
            [FieldOffset(16)]
            public UInt32 dwUseCount;
            [FieldOffset(20)]
            public UInt32 dwHitRate;
            [FieldOffset(24)]
            public UInt32 dwSizeLow;
            [FieldOffset(28)]
            public UInt32 dwSizeHigh;
            [FieldOffset(32)]
            public ComTypes.FILETIME LastModifiedTime;
            [FieldOffset(40)]
            public ComTypes.FILETIME ExpireTime;
            [FieldOffset(48)]
            public ComTypes.FILETIME LastAccessTime;
            [FieldOffset(56)]
            public ComTypes.FILETIME LastSyncTime;
            [FieldOffset(64)]
            public IntPtr lpHeaderInfo;
            [FieldOffset(68)]
            public UInt32 dwHeaderInfoSize;
            [FieldOffset(72)]
            public IntPtr lpszFileExtension;
            [FieldOffset(76)]
            public UInt32 dwReserved;
            [FieldOffset(76)]
            public UInt32 dwExemptDelta;
        }

        [DllImport("Wininet.dll", CharSet = CharSet.Unicode)]
        public static extern bool RetrieveUrlCacheEntryFile(
        String lpszUrlName,
        out INTERNET_CACHE_ENTRY_INFOA lpCacheEntryInfo,
        ref UIntPtr lpcbCacheEntryInfo,
        UInt32 dwReserved
        );

        public static string GetCacheFileByUrl(string src)
        {
            UIntPtr einfo = new UIntPtr(80);
            INTERNET_CACHE_ENTRY_INFOA cache = new INTERNET_CACHE_ENTRY_INFOA();
            RetrieveUrlCacheEntryFile(src, out cache, ref einfo, 0);
            string filename = Marshal.PtrToStringAnsi(cache.lpszLocalFileName);
            return filename;
        }

    }
}
