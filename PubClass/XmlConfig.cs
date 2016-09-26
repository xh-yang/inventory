using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace PublicClass
{
    /// <summary>
    /// XmlConfig 的摘要说明。
    /// XMl文件操作类
    /// 类库开发：
    /// 时间：2016年8月10日
    /// 功能：1、创建XML文件 
    ///       2、添加、修改和删除节点、配置项和配置值 
    ///       3、读取配置信息
    /// </summary>
    class XmlConfig
    {
        private static Hashtable list;
        private int count;
        private string xmlfile;
        private XmlDocument doc;
        private XmlElement root;

        #region 构造方法
        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="xmlfile">配置文件名</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static XmlConfig Instance(string xmlfile)
        {
            string fullName = new FileInfo(xmlfile).FullName;
            XmlConfig config = (XmlConfig)list[fullName];

            if (config == null)
            {
                config = new XmlConfig(xmlfile);
                list[fullName] = config;
            }

            config.count++;
            return config;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Dispose()
        {
            if (--count <= 0)
            {
                string fullName = new FileInfo(xmlfile).FullName;
                list.Remove(fullName);
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="xmlfile">配置文件名</param>
        public XmlConfig(string xmlfile)
        {
            list = new Hashtable();
            this.xmlfile = xmlfile;
            count = 0;

            doc = new XmlDocument();
            if (!File.Exists(xmlfile)) CreateConfigFile();
            doc.Load(xmlfile);
            root = doc.DocumentElement;
        }
        #endregion

        #region 创建配置
        /// <summary>
        /// 创建配置文件
        /// </summary>
        private void CreateConfigFile()
        {
            XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(declaration);
            //创建信息
            XmlComment comment = doc.CreateComment("Rainsoft XML Config / Copyright (c) 2016,2020 微创客团队");
            doc.AppendChild(comment);

            comment = doc.CreateComment(string.Format("Created by {0}! {1}",
            AppDomain.CurrentDomain.FriendlyName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            doc.AppendChild(comment);

            XmlElement root = doc.CreateElement("config");
            doc.AppendChild(root);
            //保存数据
            Save();
        }

        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="create">是否创建</param>
        /// <returns>返回值</returns>
        private XmlNode GetSection(string name, bool create)
        {
            XmlNode node = root.SelectSingleNode(name);
            if (create && node == null)
            {
                node = doc.CreateElement(name);
                root.AppendChild(node);
            }
            return node;
        }

        /// <summary>
        /// 获取配置项
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">配置项名称</param>
        /// <param name="create">是否创建</param>
        /// <returns>返回值</returns>
        private XmlNode GetKey(string section, string key, bool create)
        {
            XmlNode sectionNode = GetSection(section, create);
            if (sectionNode == null) return null;

            XmlNode node = sectionNode.SelectSingleNode(key);
            if (create && node == null)
            {
                node = doc.CreateElement(key);
                sectionNode.AppendChild(node);
            }

            return node;
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void Save()
        {
            doc.Save(xmlfile);
        }
        #endregion

        #region 写入配置
        /// <summary>
        /// 写入配置项和值
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">配置项名称</param>
        /// <param name="value">值</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Write(string section, string key, object value)
        {
            if (value == null) return;
            section = section.Trim().ToLower();
            key = key.Trim().ToLower();

            XmlNode keyNode = GetKey(section, key, true);
            keyNode.InnerText = value.ToString();

            Save();
        }

        /// <summary>
        /// 写入多个属性
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">配置项名称</param>
        /// <param name="attributes">属性对象</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void WriteAttributes(string section, string key, StringDictionary attributes)
        {
            section = section.Trim().ToLower();
            key = key.Trim().ToLower();

            XmlNode keyNode = GetKey(section, key, true);
            keyNode.Attributes.RemoveAll();

            foreach (string name in attributes.Keys)
            {
                ((XmlElement)keyNode).SetAttribute(name, attributes[name]);
            }

            Save();
        }

        /// <summary>
        /// 写入单个属性
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">配置项名称</param>
        /// <param name="attributeName">属性名称</param>
        /// <param name="value">属性值</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void WriteAttribute(string section, string key, string attributeName, object value)
        {
            section = section.Trim().ToLower();
            key = key.Trim().ToLower();
            attributeName = attributeName.Trim().ToLower();

            XmlNode keyNode = GetKey(section, key, true);
            ((XmlElement)keyNode).SetAttribute(attributeName, value.ToString());

            Save();
        }
        #endregion

        #region 读取单个配置
        /// <summary>
        /// 读取单个配置
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">配置项名称</param>
        /// <returns>返回配置项值</returns>
        public object Read(string section, string key)
        {
            section = section.Trim().ToLower();
            key = key.Trim().ToLower();

            XmlNode keyNode = GetKey(section, key, false);

            if (keyNode == null)
                return null;
            else
                return keyNode.InnerText;
        }

        /// <summary>
        /// 读取单个属性的属性值
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">配置项名称</param>
        /// <returns>返回属性值</returns>
        public StringDictionary ReadAttributes(string section, string key)
        {
            section = section.Trim().ToLower();
            key = key.Trim().ToLower();

            StringDictionary sd = new StringDictionary();
            XmlNode keyNode = GetKey(section, key, false);
            if (keyNode == null) return sd;

            foreach (XmlAttribute attribute in keyNode.Attributes)
            {
                sd.Add(attribute.Name, attribute.Value);
            }

            return sd;
        }

        /// <summary>
        /// 读取多个属性的属性值
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">配置项名称</param>
        /// <param name="attributeName">属性名称</param>
        /// <returns>返回属性值</returns>
        public string ReadAttribute(string section, string key, string attributeName)
        {
            section = section.Trim().ToLower();
            key = key.Trim().ToLower();
            attributeName = attributeName.Trim().ToLower();

            XmlNode keyNode = GetKey(section, key, false);
            if (keyNode == null) return null;
            return ((XmlElement)keyNode).GetAttribute(attributeName);
        }
        #endregion

        #region 删除配置
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="section">节点名称</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteSection(string section)
        {
            section = section.Trim().ToLower();

            XmlNode sectionNode = GetSection(section, false);
            if (sectionNode != null) root.RemoveChild(sectionNode);

            Save();
        }

        /// <summary>
        /// 删除配置项
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">配置项名称</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteKey(string section, string key)
        {
            section = section.Trim().ToLower();

            XmlNode sectionNode = GetSection(section, false);
            XmlNode keyNode = GetKey(section, key, false);
            if (keyNode != null) sectionNode.RemoveChild(keyNode);

            Save();
        }

        /// <summary>
        /// 清空所有配置
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Clear()
        {
            root.RemoveAll();
            Save();
        }
        #endregion

        #region 获取所有配置
        /// <summary>
        /// 获取所有节点
        /// </summary>
        /// <returns>返回节点数组</returns>
        public string[] Sections()
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < root.ChildNodes.Count; i++)
            {
                if (!(root.ChildNodes[i] is XmlComment))
                    list.Add(root.ChildNodes[i].Name);
            }

            return (string[])list.ToArray(typeof(string));
        }

        /// <summary>
        /// 获取所有配置项
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <returns>返回配置项数组</returns>
        public string[] Keys(string section)
        {
            XmlNode sectionNode = GetSection(section, false);
            if (sectionNode == null) return new string[0];

            ArrayList list = new ArrayList();
            for (int i = 0; i < sectionNode.ChildNodes.Count; i++)
            {
                if (!(sectionNode.ChildNodes[i] is XmlComment))
                    list.Add(sectionNode.ChildNodes[i].Name);
            }

            return (string[])list.ToArray(typeof(string));
        }
        #endregion

        #region 读取配置信息的多态方法
        /// <summary>
        /// 读取Ini类型数值
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">配置项</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回值</returns>
        public int Read(string section, string key, int defaultValue)
        {
            object o = Read(section, key);
            return o == null ? defaultValue : Convert.ToInt32(o);
        }

        /// <summary>
        /// 读取long类型数值
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">配置项</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回值</returns>
        public long Read(string section, string key, long defaultValue)
        {
            object o = Read(section, key);
            return o == null ? defaultValue : Convert.ToInt64(o);
        }

        /// <summary>
        /// 读取string类型数值
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">配置项</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回值</returns>
        public string Read(string section, string key, string defaultValue)
        {
            object o = Read(section, key);
            return o == null ? defaultValue : Convert.ToString(o);
        }

        /// <summary>
        /// 读取bool类型数值
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">配置项</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回值</returns>
        public bool Read(string section, string key, bool defaultValue)
        {
            object o = Read(section, key);
            return o == null ? defaultValue : Convert.ToBoolean(o);
        }

        /// <summary>
        /// 读取float类型数值
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">配置项</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回值</returns>
        public float Read(string section, string key, float defaultValue)
        {
            object o = Read(section, key);
            return o == null ? defaultValue : Convert.ToSingle(o);
        }

        /// <summary>
        /// 读取double类型数值
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">配置项</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回值</returns>
        public double Read(string section, string key, double defaultValue)
        {
            object o = Read(section, key);
            return o == null ? defaultValue : Convert.ToDouble(o);
        }

        /// <summary>
        /// 读取decimal类型数值
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">配置项</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回值</returns>
        public decimal Read(string section, string key, decimal defaultValue)
        {
            object o = Read(section, key);
            return o == null ? defaultValue : Convert.ToDecimal(o);
        }

        /// <summary>
        /// 读取DateTime类型数值
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">配置项</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回值</returns>
        public DateTime Read(string section, string key, DateTime defaultValue)
        {
            object o = Read(section, key);
            return o == null ? defaultValue : Convert.ToDateTime(o);
        }

        /// <summary>
        /// 读取byte类型数值
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">配置项</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回值</returns>
        public byte Read(string section, string key, byte defaultValue)
        {
            object o = Read(section, key);
            return o == null ? defaultValue : Convert.ToByte(o);
        }

        /// <summary>
        /// 读取char类型数值
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">配置项</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回值</returns>
        public char Read(string section, string key, char defaultValue)
        {
            object o = Read(section, key);
            return o == null ? defaultValue : Convert.ToChar(o);
        }
        #endregion

    }

}
