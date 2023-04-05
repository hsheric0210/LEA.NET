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
| **ECB_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **128** | **15.27 μs** | **0.053 μs** | **0.049 μs** | **15.21 μs** | **15.37 μs** | **15.27 μs** |  **1.00** |
| ECB_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 15.34 μs | 0.034 μs | 0.030 μs | 15.30 μs | 15.40 μs | 15.34 μs |  1.00 |
| ECB_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 15.21 μs | 0.021 μs | 0.020 μs | 15.16 μs | 15.24 μs | 15.21 μs |  1.00 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| ECB_Dec |             .NET 6.0 |             .NET 6.0 |     128 | 20.92 μs | 0.053 μs | 0.050 μs | 20.85 μs | 21.03 μs | 20.91 μs |  1.00 |
| ECB_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 30.55 μs | 0.068 μs | 0.064 μs | 30.46 μs | 30.68 μs | 30.53 μs |  1.46 |
| ECB_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 21.27 μs | 0.074 μs | 0.065 μs | 21.18 μs | 21.41 μs | 21.27 μs |  1.02 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| **ECB_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **192** | **16.91 μs** | **0.040 μs** | **0.037 μs** | **16.85 μs** | **16.97 μs** | **16.91 μs** |  **1.00** |
| ECB_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 17.01 μs | 0.024 μs | 0.020 μs | 16.97 μs | 17.04 μs | 17.01 μs |  1.01 |
| ECB_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 16.69 μs | 0.051 μs | 0.048 μs | 16.63 μs | 16.78 μs | 16.68 μs |  0.99 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| ECB_Dec |             .NET 6.0 |             .NET 6.0 |     192 | 23.85 μs | 0.035 μs | 0.031 μs | 23.79 μs | 23.91 μs | 23.85 μs |  1.00 |
| ECB_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 30.14 μs | 0.050 μs | 0.044 μs | 30.09 μs | 30.22 μs | 30.12 μs |  1.26 |
| ECB_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 23.21 μs | 0.044 μs | 0.039 μs | 23.14 μs | 23.26 μs | 23.21 μs |  0.97 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| **ECB_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **256** | **18.58 μs** | **0.065 μs** | **0.061 μs** | **18.51 μs** | **18.72 μs** | **18.56 μs** |  **1.00** |
| ECB_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 18.65 μs | 0.023 μs | 0.020 μs | 18.61 μs | 18.69 μs | 18.65 μs |  1.00 |
| ECB_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 18.43 μs | 0.033 μs | 0.027 μs | 18.39 μs | 18.49 μs | 18.43 μs |  0.99 |
|         |                      |                      |         |          |          |          |          |          |          |       |
| ECB_Dec |             .NET 6.0 |             .NET 6.0 |     256 | 25.79 μs | 0.076 μs | 0.071 μs | 25.68 μs | 25.88 μs | 25.81 μs |  1.00 |
| ECB_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 38.74 μs | 0.091 μs | 0.081 μs | 38.66 μs | 38.93 μs | 38.72 μs |  1.50 |
| ECB_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 25.21 μs | 0.038 μs | 0.035 μs | 25.16 μs | 25.29 μs | 25.21 μs |  0.98 |
