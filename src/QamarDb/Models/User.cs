using MemoryPack;

using QamarDb.Enums;

namespace QamarDb.Models
{
    [MemoryPackable]
    internal partial class User
    {
        [MemoryPackOrder(0)]
        public uint Id { get; set; }
        [MemoryPackOrder(1)]
        public string? UserName { get; set; }
        [MemoryPackOrder(3)]
        public string? Password { get; set; }
        [MemoryPackOrder(4)]
        public ulong CreateDateTime { get; set; }
        [MemoryPackOrder(5)]
        public uint CreateUserId { get; set; }
        [MemoryPackOrder(6)]
        public Memory<UserRole> Roles { get; set; }
        [MemoryPackOrder(7)]
        public List<uint>? Databases { get; set; }

    }

}
