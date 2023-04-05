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
| **CFB_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **128** | **42.52 ms** | **0.160 ms** | **0.142 ms** | **42.40 ms** | **42.83 ms** | **42.48 ms** |  **1.00** |    **0.00** |
| CFB_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 48.42 ms | 0.271 ms | 0.253 ms | 47.90 ms | 48.87 ms | 48.39 ms |  1.14 |    0.01 |
| CFB_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 44.65 ms | 0.378 ms | 0.335 ms | 43.85 ms | 45.05 ms | 44.74 ms |  1.05 |    0.01 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| CFB_Dec |             .NET 6.0 |             .NET 6.0 |     128 | 42.44 ms | 0.198 ms | 0.175 ms | 42.19 ms | 42.79 ms | 42.40 ms |  1.00 |    0.00 |
| CFB_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 47.85 ms | 0.199 ms | 0.176 ms | 47.58 ms | 48.09 ms | 47.93 ms |  1.13 |    0.01 |
| CFB_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 44.04 ms | 0.718 ms | 0.671 ms | 43.09 ms | 44.84 ms | 44.38 ms |  1.04 |    0.02 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| **CFB_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **192** | **45.62 ms** | **0.101 ms** | **0.089 ms** | **45.45 ms** | **45.80 ms** | **45.64 ms** |  **1.00** |    **0.00** |
| CFB_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 51.48 ms | 0.174 ms | 0.145 ms | 51.21 ms | 51.75 ms | 51.48 ms |  1.13 |    0.00 |
| CFB_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 48.60 ms | 0.948 ms | 0.931 ms | 47.35 ms | 50.44 ms | 48.65 ms |  1.07 |    0.02 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| CFB_Dec |             .NET 6.0 |             .NET 6.0 |     192 | 45.68 ms | 0.167 ms | 0.148 ms | 45.21 ms | 45.82 ms | 45.71 ms |  1.00 |    0.00 |
| CFB_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 51.10 ms | 0.224 ms | 0.210 ms | 50.75 ms | 51.47 ms | 51.12 ms |  1.12 |    0.01 |
| CFB_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 47.52 ms | 0.255 ms | 0.238 ms | 47.13 ms | 48.03 ms | 47.51 ms |  1.04 |    0.01 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| **CFB_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **256** | **49.21 ms** | **0.133 ms** | **0.118 ms** | **49.04 ms** | **49.45 ms** | **49.21 ms** |  **1.00** |    **0.00** |
| CFB_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 54.74 ms | 0.255 ms | 0.226 ms | 54.47 ms | 55.19 ms | 54.69 ms |  1.11 |    0.00 |
| CFB_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 50.91 ms | 0.774 ms | 0.724 ms | 49.88 ms | 51.80 ms | 50.67 ms |  1.04 |    0.01 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| CFB_Dec |             .NET 6.0 |             .NET 6.0 |     256 | 49.39 ms | 0.207 ms | 0.183 ms | 49.13 ms | 49.83 ms | 49.37 ms |  1.00 |    0.00 |
| CFB_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 54.55 ms | 0.318 ms | 0.282 ms | 54.25 ms | 55.26 ms | 54.51 ms |  1.10 |    0.01 |
| CFB_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 50.54 ms | 0.739 ms | 0.691 ms | 49.64 ms | 51.83 ms | 50.49 ms |  1.02 |    0.01 |
