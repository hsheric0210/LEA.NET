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
| **CBC_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **128** | **42.05 ms** | **0.132 ms** | **0.123 ms** | **41.90 ms** | **42.30 ms** | **42.05 ms** |  **1.00** |    **0.00** |
| CBC_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 48.76 ms | 0.283 ms | 0.264 ms | 48.35 ms | 49.26 ms | 48.69 ms |  1.16 |    0.01 |
| CBC_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 44.84 ms | 0.378 ms | 0.315 ms | 44.26 ms | 45.30 ms | 44.82 ms |  1.07 |    0.01 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| CBC_Dec |             .NET 6.0 |             .NET 6.0 |     128 | 58.29 ms | 0.123 ms | 0.109 ms | 58.11 ms | 58.54 ms | 58.30 ms |  1.00 |    0.00 |
| CBC_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 86.26 ms | 0.385 ms | 0.360 ms | 85.65 ms | 86.99 ms | 86.10 ms |  1.48 |    0.01 |
| CBC_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 55.30 ms | 0.205 ms | 0.192 ms | 55.03 ms | 55.72 ms | 55.25 ms |  0.95 |    0.00 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| **CBC_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **192** | **45.05 ms** | **0.122 ms** | **0.108 ms** | **44.90 ms** | **45.27 ms** | **45.05 ms** |  **1.00** |    **0.00** |
| CBC_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 51.58 ms | 0.208 ms | 0.184 ms | 51.28 ms | 51.83 ms | 51.62 ms |  1.15 |    0.01 |
| CBC_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 48.16 ms | 0.819 ms | 0.766 ms | 47.00 ms | 48.98 ms | 48.30 ms |  1.07 |    0.02 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| CBC_Dec |             .NET 6.0 |             .NET 6.0 |     192 | 56.43 ms | 0.165 ms | 0.146 ms | 56.08 ms | 56.63 ms | 56.43 ms |  1.00 |    0.00 |
| CBC_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 92.87 ms | 0.225 ms | 0.210 ms | 92.51 ms | 93.22 ms | 92.86 ms |  1.65 |    0.00 |
| CBC_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 58.70 ms | 0.124 ms | 0.116 ms | 58.53 ms | 58.91 ms | 58.68 ms |  1.04 |    0.00 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| **CBC_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **256** | **48.23 ms** | **0.169 ms** | **0.150 ms** | **47.99 ms** | **48.53 ms** | **48.20 ms** |  **1.00** |    **0.00** |
| CBC_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 54.92 ms | 0.159 ms | 0.133 ms | 54.64 ms | 55.17 ms | 54.96 ms |  1.14 |    0.01 |
| CBC_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 50.37 ms | 0.104 ms | 0.081 ms | 50.25 ms | 50.48 ms | 50.36 ms |  1.04 |    0.00 |
|         |                      |                      |         |          |          |          |          |          |          |       |         |
| CBC_Dec |             .NET 6.0 |             .NET 6.0 |     256 | 60.12 ms | 0.153 ms | 0.128 ms | 59.93 ms | 60.32 ms | 60.12 ms |  1.00 |    0.00 |
| CBC_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 96.59 ms | 0.321 ms | 0.284 ms | 96.19 ms | 97.22 ms | 96.50 ms |  1.61 |    0.01 |
| CBC_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 62.79 ms | 0.280 ms | 0.234 ms | 62.25 ms | 63.16 ms | 62.79 ms |  1.04 |    0.00 |
