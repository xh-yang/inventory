using System;
using System.Collections.Generic;
using System.Text;

namespace PublicClass
{
    public class GetChinese
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public GetChinese()
        {
            
        }

        #region 根据汉字获取拼音首写字母
        public string GetChineseSpell(string ChineseText)
        {
            string MyStr = "";
            for (int i = 0; i < ChineseText.Length; i++)
            {
                MyStr += GetSpell(ChineseText.Substring(i, 1));
            }
            return MyStr;
        }
        #endregion

        #region 将拆开的字符转换出首写字母
        public string GetSpell(string CNChare)
        {
            byte[] arrCN = Encoding.Default.GetBytes(CNChare);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = {45217,3,45761,46318,46826,47010,47297,47614,48119,48119,49062,49324,
									49896,50371,50614,50622,50906,51387,51446,52218,52698,52698,52698,
									52980,53689,54481};
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "*";
            }
            else return CNChare;
        }
        #endregion
    }
}
