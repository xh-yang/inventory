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
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public PageDataModel()
        {

        }
        /// <summary>
        /// ��Ҫ��ѯ�����ݱ�����
        /// </summary>
        public string TableName
        {
            set { _tablename = value; }
            get { return _tablename; }
        }
        /// <summary>
        /// ����ѯ�����ݿ�����������
        /// </summary>
        public string TableKey
        {
            set { _tablekey = value; }
            get { return _tablekey; }
        }

        /// <summary>
        /// ��Ҫ���ص���,���Ÿ�����Ҳ���Դ���*
        /// </summary>
        public string GetFields
        {
            set { _getfields = value; }
            get { return _getfields; }
        }

        /// <summary>
        /// ������ֶ���(���ŷָ�)����A Asc,B Desc��
        /// </summary>
        public string OrderFields
        {
            set { _orderfields = value; }
            get { return _orderfields; }
        }

        /// <summary>
        /// ÿҳ����ʾ�ļ�¼��
        /// </summary>
        public int PageSize
        {
            set { _pagesize = value; }
            get { return _pagesize; }
        }

        /// <summary>
        /// ��ʾ�ڼ�ҳ
        /// </summary>
        public int PageIndex
        {
            set { _pageindex = value; }
            get { return _pageindex; }
        }

        /// <summary>
        /// �Ƿ�������ؼ�¼����, 0: ������, ��0: ����
        /// </summary>
        public int IsCount
        {
            set { _iscount = value; }
            get { return _iscount; }
        }

        /// <summary>
        /// ��ѯ���� (ע��: ��Ҫ�� where)
        /// </summary>
        public string QueryStr
        {
            set { _querystr = value; }
            get { return _querystr; }
        }
    }
}
