using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace LEA.Benchmarks
{
	[SimpleJob(runtimeMoniker: RuntimeMoniker.Net60, baseline: true)]
	[SimpleJob(runtimeMoniker: RuntimeMoniker.Net462)]
	[SimpleJob(runtimeMoniker: RuntimeMoniker.NativeAot60)]
	[RPlotExporter, MinColumn, MaxColumn, StdDevColumn, MedianColumn]
	public class Ccm
	{
		private CipherBenchmarkAE bench;

		[Params(4096, 8388608)]
		public int dataSize;

		[Params(128, 192, 256)]
		public int keySize;

		[GlobalSetup]
		public void GlobalSetup() => bench = new CipherBenchmarkAE(new Lea.Ccm(), dataSize, keySize / 8);

		[Benchmark]
		public byte[] CCM_Encryption() => bench.Encryption();

		[Benchmark]
		public byte[] CCM_Decryption() => bench.Decryption();
	}
}
