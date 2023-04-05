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
| **OFB_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **128** | **41.04 ms** | **0.221 ms** | **0.196 ms** | **40.76 ms** | **41.39 ms** | **41.00 ms** |  **1.00** |    **0.00** |
| OFB_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 42.97 ms | 0.290 ms | 0.257 ms | 42.65 ms | 43.58 ms | 42.95 ms |  1.05 |    0.01 |
| OFB_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 43.21 ms | 0.630 ms | 0.589 ms | 41.96 ms | 44.26 ms | 43.40 ms |  1.05 |    0.02 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| OFB_Dec |             .NET 6.0 |             .NET 6.0 |     128 | 41.11 ms | 0.211 ms | 0.198 ms | 40.82 ms | 41.42 ms | 41.11 ms |  1.00 |    0.00 |
| OFB_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 43.05 ms | 0.293 ms | 0.274 ms | 42.74 ms | 43.72 ms | 42.99 ms |  1.05 |    0.01 |
| OFB_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 43.48 ms | 0.312 ms | 0.260 ms | 43.11 ms | 44.00 ms | 43.45 ms |  1.06 |    0.01 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| **OFB_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **192** | **44.42 ms** | **0.282 ms** | **0.250 ms** | **44.11 ms** | **44.92 ms** | **44.36 ms** |  **1.00** |    **0.00** |
| OFB_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 46.15 ms | 0.231 ms | 0.193 ms | 45.79 ms | 46.48 ms | 46.16 ms |  1.04 |    0.01 |
| OFB_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 46.59 ms | 0.760 ms | 0.711 ms | 45.69 ms | 47.78 ms | 46.20 ms |  1.05 |    0.02 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| OFB_Dec |             .NET 6.0 |             .NET 6.0 |     192 | 44.37 ms | 0.338 ms | 0.316 ms | 43.98 ms | 45.06 ms | 44.27 ms |  1.00 |    0.00 |
| OFB_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 46.08 ms | 0.178 ms | 0.149 ms | 45.81 ms | 46.35 ms | 46.11 ms |  1.04 |    0.01 |
| OFB_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 46.52 ms | 0.752 ms | 0.704 ms | 45.70 ms | 47.75 ms | 46.19 ms |  1.05 |    0.02 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| **OFB_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **256** | **47.33 ms** | **0.209 ms** | **0.163 ms** | **47.04 ms** | **47.49 ms** | **47.38 ms** |  **1.00** |    **0.00** |
| OFB_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 49.46 ms | 0.309 ms | 0.289 ms | 49.02 ms | 50.03 ms | 49.40 ms |  1.05 |    0.01 |
| OFB_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 49.73 ms | 0.781 ms | 0.731 ms | 48.47 ms | 50.66 ms | 49.74 ms |  1.05 |    0.02 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| OFB_Dec |             .NET 6.0 |             .NET 6.0 |     256 | 47.64 ms | 0.390 ms | 0.364 ms | 47.14 ms | 48.33 ms | 47.55 ms |  1.00 |    0.00 |
| OFB_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 49.16 ms | 0.167 ms | 0.139 ms | 48.96 ms | 49.41 ms | 49.11 ms |  1.03 |    0.01 |
| OFB_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 49.73 ms | 0.857 ms | 0.802 ms | 48.49 ms | 50.97 ms | 49.34 ms |  1.04 |    0.02 |
