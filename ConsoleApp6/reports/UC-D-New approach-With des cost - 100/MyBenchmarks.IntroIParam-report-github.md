``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.192)
Processor=Intel Core i7-6650U CPU 2.20GHz (Skylake), ProcessorCount=4
Frequency=2156248 Hz, Resolution=463.7685 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT


```
|           Method |                                                                                                 Field |     Mean |     Error |    StdDev |   Median |
|----------------- |------------------------------------------------------------------------------------------------------ |---------:|----------:|----------:|---------:|
|        ResizeNew | (ZombiesAreScattered startCapacity:100, originalSize:239, curCapacity:239, newSize:239, ResizeTo:521) | 3.185 ms | 0.1963 ms | 0.5788 ms | 3.136 ms |
|        ResizeOld | (ZombiesAreScattered startCapacity:100, originalSize:239, curCapacity:239, newSize:239, ResizeTo:521) | 3.350 ms | 0.1830 ms | 0.5396 ms | 3.287 ms |
| ResizeNewOnlyDes | (ZombiesAreScattered startCapacity:100, originalSize:239, curCapacity:239, newSize:239, ResizeTo:521) | 3.533 ms | 0.1713 ms | 0.5023 ms | 3.617 ms |
| ResizeOldOnlyDes | (ZombiesAreScattered startCapacity:100, originalSize:239, curCapacity:239, newSize:239, ResizeTo:521) | 2.998 ms | 0.2417 ms | 0.7126 ms | 2.711 ms |
