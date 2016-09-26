using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace PublicClass
{
    /// <summary>
    /// ����INI�ļ���
    /// </summary>
    class ConfigIni
    {
        //����INI�ļ���д�������� WritePrivateProfileString()
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        //����INI�ļ��Ķ��������� GetPrivateProfileString()
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        #region дһ��config.ini�ļ�
        /// <summary>
        /// дһ��config.ini�ļ�
        /// </summary>
        /// <param name="configureNode">���ýڵ�����</param>
        /// <param name="key">����������</param>
        /// <param name="key_Value">������ֵ</param>
        /// <param name="path">ini�ļ�������·����</param>
        private void Save_Ini(string configureNode, string key, string key_Value, string path)
        {
            WritePrivateProfileString(configureNode, key, key_Value, path);
        }
        #endregion

        #region ��ȡini�ļ��е�����
        /// <summary>
        /// ��ȡini�ļ��е�����
        /// </summary>
        /// <param name="configureNode">���ýڵ�����</param>
        /// <param name="key">����������</param>
        /// <param name="path">ini�ļ�������·����</param>
        /// <returns>����������ֵ</returns>
        private string Read_Ini(string configureNode, string key, string path)
        {
            StringBuilder str = new StringBuilder(255);
            //ȡ�����ý�[DataBaseConfigure]��DataBase����ֵ
            GetPrivateProfileString(configureNode, key, "", str, 255, path);
            //�Ի����н��Ӧ��Ϊ DataBase:DataBaseName
            return str.ToString();
        }
        #endregion
    }

}
