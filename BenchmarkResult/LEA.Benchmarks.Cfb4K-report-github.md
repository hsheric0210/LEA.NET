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
| **CFB_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **128** | **19.66 μs** | **0.026 μs** | **0.022 μs** | **19.62 μs** | **19.71 μs** | **19.65 μs** |  **1.00** |
| CFB_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 21.82 μs | 0.119 μs | 0.111 μs | 21.65 μs | 22.00 μs | 21.80 μs |  1.11 |
| CFB_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 19.61 μs | 0.042 μs | 0.039 μs | 19.54 μs | 19.66 μs | 19.62 μs |  1.00 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| CFB_Dec |             .NET 6.0 |             .NET 6.0 |     128 | 19.80 μs | 0.046 μs | 0.043 μs | 19.74 μs | 19.90 μs | 19.80 μs |  1.00 |
| CFB_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 21.67 μs | 0.109 μs | 0.102 μs | 21.55 μs | 21.84 μs | 21.61 μs |  1.09 |
| CFB_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 19.50 μs | 0.045 μs | 0.042 μs | 19.44 μs | 19.56 μs | 19.51 μs |  0.98 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| **CFB_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **192** | **21.36 μs** | **0.076 μs** | **0.067 μs** | **21.26 μs** | **21.51 μs** | **21.35 μs** |  **1.00** |
| CFB_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 23.37 μs | 0.040 μs | 0.034 μs | 23.34 μs | 23.47 μs | 23.36 μs |  1.09 |
| CFB_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 21.19 μs | 0.064 μs | 0.057 μs | 21.11 μs | 21.31 μs | 21.19 μs |  0.99 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| CFB_Dec |             .NET 6.0 |             .NET 6.0 |     192 | 21.31 μs | 0.044 μs | 0.039 μs | 21.23 μs | 21.38 μs | 21.32 μs |  1.00 |
| CFB_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 23.33 μs | 0.108 μs | 0.095 μs | 23.24 μs | 23.52 μs | 23.29 μs |  1.09 |
| CFB_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 21.17 μs | 0.058 μs | 0.054 μs | 21.09 μs | 21.29 μs | 21.17 μs |  0.99 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| **CFB_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **256** | **22.97 μs** | **0.044 μs** | **0.041 μs** | **22.92 μs** | **23.05 μs** | **22.97 μs** |  **1.00** |
| CFB_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 25.13 μs | 0.079 μs | 0.074 μs | 25.02 μs | 25.22 μs | 25.12 μs |  1.09 |
| CFB_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 22.88 μs | 0.048 μs | 0.045 μs | 22.81 μs | 22.96 μs | 22.88 μs |  1.00 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| CFB_Dec |             .NET 6.0 |             .NET 6.0 |     256 | 23.18 μs | 0.068 μs | 0.063 μs | 23.06 μs | 23.29 μs | 23.17 μs |  1.00 |
| CFB_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 24.92 μs | 0.072 μs | 0.067 μs | 24.84 μs | 25.04 μs | 24.92 μs |  1.08 |
| CFB_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 22.80 μs | 0.033 μs | 0.028 μs | 22.75 μs | 22.85 μs | 22.80 μs |  0.98 |
