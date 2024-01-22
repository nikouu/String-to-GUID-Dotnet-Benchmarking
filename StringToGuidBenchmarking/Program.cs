// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using StringToGuidBenchmarking;

var j = new Benchmarks.BenchmarkString("ABCDABCDABABCDABCDABABCDABCDABABCDABCDABABCDABCDAB");
var g = new Benchmarks();

//var result1 = g.Immo_SHA1_MemoryMarshal2(j);
//var result2 = g.Immo_SHA1_MemoryMarshal(new Benchmarks.BenchmarkString("ASDF"));

var summary = BenchmarkRunner.Run<Benchmarks>();