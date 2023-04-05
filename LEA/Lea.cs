using System.Diagnostics;
using System.Security.Cryptography;
using LEA.engine;
using LEA.OperatingMode;

namespace LEA
{
	public static class Lea
	{
		public static BlockCipher GetEngine() => new LeaEngine();

		public sealed class Ecb : EcbMode
		{
			public Ecb() : base(GetEngine())
			{
			}
		}

		public sealed class Cbc : CbcMode
		{
			public Cbc() : base(GetEngine())
			{
			}
		}

		public sealed class Ctr : CtrMode
		{
			public Ctr() : base(GetEngine())
			{
			}
		}

		public sealed class Cfb : CfbMode
		{
			public Cfb() : base(GetEngine())
			{
			}
		}

		public sealed class Ofb : OfbMode
		{
			public Ofb() : base(GetEngine())
			{
			}
		}

		public sealed class Ccm : CcmMode
		{
			public Ccm() : base(GetEngine())
			{
			}
		}

		public sealed class Gcm : GcmMode
		{
			public Gcm() : base(GetEngine())
			{
			}
		}

		public sealed class CMac : Macs.CMac
		{
			public CMac() : base(GetEngine())
			{
			}
		}

		public static byte[] EncryptCbc(byte[] plaintext, byte[] key, byte[] iv, Padding padding) => ProcessCore(new Cbc(), BlockCipher.Mode.ENCRYPT, plaintext, key, iv, padding);
		public static byte[] DecryptCbc(byte[] plaintext, byte[] key, byte[] iv, Padding padding) => ProcessCore(new Cbc(), BlockCipher.Mode.DECRYPT, plaintext, key, iv, padding);

		public static byte[] EncryptEcb(byte[] plaintext, byte[] key, Padding padding) => ProcessCore(new Ecb(), BlockCipher.Mode.ENCRYPT, plaintext, key, null, padding);
		public static byte[] DecryptEcb(byte[] plaintext, byte[] key, Padding padding) => ProcessCore(new Ecb(), BlockCipher.Mode.DECRYPT, plaintext, key, null, padding);

		public static byte[] EncryptCfb(byte[] plaintext, byte[] key, byte[] iv, Padding padding) => ProcessCore(new Cfb(), BlockCipher.Mode.ENCRYPT, plaintext, key, iv, padding);
		public static byte[] DecryptCfb(byte[] plaintext, byte[] key, byte[] iv, Padding padding) => ProcessCore(new Cfb(), BlockCipher.Mode.DECRYPT, plaintext, key, iv, padding);

		public static byte[] EncryptCtr(byte[] plaintext, byte[] key, byte[] nonce, Padding padding) => ProcessCore(new Ctr(), BlockCipher.Mode.ENCRYPT, plaintext, key, nonce, padding);
		public static byte[] DecryptCtr(byte[] plaintext, byte[] key, byte[] nonce, Padding padding) => ProcessCore(new Ctr(), BlockCipher.Mode.DECRYPT, plaintext, key, nonce, padding);

		public static byte[] EncryptOfb(byte[] plaintext, byte[] key, byte[] iv, Padding padding) => ProcessCore(new Ofb(), BlockCipher.Mode.ENCRYPT, plaintext, key, iv, padding);
		public static byte[] DecryptOfb(byte[] plaintext, byte[] key, byte[] iv, Padding padding) => ProcessCore(new Ofb(), BlockCipher.Mode.DECRYPT, plaintext, key, iv, padding);

		public static byte[] EncryptCcm(byte[] plaintext, byte[] key, byte[] iv, int tagLength, byte[] aad) => ProcessAECore(new Ccm(), BlockCipher.Mode.ENCRYPT, plaintext, key, iv, tagLength, aad);
		public static byte[] DecryptCcm(byte[] plaintext, byte[] key, byte[] iv, int tagLength, byte[] aad) => ProcessAECore(new Ccm(), BlockCipher.Mode.DECRYPT, plaintext, key, iv, tagLength, aad);

		public static byte[] EncryptGcm(byte[] plaintext, byte[] key, byte[] iv, int tagLength, byte[] aad) => ProcessAECore(new Gcm(), BlockCipher.Mode.ENCRYPT, plaintext, key, iv, tagLength, aad);
		public static byte[] DecryptGcm(byte[] plaintext, byte[] key, byte[] iv, int tagLength, byte[] aad) => ProcessAECore(new Gcm(), BlockCipher.Mode.DECRYPT, plaintext, key, iv, tagLength, aad);

		private static byte[] ProcessCore(BlockCipherMode cipher, BlockCipher.Mode mode, byte[] plaintext, byte[] key, byte[] iv, Padding padding)
		{
			if (iv == null)
				cipher.Init(mode, key);
			else
				cipher.Init(mode, key, iv);
			cipher.SetPadding(padding);
			return cipher.DoFinal(plaintext);
		}

		private static byte[] ProcessAECore(BlockCipherModeAE cipher, BlockCipher.Mode mode, byte[] plaintext, byte[] key, byte[] iv, int tagLength, byte[] aad)
		{
			cipher.Init(mode, key, iv, tagLength);
			cipher.UpdateAAD(aad);
			return cipher.DoFinal(plaintext);
		}
	}
}
