# String to GUID Dotnet Benchmarking

The goal is to find the most performant way to create a GUID from any string.

[Immo Landwerth](https://github.com/terrajobst) (_terrajobst_) [tweeted](https://twitter.com/terrajobst/status/1507808952146223106) about how he was using `Span<T>` and `stackalloc` in a neat way to make the conversion of a `string` to a `GUID` allocation free for many cases (his words verbatum).

This is a fun experiment to better understand allocationless code. The code is bite-sized enough to quickly experiment and simple enough to benchmark easily. 


Inspired by:
- 
- [GitHub repo it's used in](https://github.com/terrajobst/apisof.net/blob/31398940e1729982a7f5e56e0656beb55045c249/src/Terrajobst.UsageCrawling/ApiKey.cs#L50)

## How to run

1. Get the repo
1. In the same project as the solution run:
```
dotnet run --configuration Release --framework net8.0 --runtimes net8.0
```

## Test scenarios

### Data

The tests are based on passing in a strings of different lengths. In total there are eight test scenarios:
1. 0 length string
1. 4 length string
1. 10 length string
1. 20 length string
1. 50 length string
1. 100 length string
1. 500 length string
1. 1000 length string

These values are chosen to try best balance a representation of potentially "more common" length strings and larger strings for metrics.  

### Code

There are eight methods being benchmarked. 

| Method                | Description                                                                                                                                                                                           |
| --------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Basic_MD5             | Using a default way to get a GUID from an MD5 hash                                                                                                                                                    |
| Basic_SHA1            | Using a default way to get a GUID from an SHA1 hash                                                                                                                                                   |
| Immo_Original         | Our baseline - the code taken from the [repo](https://github.com/terrajobst/apisof.net/blob/31398940e1729982a7f5e56e0656beb55045c249/src/Terrajobst.UsageCrawling/ApiKey.cs#L50) mentioned beforehand |
| Immo_UTF8_SHA1        | Immo_Original but with MD5 replaced with SHA1. Note the original is UTF8, but the naming here is used to compare the rest of the implementations                                                      |
| Immo_UTF16_MD5        | Immo_Original but with UTF16 replacing UTF8                                                                                                                                                           |
| Immo_UTF16_SHA1       | Immo_Original but with UTF16 replacing UTF8 and SHA1 replacing MD5                                                                                                                                    |
| Immo_Memory_Optimized | My attempt at creating an allocationless version                                                                                                                                                      |
| Immo_Speed_Optimized  | My attempt at creating the most performant version based on lessons learned from all the other versions                                                                                               |

With the baseline looking like:
```csharp
public Guid Immo_Original(string input)
{
    const int maxBytesOnStack = 256;

    var encoding = Encoding.UTF8;
    var maxByteCount = encoding.GetMaxByteCount(input.Length);

    if (maxByteCount <= maxBytesOnStack)
    {
        var buffer = (Span<byte>)stackalloc byte[maxBytesOnStack];
        var written = encoding.GetBytes(input, buffer);
        var utf8Bytes = buffer[..written];
        return HashData(utf8Bytes);
    }
    else
    {
        var utf8Bytes = encoding.GetBytes(input);
        return HashData(utf8Bytes);
    }

    Guid HashData(ReadOnlySpan<byte> bytes)
    {
        var hashBytes = (Span<byte>)stackalloc byte[16];
        var written = MD5.HashData(bytes, hashBytes);

        return new Guid(hashBytes);
    }
}
```

Note: I'm extremely sure Immo would be able to create a more performant version than he used, however he was constrained by the framework at the time he created the function.

The test string is encapsulated in the `BenchmarkString` struct just for BenchmarkDotNet [presentation purposes](https://benchmarkdotnet.org/articles/features/parameterization.html#another-example).

## Results

The much larger full results can be found in [Data.md](Data.md). 

The following are trimmed down highlights:

| Method                | input |       Mean | Ratio | Allocated | Alloc Ratio |
| --------------------- | ----: | ---------: | ----: | --------: | ----------: |
| Basic_MD5             |    10 |   319.6 ns |  1.06 |      80 B |          NA |
| Basic_SHA1            |    10 |   333.2 ns |  1.11 |     128 B |          NA |
| Immo_Original         |    10 |   300.8 ns |  1.00 |         - |          NA |
| Immo_Memory_Optimized |    10 |   293.7 ns |  0.98 |         - |          NA |
| Immo_Speed_Optimized  |    10 |   290.8 ns |  0.96 |         - |          NA |
|                       |       |            |       |           |             |
| Basic_MD5             |    50 |   342.4 ns |  1.15 |     120 B |          NA |
| Basic_SHA1            |    50 |   349.6 ns |  1.17 |     168 B |          NA |
| Immo_Original         |    50 |   298.1 ns |  1.00 |         - |          NA |
| Immo_Memory_Optimized |    50 |   399.8 ns |  1.33 |         - |          NA |
| Immo_Speed_Optimized  |    50 |   306.7 ns |  1.03 |         - |          NA |
|                       |       |            |       |           |             |
| Basic_MD5             |   100 |   445.6 ns |  1.02 |     168 B |        1.31 |
| Basic_SHA1            |   100 |   467.9 ns |  1.07 |     216 B |        1.69 |
| Immo_Original         |   100 |   436.5 ns |  1.00 |     128 B |        1.00 |
| Immo_Memory_Optimized |   100 |   593.6 ns |  1.36 |         - |        0.00 |
| Immo_Speed_Optimized  |   100 |   449.5 ns |  1.03 |     128 B |        1.00 |
|                       |       |            |       |           |             |
| Basic_MD5             |  1000 | 1,942.0 ns |  1.00 |    1064 B |        1.04 |
| Basic_SHA1            |  1000 | 1,936.2 ns |  1.06 |    1112 B |        1.09 |
| Immo_Original         |  1000 | 1,936.7 ns |  1.00 |    1024 B |        1.00 |
| Immo_Memory_Optimized |  1000 | 3,142.3 ns |  1.62 |         - |        0.00 |
| Immo_Speed_Optimized  |  1000 | 1,866.3 ns |  0.96 |    1024 B |        1.00 |

These have been selected as they best represent:
1. What a regular user might do with the `Basic_*` functions
1. The `Immo_Original` benchmark
1. The two optimised functions

## Analysis

### `Immo_Original`

The `Immo_Original` function is an outstanding performer and even beats `Immo_Speed_Optimized` in some cases. This is due to MD5 being quicker for smaller/medium string sizes (sizes in respect to the rest of the test scenarios). `Immo_Original` wins in speed in the following tests (ignoring tiny difference):

| Length | Margin |
| -----: | -----: |
|     50 |     3% |
|    100 |   2-3% |


### `Immo_Speed_Optimized`

The overall winner for the benchmarks. Winning in speed in the following cases vs `Immo_Original`:

| Length | Margin |
| -----: | -----: |
|      0 |     5% |
|      4 |     4% |
|     10 |     4% |
|     20 |     2% |
|    500 |     2% |
|   1000 |     4% |

While being similar to `Immo_Original` the performance gains are via a heuristic for the smaller set of strings. After extensive testing, I settled on strings with a max byte count of 150 or less which uses following:

```csharp
ReadOnlySpan<byte> bytes = MemoryMarshal.AsBytes(input.AsSpan());
```

Otherwise, if we look at the whole function, it looks very similar to the original:

```csharp
public Guid Immo_Speed_Optimized(BenchmarkString input)
{
    const int maxByteHeuristic = 150;
    const int maxBytesOnStack = 256;

    var encoding = Encoding.UTF8;
    var maxByteCount = encoding.GetMaxByteCount(input.String.Length);

    if (maxByteCount <= maxByteHeuristic)
    {
        ReadOnlySpan<byte> bytes = MemoryMarshal.AsBytes(input.String.AsSpan());
        return HashData(bytes);
    }
    else if (maxByteCount <= maxBytesOnStack)
    {
        var buffer = (Span<byte>)stackalloc byte[maxBytesOnStack];
        var written = encoding.GetBytes(input.String, buffer);
        var utf8Bytes = buffer[..written];
        return HashData(utf8Bytes);
    }
    else
    {
        var bytes = encoding.GetBytes(input.String);
        return HashData(bytes);
    }

    Guid HashData(ReadOnlySpan<byte> bytes)
    {
        var hashBytes = (Span<byte>)stackalloc byte[20];
        var written = SHA1.HashData(bytes, hashBytes);

        return new Guid(hashBytes[..16]);
    }
}
```

That's really it in terms of performance increases.

The code does use SHA1 over MD5 which changes the `HashData()` function slightly as SHA1 returns a 20 byte array, as opposed to the 16 bytes from MD5 which fits perfectly into a GUID:

```csharp
Guid HashData(ReadOnlySpan<byte> bytes)
{
    var hashBytes = (Span<byte>)stackalloc byte[20];
    var written = SHA1.HashData(bytes, hashBytes);

    return new Guid(hashBytes[..16]);
}
```

From research, apparently there will be fewer collisions with SHA1, however if you are really looking for the best performance weigh up whether you want to bring in MD5 in `Immo_Speed_Optimized`. Remember we are just using it for a GUID, not security. But ultimately you could use other SHA algorithms because unless you are interested in micro-optimising, it won't really matter. It could also be the extra overhead of using the range operator to cut 20 bytes to 16.

### `Immo_Memory_Optimized`

While `Immo_Memory_Optimized` can be slower, it does not allocate. Does what it says 🤷‍♀️

Looks cool though with how compact it is though:

```csharp
public Guid Immo_Memory_Optimized(BenchmarkString input)
{
    var bytes = MemoryMarshal.AsBytes(input.String.AsSpan());
    var hashBytes = (Span<byte>)stackalloc byte[20];
    var written = SHA1.HashData(bytes, hashBytes);

    return new Guid(hashBytes[..16]);
}
```

### Other notes

- UTF16 tended to slow down the process
- While the two `Basic_*` functions always allocated, they were often **pretty decent performers!** Meaning they're perfectly usable if you don't care about micro-optimisation. Perhaps even perferrable as they're easy to understand among developers of most skill levels.

## References
- [GitHub repo for apisof.net](https://github.com/terrajobst/apisof.net/blob/31398940e1729982a7f5e56e0656beb55045c249/src/Terrajobst.UsageCrawling/ApiKey.cs#L50)
- [All About Span: Exploring a New .NET Mainstay](https://learn.microsoft.com/en-us/archive/msdn-magazine/2018/january/csharp-all-about-span-exploring-a-new-net-mainstay)
- [Enumerable.Range(Int32, Int32) Method](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.range?view=net-8.0)
- [Range Operator](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#range-operator-)
- [MemoryMarshal.AsBytes Method](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.memorymarshal.asbytes?view=net-8.0)

## Notes
- A big thank you to Immo for posting the code snippet on Twitter
- A thanks to replies in the [Twitter thread](https://twitter.com/terrajobst/status/1507808952146223106) to give ideas on what to test and try.
	- Including an [`ArrayPool` benchmark](https://github.com/Treit/MiscBenchmarks/tree/main/ArrayPoolVsStackAlloc) by [Mike Treit](https://twitter.com/MikeTreit) which for this case didn't work out.
- I bet there's even more ways to get more performance and I'd love to return to it in the future.