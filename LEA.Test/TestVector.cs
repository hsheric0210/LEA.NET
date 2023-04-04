namespace LEA.Test
{
	public struct TestVector
	{
		public byte[] Key;
		public byte[] IV;
		public byte[] PlainText;
		public byte[] CipherText;
	}

	public struct TestVectorMac
	{
		public byte[] Key;
		public byte[] Data;
		public byte[] Mac;
	}

	public struct TestVectorAE
	{
		public byte[] Key;
		public byte[] IV;
		public byte[] AAD;
		public byte[] PlainText;
		public byte[] CipherText;
		public byte[] Tag;
	}
}