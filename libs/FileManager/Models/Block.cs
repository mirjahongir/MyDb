using System.Buffers.Binary;
using System;
using System.Runtime.InteropServices;

using FileManager.Enums;

namespace FileManager.Models
{
    // 2^13=8192
    public class Block
    {
        // 4 byte
        public uint PageId;
        // 1 byte
        public BlockType BlockType;
        // 2 byte
        public ushort Count;
        //2 byte
        public ushort Hash;
        // not Add Disk
        public bool CanDelete
        {
            get
            {
                //BUG:
                return true;
            }
        }
        // 8192-9=8183
        public Memory<byte> Data { get; set; } // 2^13 - 10 = 8192 - 10 = 8182
    }

    //4 byte
    public struct DataHeader
    {
        //oxiridan boshlanadi
        //Aslida LastPostion disayam buladi
        public ushort LastPosition { get; set; } // 2 byte
        //Texni uzunligi
        public ushort Length { get; set; }
    }

}
