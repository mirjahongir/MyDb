# QamarDb
## Blocklar
### BaseBlock
**Asosiy Block bulib qolgan Pagelar undan nasledovat qiladi**
- PageId (4 byte)(uint)  `va UByte`
- BlockType (1byte)(byte) `BlockType`
- Hash (2byte) (ushort) `(Blockdagi Ma'lumotlarni hashlaydi) `
- UsedByte (2byte)`(ushort)` (Blokdagi Dataning used byte joyi)

### FileInfoBlock
**Fayilning boshida turadi va fayil tug`risda va sectorlar tug'risida ma'lumot saqlaydi
DataSize= 8162 /14
