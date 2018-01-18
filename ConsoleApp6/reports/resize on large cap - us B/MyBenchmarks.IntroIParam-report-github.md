``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.192)
Processor=Intel Core i7-6650U CPU 2.20GHz (Skylake), ProcessorCount=4
Frequency=2156248 Hz, Resolution=463.7685 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT


```
|    Method |                                                                                                               Field |      Mean |     Error |    StdDev |
|---------- |-------------------------------------------------------------------------------------------------------------------- |----------:|----------:|----------:|
| **ResizeNew** |      **(ZombiesAreScattered startCapacity:42046, originalSize:10000, curCapacity:43627, newSize:5000, ResizeTo:21023)** |  **33.20 ms** | **0.6580 ms** |  **1.283 ms** |
| ResizeOld |      (ZombiesAreScattered startCapacity:42046, originalSize:10000, curCapacity:43627, newSize:5000, ResizeTo:21023) |  33.11 ms | 0.6559 ms |  1.295 ms |
| **ResizeNew** | **(ZombiesAreScattered startCapacity:450614, originalSize:100000, curCapacity:467237, newSize:50000, ResizeTo:225307)** | **399.85 ms** | **7.9056 ms** | **17.844 ms** |
| ResizeOld | (ZombiesAreScattered startCapacity:450614, originalSize:100000, curCapacity:467237, newSize:50000, ResizeTo:225307) | 417.53 ms | 8.2068 ms | 14.587 ms |
