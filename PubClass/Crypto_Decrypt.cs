using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO;



public class Crypto_Decrypt
{
    /// <summary>
    /// 说明：加解密公共方法
    /// DES(数据加（解）密标准，速度较快，适用于加密大量数据的场合)
    /// RC2 加（解）密(用变长密钥对大量数据进行加密)
    /// 3DES 加（解）密(基于DES，对一块数据用三个不同的密钥进行三次加密，强度更高)
    /// AES 加（解）密(高级加（解）密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
    /// </summary>

    #region 变量定义
    private static string Base64Str = "Wkb4jv6y/ye0Cd7k89yQgQ-+";
    #endregion

    public Crypto_Decrypt()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    #region MD5不可逆加密
    //32位加密
    public string GetMD5_32(string s, string _input_charset)
    {
        MD5 md5 = new MD5CryptoServiceProvider();

        byte[] t = md5.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));

        StringBuilder sb = new StringBuilder(32);
        for (int i = 0; i < t.Length; i++)
        {
            sb.Append(t[i].ToString("x").PadLeft(2, '0'));
        }
        return sb.ToString();
    }
    //16位加密
    public static string GetMd5_16(string ConvertString)
    {
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
        t2 = t2.Replace("-", "");
        return t2;
    } 
    #endregion

    #region MD5静态加密
    /// <summary>
    /// MD5静态加密
    /// </summary>
    /// <param name="EncryptString">加密字符串</param>
    /// <returns>密文</returns>
    public static string MD5Encrypt(string EncryptString)
    {
        if (string.IsNullOrEmpty(EncryptString)) { throw (new Exception("密文不得为空")); }
        MD5 m_ClassMD5 = new MD5CryptoServiceProvider();
        string m_strEncrypt = "";
        try
        {
            m_strEncrypt = BitConverter.ToString(m_ClassMD5.ComputeHash(Encoding.Default.GetBytes(EncryptString))).Replace("-", "");
        }
        catch (ArgumentException ex) { throw ex; }
        catch (CryptographicException ex) { throw ex; }
        catch (Exception ex) { throw ex; }
        finally { m_ClassMD5.Clear(); }
        return m_strEncrypt;
    }
    // 创建Key
    public string GenerateKey()
    {
        DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
        return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
    }
    ///MD5加密
    public static string D_MD5Encrypt(string pToEncrypt, string sKey)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
        des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
        des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        StringBuilder ret = new StringBuilder();
        foreach (byte b in ms.ToArray())
        {
            ret.AppendFormat("{0:X2}", b);
        }
        ret.ToString();
        return ret.ToString();
    }

    ///MD5解密
    public static string D_MD5Decrypt(string pToDecrypt, string sKey)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
        for (int x = 0; x < pToDecrypt.Length / 2; x++)
        {
            int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
            inputByteArray[x] = (byte)i;
        }
        des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
        des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        StringBuilder ret = new StringBuilder();
        return System.Text.Encoding.Default.GetString(ms.ToArray());
    }
    #endregion

    #region DES加密
    /// <summary>
    /// DES加密
    /// </summary>
    /// <param name="EncryptString">明文字符串</param>
    /// <param name="EncryptKey">密钥</param>
    /// <returns>密文</returns>
    public static string DESEncrypt(string EncryptString, string EncryptKey)
    {
        if (string.IsNullOrEmpty(EncryptString)) { throw (new Exception("明不得为空")); }
        if (string.IsNullOrEmpty(EncryptKey)) { throw (new Exception("密钥不得为空")); }
        if (EncryptKey.Length != 8) { throw (new Exception("密钥必须为8位")); }
        byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        string m_strEncrypt = "";
        DESCryptoServiceProvider m_DESProvider = new DESCryptoServiceProvider();
        try
        {
            byte[] m_btEncryptString = Encoding.Default.GetBytes(EncryptString);
            MemoryStream m_stream = new MemoryStream();
            CryptoStream m_cstream = new CryptoStream(m_stream, m_DESProvider.CreateEncryptor(Encoding.Default.GetBytes(EncryptKey), m_btIV), CryptoStreamMode.Write);
            m_cstream.Write(m_btEncryptString, 0, m_btEncryptString.Length);
            m_cstream.FlushFinalBlock();
            m_strEncrypt = Convert.ToBase64String(m_stream.ToArray());
            m_stream.Close(); m_stream.Dispose();
            m_cstream.Close(); m_cstream.Dispose();
        }
        catch (IOException ex) { throw ex; }
        catch (CryptographicException ex) { throw ex; }
        catch (ArgumentException ex) { throw ex; }
        catch (Exception ex) { throw ex; }
        finally { m_DESProvider.Clear(); }
        return m_strEncrypt;
    }
    #endregion

    #region DES解密
    /// <summary>
    /// DES解密
    /// </summary>
    /// <param name="DecryptString">密文</param>
    /// <param name="DecryptKey">密钥</param>
    /// <returns>明文</returns>
    public static string DESDecrypt(string DecryptString, string DecryptKey)
    {
        if (string.IsNullOrEmpty(DecryptString)) { throw (new Exception("密文不得为空")); }
        if (string.IsNullOrEmpty(DecryptKey)) { throw (new Exception("密钥不得为空")); }
        if (DecryptKey.Length != 8) { throw (new Exception("密钥必须为8位")); }
        byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        string m_strDecrypt = "";
        DESCryptoServiceProvider m_DESProvider = new DESCryptoServiceProvider();
        try
        {
            byte[] m_btDecryptString = Convert.FromBase64String(DecryptString);
            MemoryStream m_stream = new MemoryStream();
            CryptoStream m_cstream = new CryptoStream(m_stream, m_DESProvider.CreateDecryptor(Encoding.Default.GetBytes(DecryptKey), m_btIV), CryptoStreamMode.Write);
            m_cstream.Write(m_btDecryptString, 0, m_btDecryptString.Length);
            m_cstream.FlushFinalBlock();
            m_strDecrypt = Encoding.Default.GetString(m_stream.ToArray());
            m_stream.Close(); m_stream.Dispose();
            m_cstream.Close(); m_cstream.Dispose();
        }
        catch (IOException ex) { throw ex; }
        catch (CryptographicException ex) { throw ex; }
        catch (ArgumentException ex) { throw ex; }
        catch (Exception ex) { throw ex; }
        finally { m_DESProvider.Clear(); }
        return m_strDecrypt;
    }
    #endregion

    #region RC2加密
    /// <summary>
    /// RC2加密
    /// </summary>
    /// <param name="EncryptString">明文字符串</param>
    /// <param name="EncryptKey">密钥</param>
    /// <returns>密文</returns>
    public static string RC2Encrypt(string EncryptString, string EncryptKey)
    {
        if (string.IsNullOrEmpty(EncryptString)) { throw (new Exception("明文不得为空")); }
        if (string.IsNullOrEmpty(EncryptKey)) { throw (new Exception("密钥不得为空")); }
        if (EncryptKey.Length < 5 || EncryptKey.Length > 16) { throw (new Exception("密钥必须为5-16位")); }
        string m_strEncrypt = "";
        byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        RC2CryptoServiceProvider m_RC2Provider = new RC2CryptoServiceProvider();
        try
        {
            byte[] m_btEncryptString = Encoding.Default.GetBytes(EncryptString);
            MemoryStream m_stream = new MemoryStream();
            CryptoStream m_cstream = new CryptoStream(m_stream, m_RC2Provider.CreateEncryptor(Encoding.Default.GetBytes(EncryptKey), m_btIV), CryptoStreamMode.Write);
            m_cstream.Write(m_btEncryptString, 0, m_btEncryptString.Length);
            m_cstream.FlushFinalBlock();
            m_strEncrypt = Convert.ToBase64String(m_stream.ToArray());
            m_stream.Close(); m_stream.Dispose();
            m_cstream.Close(); m_cstream.Dispose();
        }
        catch (IOException ex) { throw ex; }
        catch (CryptographicException ex) { throw ex; }
        catch (ArgumentException ex) { throw ex; }
        catch (Exception ex) { throw ex; }
        finally { m_RC2Provider.Clear(); }
        return m_strEncrypt;
    }
    #endregion

    #region RC2解密
    /// <summary>
    /// RC2解密
    /// </summary>
    /// <param name="DecryptString">密文</param>
    /// <param name="DecryptKey">密钥</param>
    /// <returns>明文</returns>
    public static string RC2Decrypt(string DecryptString, string DecryptKey)
    {
        if (string.IsNullOrEmpty(DecryptString)) { throw (new Exception("密文不得为空")); }
        if (string.IsNullOrEmpty(DecryptKey)) { throw (new Exception("密钥不得为空")); }
        if (DecryptKey.Length < 5 || DecryptKey.Length > 16) { throw (new Exception("密钥必须为5-16位")); }
        byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        string m_strDecrypt = "";
        RC2CryptoServiceProvider m_RC2Provider = new RC2CryptoServiceProvider();
        try
        {
            byte[] m_btDecryptString = Convert.FromBase64String(DecryptString);
            MemoryStream m_stream = new MemoryStream();
            CryptoStream m_cstream = new CryptoStream(m_stream, m_RC2Provider.CreateDecryptor(Encoding.Default.GetBytes(DecryptKey), m_btIV), CryptoStreamMode.Write);
            m_cstream.Write(m_btDecryptString, 0, m_btDecryptString.Length);
            m_cstream.FlushFinalBlock();
            m_strDecrypt = Encoding.Default.GetString(m_stream.ToArray());
            m_stream.Close(); m_stream.Dispose();
            m_cstream.Close(); m_cstream.Dispose();
        }
        catch (IOException ex) { throw ex; }
        catch (CryptographicException ex) { throw ex; }
        catch (ArgumentException ex) { throw ex; }
        catch (Exception ex) { throw ex; }
        finally { m_RC2Provider.Clear(); }
        return m_strDecrypt;
    }
    #endregion

    #region 3DES加密
    /// <summary>
    /// 3DES加密
    /// </summary>
    /// <param name="EncryptString">明文</param>
    /// <param name="EncryptKey1">密钥1</param>
    /// <param name="EncryptKey2">密钥2</param>
    /// <param name="EncryptKey3">密钥3</param>
    /// <returns>密文</returns>
    public static string DES3Encrypt(string EncryptString, string EncryptKey1, string EncryptKey2, string EncryptKey3)
    {
        string m_strEncrypt = "";
        try
        {
            m_strEncrypt = DESEncrypt(EncryptString, EncryptKey3);
            m_strEncrypt = DESEncrypt(m_strEncrypt, EncryptKey2);
            m_strEncrypt = DESEncrypt(m_strEncrypt, EncryptKey1);
        }
        catch (Exception ex) { throw ex; }
        return m_strEncrypt;
    }
    #endregion

    #region 3DES解密
    /// <summary>
    /// 3DES解密
    /// </summary>
    /// <param name="DecryptString">密文</param>
    /// <param name="DecryptKey1">密钥1</param>
    /// <param name="DecryptKey2">密钥2</param>
    /// <param name="DecryptKey3">密钥3</param>
    /// <returns></returns>
    public static string DES3Decrypt(string DecryptString, string DecryptKey1, string DecryptKey2, string DecryptKey3)
    {
        string m_strDecrypt = "";
        try
        {
            m_strDecrypt = DESDecrypt(DecryptString, DecryptKey1);
            m_strDecrypt = DESDecrypt(m_strDecrypt, DecryptKey2);
            m_strDecrypt = DESDecrypt(m_strDecrypt, DecryptKey3);
        }
        catch (Exception ex) { throw ex; }
        return m_strDecrypt;
    }
    #endregion

    #region AES加密
    /// <summary>
    /// AES加密
    /// </summary>
    /// <param name="EncryptString">明文</param>
    /// <param name="EncryptKey">密钥</param>
    /// <returns>密文</returns>
    public static string AESEncrypt(string EncryptString, string EncryptKey)
    {
        if (string.IsNullOrEmpty(EncryptString)) { throw (new Exception("明文不得为空")); }
        if (string.IsNullOrEmpty(EncryptKey)) { throw (new Exception("密钥不得为空")); }
        string m_strEncrypt = "";
        byte[] m_btIV = Convert.FromBase64String(Base64Str);
        Rijndael m_AESProvider = Rijndael.Create();
        try
        {
            byte[] m_btEncryptString = Encoding.Default.GetBytes(EncryptString);
            MemoryStream m_stream = new MemoryStream();
            CryptoStream m_csstream = new CryptoStream(m_stream, m_AESProvider.CreateEncryptor(Encoding.Default.GetBytes(EncryptKey), m_btIV), CryptoStreamMode.Write);
            m_csstream.Write(m_btEncryptString, 0, m_btEncryptString.Length); m_csstream.FlushFinalBlock();
            m_strEncrypt = Convert.ToBase64String(m_stream.ToArray());
            m_stream.Close(); m_stream.Dispose();
            m_csstream.Close(); m_csstream.Dispose();
        }
        catch (IOException ex) { throw ex; }
        catch (CryptographicException ex) { throw ex; }
        catch (ArgumentException ex) { throw ex; }
        catch (Exception ex) { throw ex; }
        finally { m_AESProvider.Clear(); }
        return m_strEncrypt;
    }
    #endregion

    #region AES解密
    /// <summary>
    /// AES解密
    /// </summary>
    /// <param name="DecryptString">密文</param>
    /// <param name="DecryptKey">密钥</param>
    /// <returns>明文</returns>
    public static string AESDecrypt(string DecryptString, string DecryptKey)
    {
        if (string.IsNullOrEmpty(DecryptString)) { throw (new Exception("密文不得为空")); }
        if (string.IsNullOrEmpty(DecryptKey)) { throw (new Exception("密钥不得为空")); }
        string m_strDecrypt = "";
        byte[] m_btIV = Convert.FromBase64String(Base64Str);
        Rijndael m_AESProvider = Rijndael.Create();
        try
        {
            byte[] m_btDecryptString = Convert.FromBase64String(DecryptString);
            MemoryStream m_stream = new MemoryStream();
            CryptoStream m_csstream = new CryptoStream(m_stream, m_AESProvider.CreateDecryptor(Encoding.Default.GetBytes(DecryptKey), m_btIV), CryptoStreamMode.Write);
            m_csstream.Write(m_btDecryptString, 0, m_btDecryptString.Length); m_csstream.FlushFinalBlock();
            m_strDecrypt = Encoding.Default.GetString(m_stream.ToArray());
            m_stream.Close(); m_stream.Dispose();
            m_csstream.Close(); m_csstream.Dispose();
        }
        catch (IOException ex) { throw ex; }
        catch (CryptographicException ex) { throw ex; }
        catch (ArgumentException ex) { throw ex; }
        catch (Exception ex) { throw ex; }
        finally { m_AESProvider.Clear(); }
        return m_strDecrypt;
    }
    #endregion


}
