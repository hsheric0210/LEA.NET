using System.IO;

namespace LEA
{
	internal static class MemoryStreamCompatExtension
	{
		public static byte[] BufferData(this MemoryStream stream)
		{
#if NETSTANDARD2_0_OR_GREATER
			return stream.GetBuffer();
#else
			return stream.ToArray();
#endif
		}
	}
}
