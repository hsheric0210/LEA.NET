``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19044.2728/21H2/November2021Update)
AMD Ryzen 7 5800X, 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.103
  [Host]               : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT AVX2
  .NET 6.0             : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT AVX2
  .NET Framework 4.6.2 : .NET Framework 4.8.1 (4.8.9139.0), X64 RyuJIT VectorSize=256
  NativeAOT 6.0        : .NET 6.0.0-rc.1.21420.1, X64 NativeAOT SSE4.2


```
|  Method |                  Job |              Runtime | keySize |     Mean |    Error |   StdDev |      Min |      Max |   Median | Ratio |
|-------- |--------------------- |--------------------- |-------- |---------:|---------:|---------:|---------:|---------:|---------:|------:|
| **CCM_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **128** | **35.92 μs** | **0.085 μs** | **0.075 μs** | **35.81 μs** | **36.08 μs** | **35.90 μs** |  **1.00** |
| CCM_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 36.22 μs | 0.065 μs | 0.054 μs | 36.13 μs | 36.35 μs | 36.21 μs |  1.01 |
| CCM_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 36.05 μs | 0.326 μs | 0.305 μs | 35.81 μs | 36.60 μs | 35.91 μs |  1.00 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| CCM_Dec |             .NET 6.0 |             .NET 6.0 |     128 | 35.91 μs | 0.090 μs | 0.084 μs | 35.74 μs | 36.04 μs | 35.93 μs |  1.00 |
| CCM_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 36.30 μs | 0.082 μs | 0.077 μs | 36.16 μs | 36.44 μs | 36.32 μs |  1.01 |
| CCM_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 36.55 μs | 0.088 μs | 0.082 μs | 36.42 μs | 36.69 μs | 36.54 μs |  1.02 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| **CCM_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **192** | **39.26 μs** | **0.089 μs** | **0.079 μs** | **39.12 μs** | **39.40 μs** | **39.26 μs** |  **1.00** |
| CCM_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 39.45 μs | 0.060 μs | 0.050 μs | 39.38 μs | 39.53 μs | 39.44 μs |  1.00 |
| CCM_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 39.89 μs | 0.389 μs | 0.364 μs | 39.37 μs | 40.40 μs | 40.06 μs |  1.02 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| CCM_Dec |             .NET 6.0 |             .NET 6.0 |     192 | 39.27 μs | 0.126 μs | 0.105 μs | 38.99 μs | 39.44 μs | 39.29 μs |  1.00 |
| CCM_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 39.51 μs | 0.037 μs | 0.032 μs | 39.47 μs | 39.56 μs | 39.51 μs |  1.01 |
| CCM_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 39.46 μs | 0.267 μs | 0.237 μs | 39.09 μs | 39.91 μs | 39.42 μs |  1.01 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| **CCM_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **256** | **42.47 μs** | **0.139 μs** | **0.123 μs** | **42.30 μs** | **42.71 μs** | **42.41 μs** |  **1.00** |
| CCM_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 42.74 μs | 0.050 μs | 0.042 μs | 42.67 μs | 42.84 μs | 42.74 μs |  1.01 |
| CCM_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 42.74 μs | 0.096 μs | 0.085 μs | 42.63 μs | 42.93 μs | 42.72 μs |  1.01 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| CCM_Dec |             .NET 6.0 |             .NET 6.0 |     256 | 42.50 μs | 0.104 μs | 0.092 μs | 42.37 μs | 42.70 μs | 42.48 μs |  1.00 |
| CCM_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 42.83 μs | 0.082 μs | 0.077 μs | 42.74 μs | 42.95 μs | 42.79 μs |  1.01 |
| CCM_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 42.61 μs | 0.075 μs | 0.066 μs | 42.52 μs | 42.72 μs | 42.61 μs |  1.00 |
