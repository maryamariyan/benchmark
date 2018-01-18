``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.192)
Processor=Intel Core i7-6650U CPU 2.20GHz (Skylake), ProcessorCount=4
Frequency=2156248 Hz, Resolution=463.7685 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT


```
|    Method |                                                                                                                Field |        Mean |     Error |    StdDev |
|---------- |--------------------------------------------------------------------------------------------------------------------- |------------:|----------:|----------:|
| **ResizeNew** |      **(ZombiesAreScattered startCapacity:10000, originalSize:21023, curCapacity:75431, newSize:11023, ResizeTo:42046)** |    **78.42 ms** |  **1.555 ms** |  **2.076 ms** |
| ResizeOld |      (ZombiesAreScattered startCapacity:10000, originalSize:21023, curCapacity:75431, newSize:11023, ResizeTo:42046) |    78.92 ms |  1.503 ms |  1.954 ms |
| **ResizeNew** | **(ZombiesAreScattered startCapacity:100000, originalSize:225307, curCapacity:807403, newSize:125307, ResizeTo:450614)** | **1,059.76 ms** | **21.088 ms** | **46.289 ms** |
| ResizeOld | (ZombiesAreScattered startCapacity:100000, originalSize:225307, curCapacity:807403, newSize:125307, ResizeTo:450614) | 1,086.05 ms | 21.571 ms | 37.780 ms |
