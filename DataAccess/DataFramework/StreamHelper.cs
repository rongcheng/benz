using System;
using System.IO;
using System.Collections;

namespace QJVRMS.DataAccess
{
	/// <summary>
	/// StreamHelper 的摘要说明。
	/// </summary>
	public class StreamHelper
	{
 
		public static byte[] ToBuffer (Stream stream)
		{


			//try to access the target folder
			byte[] buffer = new byte[stream.Length];

			int curidx = 0;
			int buffersize = 1024;
			//the stream.Lenth is a long type. 
			//It's not make sense to convert it into INT type directly 
			while(curidx + buffersize <= stream.Length)
			{
				stream.Read(buffer, curidx,  buffersize);
				curidx = curidx + buffersize;
			}
			stream.Read(buffer,curidx ,(int)(stream.Length - curidx));


			return  buffer;






		}
		public static Stream ToStream (byte[] buffer)
		{
			MemoryStream oMemoryStream = new MemoryStream(buffer);
			return oMemoryStream;
	
		}



	}
}
