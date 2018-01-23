``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 1 [1607, Anniversary Update] (10.0.14393.1198)
Processor=Intel Core i7-3517U CPU 1.90GHz (Ivy Bridge), ProcessorCount=4
Frequency=2338438 Hz, Resolution=427.6359 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT


```
|    Method |                                                                                                 Field |     Mean |     Error |    StdDev |
|---------- |------------------------------------------------------------------------------------------------------ |---------:|----------:|----------:|
| ResizeNew | (ZombiesAreScattered startCapacity:100, originalSize:239, curCapacity:239, newSize:239, ResizeTo:521) | 2.208 us | 0.0077 us | 0.0060 us |
| ResizeOld | (ZombiesAreScattered startCapacity:100, originalSize:239, curCapacity:239, newSize:239, ResizeTo:521) | 2.121 us | 0.0191 us | 0.0179 us |
