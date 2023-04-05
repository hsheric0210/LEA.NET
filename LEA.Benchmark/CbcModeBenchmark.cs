using BenchmarkDotNet.Jobs;
using LEA.Paddings;
using System.Security.Cryptography;

namespace LEA.Benchmark
{
	[SimpleJob(runtimeMoniker: RuntimeMoniker.Net60, baseline: true)]
	[SimpleJob(runtimeMoniker: RuntimeMoniker.Net70)]
	[SimpleJob(runtimeMoniker: RuntimeMoniker.NativeAot60)]
	[SimpleJob(runtimeMoniker: RuntimeMoniker.NativeAot70)]
	[SimpleJob(runtimeMoniker: RuntimeMoniker.Net462)]
	[SimpleJob(runtimeMoniker: RuntimeMoniker.Net472)]
	[SimpleJob(runtimeMoniker: RuntimeMoniker.Net481)]
	[SimpleJob(runtimeMoniker: RuntimeMoniker.NetCoreApp20)]
	public class CbcModeBenchmark
	{
		private byte[] plaintext;
		private byte[][] ciphertexts;
		private byte[][] keys;
		private byte[][] ivs;

		[Params(4096, 65536)]
		public int dataSize;

		[GlobalSetup]
		public void GlobalSetup()
		{
			plaintext = RandomNumberGenerator.GetBytes(dataSize);
			ciphertexts = new byte[3][];
			keys = new byte[3][];
			ivs = new byte[3][];
			for (var i = 0; i < 3; i++)
			{
				keys[i] = RandomNumberGenerator.GetBytes(16 + i * 8);
				ivs[i] = RandomNumberGenerator.GetBytes(16);
				ciphertexts[i] = Lea.EncryptCbc(RandomNumberGenerator.GetBytes(dataSize), keys[i], ivs[i], new Pkcs5Padding(16));
			}
		}

		[Benchmark]
		public byte[] Lea128CbcEncrypt() => Lea.EncryptCbc(plaintext, keys[0], ivs[0], new Pkcs5Padding(16));

		[Benchmark]
		public byte[] Lea192CbcEncrypt() => Lea.EncryptCbc(plaintext, keys[1], ivs[1], new Pkcs5Padding(16));

		[Benchmark]
		public byte[] Lea256CbcEncrypt() => Lea.EncryptCbc(plaintext, keys[2], ivs[2], new Pkcs5Padding(16));

		[Benchmark]
		public byte[] Lea128CbcDecrypt() => Lea.DecryptCbc(ciphertexts[0], keys[0], ivs[0], new Pkcs5Padding(16));

		[Benchmark]
		public byte[] Lea192CbcDecrypt() => Lea.DecryptCbc(ciphertexts[1], keys[1], ivs[1], new Pkcs5Padding(16));

		[Benchmark]
		public byte[] Lea256CbcDecrypt() => Lea.DecryptCbc(ciphertexts[2], keys[2], ivs[2], new Pkcs5Padding(16));
	}
}
