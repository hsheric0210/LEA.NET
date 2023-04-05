using BenchmarkDotNet.Running;
using LEA.Benchmark;

internal class Program
{
	private static void Main(string[] args)
	{
		BenchmarkRunner.Run<CbcModeBenchmark>();
	}
}