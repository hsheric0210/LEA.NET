``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19044.2728/21H2/November2021Update)
AMD Ryzen 7 5800X, 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.103
  [Host]               : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT AVX2
  .NET 6.0             : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT AVX2
  .NET Framework 4.6.2 : .NET Framework 4.8.1 (4.8.9139.0), X64 RyuJIT VectorSize=256
  NativeAOT 6.0        : .NET 6.0.0-rc.1.21420.1, X64 NativeAOT SSE4.2


```
|  Method |                  Job |              Runtime | keySize |     Mean |   Error |  StdDev |      Min |      Max |   Median | Ratio | RatioSD |
|-------- |--------------------- |--------------------- |-------- |---------:|--------:|--------:|---------:|---------:|---------:|------:|--------:|
| **GCM_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **128** | **248.3 ms** | **0.54 ms** | **0.42 ms** | **247.6 ms** | **249.0 ms** | **248.4 ms** |  **1.00** |    **0.00** |
| GCM_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 249.0 ms | 1.25 ms | 1.04 ms | 246.4 ms | 250.2 ms | 249.2 ms |  1.00 |    0.00 |
| GCM_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 248.8 ms | 1.77 ms | 1.65 ms | 247.1 ms | 252.4 ms | 248.3 ms |  1.00 |    0.01 |
|         |                      |                      |         |          |         |         |          |          |          |       |         |
| GCM_Dec |             .NET 6.0 |             .NET 6.0 |     128 | 252.2 ms | 1.17 ms | 1.03 ms | 250.2 ms | 254.3 ms | 252.2 ms |  1.00 |    0.00 |
| GCM_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     128 | 254.0 ms | 1.75 ms | 1.37 ms | 251.8 ms | 256.4 ms | 253.9 ms |  1.01 |    0.01 |
| GCM_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     128 | 252.3 ms | 1.20 ms | 1.07 ms | 250.4 ms | 254.4 ms | 252.3 ms |  1.00 |    0.01 |
|         |                      |                      |         |          |         |         |          |          |          |       |         |
| **GCM_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **192** | **251.3 ms** | **1.61 ms** | **1.42 ms** | **249.1 ms** | **254.4 ms** | **251.1 ms** |  **1.00** |    **0.00** |
| GCM_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 258.5 ms | 1.73 ms | 1.44 ms | 256.4 ms | 261.8 ms | 258.5 ms |  1.03 |    0.01 |
| GCM_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 255.2 ms | 2.82 ms | 2.64 ms | 250.9 ms | 259.5 ms | 255.4 ms |  1.01 |    0.01 |
|         |                      |                      |         |          |         |         |          |          |          |       |         |
| GCM_Dec |             .NET 6.0 |             .NET 6.0 |     192 | 257.8 ms | 2.24 ms | 2.10 ms | 255.2 ms | 261.6 ms | 257.5 ms |  1.00 |    0.00 |
| GCM_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     192 | 258.2 ms | 2.63 ms | 2.46 ms | 254.8 ms | 262.5 ms | 257.6 ms |  1.00 |    0.01 |
| GCM_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     192 | 255.8 ms | 2.32 ms | 2.06 ms | 253.9 ms | 260.5 ms | 254.8 ms |  0.99 |    0.01 |
|         |                      |                      |         |          |         |         |          |          |          |       |         |
| **GCM_Enc** |             **.NET 6.0** |             **.NET 6.0** |     **256** | **258.6 ms** | **3.98 ms** | **3.52 ms** | **255.1 ms** | **266.3 ms** | **257.1 ms** |  **1.00** |    **0.00** |
| GCM_Enc | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 253.3 ms | 1.55 ms | 1.38 ms | 251.9 ms | 256.2 ms | 252.6 ms |  0.98 |    0.02 |
| GCM_Enc |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 254.8 ms | 1.41 ms | 1.18 ms | 253.2 ms | 257.4 ms | 254.4 ms |  0.99 |    0.01 |
|         |                      |                      |         |          |         |         |          |          |          |       |         |
| GCM_Dec |             .NET 6.0 |             .NET 6.0 |     256 | 258.4 ms | 1.51 ms | 1.34 ms | 256.1 ms | 261.0 ms | 258.4 ms |  1.00 |    0.00 |
| GCM_Dec | .NET Framework 4.6.2 | .NET Framework 4.6.2 |     256 | 261.6 ms | 2.93 ms | 2.60 ms | 256.9 ms | 266.4 ms | 261.4 ms |  1.01 |    0.01 |
| GCM_Dec |        NativeAOT 6.0 |        NativeAOT 6.0 |     256 | 257.4 ms | 1.75 ms | 1.64 ms | 255.1 ms | 260.4 ms | 257.0 ms |  1.00 |    0.01 |
