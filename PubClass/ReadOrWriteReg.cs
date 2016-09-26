using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace PublicClass
{
    /// <summary>
    /// RWReg ��ժҪ˵����
    /// ע��������
    /// ��⿪��
    /// ʱ�䣺2016��8��07��
    /// ���ܣ�ע������
    /// </summary>
    class ReadOrWriteReg
    {
        private static RegistryKey rootkey;

        #region �������ΪRootKey��ע�������࣬ȱʡ��Current_User����
        /// <summary>
        /// �������ΪRootKey��ע�������࣬ȱʡ��Current_User����
        /// </summary>
        /// <param name="RootKey">���ڵ�����</param>
        public void RWReg(string RootKey)
        {
            switch (RootKey.ToUpper())
            {
                case "CLASSES_ROOT":
                    rootkey = Registry.ClassesRoot;
                    break;
                case "CURRENT_USER":
                    rootkey = Registry.CurrentUser;
                    break;
                case "LOCAL_MACHINE":
                    rootkey = Registry.LocalMachine;
                    break;
                case "USERS":
                    rootkey = Registry.Users;
                    break;
                case "CURRENT_CONFIG":
                    rootkey = Registry.CurrentConfig;
                    break;
                case "DYN_DATA":
                    rootkey = Registry.DynData;
                    break;
                case "PERFORMANCE_DATA":
                    rootkey = Registry.PerformanceData;
                    break;
                default:
                    rootkey = Registry.CurrentUser;
                    break;
            }
        }
        #endregion

        #region ��ȡ·��Ϊkeypath������Ϊkeyname��ע����ֵ��ȱʡ����def
        /// <summary>
        /// ��ȡ·��Ϊkeypath������Ϊkeyname��ע����ֵ��ȱʡ����def
        /// </summary>
        /// <param name="keypath">��·�����磺SOFTWARE\xkc��</param>
        /// <param name="keyname">������</param>
        /// <param name="def">Ĭ��ֵ�������ȡʧ�ܣ��򷵻ش�Ĭ��ֵ��</param>
        /// <returns>����ֵ</returns>
        public string GetRegVal(string keypath, string keyname, string def)
        {
            try
            {
                RegistryKey key = rootkey.OpenSubKey(keypath);
                return key.GetValue(keyname, (object)def).ToString();
            }
            catch
            {
                return def;
            }
        }
        #endregion

        #region ����·��Ϊkeypath������Ϊkeyname��ע����ֵΪkeyval
        /// <summary>
        /// ����·��Ϊkeypath������Ϊkeyname��ע����ֵΪkeyval
        /// </summary>
        /// <param name="keypath">��·�����磺SOFTWARE\xkc��</param>
        /// <param name="keyname">������</param>
        /// <param name="keyval">��ֵ</param>
        public bool SetRegVal(string keypath, string keyname, string keyval)
        {
            try
            {
                RegistryKey key = rootkey.OpenSubKey(keypath, true);
                key.SetValue(keyname, (object)keyval);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region ����·��Ϊkeypath�ļ�
        /// <summary>
        /// ����·��Ϊkeypath�ļ�
        /// </summary>
        /// <param name="keypath">��·�����磺SOFTWARE\xkc��</param>
        /// <returns></returns>
        public RegistryKey CreateRegKey(string keypath)
        {
            try
            {
                //�Կ�д�ķ�ʽ��SOFTWARE�ӽ�
                RegistryKey softwarekey = Registry.LocalMachine.OpenSubKey("SOFTWARE", true);
                //���δ����ӽ�
                softwarekey.CreateSubKey("xkcsoft");
                RegistryKey xkcsoftkey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\xkcsoft", true);
                xkcsoftkey.CreateSubKey("FWF");
                RegistryKey fwfkey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\xkcsoft\\FWF", true);
                fwfkey.SetValue("path", "df\\dd\\ff.exe");
                return xkcsoftkey.CreateSubKey(keypath);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region ɾ��·��Ϊkeypath������
        /// <summary>
        /// ɾ��·��Ϊkeypath������
        /// </summary>
        /// <param name="keypath">��·�����磺SOFTWARE\xkc��</param>
        /// <returns>����true��false</returns>
        public bool DelRegSubKey(string keypath)
        {
            try
            {
                rootkey.DeleteSubKey(keypath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region ɾ��·��Ϊkeypath������丽������
        /// <summary>
        /// ɾ��·��Ϊkeypath������丽������
        /// </summary>
        /// <param name="keypath">��·�����磺SOFTWARE\xkc��</param>
        /// <returns>����true��false</returns>
        public bool DelRegSubKeyTree(string keypath)
        {
            try
            {
                rootkey.DeleteSubKeyTree(keypath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region ɾ��·��Ϊkeypath�¼���Ϊkeyname�ļ�ֵ
        /// <summary>
        /// ɾ��·��Ϊkeypath�¼���Ϊkeyname�ļ�ֵ
        /// </summary>
        /// <param name="keypath">��·�����磺SOFTWARE\xkc��</param>
        /// <param name="keyname">������</param>
        /// <returns>����true��false</returns>
        public bool DelRegKeyVal(string keypath, string keyname)
        {
            try
            {
                RegistryKey key = rootkey.OpenSubKey(keypath, true);
                key.DeleteValue(keyname);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }

}
