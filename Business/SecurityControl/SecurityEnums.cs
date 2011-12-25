using System;
using System.Collections.Generic;
using System.Text;

namespace QJVRMS.Business.SecurityControl
{

    /// <summary>
    /// ��OperatorMethod.xml��Ӧ
    /// </summary>
    public enum OperatorMethod
    {
        /// <summary>
        /// ��ȡ0
        /// </summary>
        Read,
        /// <summary>
        /// д��1
        /// </summary>
        Write, 
        /// <summary>
        /// �޸�2
        /// </summary>
        Modify,
        /// <summary>
        /// ɾ��3
        /// </summary>
        Delete,
        /// <summary>
        /// �Է����з���Ȩ��4
        /// </summary>
        Access,
        /// <summary>
        /// ����5
        /// </summary>
        Download,
        /// <summary>
        /// �ܾ�����6
        /// </summary>
        Deny 
    }

    /// <summary>
    /// ����������
    /// </summary>
    public enum OperatorType
    {
        /// <summary>
        /// �û�����
        /// </summary>
        Role,
        /// <summary>
        /// �û� 
        /// </summary>
        User
    }

    /// <summary>
    /// ��ȫ��������
    /// </summary>
    public enum SecurityObjectType
    {
        /// <summary>
        /// ������
        /// </summary>
        Item,
        /// <summary>
        /// ���ݼ���
        /// </summary>
        Items,
        /// <summary>
        /// ����ģ��
        /// </summary>
        Function,
        /// <summary>
        /// �������еĵ�Ԫ
        /// </summary>
        CellOfItem
    }
}
