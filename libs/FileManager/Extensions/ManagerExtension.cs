
namespace FileManager.Extensions
{
    internal static class ManagerExtension
    {
        public static uint GetFileName(this string path)
        {
            string fileName = Path.GetFileName(path); // faqat fayl nomi (report.pdf)
            var str = Path.GetFileNameWithoutExtension(path); // report
            return uint.Parse(str);
        }
    }
}
