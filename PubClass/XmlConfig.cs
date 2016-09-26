using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace PublicClass
{
    /// <summary>
    /// XmlConfig ��ժҪ˵����
    /// XMl�ļ�������
    /// ��⿪����
    /// ʱ�䣺2016��8��10��
    /// ���ܣ�1������XML�ļ� 
    ///       2����ӡ��޸ĺ�ɾ���ڵ㡢�����������ֵ 
    ///       3����ȡ������Ϣ
    /// </summary>
    class XmlConfig
    {
        private static Hashtable list;
        private int count;
        private string xmlfile;
        private XmlDocument doc;
        private XmlElement root;

        #region ���췽��
        /// <summary>
        /// ����ʵ��
        /// </summary>
        /// <param name="xmlfile">�����ļ���</param>
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
        /// �ͷ���Դ
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
        /// ���췽��
        /// </summary>
        /// <param name="xmlfile">�����ļ���</param>
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

        #region ��������
        /// <summary>
        /// ���������ļ�
        /// </summary>
        private void CreateConfigFile()
        {
            XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(declaration);
            //������Ϣ
            XmlComment comment = doc.CreateComment("Rainsoft XML Config / Copyright (c) 2016,2020 ΢�����Ŷ�");
            doc.AppendChild(comment);

            comment = doc.CreateComment(string.Format("Created by {0}! {1}",
            AppDomain.CurrentDomain.FriendlyName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            doc.AppendChild(comment);

            XmlElement root = doc.CreateElement("config");
            doc.AppendChild(root);
            //��������
            Save();
        }

        /// <summary>
        /// ��ȡ�ڵ�
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="create">�Ƿ񴴽�</param>
        /// <returns>����ֵ</returns>
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
        /// ��ȡ������
        /// </summary>
        /// <param name="section">�ڵ�����</param>
        /// <param name="key">����������</param>
        /// <param name="create">�Ƿ񴴽�</param>
        /// <returns>����ֵ</returns>
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
        /// ���������ļ�
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void Save()
        {
            doc.Save(xmlfile);
        }
        #endregion

        #region д������
        /// <summary>
        /// д���������ֵ
        /// </summary>
        /// <param name="section">�ڵ�����</param>
        /// <param name="key">����������</param>
        /// <param name="value">ֵ</param>
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
        /// д��������
        /// </summary>
        /// <param name="section">�ڵ�����</param>
        /// <param name="key">����������</param>
        /// <param name="attributes">���Զ���</param>
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
        /// д�뵥������
        /// </summary>
        /// <param name="section">�ڵ�����</param>
        /// <param name="key">����������</param>
        /// <param name="attributeName">��������</param>
        /// <param name="value">����ֵ</param>
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

        #region ��ȡ��������
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="section">�ڵ�����</param>
        /// <param name="key">����������</param>
        /// <returns>����������ֵ</returns>
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
        /// ��ȡ�������Ե�����ֵ
        /// </summary>
        /// <param name="section">�ڵ�����</param>
        /// <param name="key">����������</param>
        /// <returns>��������ֵ</returns>
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
        /// ��ȡ������Ե�����ֵ
        /// </summary>
        /// <param name="section">�ڵ�����</param>
        /// <param name="key">����������</param>
        /// <param name="attributeName">��������</param>
        /// <returns>��������ֵ</returns>
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

        #region ɾ������
        /// <summary>
        /// ɾ���ڵ�
        /// </summary>
        /// <param name="section">�ڵ�����</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteSection(string section)
        {
            section = section.Trim().ToLower();

            XmlNode sectionNode = GetSection(section, false);
            if (sectionNode != null) root.RemoveChild(sectionNode);

            Save();
        }

        /// <summary>
        /// ɾ��������
        /// </summary>
        /// <param name="section">�ڵ�����</param>
        /// <param name="key">����������</param>
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
        /// �����������
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Clear()
        {
            root.RemoveAll();
            Save();
        }
        #endregion

        #region ��ȡ��������
        /// <summary>
        /// ��ȡ���нڵ�
        /// </summary>
        /// <returns>���ؽڵ�����</returns>
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
        /// ��ȡ����������
        /// </summary>
        /// <param name="section">�ڵ�����</param>
        /// <returns>��������������</returns>
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

        #region ��ȡ������Ϣ�Ķ�̬����
        /// <summary>
        /// ��ȡIni������ֵ
        /// </summary>
        /// <param name="section">�ڵ�����</param>
        /// <param name="key">������</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <returns>����ֵ</returns>
        public int Read(string section, string key, int defaultValue)
        {
            object o = Read(section, key);
            return o == null ? defaultValue : Convert.ToInt32(o);
        }

        /// <summary>
        /// ��ȡlong������ֵ
        /// </summary>
        /// <param name="section">�ڵ�����</param>
        /// <param name="key">������</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <returns>����ֵ</returns>
        public long Read(string section, string key, long defaultValue)
        {
            object o = Read(section, key);
            return o == null ? defaultValue : Convert.ToInt64(o);
        }

        /// <summary>
        /// ��ȡstring������ֵ
        /// </summary>
        /// <param name="section">�ڵ�����</param>
        /// <param name="key">������</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <returns>����ֵ</returns>
        public string Read(string section, string key, string defaultValue)
        {
            object o = Read(section, key);
            return o == null ? defaultValue : Convert.ToString(o);
        }

        /// <summary>
        /// ��ȡbool������ֵ
        /// </summary>
        /// <param name="section">�ڵ�����</param>
        /// <param name="key">������</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <returns>����ֵ</returns>
        public bool Read(string section, string key, bool defaultValue)
        {
            object o = Read(section, key);
            return o == null ? defaultValue : Convert.ToBoolean(o);
        }

        /// <summary>
        /// ��ȡfloat������ֵ
        /// </summary>
        /// <param name="section">�ڵ�����</param>
        /// <param name="key">������</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <returns>����ֵ</returns>
        public float Read(string section, string key, float defaultValue)
        {
            object o = Read(section, key);
            return o == null ? defaultValue : Convert.ToSingle(o);
        }

        /// <summary>
        /// ��ȡdouble������ֵ
        /// </summary>
        /// <param name="section">�ڵ�����</param>
        /// <param name="key">������</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <returns>����ֵ</returns>
        public double Read(string section, string key, double defaultValue)
        {
            object o = Read(section, key);
            return o == null ? defaultValue : Convert.ToDouble(o);
        }

        /// <summary>
        /// ��ȡdecimal������ֵ
        /// </summary>
        /// <param name="section">�ڵ�����</param>
        /// <param name="key">������</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <returns>����ֵ</returns>
        public decimal Read(string section, string key, decimal defaultValue)
        {
            object o = Read(section, key);
            return o == null ? defaultValue : Convert.ToDecimal(o);
        }

        /// <summary>
        /// ��ȡDateTime������ֵ
        /// </summary>
        /// <param name="section">�ڵ�����</param>
        /// <param name="key">������</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <returns>����ֵ</returns>
        public DateTime Read(string section, string key, DateTime defaultValue)
        {
            object o = Read(section, key);
            return o == null ? defaultValue : Convert.ToDateTime(o);
        }

        /// <summary>
        /// ��ȡbyte������ֵ
        /// </summary>
        /// <param name="section">�ڵ�����</param>
        /// <param name="key">������</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <returns>����ֵ</returns>
        public byte Read(string section, string key, byte defaultValue)
        {
            object o = Read(section, key);
            return o == null ? defaultValue : Convert.ToByte(o);
        }

        /// <summary>
        /// ��ȡchar������ֵ
        /// </summary>
        /// <param name="section">�ڵ�����</param>
        /// <param name="key">������</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <returns>����ֵ</returns>
        public char Read(string section, string key, char defaultValue)
        {
            object o = Read(section, key);
            return o == null ? defaultValue : Convert.ToChar(o);
        }
        #endregion

    }

}
