using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static LEA.BlockCipher;

namespace LEA.Benchmarks
{
	internal class CipherBenchmark
	{
		private readonly BlockCipherMode cipher;
		private readonly byte[] plaintext;
		private readonly byte[] ciphertext;
		private readonly byte[] key;
		private readonly byte[] iv;

		public CipherBenchmark(BlockCipherMode cipher, int dataSize, int keySize)
		{
			this.cipher = cipher;
			var prng = RandomNumberGenerator.Create();

			plaintext = new byte[dataSize];
			prng.GetBytes(plaintext, 0, dataSize);

			key = new byte[keySize];
			prng.GetBytes(key, 0, keySize);

			iv = new byte[16];
			prng.GetBytes(iv, 0, 16);

			var data = new byte[dataSize];
			prng.GetBytes(data, 0, dataSize);

			cipher.Init(Mode.ENCRYPT, key, iv);
			ciphertext = cipher.DoFinal(data);
		}

		public byte[] Encryption()
		{
			cipher.Init(Mode.ENCRYPT, key, iv);
			return cipher.DoFinal(plaintext);
		}

		public byte[] Decryption()
		{
			cipher.Init(Mode.DECRYPT, key, iv);
			return cipher.DoFinal(ciphertext);
		}
	}
}
