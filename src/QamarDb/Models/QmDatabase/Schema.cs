using MemoryPack;

namespace QamarDb.Models.QmDatabase
{
    [MemoryPackable]
    internal class Schema
    {
        [MemoryPackOrder(0)]
        public ushort Id { get; set; }
        [MemoryPackOrder(1)]
        public string? Name { get; set; }
        [MemoryPackOrder(2)]
        public Dictionary<string, Table>? Tables { get; set; }
        [MemoryPackIgnore]
        public Database? MyDatabase { get; set; }
    }
}
