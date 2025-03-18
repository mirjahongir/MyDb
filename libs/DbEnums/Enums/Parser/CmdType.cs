
namespace DbEnums.Enums.Parser
{
    public enum CmdType
    {
        NONE = 0,
        //DDL (Data Definition Language) - Ma'lumotlar Tuzilmasini Boshqarish
        CREATE,
        ALTER,
        DROP,
        TRUNCATE,
        //DML (Data Manipulation Language) - Ma'lumotlarni O'zgartirish
        SELECT,
        INSERT,
        UPDATE,
        DELETE,



    }

}
