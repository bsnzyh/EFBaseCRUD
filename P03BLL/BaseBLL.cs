using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace P03BLL
{
    public abstract class BaseBLL<T> where T:class,new()
    {
        //业务层父类中 要定义一个 数据操作对象，方便 后面的方法里 操作数据层方法
        //但是，业务父类中 无法确定 要用 实例化数据层 的哪个类
        protected P04DAL.BaseDAL<T> dal = null;

        //所以，声明一个 抽象方法，要子类去重写，并在重写方法中，为 业务父类 里的 数据操作对象实例化
        public abstract void SetDAL();

        public BaseBLL()
        {
            SetDAL();
        }


        #region 1.0 新增 实体 +int Add(T model)
        /// <summary>
        /// 新增 实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(T model)
        {
            return dal.Add(model);
        }
        #endregion

        #region 2。0 根据 用户 id 删除 +int Del(int uId)
        /// <summary>
        /// 根据 用户 id 删除
        /// </summary>
        /// <param name="uId"></param>
        /// <returns></returns>
        public int Del(T model)
        {
            return dal.Del(model);
        }
        #endregion

        #region 3.0 根据条件删除 +int DelBy(Expression<Func<T, bool>> delWhere)
        /// <summary>
        /// 3.0 根据条件删除
        /// </summary>
        /// <param name="delWhere"></param>
        /// <returns></returns>
        public int DelBy(Expression<Func<T, bool>> delWhere)
        {
            return dal.DelBy(delWhere);
        }
        #endregion

        #region 4.0 修改 +int Modify(T model, params string[] proNames)
        /// <summary>
        /// 4.0 修改，如：
        /// </summary>
        /// <param name="model">要修改的实体对象</param>
        /// <param name="proNames">要修改的 属性 名称</param>
        /// <returns></returns>
        public int Modify(T model, params string[] proNames)
        {
            return dal.Modify(model, proNames);
        }
        #endregion

        #region  4.0 批量修改 +int Modify(T model, Expression<Func<T, bool>> whereLambda, params string[] modifiedProNames)
        /// <summary>
        ///  4.0 批量修改 +int Modify(T model, Expression<Func<T, bool>> whereLambda, params string[] modifiedProNames)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="whereLambda"></param>
        /// <param name="modifiedProNames"></param>
        /// <returns></returns>
        public int ModifyBy(T model, Expression<Func<T, bool>> whereLambda, params string[] modifiedProNames)
        {
            return dal.ModifyBy(model, whereLambda, modifiedProNames);
        }
        #endregion

        #region 5.0 根据条件查询 +List<T> GetListBy(Expression<Func<T,bool>> whereLambda)
        /// <summary>
        /// 5.0 根据条件查询 +List<T> GetListBy(Expression<Func<T,bool>> whereLambda)
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public List<T> GetListBy(Expression<Func<T, bool>> whereLambda)
        {
            return dal.GetListBy(whereLambda);
        }
        #endregion

        #region 5.1 根据条件 排序 和查询 + List<T> GetListBy<TKey>
        /// <summary>
        /// 5.1 根据条件 排序 和查询
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="whereLambda">查询条件 lambda表达式</param>
        /// <param name="orderLambda">排序条件 lambda表达式</param>
        /// <returns></returns>
        public List<T> GetListBy<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda)
        {
            return dal.GetListBy(whereLambda, orderLambda);
        }
        #endregion

        #region 6.0 分页查询 + List<T> GetPagedList<TKey>
        /// <summary>
        /// 6.0 分页查询 + List<T> GetPagedList<TKey>
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereLambda">条件 lambda表达式</param>
        /// <param name="orderBy">排序 lambda表达式</param>
        /// <returns></returns>
        public List<T> GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderBy)
        {
            return dal.GetPagedList(pageIndex, pageSize, whereLambda, orderBy);
        }
        #endregion
    }
}
