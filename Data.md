```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.3930/22H2/2022Update)
Intel Core i7-7700K CPU 4.20GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.200-preview.23624.5
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


```
| Method                | input |       Mean |    Error |   StdDev |     Median | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
| --------------------- | ----: | ---------: | -------: | -------: | ---------: | ----: | ------: | -----: | --------: | ----------: |
| Basic_MD5             |     0 |   303.5 ns |  6.07 ns |  9.62 ns |   302.8 ns |  1.03 |    0.05 | 0.0095 |      40 B |          NA |
| Basic_SHA1            |     0 |   315.3 ns |  6.24 ns |  9.71 ns |   313.9 ns |  1.07 |    0.04 | 0.0210 |      88 B |          NA |
| Immo_Original         |     0 |   292.8 ns |  5.83 ns |  7.58 ns |   292.0 ns |  1.00 |    0.00 |      - |         - |          NA |
| Immo_UTF8_SHA1        |     0 |   292.9 ns |  5.77 ns |  7.29 ns |   292.1 ns |  1.00 |    0.04 |      - |         - |          NA |
| Immo_UTF16_MD5        |     0 |   288.1 ns |  5.63 ns |  7.32 ns |   284.2 ns |  0.98 |    0.04 |      - |         - |          NA |
| Immo_UTF16_SHA1       |     0 |   289.2 ns |  5.72 ns | 10.16 ns |   285.3 ns |  0.99 |    0.05 |      - |         - |          NA |
| Immo_Memory_Optimized |     0 |   284.2 ns |  5.50 ns |  6.76 ns |   280.9 ns |  0.97 |    0.04 |      - |         - |          NA |
| Immo_Speed_Optimized  |     0 |   279.7 ns |  5.44 ns |  5.82 ns |   277.1 ns |  0.95 |    0.03 |      - |         - |          NA |
|                       |       |            |          |          |            |       |         |        |           |             |
| Basic_MD5             |     4 |   313.8 ns |  6.18 ns | 11.90 ns |   311.8 ns |  1.03 |    0.05 | 0.0172 |      72 B |          NA |
| Basic_SHA1            |     4 |   329.7 ns |  5.20 ns |  4.61 ns |   328.8 ns |  1.07 |    0.04 | 0.0286 |     120 B |          NA |
| Immo_Original         |     4 |   304.9 ns |  5.65 ns |  9.59 ns |   304.0 ns |  1.00 |    0.00 |      - |         - |          NA |
| Immo_UTF8_SHA1        |     4 |   310.6 ns |  6.07 ns |  8.10 ns |   313.0 ns |  1.01 |    0.04 |      - |         - |          NA |
| Immo_UTF16_MD5        |     4 |   309.5 ns |  5.84 ns |  5.18 ns |   308.1 ns |  1.00 |    0.04 |      - |         - |          NA |
| Immo_UTF16_SHA1       |     4 |   309.6 ns |  6.21 ns |  7.15 ns |   310.5 ns |  1.01 |    0.05 |      - |         - |          NA |
| Immo_Memory_Optimized |     4 |   295.5 ns |  5.94 ns |  8.52 ns |   296.7 ns |  0.96 |    0.04 |      - |         - |          NA |
| Immo_Speed_Optimized  |     4 |   295.9 ns |  5.57 ns |  5.48 ns |   295.0 ns |  0.96 |    0.04 |      - |         - |          NA |
|                       |       |            |          |          |            |       |         |        |           |             |
| Basic_MD5             |    10 |   319.6 ns |  6.26 ns | 10.97 ns |   320.1 ns |  1.06 |    0.05 | 0.0191 |      80 B |          NA |
| Basic_SHA1            |    10 |   333.2 ns |  6.51 ns |  8.91 ns |   330.3 ns |  1.11 |    0.05 | 0.0305 |     128 B |          NA |
| Immo_Original         |    10 |   300.8 ns |  5.94 ns |  9.76 ns |   298.9 ns |  1.00 |    0.00 |      - |         - |          NA |
| Immo_UTF8_SHA1        |    10 |   299.6 ns |  5.62 ns |  5.26 ns |   297.3 ns |  1.00 |    0.03 |      - |         - |          NA |
| Immo_UTF16_MD5        |    10 |   309.8 ns |  6.06 ns |  8.50 ns |   306.3 ns |  1.03 |    0.04 |      - |         - |          NA |
| Immo_UTF16_SHA1       |    10 |   307.8 ns |  5.51 ns |  9.05 ns |   303.9 ns |  1.02 |    0.05 |      - |         - |          NA |
| Immo_Memory_Optimized |    10 |   293.7 ns |  5.76 ns |  8.07 ns |   291.2 ns |  0.98 |    0.04 |      - |         - |          NA |
| Immo_Speed_Optimized  |    10 |   290.8 ns |  5.83 ns |  6.72 ns |   290.2 ns |  0.96 |    0.04 |      - |         - |          NA |
|                       |       |            |          |          |            |       |         |        |           |             |
| Basic_MD5             |    20 |   315.8 ns |  5.86 ns |  8.95 ns |   314.2 ns |  1.05 |    0.04 | 0.0210 |      88 B |          NA |
| Basic_SHA1            |    20 |   336.2 ns |  6.49 ns |  7.48 ns |   333.9 ns |  1.11 |    0.04 | 0.0324 |     136 B |          NA |
| Immo_Original         |    20 |   301.7 ns |  6.05 ns |  7.65 ns |   300.8 ns |  1.00 |    0.00 |      - |         - |          NA |
| Immo_UTF8_SHA1        |    20 |   305.7 ns |  6.11 ns |  7.51 ns |   303.4 ns |  1.01 |    0.03 |      - |         - |          NA |
| Immo_UTF16_MD5        |    20 |   314.3 ns |  5.12 ns |  5.03 ns |   312.1 ns |  1.04 |    0.04 |      - |         - |          NA |
| Immo_UTF16_SHA1       |    20 |   320.3 ns |  6.30 ns |  9.04 ns |   320.4 ns |  1.07 |    0.05 |      - |         - |          NA |
| Immo_Memory_Optimized |    20 |   299.9 ns |  6.01 ns |  7.38 ns |   300.8 ns |  0.99 |    0.03 |      - |         - |          NA |
| Immo_Speed_Optimized  |    20 |   294.4 ns |  5.87 ns |  8.04 ns |   291.5 ns |  0.98 |    0.04 |      - |         - |          NA |
|                       |       |            |          |          |            |       |         |        |           |             |
| Basic_MD5             |    50 |   342.4 ns |  6.83 ns | 10.22 ns |   341.1 ns |  1.15 |    0.04 | 0.0286 |     120 B |          NA |
| Basic_SHA1            |    50 |   349.6 ns |  7.00 ns | 10.04 ns |   349.9 ns |  1.17 |    0.05 | 0.0401 |     168 B |          NA |
| Immo_Original         |    50 |   298.1 ns |  5.75 ns |  8.25 ns |   295.4 ns |  1.00 |    0.00 |      - |         - |          NA |
| Immo_UTF8_SHA1        |    50 |   296.1 ns |  4.52 ns |  3.78 ns |   295.5 ns |  0.99 |    0.03 |      - |         - |          NA |
| Immo_UTF16_MD5        |    50 |   411.3 ns |  7.47 ns |  6.24 ns |   409.8 ns |  1.38 |    0.05 |      - |         - |          NA |
| Immo_UTF16_SHA1       |    50 |   424.9 ns |  8.41 ns | 11.51 ns |   421.1 ns |  1.43 |    0.05 |      - |         - |          NA |
| Immo_Memory_Optimized |    50 |   399.8 ns |  7.64 ns |  7.85 ns |   398.0 ns |  1.33 |    0.05 |      - |         - |          NA |
| Immo_Speed_Optimized  |    50 |   306.7 ns |  5.87 ns |  7.21 ns |   307.3 ns |  1.03 |    0.03 |      - |         - |          NA |
|                       |       |            |          |          |            |       |         |        |           |             |
| Basic_MD5             |   100 |   445.6 ns |  8.88 ns |  9.12 ns |   445.5 ns |  1.02 |    0.04 | 0.0401 |     168 B |        1.31 |
| Basic_SHA1            |   100 |   467.9 ns |  9.34 ns | 12.78 ns |   467.3 ns |  1.07 |    0.04 | 0.0515 |     216 B |        1.69 |
| Immo_Original         |   100 |   436.5 ns |  7.24 ns | 10.38 ns |   434.9 ns |  1.00 |    0.00 | 0.0305 |     128 B |        1.00 |
| Immo_UTF8_SHA1        |   100 |   449.3 ns |  8.63 ns |  8.07 ns |   448.4 ns |  1.03 |    0.03 | 0.0305 |     128 B |        1.00 |
| Immo_UTF16_MD5        |   100 |   622.6 ns | 12.18 ns | 13.53 ns |   614.0 ns |  1.42 |    0.04 |      - |         - |        0.00 |
| Immo_UTF16_SHA1       |   100 |   629.8 ns | 12.06 ns | 13.89 ns |   631.4 ns |  1.44 |    0.05 |      - |         - |        0.00 |
| Immo_Memory_Optimized |   100 |   593.6 ns | 11.57 ns | 13.33 ns |   592.8 ns |  1.36 |    0.05 |      - |         - |        0.00 |
| Immo_Speed_Optimized  |   100 |   449.5 ns |  8.97 ns |  8.39 ns |   452.9 ns |  1.03 |    0.02 | 0.0305 |     128 B |        1.00 |
|                       |       |            |          |          |            |       |         |        |           |             |
| Basic_MD5             |   500 | 1,114.3 ns | 19.58 ns | 20.95 ns | 1,109.8 ns |  1.02 |    0.03 | 0.1354 |     568 B |        1.08 |
| Basic_SHA1            |   500 | 1,094.8 ns | 21.53 ns | 27.23 ns | 1,085.8 ns |  1.00 |    0.04 | 0.1469 |     616 B |        1.17 |
| Immo_Original         |   500 | 1,093.4 ns | 20.54 ns | 19.21 ns | 1,087.9 ns |  1.00 |    0.00 | 0.1259 |     528 B |        1.00 |
| Immo_UTF8_SHA1        |   500 | 1,065.2 ns | 16.03 ns | 13.39 ns | 1,061.2 ns |  0.97 |    0.02 | 0.1259 |     528 B |        1.00 |
| Immo_UTF16_MD5        |   500 | 2,022.0 ns | 39.63 ns | 48.66 ns | 2,002.8 ns |  1.86 |    0.05 | 0.2441 |    1024 B |        1.94 |
| Immo_UTF16_SHA1       |   500 | 1,946.1 ns | 21.50 ns | 19.06 ns | 1,938.6 ns |  1.78 |    0.04 | 0.2441 |    1024 B |        1.94 |
| Immo_Memory_Optimized |   500 | 1,687.2 ns | 12.76 ns | 11.31 ns | 1,687.9 ns |  1.54 |    0.03 |      - |         - |        0.00 |
| Immo_Speed_Optimized  |   500 | 1,077.0 ns | 21.39 ns | 33.31 ns | 1,067.0 ns |  0.98 |    0.03 | 0.1259 |     528 B |        1.00 |
|                       |       |            |          |          |            |       |         |        |           |             |
| Basic_MD5             |  1000 | 1,942.0 ns | 18.56 ns | 16.46 ns | 1,939.2 ns |  1.00 |    0.02 | 0.2518 |    1064 B |        1.04 |
| Basic_SHA1            |  1000 | 1,936.2 ns | 38.57 ns | 92.41 ns | 1,890.5 ns |  1.06 |    0.03 | 0.2632 |    1112 B |        1.09 |
| Immo_Original         |  1000 | 1,936.7 ns | 32.00 ns | 29.93 ns | 1,924.1 ns |  1.00 |    0.00 | 0.2441 |    1024 B |        1.00 |
| Immo_UTF8_SHA1        |  1000 | 1,860.5 ns | 34.74 ns | 32.50 ns | 1,854.7 ns |  0.96 |    0.02 | 0.2441 |    1024 B |        1.00 |
| Immo_UTF16_MD5        |  1000 | 3,706.0 ns | 26.80 ns | 22.38 ns | 3,702.3 ns |  1.91 |    0.03 | 0.4807 |    2024 B |        1.98 |
| Immo_UTF16_SHA1       |  1000 | 3,596.7 ns | 68.23 ns | 70.07 ns | 3,577.0 ns |  1.86 |    0.05 | 0.4807 |    2024 B |        1.98 |
| Immo_Memory_Optimized |  1000 | 3,142.3 ns | 56.38 ns | 52.74 ns | 3,148.4 ns |  1.62 |    0.04 |      - |         - |        0.00 |
| Immo_Speed_Optimized  |  1000 | 1,866.3 ns | 29.82 ns | 27.90 ns | 1,872.0 ns |  0.96 |    0.01 | 0.2441 |    1024 B |        1.00 |

```
input       : Value of the 'input' parameter
Mean        : Arithmetic mean of all measurements
Error       : Half of 99.9% confidence interval
StdDev      : Standard deviation of all measurements
Median      : Value separating the higher half of all measurements (50th percentile)
Ratio       : Mean of the ratio distribution ([Current]/[Baseline])
RatioSD     : Standard deviation of the ratio distribution ([Current]/[Baseline])
Gen0        : GC Generation 0 collects per 1000 operations
Allocated   : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)
Alloc Ratio : Allocated memory ratio distribution ([Current]/[Baseline])
1 ns        : 1 Nanosecond (0.000000001 sec)
```