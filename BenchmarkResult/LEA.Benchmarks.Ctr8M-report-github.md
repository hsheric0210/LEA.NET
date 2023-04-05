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
| **CTR_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **128** | **40.60 ms** | **0.200 ms** | **0.177 ms** | **40.34 ms** | **40.92 ms** | **40.53 ms** |  **1.00** |    **0.00** |
| CTR_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 42.21 ms | 0.119 ms | 0.093 ms | 41.98 ms | 42.31 ms | 42.24 ms |  1.04 |    0.00 |
| CTR_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 42.29 ms | 0.713 ms | 0.667 ms | 41.39 ms | 43.37 ms | 42.18 ms |  1.04 |    0.02 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| CTR_Dec |             .NET 6.0 |             .NET 6.0 |     128 | 40.64 ms | 0.250 ms | 0.233 ms | 40.37 ms | 41.18 ms | 40.62 ms |  1.00 |    0.00 |
| CTR_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 42.42 ms | 0.215 ms | 0.179 ms | 42.18 ms | 42.87 ms | 42.43 ms |  1.04 |    0.01 |
| CTR_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 42.17 ms | 0.641 ms | 0.599 ms | 41.33 ms | 43.27 ms | 42.21 ms |  1.04 |    0.02 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| **CTR_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **192** | **43.66 ms** | **0.125 ms** | **0.104 ms** | **43.51 ms** | **43.91 ms** | **43.63 ms** |  **1.00** |    **0.00** |
| CTR_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 45.47 ms | 0.154 ms | 0.144 ms | 45.27 ms | 45.76 ms | 45.47 ms |  1.04 |    0.00 |
| CTR_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 45.79 ms | 0.719 ms | 0.672 ms | 44.66 ms | 46.52 ms | 46.14 ms |  1.05 |    0.01 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| CTR_Dec |             .NET 6.0 |             .NET 6.0 |     192 | 43.86 ms | 0.253 ms | 0.211 ms | 43.60 ms | 44.33 ms | 43.80 ms |  1.00 |    0.00 |
| CTR_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 45.52 ms | 0.083 ms | 0.073 ms | 45.42 ms | 45.65 ms | 45.51 ms |  1.04 |    0.01 |
| CTR_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 45.90 ms | 0.605 ms | 0.566 ms | 44.63 ms | 46.42 ms | 46.12 ms |  1.05 |    0.02 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| **CTR_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **256** | **46.92 ms** | **0.109 ms** | **0.096 ms** | **46.72 ms** | **47.10 ms** | **46.93 ms** |  **1.00** |    **0.00** |
| CTR_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 48.78 ms | 0.278 ms | 0.246 ms | 48.50 ms | 49.40 ms | 48.69 ms |  1.04 |    0.01 |
| CTR_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 49.00 ms | 0.292 ms | 0.259 ms | 48.58 ms | 49.45 ms | 49.02 ms |  1.04 |    0.01 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| CTR_Dec |             .NET 6.0 |             .NET 6.0 |     256 | 46.80 ms | 0.125 ms | 0.117 ms | 46.59 ms | 46.97 ms | 46.78 ms |  1.00 |    0.00 |
| CTR_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 48.84 ms | 0.154 ms | 0.128 ms | 48.69 ms | 49.16 ms | 48.83 ms |  1.04 |    0.00 |
| CTR_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 48.93 ms | 0.272 ms | 0.254 ms | 48.52 ms | 49.30 ms | 48.93 ms |  1.05 |    0.01 |
