``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.192)
Processor=Intel Core i7-6650U CPU 2.20GHz (Skylake), ProcessorCount=4
Frequency=2156248 Hz, Resolution=463.7685 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT


```
|    Method |                                                                                                           Field |      Mean |     Error |     StdDev |
|---------- |---------------------------------------------------------------------------------------------------------------- |----------:|----------:|-----------:|
| **ResizeNew** |      **(DictionaryFull startCapacity:42046, originalSize:10000, curCapacity:43627, newSize:10000, ResizeTo:21023)** |  **70.44 ms** |  **1.157 ms** |  **0.9036 ms** |
| ResizeOld |      (DictionaryFull startCapacity:42046, originalSize:10000, curCapacity:43627, newSize:10000, ResizeTo:21023) |  70.82 ms |  1.389 ms |  1.9477 ms |
| **ResizeNew** | **(DictionaryFull startCapacity:450614, originalSize:100000, curCapacity:467237, newSize:100000, ResizeTo:225307)** | **830.43 ms** | **16.608 ms** | **39.1471 ms** |
| ResizeOld | (DictionaryFull startCapacity:450614, originalSize:100000, curCapacity:467237, newSize:100000, ResizeTo:225307) | 854.69 ms | 17.037 ms | 26.5244 ms |
