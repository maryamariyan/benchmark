``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 1 [1607, Anniversary Update] (10.0.14393.1198)
Processor=Intel Core i7-3517U CPU 1.90GHz (Ivy Bridge), ProcessorCount=4
Frequency=2338438 Hz, Resolution=427.6359 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT


```
|    Method |                                                                                                           Field |     Mean |    Error |   StdDev |
|---------- |---------------------------------------------------------------------------------------------------------------- |---------:|---------:|---------:|
| ResizeNew | (ZombiesAreScattered startCapacity:10000, originalSize:21023, curCapacity:21023, newSize:21023, ResizeTo:43627) | 410.8 us | 7.997 us | 7.854 us |
| ResizeOld | (ZombiesAreScattered startCapacity:10000, originalSize:21023, curCapacity:21023, newSize:21023, ResizeTo:43627) | 398.8 us | 3.078 us | 2.729 us |
