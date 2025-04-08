using FileManager.Extensions;
using FileManager.Models;

namespace FileManagers
{
    public sealed class MemoryCache
    {
        #region Bu qismini keyinchalik ozgartirish kerak
        /// <summary>
        ///  1. Cache uchun bloklar
        /// Birinchi block bu insert uchun block
        /// 2 Block va uchunchi block bu kup zapros uchun atak buni boshqatdan kurish kerak
        /// bunda faqat 3 block saqlanadi
        /// </summary>
        //   Memory<Block> _cache1;
        /// <summary>
        /// case 2 da faqat 6 ta blok bulishi mumkin
        /// </summary>
        //  Memory<Block> _cache2;
        #endregion
        Block InserBlock;
        uint counter;
        //delegate void InsertBlockFinishHandler(Block block);
        //event InsertBlockFinishHandler? Notify;

        public MemoryCache()
        {
            counter = 0;
        }

        public void SetBlock(ref Block block)
        {

        }
        public void RemoveBlock(uint counter)
        {

        }
        public void InserData(byte[] data)
        {
          var valueLength=  data.Length.Pow();
            
        }
    }

}
