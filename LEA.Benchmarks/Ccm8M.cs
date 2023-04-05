using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace LEA.Benchmarks
{
	[SimpleJob(runtimeMoniker: RuntimeMoniker.Net60, baseline: true)]
	[SimpleJob(runtimeMoniker: RuntimeMoniker.Net462)]
	[SimpleJob(runtimeMoniker: RuntimeMoniker.NativeAot60)]
	[RPlotExporter, MinColumn, MaxColumn, StdDevColumn, MedianColumn]
	public class Ccm8M
	{
		private const int DataSize = 8388608;
		private CipherBenchmarkAE bench;

		[Params(128, 192, 256)]
		public int keySize;

		[GlobalSetup]
		public void GlobalSetup() => bench = new CipherBenchmarkAE(new Lea.Ccm(), DataSize, keySize / 8, 13);

		[Benchmark]
		public byte[] CCM_Enc() => bench.Encryption();

		[Benchmark]
		public byte[] CCM_Dec() => bench.Decryption();
	}
}
