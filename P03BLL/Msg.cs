using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P03BLL
{
    /// <summary>
    /// 业务子类 继承 业务父类
    /// </summary>
    public class Msg : BaseBLL<P05MODEL.Msg>
    {
        P04DAL.Msg dalMsg = null;
        //重写 业务父类的 方法
        public override void SetDAL()
        {
            //为 父类 里的 数据操作对象 实例化（数据层里对应的 数据操作类）
            dal = new P04DAL.Msg();
            //为了方便子类里调用 数据子类的方法，同时 为 业务子类的 数据操作子类对象 赋值
            dalMsg = dal as P04DAL.Msg;
        }

        /// <summary>
        /// 在业务子类中 可以很方便 的调用 数据子类里的方法了！！
        /// </summary>
        public string TestForEf()
        {
           return  dalMsg.TestForEf();
        }
    }
}
