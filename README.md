# String to GUID Dotnet Benchmarking

The goal is to find the most performant way to create a GUID from any string.

A fun experiment to better understand allocationless code. The code is bite-sized enough to quickly experiment and simple enough to benchmark easily. 

Inspired by:
- [Via Immo Landwerth (terrajobst)](https://twitter.com/terrajobst/status/1507808952146223106)
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
| Immo_Original         | Our baseline - the code taken from the [repo](https://github.com/terrajobst/apisof.net/blob/31398940e1729982a7f5e56e0656beb55045c249/src/Terrajobst.UsageCrawling/ApiKey.cs#L11) mentioned beforehand |
| Immo_UTF8_SHA1        | Immo_Original but with MD5 replaced with SHA1. Note the original is UTF8, but the naming here is used to compare the rest of the implementations                                                      |
| Immo_UTF16_MD5        | Immo_Original but with UTF16 replacing UTF8                                                                                                                                                           |
| Immo_UTF16_SHA1       | Immo_Original but with UTF16 replacing UTF8 and SHA1 replacing MD5                                                                                                                                    |
| Immo_Memory_Optimized | My attempt at creating an allocationless version                                                                                                                                                      |
| Immo_Speed_Optimized  | My attempt at creating the most performant version based on lessons learned from all the other versions                                                                                               |

Note: I'm extremely sure Immo would be able to create a more performant version than he used, however he was constrained by the framework at the time he created the function.

## Results

Full results can be found in [Data.md](Data.md). 



## Analysis

## References

## Notes
