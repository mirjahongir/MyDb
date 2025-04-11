namespace FileManager.Configs
{
    public static class FileConfig
    {
        internal static string FilePath { get; set; } = string.Empty;
        internal static ushort BlockSize { get; set; } = 8192;
        internal static ushort HeaderDataSize => 5; //4 bayt
        internal static ushort BlockDataSize => (ushort)(BlockSize - 10); // 2^13 - 10 = 8192 - 10 = 8182
        internal static byte BaseBlockSize => 12; // 8 + 1 + 2 = 11
    }
}
