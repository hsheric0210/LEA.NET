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
| **CTR_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **128** | **18.71 μs** | **0.033 μs** | **0.031 μs** | **18.66 μs** | **18.76 μs** | **18.72 μs** |  **1.00** |
| CTR_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 18.96 μs | 0.014 μs | 0.011 μs | 18.94 μs | 18.98 μs | 18.95 μs |  1.01 |
| CTR_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 18.68 μs | 0.062 μs | 0.058 μs | 18.61 μs | 18.82 μs | 18.67 μs |  1.00 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| CTR_Dec |             .NET 6.0 |             .NET 6.0 |     128 | 18.75 μs | 0.033 μs | 0.028 μs | 18.70 μs | 18.79 μs | 18.75 μs |  1.00 |
| CTR_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 19.00 μs | 0.032 μs | 0.029 μs | 18.96 μs | 19.05 μs | 18.99 μs |  1.01 |
| CTR_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 18.70 μs | 0.041 μs | 0.038 μs | 18.64 μs | 18.76 μs | 18.71 μs |  1.00 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| **CTR_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **192** | **20.39 μs** | **0.024 μs** | **0.023 μs** | **20.36 μs** | **20.43 μs** | **20.40 μs** |  **1.00** |
| CTR_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 20.63 μs | 0.035 μs | 0.031 μs | 20.59 μs | 20.69 μs | 20.64 μs |  1.01 |
| CTR_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 20.30 μs | 0.041 μs | 0.034 μs | 20.26 μs | 20.37 μs | 20.30 μs |  1.00 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| CTR_Dec |             .NET 6.0 |             .NET 6.0 |     192 | 20.38 μs | 0.038 μs | 0.036 μs | 20.33 μs | 20.47 μs | 20.38 μs |  1.00 |
| CTR_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 20.62 μs | 0.023 μs | 0.020 μs | 20.59 μs | 20.66 μs | 20.63 μs |  1.01 |
| CTR_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 20.38 μs | 0.099 μs | 0.092 μs | 20.27 μs | 20.57 μs | 20.33 μs |  1.00 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| **CTR_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **256** | **22.04 μs** | **0.051 μs** | **0.048 μs** | **21.95 μs** | **22.11 μs** | **22.06 μs** |  **1.00** |
| CTR_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 22.31 μs | 0.038 μs | 0.036 μs | 22.26 μs | 22.37 μs | 22.29 μs |  1.01 |
| CTR_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 22.04 μs | 0.049 μs | 0.046 μs | 21.97 μs | 22.11 μs | 22.05 μs |  1.00 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| CTR_Dec |             .NET 6.0 |             .NET 6.0 |     256 | 22.03 μs | 0.054 μs | 0.048 μs | 21.95 μs | 22.13 μs | 22.04 μs |  1.00 |
| CTR_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 22.30 μs | 0.045 μs | 0.038 μs | 22.26 μs | 22.38 μs | 22.30 μs |  1.01 |
| CTR_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 22.04 μs | 0.049 μs | 0.043 μs | 21.97 μs | 22.13 μs | 22.03 μs |  1.00 |
