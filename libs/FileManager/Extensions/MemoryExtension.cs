namespace FileManager.Extensions
{
    //65535
    public static class MemoryExtension
    {
        public static uint Pow(this int length)
        {
            uint a = 8;
            while (true)
            {
                a = 8 * 2;
                if (length <= a) return a;
            }
        }
        public static ushort GetFastHashCode(ref Memory<byte> memory)
        {
            var pageData = memory.Span;
            ushort checksum = 0;
            int checksumOffset = 8; // pd_checksum offset (PageHeaderData ichida odatda 8-chi baytdan boshlanadi)

            for (int i = 0; i < pageData.Length; i += 2)
            {
                if (i == checksumOffset)
                    continue;

                ushort value = BitConverter.ToUInt16(pageData.Slice(i, 2));
                checksum ^= value;
                checksum = (ushort)((checksum << 1) | (checksum >> 15)); // rotate left
            }

            checksum ^= (ushort)(64579 & 0xFFFF);
            checksum ^= (ushort)((64579 >> 16) & 0xFFFF);

            return checksum;
        }
    }
}
