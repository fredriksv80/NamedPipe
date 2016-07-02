using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class StreamHelper
    {
		 public static string ReadString(Stream ioStream)
		{
			int len;
			len = ioStream.ReadByte() * 256;
			len += ioStream.ReadByte();
			byte[] inBuffer = new byte[len];
			ioStream.Read(inBuffer, 0, len);

			UnicodeEncoding streamEncoding;
			streamEncoding = new UnicodeEncoding();

			return streamEncoding.GetString(inBuffer);
		}

		public static int WriteString(string outString, Stream ioStream)
		{
			UnicodeEncoding streamEncoding;
			streamEncoding = new UnicodeEncoding();

			byte[] outBuffer = streamEncoding.GetBytes(outString);
			int len = outBuffer.Length;
			if (len > UInt16.MaxValue)
			{
				len = (int)UInt16.MaxValue;
			}
			ioStream.WriteByte((byte)(len / 256));
			ioStream.WriteByte((byte)(len & 255));
			ioStream.Write(outBuffer, 0, len);
			ioStream.Flush();

			return outBuffer.Length + 2;
		}
	}
}
