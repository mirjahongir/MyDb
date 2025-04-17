using MemoryPack;

namespace QamarDb.Models.QmDatabase
{
    [MemoryPackable]
    internal partial class Database
    {
        [MemoryPackOrder(0)]
        public uint Id { get; set; }
        [MemoryPackOrder(1)]
        public required string Name { get; set; }
        [MemoryPackOrder(2)]
        public uint CreateUserId { get; set; }
        [MemoryPackOrder(3)]
        public required Dictionary<string, Schema> Schemas { get; set; }
    }
}
