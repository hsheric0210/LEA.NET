using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace LEA.Benchmarks
{
	[SimpleJob(runtimeMoniker: RuntimeMoniker.Net60, baseline: true)]
	[SimpleJob(runtimeMoniker: RuntimeMoniker.Net462)]
	[SimpleJob(runtimeMoniker: RuntimeMoniker.NativeAot60)]
	[RPlotExporter, MinColumn, MaxColumn, StdDevColumn, MedianColumn]
	public class Cbc4K
	{
		private const int DataSize = 4096;
		private CipherBenchmark bench;

		[Params(128, 192, 256)]
		public int keySize;

		[GlobalSetup]
		public void GlobalSetup() => bench = new CipherBenchmark(new Lea.Cbc(), DataSize, keySize / 8);

		[Benchmark]
		public byte[] CBC_Enc() => bench.Encryption();

		[Benchmark]
		public byte[] CBC_Dec() => bench.Decryption();
	}
}
