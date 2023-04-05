using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static LEA.BlockCipher;

namespace LEA.Benchmarks
{
	[SimpleJob(runtimeMoniker: RuntimeMoniker.Net60, baseline: true)]
	[SimpleJob(runtimeMoniker: RuntimeMoniker.Net462)]
	[SimpleJob(runtimeMoniker: RuntimeMoniker.NativeAot60)]
	[RPlotExporter, MinColumn, MaxColumn, StdDevColumn, MedianColumn]
	public class Ecb
	{
		private BlockCipherMode cipher;
		private byte[] plaintext;
		private byte[] ciphertext;
		private byte[] key;

		[Params(4096, 8388608)]
		public int dataSize;

		[Params(128, 192, 256)]
		public int keySize;

		[GlobalSetup]
		public void GlobalSetup()
		{
			var prng = RandomNumberGenerator.Create();

			plaintext = new byte[dataSize];
			prng.GetBytes(plaintext, 0, dataSize);

			key = new byte[keySize / 8];
			prng.GetBytes(key, 0, key.Length);

			var data = new byte[dataSize];
			prng.GetBytes(data, 0, dataSize);

			cipher = new Lea.Ecb();
			cipher.Init(Mode.ENCRYPT, key);
			ciphertext = cipher.DoFinal(data);
		}

		[Benchmark]
		public byte[] ECB_Encryption()
		{
			cipher.Init(Mode.ENCRYPT, key);
			return cipher.DoFinal(plaintext);
		}

		[Benchmark]
		public byte[] ECB_Decryption()
		{
			cipher.Init(Mode.DECRYPT, key);
			return cipher.DoFinal(ciphertext);
		}
	}
}
