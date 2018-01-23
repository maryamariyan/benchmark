``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 1 [1607, Anniversary Update] (10.0.14393.1198)
Processor=Intel Core i7-3517U CPU 1.90GHz (Ivy Bridge), ProcessorCount=4
Frequency=2338438 Hz, Resolution=427.6359 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT


```
|           Method |                                                                                                           Field |     Mean |     Error |    StdDev |
|----------------- |---------------------------------------------------------------------------------------------------------------- |---------:|----------:|----------:|
|        ResizeNew | (ZombiesAreScattered startCapacity:10000, originalSize:21023, curCapacity:21023, newSize:21023, ResizeTo:43627) | 326.7 ms | 1.6001 ms | 1.4185 ms |
|        ResizeOld | (ZombiesAreScattered startCapacity:10000, originalSize:21023, curCapacity:21023, newSize:21023, ResizeTo:43627) | 325.7 ms | 1.3876 ms | 1.2301 ms |
| ResizeNewOnlyDes | (ZombiesAreScattered startCapacity:10000, originalSize:21023, curCapacity:21023, newSize:21023, ResizeTo:43627) | 325.9 ms | 0.4437 ms | 0.4150 ms |
| ResizeOldOnlyDes | (ZombiesAreScattered startCapacity:10000, originalSize:21023, curCapacity:21023, newSize:21023, ResizeTo:43627) | 327.6 ms | 1.8284 ms | 1.7103 ms |
