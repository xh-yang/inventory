using System;
using System.Collections.Generic;
using System.Text;

namespace PublicClass
{
    class CMsgText
    {
        public CMsgText()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ���������Լ�飬������Ϻõ���ʾ��Ϣ
        /// <summary>
        /// ���������Լ�飬������Ϻõ���ʾ��Ϣ
        /// </summary>
        /// <param name="MsgText"></param>
        /// <returns></returns>
        public static string GetSaveCheck(string MsgText)
        {
            string ResultStr = "�������������ɣ�" + MsgText.Trim() + "��������¼�룡";
            return ResultStr;
        }
        #endregion

        #region �������ϵͳ��������ʾ��Ϣ
        /// <summary>
        /// �������ϵͳ��������ʾ��Ϣ
        /// </summary>
        /// <returns></returns>
        public static string GetSaveSysError()
        {
            string ResultStr = "�������������ɣ��뼰ʱ��ϵͳ����Ա��ϵ��";
            return ResultStr;
        }
        #endregion

        #region ������ֲ���ȷ��������
        /// <summary>
        /// ������ֲ���ȷ��������
        /// </summary>
        /// <returns></returns>
        public static string GetSaveUnKnow()
        {
            string ResultStr = "�������������ɣ���ο��û��ֲ�������ݵ���ȷ�ԣ�";
            return ResultStr;
        }
        #endregion

        #region ɾ������ϵͳ�������
        /// <summary>
        /// ɾ������ϵͳ�������
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteSysError()
        {
            string ResultStr = "ɾ������������ɣ��뼰ʱ��ϵͳ����Ա��ϵ��";
            return ResultStr;
        }
        #endregion

        #region ɾ������ҵ���߼�����
        /// <summary>
        /// ɾ������ҵ���߼�����
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteUnKnow()
        {
            string ResultStr = "ɾ������������ɣ���ο��û��ֲ�������ݵ���ȷ�ԣ�";
            return ResultStr;
        }
        #endregion

        #region ɾ��ǰȷ�ϵı�׼��Ϣ
        /// <summary>
        /// ɾ��ǰȷ���ı�׼��Ϣ
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteMsg()
        {
            string ResultStr = "ɾ�������ݲ��ָܻ�����ȷ��Ҫ����ɾ����";
            return ResultStr;
        }
        #endregion
    }
}
