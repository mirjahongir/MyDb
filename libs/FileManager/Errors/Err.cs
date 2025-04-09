using Core.Models;

namespace FileManager.Errors
{
    public static class Err
    {
        public static Error FileExist => Error.Create(1, 2, "File already exist");
        public static Error FileNotFound => Error.Create(2, 2, "File not Found");

        public static Error BlockSizeError { get; internal set; }
    }
}
