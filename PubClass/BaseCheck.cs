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

        #region ���ж��Ƿ����������֡� IsNumber(string str)
        /// <summary>
        /// ���ܣ��ж��Ƿ�������
        /// ������strҪ��֤��ֵ
        /// ���أ�True��ȷ��False����
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

        #region ���ж��Ƿ�����ֵ�͡�(�磺�����С���������) IsDecimal(string str)
        /// <summary>
        /// ���ܣ� �ж��Ƿ�����ֵ��(�磺�����С���������)
        /// ������strҪ��֤��ֵ
        /// ���أ�True��ȷ��False���� 
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

        #region �����������ת����ָ���ĵ�λ�Ľ�( �磺4�� ==> 40000.00 )
        /// <summary>
        /// ���ܣ����������ת����ָ���ĵ�λ�Ľ��( �磺4�� ==> 40000.00 )
        /// ������_Money Ҫת���Ľ�
        ///		  _ConvertType Ҫת����Ϊ�ĵ�λ���ͣ�1��Ԫ��2ǧԪ��3Ԫ��
        /// ���أ�ת����Ľ��
        /// </summary>
        /// <param name="_Money"></param>
        /// <param name="ConvertType"></param>
        /// <returns></returns>
        public static double ConvertToMoney(double _Money, int _ConvertType)
        {
            switch (_ConvertType)
            {
                case 1: //��Ԫ
                    _Money = _Money * 10000;
                    break;
                case 2://ǧԪ
                    _Money = _Money * 1000;
                    break;
                default: //Ԫ
                    _Money = _Money * 1;
                    break;
            }
            return _Money;
        }
        #endregion

        #region ����Сд���ת���ɴ�д�� LowerToUpper_Money( double _Money )
        /// <summary>
        /// ���ܣ���Сд���ת���ɴ�д���
        /// ������Ҫת����Сд���
        /// ���أ���д���
        /// </summary>
        /// <param name="_Money"></param>
        /// <returns></returns>
        public static string LowerToUpper_Money(decimal _Money)
        {
            string _UpperMoney = null;
            return _UpperMoney;
        }
        #endregion

        #region ���ж��Ƿ��������͡� IsDate(string str)
        /// <summary>
        /// ���ܣ��ж��Ƿ���������
        /// ������strҪ��֤��ֵ
        /// ���أ�True��ȷ��False����
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

        #region ���ж��Ƿ�����Ч�ĵ����ʼ���ʽ�� IsDate(string str)
        /// <summary>
        /// ���ܣ��ж��Ƿ�����Ч�ĵ����ʼ���ʽ
        /// ������strInҪ��֤��ֵ
        /// ���أ�True��ȷ��False����
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
