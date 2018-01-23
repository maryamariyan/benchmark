``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 1 [1607, Anniversary Update] (10.0.14393.1198)
Processor=Intel Core i7-3517U CPU 1.90GHz (Ivy Bridge), ProcessorCount=4
Frequency=2338438 Hz, Resolution=427.6359 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT


```
|           Method |                                                                                                               Field |    Mean |    Error |   StdDev |
|----------------- |-------------------------------------------------------------------------------------------------------------------- |--------:|---------:|---------:|
|        ResizeNew | (ZombiesAreScattered startCapacity:60000, originalSize:130363, curCapacity:130363, newSize:130363, ResizeTo:270371) | 2.081 s | 0.0283 s | 0.0265 s |
|        ResizeOld | (ZombiesAreScattered startCapacity:60000, originalSize:130363, curCapacity:130363, newSize:130363, ResizeTo:270371) | 2.096 s | 0.0292 s | 0.0273 s |
| ResizeNewOnlyDes | (ZombiesAreScattered startCapacity:60000, originalSize:130363, curCapacity:130363, newSize:130363, ResizeTo:270371) | 2.067 s | 0.0295 s | 0.0276 s |
| ResizeOldOnlyDes | (ZombiesAreScattered startCapacity:60000, originalSize:130363, curCapacity:130363, newSize:130363, ResizeTo:270371) | 2.072 s | 0.0317 s | 0.0297 s |
