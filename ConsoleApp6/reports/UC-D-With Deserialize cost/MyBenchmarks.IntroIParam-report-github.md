``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.192)
Processor=Intel Core i7-6650U CPU 2.20GHz (Skylake), ProcessorCount=4
Frequency=2156248 Hz, Resolution=463.7685 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT


```
|           Method |                                                                                                               Field |     Mean |     Error |    StdDev |
|----------------- |-------------------------------------------------------------------------------------------------------------------- |---------:|----------:|----------:|
|        ResizeNew | (ZombiesAreScattered startCapacity:50000, originalSize:108631, curCapacity:108631, newSize:108631, ResizeTo:225307) | 768.8 ms | 12.530 ms |  9.783 ms |
|        ResizeOld | (ZombiesAreScattered startCapacity:50000, originalSize:108631, curCapacity:108631, newSize:108631, ResizeTo:225307) | 756.7 ms |  5.167 ms |  4.833 ms |
| ResizeNewOnlyDes | (ZombiesAreScattered startCapacity:50000, originalSize:108631, curCapacity:108631, newSize:108631, ResizeTo:225307) | 765.7 ms | 13.871 ms | 10.829 ms |
| ResizeOldOnlyDes | (ZombiesAreScattered startCapacity:50000, originalSize:108631, curCapacity:108631, newSize:108631, ResizeTo:225307) | 764.1 ms | 11.236 ms |  9.960 ms |
