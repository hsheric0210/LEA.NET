using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static LEA.BlockCipher;

namespace LEA.Benchmarks
{
	internal class CipherBenchmarkAE
	{
		private readonly BlockCipherModeAE cipher;
		private readonly byte[] plaintext;
		private readonly byte[] ciphertext;
		private readonly byte[] key;
		private readonly byte[] nonce;

		public CipherBenchmarkAE(BlockCipherModeAE cipher, int dataSize, int keySize, int nonceSize)
		{
			this.cipher = cipher;
			var prng = RandomNumberGenerator.Create();

			plaintext = new byte[dataSize];
			prng.GetBytes(plaintext, 0, dataSize);
			key = new byte[keySize];
			prng.GetBytes(key, 0, keySize);

			nonce = new byte[nonceSize];
			prng.GetBytes(nonce, 0, nonceSize);

			var data = new byte[dataSize];
			prng.GetBytes(data, 0, dataSize);

			cipher.Init(Mode.ENCRYPT, key, nonce, 16);
			ciphertext = cipher.DoFinal(data);
		}

		public byte[] Encryption()
		{
			cipher.Init(Mode.ENCRYPT, key, nonce, 16);
			return cipher.DoFinal(plaintext);
		}

		public byte[] Decryption()
		{
			cipher.Init(Mode.DECRYPT, key, nonce, 16);
			return cipher.DoFinal(ciphertext);
		}
	}
}
