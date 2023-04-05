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
	public class CMac
	{
		private Mac mac;
		private byte[] plaintext;
		private byte[] key;

		[Params(4096, 8388608)]
		public int dataSize;

		[Params(128, 192, 256)]
		public int keySize;

		[GlobalSetup]
		public void Setup()
		{
			mac = new Lea.CMac();
			var prng = RandomNumberGenerator.Create();

			plaintext = new byte[dataSize];
			prng.GetBytes(plaintext, 0, dataSize);

			key = new byte[keySize / 8];
			prng.GetBytes(key, 0, key.Length);
		}

		[Benchmark]
		public byte[] MAC()
		{
			mac.Init(key);
			return mac.DoFinal(plaintext);
		}
	}
}
