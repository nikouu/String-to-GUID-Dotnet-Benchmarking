// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using StringToGuidBenchmarking;

var j = new Benchmarks.BenchmarkString("ABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDAB");
var g = new Benchmarks();



var result1 = g.Immo_Speed_Optimized(j);
var result2 = g.Immo_Original(j);

var summary = BenchmarkRunner.Run<Benchmarks>();