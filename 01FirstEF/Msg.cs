//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace _01FirstEF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Msg
    {
        public int mId { get; set; }
        public int mFromUser { get; set; }
        public int mToUser { get; set; }
        public string mMsg { get; set; }
        public System.DateTime mAddtime { get; set; }
        public bool mIsDel { get; set; }
    
        public virtual User mFromUsers { get; set; }
        public virtual User mToUsers { get; set; }
    }
}
