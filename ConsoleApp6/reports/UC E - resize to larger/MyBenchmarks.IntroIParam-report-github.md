``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 1 [1607, Anniversary Update] (10.0.14393.1198)
Processor=Intel Core i7-3517U CPU 1.90GHz (Ivy Bridge), ProcessorCount=4
Frequency=2338438 Hz, Resolution=427.6359 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT


```
|    Method |                                                                                                                Field |        Mean |      Error |     StdDev |
|---------- |--------------------------------------------------------------------------------------------------------------------- |------------:|-----------:|-----------:|
| **ResizeNew** |           **(DictionaryFull startCapacity:42046, originalSize:10000, curCapacity:43627, newSize:10000, ResizeTo:63069)** |    **98.07 ms** |  **0.5701 ms** |  **0.5333 ms** |
| ResizeOld |           (DictionaryFull startCapacity:42046, originalSize:10000, curCapacity:43627, newSize:10000, ResizeTo:63069) |    97.04 ms |  0.8857 ms |  0.7396 ms |
| **ResizeNew** |      **(DictionaryFull startCapacity:450614, originalSize:100000, curCapacity:467237, newSize:100000, ResizeTo:675921)** | **1,006.10 ms** |  **5.1495 ms** |  **4.0204 ms** |
| ResizeOld |      (DictionaryFull startCapacity:450614, originalSize:100000, curCapacity:467237, newSize:100000, ResizeTo:675921) | 1,012.50 ms |  3.6518 ms |  2.8511 ms |
| **ResizeNew** |      **(ZombiesAreScattered startCapacity:10000, originalSize:21023, curCapacity:75431, newSize:11023, ResizeTo:84092)** |   **106.83 ms** |  **0.5696 ms** |  **0.5049 ms** |
| ResizeOld |      (ZombiesAreScattered startCapacity:10000, originalSize:21023, curCapacity:75431, newSize:11023, ResizeTo:84092) |   106.85 ms |  0.3707 ms |  0.3468 ms |
| **ResizeNew** | **(ZombiesAreScattered startCapacity:100000, originalSize:225307, curCapacity:807403, newSize:125307, ResizeTo:901228)** | **1,293.48 ms** | **38.9684 ms** | **34.5444 ms** |
| ResizeOld | (ZombiesAreScattered startCapacity:100000, originalSize:225307, curCapacity:807403, newSize:125307, ResizeTo:901228) | 1,269.52 ms |  6.8357 ms |  5.7082 ms |
| **ResizeNew** |       **(ZombiesAreScattered startCapacity:42046, originalSize:10000, curCapacity:43627, newSize:5000, ResizeTo:63069)** |    **49.43 ms** |  **4.3402 ms** |  **4.6440 ms** |
| ResizeOld |       (ZombiesAreScattered startCapacity:42046, originalSize:10000, curCapacity:43627, newSize:5000, ResizeTo:63069) |    47.86 ms |  0.3675 ms |  0.3438 ms |
| **ResizeNew** |  **(ZombiesAreScattered startCapacity:450614, originalSize:100000, curCapacity:467237, newSize:50000, ResizeTo:675921)** |   **506.94 ms** |  **2.0138 ms** |  **1.5723 ms** |
| ResizeOld |  (ZombiesAreScattered startCapacity:450614, originalSize:100000, curCapacity:467237, newSize:50000, ResizeTo:675921) |   508.18 ms |  2.6384 ms |  2.0599 ms |
