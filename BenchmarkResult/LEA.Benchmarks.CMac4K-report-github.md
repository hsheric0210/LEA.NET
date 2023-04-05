``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19044.2728/21H2/November2021Update)
AMD Ryzen 7 5800X, 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.103
  [Host]               : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT AVX2
  .NET 6.0             : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT AVX2
  .NET Framework 4.6.2 : .NET Framework 4.8.1 (4.8.9139.0), X64 RyuJIT VectorSize=256
  NativeAOT 6.0        : .NET 6.0.0-rc.1.21420.1, X64 NativeAOT SSE4.2


```
| Method |                  Job |              Runtime | keySize |     Mean |    Error |   StdDev |      Min |      Max |   Median | Ratio |
|------- |--------------------- |--------------------- |-------- |---------:|---------:|---------:|---------:|---------:|---------:|------:|
|    **MAC** |             **.NET 6.0** |             **.NET 6.0** |     **128** | **17.68 μs** | **0.034 μs** | **0.030 μs** | **17.62 μs** | **17.72 μs** | **17.69 μs** |  **1.00** |
|    MAC | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 20.89 μs | 0.159 μs | 0.148 μs | 20.67 μs | 21.07 μs | 20.84 μs |  1.18 |
|    MAC |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 19.05 μs | 0.027 μs | 0.023 μs | 19.02 μs | 19.08 μs | 19.06 μs |  1.08 |
|        |                      |                      |         |          |          |          |          |          |          |       |
|    **MAC** |             **.NET 6.0** |             **.NET 6.0** |     **192** | **19.36 μs** | **0.069 μs** | **0.054 μs** | **19.30 μs** | **19.45 μs** | **19.33 μs** |  **1.00** |
|    MAC | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 22.41 μs | 0.232 μs | 0.217 μs | 22.07 μs | 22.89 μs | 22.38 μs |  1.16 |
|    MAC |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 20.94 μs | 0.165 μs | 0.154 μs | 20.76 μs | 21.27 μs | 20.94 μs |  1.08 |
|        |                      |                      |         |          |          |          |          |          |          |       |
|    **MAC** |             **.NET 6.0** |             **.NET 6.0** |     **256** | **20.91 μs** | **0.040 μs** | **0.035 μs** | **20.85 μs** | **20.97 μs** | **20.92 μs** |  **1.00** |
|    MAC | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 23.96 μs | 0.104 μs | 0.092 μs | 23.83 μs | 24.12 μs | 23.94 μs |  1.15 |
|    MAC |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 22.49 μs | 0.119 μs | 0.111 μs | 22.36 μs | 22.67 μs | 22.46 μs |  1.08 |
