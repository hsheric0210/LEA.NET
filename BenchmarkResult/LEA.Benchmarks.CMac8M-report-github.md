``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19044.2728/21H2/November2021Update)
AMD Ryzen 7 5800X, 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.103
  [Host]               : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT AVX2
  .NET 6.0             : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT AVX2
  .NET Framework 4.6.2 : .NET Framework 4.8.1 (4.8.9139.0), X64 RyuJIT VectorSize=256
  NativeAOT 6.0        : .NET 6.0.0-rc.1.21420.1, X64 NativeAOT SSE4.2


```
| Method |                  Job |              Runtime | keySize |     Mean |    Error |   StdDev |      Min |      Max |   Median | Ratio | RatioSD |
|------- |--------------------- |--------------------- |-------- |---------:|---------:|---------:|---------:|---------:|---------:|------:|--------:|
|    **MAC** |             **.NET 6.0** |             **.NET 6.0** |     **128** | **35.82 ms** | **0.417 ms** | **0.370 ms** | **35.05 ms** | **36.37 ms** | **35.75 ms** |  **1.00** |    **0.00** |
|    MAC | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 43.08 ms | 0.845 ms | 1.069 ms | 41.76 ms | 45.10 ms | 42.96 ms |  1.19 |    0.03 |
|    MAC |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 38.11 ms | 0.152 ms | 0.127 ms | 37.95 ms | 38.42 ms | 38.10 ms |  1.06 |    0.01 |
|        |                      |                      |         |          |          |          |          |          |          |       |         |
|    **MAC** |             **.NET 6.0** |             **.NET 6.0** |     **192** | **38.75 ms** | **0.256 ms** | **0.227 ms** | **38.41 ms** | **39.10 ms** | **38.80 ms** |  **1.00** |    **0.00** |
|    MAC | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 45.49 ms | 0.901 ms | 0.926 ms | 44.12 ms | 46.97 ms | 45.40 ms |  1.17 |    0.03 |
|    MAC |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 41.83 ms | 0.361 ms | 0.320 ms | 41.45 ms | 42.45 ms | 41.72 ms |  1.08 |    0.01 |
|        |                      |                      |         |          |          |          |          |          |          |       |         |
|    **MAC** |             **.NET 6.0** |             **.NET 6.0** |     **256** | **42.41 ms** | **0.368 ms** | **0.326 ms** | **41.74 ms** | **43.04 ms** | **42.45 ms** |  **1.00** |    **0.00** |
|    MAC | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 47.74 ms | 0.454 ms | 0.402 ms | 46.83 ms | 48.26 ms | 47.84 ms |  1.13 |    0.01 |
|    MAC |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 44.89 ms | 0.184 ms | 0.172 ms | 44.67 ms | 45.18 ms | 44.84 ms |  1.06 |    0.01 |
