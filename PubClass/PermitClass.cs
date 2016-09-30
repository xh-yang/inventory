using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PubClass
{
    /// <summary>
    /// 权限枚举
    /// </summary>
    enum PermitType:int
    {
        [Description("权限（拒绝访问）")]
        deny=0,
        [Description("权限（授权访问）")]
        access=1,
        [Description("权限（授权访问列表）")]
        showlist=2,
        [Description("权限（添加）")]
        add=3,
        [Description("权限（修改）")]
        update=4,
        [Description("权限（删除）")]
        delete=5,
        [Description("权限（查询）")]
        select = 6,
        [Description("权限（数据库备份）")]
        bak = 7,
        [Description("权限（数据库还原）")]
        rebak = 8,
        [Description("权限（授权）")]
        grant=99
    }

   public static class PermitClass
   {
       #region 获取枚举描述
       /// <summary>
       /// 获取枚举描述
       /// </summary>
       /// <param name="e">枚举类别</param>
       /// <returns>枚举描述</returns>
       public static String GetEnumDesc(PermitType e)
       {
           FieldInfo EnumInfo = e.GetType().GetField(e.ToString());
           DescriptionAttribute[] EnumAttributes = (DescriptionAttribute[])EnumInfo.
               GetCustomAttributes(typeof(DescriptionAttribute), false);
           if (EnumAttributes.Length > 0)
           {
               return EnumAttributes[0].Description;
           }
           return e.ToString();
       }
       #endregion
   }

}
