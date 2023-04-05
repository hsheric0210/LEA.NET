using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace LEA.Benchmarks
{
	[SimpleJob(runtimeMoniker: RuntimeMoniker.Net60, baseline: true)]
	[SimpleJob(runtimeMoniker: RuntimeMoniker.Net462)]
	[SimpleJob(runtimeMoniker: RuntimeMoniker.NativeAot60)]
	[RPlotExporter, MinColumn, MaxColumn, StdDevColumn, MedianColumn]
	public class Gcm4K
	{
		private const int DataSize = 4096;
		private CipherBenchmarkAE bench;

		[Params(128, 192, 256)]
		public int keySize;

		[GlobalSetup]
		public void GlobalSetup() => bench = new CipherBenchmarkAE(new Lea.Gcm(), DataSize, keySize / 8, 16);

		[Benchmark]
		public byte[] GCM_Enc() => bench.Encryption();

		[Benchmark]
		public byte[] GCM_Dec() => bench.Decryption();
	}
}
