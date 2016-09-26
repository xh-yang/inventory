using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PublicClass
{
    public class BaseCheck
    {
        public BaseCheck()
        {

        }

        #region 【判断是否是整型数字】 IsNumber(string str)
        /// <summary>
        /// 功能：判断是否是整型
        /// 参数：str要验证的值
        /// 返回：True正确、False错误
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumber(string str)
        {
            foreach (char c in str)
            {
                if (!Char.IsNumber(c))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 【判断是否是数值型】(如：金额或带小数点的数字) IsDecimal(string str)
        /// <summary>
        /// 功能： 判断是否是数值型(如：金额或带小数点的数字)
        /// 参数：str要验证的值
        /// 返回：True正确、False错误 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDecimal(string str)
        {
            System.Decimal decValue;
            if (str == "")
            {
                str = "0";
            }

            try
            {
                decValue = System.Convert.ToDecimal(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 【将金额数据转换成指定的单位的金额】( 如：4万 ==> 40000.00 )
        /// <summary>
        /// 功能：将金额数据转换成指定的单位的金额( 如：4万 ==> 40000.00 )
        /// 参数：_Money 要转换的金额；
        ///		  _ConvertType 要转换成为的单位类型（1万元、2千元、3元）
        /// 近回：转换后的金额
        /// </summary>
        /// <param name="_Money"></param>
        /// <param name="ConvertType"></param>
        /// <returns></returns>
        public static double ConvertToMoney(double _Money, int _ConvertType)
        {
            switch (_ConvertType)
            {
                case 1: //万元
                    _Money = _Money * 10000;
                    break;
                case 2://千元
                    _Money = _Money * 1000;
                    break;
                default: //元
                    _Money = _Money * 1;
                    break;
            }
            return _Money;
        }
        #endregion

        #region 【将小写金额转换成大写金额】 LowerToUpper_Money( double _Money )
        /// <summary>
        /// 功能：将小写金额转换成大写金额
        /// 参数：要转换的小写金额
        /// 返回：大写金额
        /// </summary>
        /// <param name="_Money"></param>
        /// <returns></returns>
        public static string LowerToUpper_Money(decimal _Money)
        {
            string _UpperMoney = null;
            return _UpperMoney;
        }
        #endregion

        #region 【判断是否是日期型】 IsDate(string str)
        /// <summary>
        /// 功能：判断是否是日期型
        /// 参数：str要验证的值
        /// 返回：True正确、False错误
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDate(string str)
        {
            bool _isDate = true;
            System.DateTime datValue;
            try
            {
                datValue = System.Convert.ToDateTime(str);
                if (datValue >= System.Convert.ToDateTime("1990-01-01") && datValue <= System.Convert.ToDateTime("2050-12-31"))
                {
                    _isDate = true;
                }
                else
                {
                    _isDate = false;
                }
            }
            catch
            {
                _isDate = false;
            }
            return _isDate;
        }
        #endregion

        #region 【判断是否是有效的电子邮件格式】 IsDate(string str)
        /// <summary>
        /// 功能：判断是否是有效的电子邮件格式
        /// 参数：strIn要验证的值
        /// 返回：True正确、False错误
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        #endregion

    }
}
