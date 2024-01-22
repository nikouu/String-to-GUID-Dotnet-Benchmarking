// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using StringToGuidBenchmarking;

//var j = new Benchmarks.BenchmarkString("ABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDAB");
//var g = new Benchmarks();

//var result1 = g.Immo_Speed_Optimized(j);
//var result2 = g.Immo_Original(j);
//var result3 = g.Basic_MD5(j);
//var result4 = g.Basic_SHA1(j);
//var result5 = g.Immo_UTF16_SHA1(j);
//var result6 = g.Immo_UTF16_MD5(j);
//var result7 = g.Immo_UTF8_SHA1(j);
//var result8 = g.Immo_Memory_Optimized(j);

var summary = BenchmarkRunner.Run<Benchmarks>();