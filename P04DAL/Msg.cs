using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P04DAL
{
    /// <summary>
    /// 消息表数据操作类
    /// </summary>
    public class Msg : BaseDAL<P05MODEL.Msg>
    {
        public string TestForEf()
        {
            return "EF哈哈";
        }
    }
}
