using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace PublicClass
{
    /// <summary>
    /// RWReg 的摘要说明。
    /// 注册表操作类
    /// 类库开发
    /// 时间：2016年8月07日
    /// 功能：注册表操作
    /// </summary>
    class ReadOrWriteReg
    {
        private static RegistryKey rootkey;

        #region 构造根键为RootKey的注册表操作类，缺省打开Current_User主键
        /// <summary>
        /// 构造根键为RootKey的注册表操作类，缺省打开Current_User主键
        /// </summary>
        /// <param name="RootKey">根节点名称</param>
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

        #region 读取路径为keypath，键名为keyname的注册表键值，缺省返回def
        /// <summary>
        /// 读取路径为keypath，键名为keyname的注册表键值，缺省返回def
        /// </summary>
        /// <param name="keypath">键路径（如：SOFTWARE\xkc）</param>
        /// <param name="keyname">键名称</param>
        /// <param name="def">默认值（如果读取失败，则返回此默认值）</param>
        /// <returns>返回值</returns>
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

        #region 设置路径为keypath，键名为keyname的注册表键值为keyval
        /// <summary>
        /// 设置路径为keypath，键名为keyname的注册表键值为keyval
        /// </summary>
        /// <param name="keypath">键路径（如：SOFTWARE\xkc）</param>
        /// <param name="keyname">键名称</param>
        /// <param name="keyval">键值</param>
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

        #region 创建路径为keypath的键
        /// <summary>
        /// 创建路径为keypath的键
        /// </summary>
        /// <param name="keypath">键路径（如：SOFTWARE\xkc）</param>
        /// <returns></returns>
        public RegistryKey CreateRegKey(string keypath)
        {
            try
            {
                //以可写的方式打开SOFTWARE子健
                RegistryKey softwarekey = Registry.LocalMachine.OpenSubKey("SOFTWARE", true);
                //依次创建子健
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

        #region 删除路径为keypath的子项
        /// <summary>
        /// 删除路径为keypath的子项
        /// </summary>
        /// <param name="keypath">键路径（如：SOFTWARE\xkc）</param>
        /// <returns>返回true或false</returns>
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

        #region 删除路径为keypath的子项及其附属子项
        /// <summary>
        /// 删除路径为keypath的子项及其附属子项
        /// </summary>
        /// <param name="keypath">键路径（如：SOFTWARE\xkc）</param>
        /// <returns>返回true或false</returns>
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

        #region 删除路径为keypath下键名为keyname的键值
        /// <summary>
        /// 删除路径为keypath下键名为keyname的键值
        /// </summary>
        /// <param name="keypath">键路径（如：SOFTWARE\xkc）</param>
        /// <param name="keyname">键名称</param>
        /// <returns>返回true或false</returns>
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
