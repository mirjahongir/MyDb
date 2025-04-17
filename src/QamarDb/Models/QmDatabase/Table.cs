
using MemoryPack;

namespace QamarDb.Models.QmDatabase
{
    [MemoryPackable]
    internal partial class Table
    {
        [MemoryPackOrder(0)]
        public ushort Id { get; set; }
        [MemoryPackOrder(1)]
        public required string Name { get; set; }
        [MemoryPackOrder(2)]
        public Schema? Schema { get; set; }
        [MemoryPackOrder(3)]
        public Dictionary<string, Field> Fields { get; set; }
    }
}
