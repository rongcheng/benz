using System;
using System.Collections.Generic;
using System.Text;

namespace QJVRMS.Business.SecurityControl
{

    /// <summary>
    /// 与OperatorMethod.xml对应
    /// </summary>
    public enum OperatorMethod
    {
        /// <summary>
        /// 读取0
        /// </summary>
        Read,
        /// <summary>
        /// 写入1
        /// </summary>
        Write, 
        /// <summary>
        /// 修改2
        /// </summary>
        Modify,
        /// <summary>
        /// 删除3
        /// </summary>
        Delete,
        /// <summary>
        /// 对方发有访问权限4
        /// </summary>
        Access,
        /// <summary>
        /// 下载5
        /// </summary>
        Download,
        /// <summary>
        /// 拒绝访问6
        /// </summary>
        Deny 
    }

    /// <summary>
    /// 操作者类型
    /// </summary>
    public enum OperatorType
    {
        /// <summary>
        /// 用户组组
        /// </summary>
        Role,
        /// <summary>
        /// 用户 
        /// </summary>
        User
    }

    /// <summary>
    /// 安全对象类型
    /// </summary>
    public enum SecurityObjectType
    {
        /// <summary>
        /// 数据项
        /// </summary>
        Item,
        /// <summary>
        /// 数据集合
        /// </summary>
        Items,
        /// <summary>
        /// 功能模块
        /// </summary>
        Function,
        /// <summary>
        /// 数据项中的单元
        /// </summary>
        CellOfItem
    }
}
