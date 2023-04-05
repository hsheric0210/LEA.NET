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
| **CBC_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **128** | **19.72 μs** | **0.098 μs** | **0.086 μs** | **19.62 μs** | **19.93 μs** | **19.70 μs** |  **1.00** |
| CBC_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 22.02 μs | 0.092 μs | 0.086 μs | 21.89 μs | 22.19 μs | 21.99 μs |  1.12 |
| CBC_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 19.84 μs | 0.048 μs | 0.045 μs | 19.77 μs | 19.92 μs | 19.84 μs |  1.01 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| CBC_Dec |             .NET 6.0 |             .NET 6.0 |     128 | 24.67 μs | 0.061 μs | 0.054 μs | 24.58 μs | 24.81 μs | 24.66 μs |  1.00 |
| CBC_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 40.26 μs | 0.155 μs | 0.145 μs | 39.97 μs | 40.50 μs | 40.23 μs |  1.63 |
| CBC_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 25.84 μs | 0.163 μs | 0.144 μs | 25.63 μs | 26.14 μs | 25.81 μs |  1.05 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| **CBC_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **192** | **21.01 μs** | **0.066 μs** | **0.062 μs** | **20.94 μs** | **21.15 μs** | **20.99 μs** |  **1.00** |
| CBC_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 23.63 μs | 0.128 μs | 0.119 μs | 23.48 μs | 23.90 μs | 23.60 μs |  1.12 |
| CBC_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 21.49 μs | 0.055 μs | 0.049 μs | 21.43 μs | 21.58 μs | 21.50 μs |  1.02 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| CBC_Dec |             .NET 6.0 |             .NET 6.0 |     192 | 26.73 μs | 0.068 μs | 0.064 μs | 26.62 μs | 26.85 μs | 26.74 μs |  1.00 |
| CBC_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 43.59 μs | 0.104 μs | 0.087 μs | 43.49 μs | 43.77 μs | 43.57 μs |  1.63 |
| CBC_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 27.38 μs | 0.045 μs | 0.038 μs | 27.34 μs | 27.46 μs | 27.37 μs |  1.02 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| **CBC_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **256** | **22.64 μs** | **0.054 μs** | **0.045 μs** | **22.55 μs** | **22.72 μs** | **22.64 μs** |  **1.00** |
| CBC_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 25.18 μs | 0.108 μs | 0.101 μs | 24.97 μs | 25.35 μs | 25.15 μs |  1.11 |
| CBC_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 23.01 μs | 0.041 μs | 0.038 μs | 22.95 μs | 23.09 μs | 23.01 μs |  1.02 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| CBC_Dec |             .NET 6.0 |             .NET 6.0 |     256 | 28.70 μs | 0.077 μs | 0.068 μs | 28.61 μs | 28.83 μs | 28.68 μs |  1.00 |
| CBC_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 46.17 μs | 0.096 μs | 0.090 μs | 46.04 μs | 46.38 μs | 46.17 μs |  1.61 |
| CBC_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 29.32 μs | 0.038 μs | 0.030 μs | 29.28 μs | 29.37 μs | 29.31 μs |  1.02 |
