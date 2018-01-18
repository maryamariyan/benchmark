``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.192)
Processor=Intel Core i7-6650U CPU 2.20GHz (Skylake), ProcessorCount=4
Frequency=2156248 Hz, Resolution=463.7685 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT


```
|    Method |                                                                                            Field |       Mean |     Error |    StdDev |
|---------- |------------------------------------------------------------------------------------------------- |-----------:|----------:|----------:|
| **ResizeNew** | **(DictionaryAllEntriesRemovedAddAgain startCapacity:0 originalSize:100000, methodArgument:150000)** | **1,195.5 ms** | **24.709 ms** | **36.218 ms** |
| ResizeOld | (DictionaryAllEntriesRemovedAddAgain startCapacity:0 originalSize:100000, methodArgument:150000) | 1,169.4 ms | 19.703 ms | 17.466 ms |
| **ResizeNew** |                  **(ZombiesAreScattered startCapacity:0 originalSize:100000, methodArgument:50000)** |   **383.4 ms** |  **7.603 ms** |  **9.337 ms** |
| ResizeOld |                  (ZombiesAreScattered startCapacity:0 originalSize:100000, methodArgument:50000) |   371.7 ms |  6.769 ms |  6.001 ms |
| **ResizeNew** |                         **(ZombiesAtEnd startCapacity:0 originalSize:100000, methodArgument:50000)** |   **384.8 ms** | **15.498 ms** | **15.915 ms** |
| ResizeOld |                         (ZombiesAtEnd startCapacity:0 originalSize:100000, methodArgument:50000) |   372.3 ms |  6.575 ms |  5.828 ms |
