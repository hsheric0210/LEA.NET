``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19044.2728/21H2/November2021Update)
AMD Ryzen 7 5800X, 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.103
  [Host]               : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT AVX2
  .NET 6.0             : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT AVX2
  .NET Framework 4.6.2 : .NET Framework 4.8.1 (4.8.9139.0), X64 RyuJIT VectorSize=256
  NativeAOT 6.0        : .NET 6.0.0-rc.1.21420.1, X64 NativeAOT SSE4.2


```
|  Method |                  Job |              Runtime | keySize | Mean | Error | StdDev | Min | Max | Median | Ratio | RatioSD |
|-------- |--------------------- |--------------------- |-------- |-----:|------:|-------:|----:|----:|-------:|------:|--------:|
| **CCM_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **128** |   **NA** |    **NA** |     **NA** |  **NA** |  **NA** |     **NA** |     **?** |       **?** |
| CCM_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 |   NA |    NA |     NA |  NA |  NA |     NA |     ? |       ? |
| CCM_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 |   NA |    NA |     NA |  NA |  NA |     NA |     ? |       ? |
|         |                      |                      |         |      |       |        |     |     |        |       |         |
| CCM_Dec |             .NET 6.0 |             .NET 6.0 |     128 |   NA |    NA |     NA |  NA |  NA |     NA |     ? |       ? |
| CCM_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 |   NA |    NA |     NA |  NA |  NA |     NA |     ? |       ? |
| CCM_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 |   NA |    NA |     NA |  NA |  NA |     NA |     ? |       ? |
|         |                      |                      |         |      |       |        |     |     |        |       |         |
| **CCM_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **192** |   **NA** |    **NA** |     **NA** |  **NA** |  **NA** |     **NA** |     **?** |       **?** |
| CCM_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 |   NA |    NA |     NA |  NA |  NA |     NA |     ? |       ? |
| CCM_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 |   NA |    NA |     NA |  NA |  NA |     NA |     ? |       ? |
|         |                      |                      |         |      |       |        |     |     |        |       |         |
| CCM_Dec |             .NET 6.0 |             .NET 6.0 |     192 |   NA |    NA |     NA |  NA |  NA |     NA |     ? |       ? |
| CCM_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 |   NA |    NA |     NA |  NA |  NA |     NA |     ? |       ? |
| CCM_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 |   NA |    NA |     NA |  NA |  NA |     NA |     ? |       ? |
|         |                      |                      |         |      |       |        |     |     |        |       |         |
| **CCM_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **256** |   **NA** |    **NA** |     **NA** |  **NA** |  **NA** |     **NA** |     **?** |       **?** |
| CCM_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 |   NA |    NA |     NA |  NA |  NA |     NA |     ? |       ? |
| CCM_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 |   NA |    NA |     NA |  NA |  NA |     NA |     ? |       ? |
|         |                      |                      |         |      |       |        |     |     |        |       |         |
| CCM_Dec |             .NET 6.0 |             .NET 6.0 |     256 |   NA |    NA |     NA |  NA |  NA |     NA |     ? |       ? |
| CCM_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 |   NA |    NA |     NA |  NA |  NA |     NA |     ? |       ? |
| CCM_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 |   NA |    NA |     NA |  NA |  NA |     NA |     ? |       ? |

Benchmarks with issues:
  Ccm8M.CCM_Enc: .NET 6.0(Runtime=.NET 6.0) [keySize=128]
  Ccm8M.CCM_Enc: .NET Framework 4.6.2(Runtime=.NET Framework 4.6.2) [keySize=128]
  Ccm8M.CCM_Enc: NativeAOT 6.0(Runtime=NativeAOT 6.0) [keySize=128]
  Ccm8M.CCM_Dec: .NET 6.0(Runtime=.NET 6.0) [keySize=128]
  Ccm8M.CCM_Dec: .NET Framework 4.6.2(Runtime=.NET Framework 4.6.2) [keySize=128]
  Ccm8M.CCM_Dec: NativeAOT 6.0(Runtime=NativeAOT 6.0) [keySize=128]
  Ccm8M.CCM_Enc: .NET 6.0(Runtime=.NET 6.0) [keySize=192]
  Ccm8M.CCM_Enc: .NET Framework 4.6.2(Runtime=.NET Framework 4.6.2) [keySize=192]
  Ccm8M.CCM_Enc: NativeAOT 6.0(Runtime=NativeAOT 6.0) [keySize=192]
  Ccm8M.CCM_Dec: .NET 6.0(Runtime=.NET 6.0) [keySize=192]
  Ccm8M.CCM_Dec: .NET Framework 4.6.2(Runtime=.NET Framework 4.6.2) [keySize=192]
  Ccm8M.CCM_Dec: NativeAOT 6.0(Runtime=NativeAOT 6.0) [keySize=192]
  Ccm8M.CCM_Enc: .NET 6.0(Runtime=.NET 6.0) [keySize=256]
  Ccm8M.CCM_Enc: .NET Framework 4.6.2(Runtime=.NET Framework 4.6.2) [keySize=256]
  Ccm8M.CCM_Enc: NativeAOT 6.0(Runtime=NativeAOT 6.0) [keySize=256]
  Ccm8M.CCM_Dec: .NET 6.0(Runtime=.NET 6.0) [keySize=256]
  Ccm8M.CCM_Dec: .NET Framework 4.6.2(Runtime=.NET Framework 4.6.2) [keySize=256]
  Ccm8M.CCM_Dec: NativeAOT 6.0(Runtime=NativeAOT 6.0) [keySize=256]
