``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19044.2728/21H2/November2021Update)
AMD Ryzen 7 5800X, 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.103
  [Host]               : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT AVX2
  .NET 6.0             : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT AVX2
  .NET Framework 4.6.2 : .NET Framework 4.8.1 (4.8.9139.0), X64 RyuJIT VectorSize=256
  NativeAOT 6.0        : .NET 6.0.0-rc.1.21420.1, X64 NativeAOT SSE4.2


```
|  Method |                  Job |              Runtime | keySize |     Mean |    Error |   StdDev |      Min |      Max |   Median | Ratio | RatioSD |
|-------- |--------------------- |--------------------- |-------- |---------:|---------:|---------:|---------:|---------:|---------:|------:|--------:|
| **ECB_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **128** | **33.87 ms** | **0.073 ms** | **0.061 ms** | **33.76 ms** | **33.97 ms** | **33.86 ms** |  **1.00** |    **0.00** |
| ECB_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 35.10 ms | 0.127 ms | 0.106 ms | 34.94 ms | 35.35 ms | 35.10 ms |  1.04 |    0.00 |
| ECB_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 35.59 ms | 0.670 ms | 0.688 ms | 34.25 ms | 36.87 ms | 35.51 ms |  1.05 |    0.02 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| ECB_Dec |             .NET 6.0 |             .NET 6.0 |     128 | 46.77 ms | 0.227 ms | 0.212 ms | 46.26 ms | 47.12 ms | 46.74 ms |  1.00 |    0.00 |
| ECB_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 66.19 ms | 0.197 ms | 0.184 ms | 65.92 ms | 66.49 ms | 66.20 ms |  1.42 |    0.01 |
| ECB_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 47.72 ms | 0.774 ms | 0.724 ms | 46.26 ms | 48.77 ms | 47.71 ms |  1.02 |    0.02 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| **ECB_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **192** | **36.69 ms** | **0.231 ms** | **0.216 ms** | **36.00 ms** | **36.98 ms** | **36.70 ms** |  **1.00** |    **0.00** |
| ECB_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 38.10 ms | 0.167 ms | 0.156 ms | 37.92 ms | 38.46 ms | 38.07 ms |  1.04 |    0.01 |
| ECB_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 38.31 ms | 0.586 ms | 0.548 ms | 37.67 ms | 38.97 ms | 38.69 ms |  1.04 |    0.02 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| ECB_Dec |             .NET 6.0 |             .NET 6.0 |     192 | 51.03 ms | 0.237 ms | 0.198 ms | 50.84 ms | 51.53 ms | 51.01 ms |  1.00 |    0.00 |
| ECB_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 67.16 ms | 0.139 ms | 0.130 ms | 66.99 ms | 67.40 ms | 67.11 ms |  1.32 |    0.00 |
| ECB_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 50.83 ms | 0.147 ms | 0.137 ms | 50.48 ms | 50.99 ms | 50.83 ms |  1.00 |    0.01 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| **ECB_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **256** | **39.98 ms** | **0.132 ms** | **0.123 ms** | **39.79 ms** | **40.19 ms** | **39.96 ms** |  **1.00** |    **0.00** |
| ECB_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 41.27 ms | 0.101 ms | 0.084 ms | 41.18 ms | 41.48 ms | 41.24 ms |  1.03 |    0.00 |
| ECB_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 41.08 ms | 0.615 ms | 0.575 ms | 40.50 ms | 42.25 ms | 40.78 ms |  1.03 |    0.01 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| ECB_Dec |             .NET 6.0 |             .NET 6.0 |     256 | 54.80 ms | 0.211 ms | 0.176 ms | 54.61 ms | 55.09 ms | 54.78 ms |  1.00 |    0.00 |
| ECB_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 72.40 ms | 0.281 ms | 0.263 ms | 71.95 ms | 72.79 ms | 72.43 ms |  1.32 |    0.01 |
| ECB_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 54.68 ms | 0.152 ms | 0.134 ms | 54.43 ms | 54.93 ms | 54.71 ms |  1.00 |    0.00 |
