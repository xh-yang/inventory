using System;
using System.Collections.Generic;
using System.Text;

namespace PublicClass
{
    public class PageDataModel
    {
        private string _tablename;
        private string _tablekey;
        private string _getfields;
        private string _orderfields;
        private int _pagesize;
        private int _pageindex;
        private int _iscount;
        private string _querystr;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PageDataModel()
        {

        }
        /// <summary>
        /// 需要查询的数据表名称
        /// </summary>
        public string TableName
        {
            set { _tablename = value; }
            get { return _tablename; }
        }
        /// <summary>
        /// 所查询的数据库表的主键名称
        /// </summary>
        public string TableKey
        {
            set { _tablekey = value; }
            get { return _tablekey; }
        }

        /// <summary>
        /// 需要返回的列,逗号隔开，也可以传入*
        /// </summary>
        public string GetFields
        {
            set { _getfields = value; }
            get { return _getfields; }
        }

        /// <summary>
        /// 排序的字段名(逗号分隔)，如A Asc,B Desc等
        /// </summary>
        public string OrderFields
        {
            set { _orderfields = value; }
            get { return _orderfields; }
        }

        /// <summary>
        /// 每页所显示的记录数
        /// </summary>
        public int PageSize
        {
            set { _pagesize = value; }
            get { return _pagesize; }
        }

        /// <summary>
        /// 显示第几页
        /// </summary>
        public int PageIndex
        {
            set { _pageindex = value; }
            get { return _pageindex; }
        }

        /// <summary>
        /// 是否仅仅返回记录总数, 0: 不返回, 非0: 返回
        /// </summary>
        public int IsCount
        {
            set { _iscount = value; }
            get { return _iscount; }
        }

        /// <summary>
        /// 查询条件 (注意: 不要加 where)
        /// </summary>
        public string QueryStr
        {
            set { _querystr = value; }
            get { return _querystr; }
        }
    }
}
