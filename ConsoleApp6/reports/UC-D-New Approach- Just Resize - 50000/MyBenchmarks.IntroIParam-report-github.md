``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 1 [1607, Anniversary Update] (10.0.14393.1198)
Processor=Intel Core i7-3517U CPU 1.90GHz (Ivy Bridge), ProcessorCount=4
Frequency=2338438 Hz, Resolution=427.6359 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT


```
|    Method |                                                                                                               Field |     Mean |     Error |    StdDev |
|---------- |-------------------------------------------------------------------------------------------------------------------- |---------:|----------:|----------:|
| ResizeNew | (ZombiesAreScattered startCapacity:50000, originalSize:108631, curCapacity:108631, newSize:108631, ResizeTo:225307) | 2.477 ms | 0.0154 ms | 0.0137 ms |
| ResizeOld | (ZombiesAreScattered startCapacity:50000, originalSize:108631, curCapacity:108631, newSize:108631, ResizeTo:225307) | 2.395 ms | 0.0071 ms | 0.0059 ms |
