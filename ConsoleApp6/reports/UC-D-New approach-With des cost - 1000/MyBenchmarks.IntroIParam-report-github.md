``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 1 [1607, Anniversary Update] (10.0.14393.1198)
Processor=Intel Core i7-3517U CPU 1.90GHz (Ivy Bridge), ProcessorCount=4
Frequency=2338438 Hz, Resolution=427.6359 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT


```
|           Method |                                                                                                      Field |     Mean |     Error |    StdDev |
|----------------- |----------------------------------------------------------------------------------------------------------- |---------:|----------:|----------:|
|        ResizeNew | (ZombiesAreScattered startCapacity:1000, originalSize:2333, curCapacity:2333, newSize:2333, ResizeTo:4861) | 33.79 ms | 0.3228 ms | 0.2520 ms |
|        ResizeOld | (ZombiesAreScattered startCapacity:1000, originalSize:2333, curCapacity:2333, newSize:2333, ResizeTo:4861) | 33.88 ms | 0.1122 ms | 0.0995 ms |
| ResizeNewOnlyDes | (ZombiesAreScattered startCapacity:1000, originalSize:2333, curCapacity:2333, newSize:2333, ResizeTo:4861) | 33.64 ms | 0.0692 ms | 0.0613 ms |
| ResizeOldOnlyDes | (ZombiesAreScattered startCapacity:1000, originalSize:2333, curCapacity:2333, newSize:2333, ResizeTo:4861) | 34.19 ms | 0.1640 ms | 0.1454 ms |
