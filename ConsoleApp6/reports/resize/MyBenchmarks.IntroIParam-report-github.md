``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.192)
Processor=Intel Core i7-6650U CPU 2.20GHz (Skylake), ProcessorCount=4
Frequency=2156248 Hz, Resolution=463.7685 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT


```
|    Method |                                                                                                                                Field |     Mean |    Error |   StdDev |
|---------- |------------------------------------------------------------------------------------------------------------------------------------- |---------:|---------:|---------:|
| ResizeNew | (DictionaryAllEntriesRemovedAddAgain startCapacity:108631, originalSize:108631, curCapacity:108631, newSize:100000, ResizeTo:108631) | 857.8 ms | 19.71 ms | 22.70 ms |
| ResizeOld | (DictionaryAllEntriesRemovedAddAgain startCapacity:108631, originalSize:108631, curCapacity:108631, newSize:100000, ResizeTo:108631) | 844.1 ms | 16.55 ms | 22.66 ms |
