using System;
using System.Collections.Generic;
using System.Text;

namespace PublicClass
{
    class CMsgText
    {
        public CMsgText()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 保存完整性检查，返回组合好的提示信息
        /// <summary>
        /// 保存完整性检查，返回组合好的提示信息
        /// </summary>
        /// <param name="MsgText"></param>
        /// <returns></returns>
        public static string GetSaveCheck(string MsgText)
        {
            string ResultStr = "保存操作不能完成，" + MsgText.Trim() + "，请重新录入！";
            return ResultStr;
        }
        #endregion

        #region 保存出现系统级错误提示信息
        /// <summary>
        /// 保存出现系统级错误提示信息
        /// </summary>
        /// <returns></returns>
        public static string GetSaveSysError()
        {
            string ResultStr = "保存操作不能完成，请及时与系统管理员联系！";
            return ResultStr;
        }
        #endregion

        #region 保存出现不能确定的问题
        /// <summary>
        /// 保存出现不能确定的问题
        /// </summary>
        /// <returns></returns>
        public static string GetSaveUnKnow()
        {
            string ResultStr = "保存操作不能完成，请参看用户手册后检查数据的正确性！";
            return ResultStr;
        }
        #endregion

        #region 删除出现系统级别错误
        /// <summary>
        /// 删除出现系统级别错误
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteSysError()
        {
            string ResultStr = "删除操作不能完成，请及时与系统管理员联系！";
            return ResultStr;
        }
        #endregion

        #region 删除出现业务逻辑错误
        /// <summary>
        /// 删除出现业务逻辑错误
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteUnKnow()
        {
            string ResultStr = "删除操作不能完成，请参看用户手册后检查数据的正确性！";
            return ResultStr;
        }
        #endregion

        #region 删除前确认的标准信息
        /// <summary>
        /// 删除前确定的标准信息
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteMsg()
        {
            string ResultStr = "删除后数据不能恢复，您确定要进行删除吗？";
            return ResultStr;
        }
        #endregion
    }
}
