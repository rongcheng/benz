using System;
using System.Text;

namespace QJVRMS.Common.Base64
{
	/// <summary>
	/// base64 的摘要说明。
	/// </summary>
	public class base64
	{
		public base64()
		{
		}
		public static string Encode(string InputString)
		{
			string BackValue;

			byte[] ConvertBytes = Encoding.GetEncoding("GB2312").GetBytes(InputString);

			BackValue = Convert.ToBase64String(ConvertBytes);

			return BackValue;
		}

		public static string Decode(string InputString)
		{
			string BackValue;

			byte[] ConvertBytes = Convert.FromBase64String(InputString);

			BackValue = Encoding.GetEncoding("GB2312").GetString(ConvertBytes);

			return BackValue;
		}

	}
}
