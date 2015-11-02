using P05MODEL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _02WebApp
{
    public partial class Index : System.Web.UI.Page
    {
        LeaveWordBoradEntities db = new LeaveWordBoradEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            Delete();
            Response.Write("<br/>OK");
        }

        #region 1.0简单的查询+void Query()
        private void Query1()
        {
            var user = db.Users.Where(u => u.uId == 1).FirstOrDefault();
            Response.Write(user.ToString());
        } 
        #endregion

        #region 2.0 简单查询 -void Query()
        /// <summary>
        /// 2.0 简单查询
        /// </summary>
        private void Query()
        {
            //2.0 IEnumerable（集合） 和 IQueryable（EF里的DbSet<T>） 里的 SQO 本质不一样！：
            //2.1 集合 的 标准查询运算符 方法 ，是来自于 System.Linq.Enumerable 里 给 IEnumerable 接口添加的扩展方法 
            //List<string> listStr = new List<string>();
            //listStr.Where

            //EF上下文里的 DBSet<T> 里的 标准查询运算符 方法，来自于 System.Linq.Queryable 里给 IQueryable接口 添加的扩展方法
            //2.2 变相的 即时查询
            List<User> list = db.Users.Where(u => u.uName == "刘德华").ToList();
            list.ForEach(u => Console.WriteLine(u.ToString()));
        }
        #endregion

        #region 2.1 简单查询 延迟加载 -void QueryDelay_01()
        //【延时加载】 看成两种：
        //2.1 EF本身查询方法 返回的都是 IQueryable接口，此时并未查询数据库；只有当调用接口方法 获取 数据时，才会查询数据库
        //2.1.1 【延时加载】，本质原因之一：当前可能通过多个SQO方法 来组合 查询条件，那么每个方法 都只是添加一个查询条件而已，无法确定本次查询条件 是否 已经添加结束
        //                          所以，没有办法在每个SQO方法的时候确定SQL语句是什么，只能返回一个 包含了所有添加的条件的 DBQuery 对象，
        //                          当使用这个 DBQuery对象 的时候，才根据所有条件 生成 sql语句，查询数据库
        private void QueryDelay_01()
        {
            DbQuery<User> dbQuery = db.Users.Where(u => u.uLoginName == "刘德华").OrderBy(u => u.uName).Take(2) as System.Data.Entity.Infrastructure.DbQuery<User>;
            //获得 延迟查询对象后，调用对象的 获取第一个数据方法，此时，【就会根据之前的条件】，生成sql语句，查询数据库了~~！
            User usr01 = dbQuery.FirstOrDefault();// ToList()......
            Console.WriteLine(usr01.uLoginName);
        }

        //2.1.2【延迟加载】- 针对于 外键实体 的延迟(按需加载)！
        //                  本质原因之二：对于外键属性而言，EF会在用到这个外键属性的时候才去查询 对应的 表。
        private void QueryDelay_02()
        {
            IQueryable<UsersAddress> addrs = db.UsersAddresses.Where(a => a.udUId == 1);//真实返回的 DbQuery 对象，以接口方式返回
            //a.此时只查询了 地址表
            UsersAddress addr01 = addrs.FirstOrDefault();
            //b.当访问 地址对象 里的 外键实体时，EF会查询 地址对应 的用户表;查询到之后，将数据 装入 这个外键实体
            Console.WriteLine(addr01.User.uName);

            //c.【延迟加载】按需加载 的缺点：每次调用外键实体时，都会去查询数据库（EF有小优化：相同的外键实体只查一次）
            IQueryable<UsersAddress> addrs2 = db.UsersAddresses;
            foreach (UsersAddress add in addrs2)
            {
                Console.WriteLine(add.udAddress + ":userName=" + add.User.uName);
            }
        }
        #endregion

        #region 2.2 连接查询(生成 inner join) -void QueryInnerJoin()
        /// <summary>
        /// 2.2 连接查询(生成 inner join)
        /// </summary>
        private void QueryInnerJoin()
        {
            //通过Include方法，设置 EF 生成 sql 语句时，使用 inner join 把 地址表对应的 User属性 也查出来
            //  select * from UsersAddresses a inner join Users u on a.udId =u.id 
            IQueryable<UsersAddress> addrs = db.UsersAddresses.Include("User").Where(a => a.udId == 1);
            foreach (UsersAddress add in addrs)
            {
                Console.WriteLine(add.udAddress + ":userName=" + add.User.uName);
            }

            //练习：查询消息表的同时，显示 消息发送人 和 接收名字
            IQueryable<Msg> msgs = db.Msgs.Include("User").Include("User1");
            foreach (Msg m in msgs)
            {
                Console.WriteLine(m.mId + ",发送人：" + m.User.uName + ",接收人：" + m.User1.uName + ",消息内容：" + m.mMsg);
            }
        }
        #endregion

        #region 3.0基本的修改(官方推荐的方式)+void Edit()
        private void Edit()
        {
            //1先查询出实体
            User user = db.Users.Where(u => u.uId == 1).FirstOrDefault();
            //2.修改内容
            user.uName = "张惠妹";
            //3保存修改
            db.SaveChanges();
        } 
        #endregion
       
        #region 4.0基本的增加+oid Add()
        private void Add()
        {
            User user = new User()
            {
                uName = "张信哲1",
                uAddtime = DateTime.Now,
                uLoginName = "zyh1",
                uPwd = "123",
                uIsDel = false
            };
            db.Users.Add(user);
            db.SaveChanges();
            Response.Write("添加用户成功了");

        } 
        #endregion


        #region 4.0基本的的删除
        private void Delete()
        {
            User user = new User() { uId=6};
            db.Users.Attach(user);
            db.Users.Remove(user);
            db.SaveChanges();
            Response.Write("删除成功");
        }
        #endregion

    }
}