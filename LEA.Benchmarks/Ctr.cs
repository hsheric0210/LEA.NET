﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace LEA.Benchmarks
{
	[SimpleJob(runtimeMoniker: RuntimeMoniker.Net60, baseline: true)]
	[SimpleJob(runtimeMoniker: RuntimeMoniker.Net462)]
	[SimpleJob(runtimeMoniker: RuntimeMoniker.NativeAot60)]
	[RPlotExporter, MinColumn, MaxColumn, StdDevColumn, MedianColumn]
	public class Ctr
	{
		private CipherBenchmark bench;

		[Params(4096, 8388608)]
		public int dataSize;

		[Params(128, 192, 256)]
		public int keySize;

		[GlobalSetup]
		public void GlobalSetup() => bench = new CipherBenchmark(new Lea.Ctr(), dataSize, keySize / 8);

		[Benchmark]
		public byte[] CTR_Encryption() => bench.Encryption();

		[Benchmark]
		public byte[] CTR_Decryption() => bench.Decryption();
	}
}
