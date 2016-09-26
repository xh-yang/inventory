using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace PublicClass
{
    /// <summary>
    /// 操作INI文件类
    /// </summary>
    class ConfigIni
    {
        //声明INI文件的写操作函数 WritePrivateProfileString()
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        //声明INI文件的读操作函数 GetPrivateProfileString()
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        #region 写一个config.ini文件
        /// <summary>
        /// 写一个config.ini文件
        /// </summary>
        /// <param name="configureNode">配置节点名称</param>
        /// <param name="key">配置项名称</param>
        /// <param name="key_Value">配置项值</param>
        /// <param name="path">ini文件（包括路径）</param>
        private void Save_Ini(string configureNode, string key, string key_Value, string path)
        {
            WritePrivateProfileString(configureNode, key, key_Value, path);
        }
        #endregion

        #region 读取ini文件中的配置
        /// <summary>
        /// 读取ini文件中的配置
        /// </summary>
        /// <param name="configureNode">配置节点名称</param>
        /// <param name="key">配置项名称</param>
        /// <param name="path">ini文件（包括路径）</param>
        /// <returns>返回配置项值</returns>
        private string Read_Ini(string configureNode, string key, string path)
        {
            StringBuilder str = new StringBuilder(255);
            //取得配置节[DataBaseConfigure]的DataBase键的值
            GetPrivateProfileString(configureNode, key, "", str, 255, path);
            //对话框中结果应该为 DataBase:DataBaseName
            return str.ToString();
        }
        #endregion
    }

}
