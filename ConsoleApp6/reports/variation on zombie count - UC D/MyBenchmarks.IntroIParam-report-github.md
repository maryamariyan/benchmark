``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 1 [1607, Anniversary Update] (10.0.14393.1198)
Processor=Intel Core i7-3517U CPU 1.90GHz (Ivy Bridge), ProcessorCount=4
Frequency=2338438 Hz, Resolution=427.6359 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT


```
|    Method |                                                                                                               Field |       Mean |      Error |     StdDev |
|---------- |-------------------------------------------------------------------------------------------------------------------- |-----------:|-----------:|-----------:|
| **ResizeNew** |      **(ZombiesAreScattered startCapacity:42046, originalSize:10000, curCapacity:43627, newSize:1000, ResizeTo:21023)** |  **10.006 ms** |  **0.1963 ms** |  **0.1837 ms** |
| ResizeOld |      (ZombiesAreScattered startCapacity:42046, originalSize:10000, curCapacity:43627, newSize:1000, ResizeTo:21023) |   9.970 ms |  0.1099 ms |  0.0974 ms |
| **ResizeNew** |      **(ZombiesAreScattered startCapacity:42046, originalSize:10000, curCapacity:43627, newSize:5000, ResizeTo:21023)** |  **47.936 ms** |  **0.9076 ms** |  **0.8490 ms** |
| ResizeOld |      (ZombiesAreScattered startCapacity:42046, originalSize:10000, curCapacity:43627, newSize:5000, ResizeTo:21023) |  48.075 ms |  0.2181 ms |  0.1934 ms |
| **ResizeNew** |      **(ZombiesAreScattered startCapacity:42046, originalSize:10000, curCapacity:43627, newSize:9000, ResizeTo:21023)** |  **89.538 ms** |  **0.7304 ms** |  **0.6832 ms** |
| ResizeOld |      (ZombiesAreScattered startCapacity:42046, originalSize:10000, curCapacity:43627, newSize:9000, ResizeTo:21023) |  89.765 ms |  0.6534 ms |  0.6112 ms |
| **ResizeNew** | **(ZombiesAreScattered startCapacity:450614, originalSize:100000, curCapacity:467237, newSize:10000, ResizeTo:225307)** | **100.653 ms** |  **0.5113 ms** |  **0.4533 ms** |
| ResizeOld | (ZombiesAreScattered startCapacity:450614, originalSize:100000, curCapacity:467237, newSize:10000, ResizeTo:225307) | 101.261 ms |  0.8737 ms |  0.6821 ms |
| **ResizeNew** | **(ZombiesAreScattered startCapacity:450614, originalSize:100000, curCapacity:467237, newSize:50000, ResizeTo:225307)** | **509.139 ms** |  **2.2903 ms** |  **1.9125 ms** |
| ResizeOld | (ZombiesAreScattered startCapacity:450614, originalSize:100000, curCapacity:467237, newSize:50000, ResizeTo:225307) | 512.562 ms |  2.7323 ms |  2.4221 ms |
| **ResizeNew** | **(ZombiesAreScattered startCapacity:450614, originalSize:100000, curCapacity:467237, newSize:90000, ResizeTo:225307)** | **923.998 ms** | **19.5206 ms** | **16.3006 ms** |
| ResizeOld | (ZombiesAreScattered startCapacity:450614, originalSize:100000, curCapacity:467237, newSize:90000, ResizeTo:225307) | 925.274 ms |  2.0405 ms |  1.7039 ms |
