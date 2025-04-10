using FileManager.Enums;

namespace FileManager.Models
{
    // Har bir ma`lumotning boshida header buladi
    //5 byte
    public struct DataHeader
    {
        //oxiridan boshlanadi
        //Aslida LastPostion disayam buladi
        public ushort LastPosition { get; set; } // 2 byte
        //Texni uzunligi
        public ushort Length { get; set; }
        public Status DataStatus { get; set; }
    }

}
