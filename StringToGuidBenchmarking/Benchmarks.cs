using BenchmarkDotNet.Attributes;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace StringToGuidBenchmarking
{
    [MemoryDiagnoser]
    public class Benchmarks
    {
        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public Guid Basic_MD5(BenchmarkString input)
        {
            var hash = MD5.HashData(Encoding.UTF8.GetBytes(input.String));
            var result = new Guid(hash);
            return result;
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public Guid Basic_SHA1(BenchmarkString input)
        {
            var bytes = Encoding.UTF8.GetBytes(input.String);
            var hash = SHA1.HashData(bytes);
            var result = new Guid(hash[..16]); //range operator
            return result;
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public Guid Immo_Original(BenchmarkString input)
        {
            const int maxBytesOnStack = 256;

            var encoding = Encoding.UTF8;
            var maxByteCount = encoding.GetMaxByteCount(input.String.Length);

            if (maxByteCount <= maxBytesOnStack)
            {
                var buffer = (Span<byte>)stackalloc byte[maxBytesOnStack];
                var written = encoding.GetBytes(input.String, buffer);
                var utf8Bytes = buffer[..written];
                return HashData(utf8Bytes);
            }
            else
            {
                var utf8Bytes = encoding.GetBytes(input.String);
                return HashData(utf8Bytes);
            }

            Guid HashData(ReadOnlySpan<byte> bytes)
            {
                var hashBytes = (Span<byte>)stackalloc byte[16];
                var written = MD5.HashData(bytes, hashBytes);

                return new Guid(hashBytes);
            }
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public Guid Immo_UTF8_SHA1(BenchmarkString input)
        {
            const int maxBytesOnStack = 256;

            var encoding = Encoding.UTF8;
            var maxByteCount = encoding.GetMaxByteCount(input.String.Length);

            if (maxByteCount <= maxBytesOnStack)
            {
                var buffer = (Span<byte>)stackalloc byte[maxBytesOnStack];
                var written = encoding.GetBytes(input.String, buffer);
                var utf8Bytes = buffer[..written];
                return HashData(utf8Bytes);
            }
            else
            {
                var utf8Bytes = encoding.GetBytes(input.String);
                return HashData(utf8Bytes);
            }

            Guid HashData(ReadOnlySpan<byte> bytes)
            {
                var hashBytes = (Span<byte>)stackalloc byte[20];
                var written = SHA1.HashData(bytes, hashBytes);

                return new Guid(hashBytes[..16]);
            }
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public Guid Immo_UTF16_MD5(BenchmarkString input)
        {
            const int maxBytesOnStack = 256;

            var encoding = Encoding.Unicode;
            var maxByteCount = encoding.GetMaxByteCount(input.String.Length);

            if (maxByteCount <= maxBytesOnStack)
            {
                var buffer = (Span<byte>)stackalloc byte[maxBytesOnStack];
                var written = encoding.GetBytes(input.String, buffer);
                var utf16Bytes = buffer[..written];
                return HashData(utf16Bytes);
            }
            else
            {
                var utf16Bytes = encoding.GetBytes(input.String);
                return HashData(utf16Bytes);
            }

            Guid HashData(ReadOnlySpan<byte> bytes)
            {
                var hashBytes = (Span<byte>)stackalloc byte[16];
                var written = MD5.HashData(bytes, hashBytes);

                return new Guid(hashBytes);
            }
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public Guid Immo_UTF16_SHA1(BenchmarkString input)
        {
            const int maxBytesOnStack = 256;

            var encoding = Encoding.Unicode;
            var maxByteCount = encoding.GetMaxByteCount(input.String.Length);

            if (maxByteCount <= maxBytesOnStack)
            {
                var buffer = (Span<byte>)stackalloc byte[maxBytesOnStack];
                var written = encoding.GetBytes(input.String, buffer);
                var utf16Bytes = buffer[..written];
                return HashData(utf16Bytes);
            }
            else
            {
                var utf16Bytes = encoding.GetBytes(input.String);
                return HashData(utf16Bytes);
            }

            Guid HashData(ReadOnlySpan<byte> bytes)
            {
                var hashBytes = (Span<byte>)stackalloc byte[20];
                var written = SHA1.HashData(bytes, hashBytes);

                return new Guid(hashBytes[..16]);
            }
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public Guid Immo_Memory_Optimized(BenchmarkString input)
        {
            var bytes = MemoryMarshal.AsBytes(input.String.AsSpan());
            var hashBytes = (Span<byte>)stackalloc byte[20];
            var written = SHA1.HashData(bytes, hashBytes);

            return new Guid(hashBytes[..16]);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public Guid Immo_Speed_Optimized(BenchmarkString input)
        {
            const int maxByteHeuristic = 150;

            var encoding = Encoding.UTF8;
            var maxByteCount = encoding.GetMaxByteCount(input.String.Length);

            ReadOnlySpan<byte> bytes = maxByteCount <= maxByteHeuristic
                ? MemoryMarshal.AsBytes(input.String.AsSpan())
                : encoding.GetBytes(input.String).AsSpan();

            var hashBytes = (Span<byte>)stackalloc byte[20];
            var written = SHA1.HashData(bytes, hashBytes);

            return new Guid(hashBytes[..16]);
        }

        public IEnumerable<object> Data()
        {
            // 0
            yield return new BenchmarkString("");
            // 4
            yield return new BenchmarkString("ABCD");
            // 10
            yield return new BenchmarkString("ABCDABCDAB");
            // 20
            yield return new BenchmarkString("ABCDABCDABABCDABCDAB");
            // 50
            yield return new BenchmarkString("ABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDAB");
            // 100
            yield return new BenchmarkString("ABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDAB");
            // 500
            yield return new BenchmarkString("ABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDAB");
            // 1000
            yield return new BenchmarkString("ABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDAB");
        }

        // Makes it easier to change the display names on the results
        public record struct BenchmarkString(string String)
        {
            public override string ToString() => $"{String.Length,5}";
        }
    }
}
