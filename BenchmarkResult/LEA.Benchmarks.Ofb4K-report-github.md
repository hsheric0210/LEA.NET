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
| **OFB_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **128** | **19.05 μs** | **0.086 μs** | **0.072 μs** | **18.96 μs** | **19.19 μs** | **19.03 μs** |  **1.00** |    **0.00** |
| OFB_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 19.37 μs | 0.091 μs | 0.085 μs | 19.24 μs | 19.48 μs | 19.39 μs |  1.02 |    0.01 |
| OFB_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 19.29 μs | 0.145 μs | 0.136 μs | 19.14 μs | 19.57 μs | 19.24 μs |  1.01 |    0.01 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| OFB_Dec |             .NET 6.0 |             .NET 6.0 |     128 | 19.25 μs | 0.061 μs | 0.054 μs | 19.17 μs | 19.35 μs | 19.24 μs |  1.00 |    0.00 |
| OFB_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 19.37 μs | 0.099 μs | 0.093 μs | 19.25 μs | 19.52 μs | 19.35 μs |  1.01 |    0.01 |
| OFB_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 19.29 μs | 0.216 μs | 0.180 μs | 19.06 μs | 19.73 μs | 19.29 μs |  1.00 |    0.01 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| **OFB_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **192** | **20.93 μs** | **0.230 μs** | **0.215 μs** | **20.64 μs** | **21.36 μs** | **20.85 μs** |  **1.00** |    **0.00** |
| OFB_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 21.12 μs | 0.244 μs | 0.228 μs | 20.83 μs | 21.55 μs | 21.07 μs |  1.01 |    0.02 |
| OFB_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 20.76 μs | 0.109 μs | 0.102 μs | 20.65 μs | 21.02 μs | 20.74 μs |  0.99 |    0.01 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| OFB_Dec |             .NET 6.0 |             .NET 6.0 |     192 | 20.89 μs | 0.121 μs | 0.101 μs | 20.79 μs | 21.16 μs | 20.86 μs |  1.00 |    0.00 |
| OFB_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 21.02 μs | 0.191 μs | 0.178 μs | 20.78 μs | 21.35 μs | 20.96 μs |  1.01 |    0.01 |
| OFB_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 20.87 μs | 0.069 μs | 0.058 μs | 20.78 μs | 21.00 μs | 20.87 μs |  1.00 |    0.00 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| **OFB_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **256** | **22.48 μs** | **0.111 μs** | **0.104 μs** | **22.32 μs** | **22.65 μs** | **22.48 μs** |  **1.00** |    **0.00** |
| OFB_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 22.85 μs | 0.138 μs | 0.129 μs | 22.68 μs | 23.13 μs | 22.84 μs |  1.02 |    0.01 |
| OFB_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 22.55 μs | 0.100 μs | 0.088 μs | 22.41 μs | 22.71 μs | 22.53 μs |  1.00 |    0.01 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| OFB_Dec |             .NET 6.0 |             .NET 6.0 |     256 | 22.42 μs | 0.193 μs | 0.172 μs | 22.20 μs | 22.81 μs | 22.38 μs |  1.00 |    0.00 |
| OFB_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 22.52 μs | 0.086 μs | 0.076 μs | 22.40 μs | 22.69 μs | 22.53 μs |  1.00 |    0.01 |
| OFB_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 22.72 μs | 0.176 μs | 0.165 μs | 22.51 μs | 23.00 μs | 22.67 μs |  1.01 |    0.01 |
