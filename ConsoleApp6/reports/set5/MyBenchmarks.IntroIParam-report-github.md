``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.192)
Processor=Intel Core i7-6650U CPU 2.20GHz (Skylake), ProcessorCount=4
Frequency=2156248 Hz, Resolution=463.7685 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT


```
|    Method |                                         Field |     Mean |     Error |    StdDev |   Median |
|---------- |---------------------------------------------- |---------:|----------:|----------:|---------:|
| **ResizeNew** | **(WithPercentageAsZombiesAtRandom 0,1000,1000)** | **3.779 ms** | **0.1284 ms** | **0.3726 ms** | **3.684 ms** |
| ResizeOld | (WithPercentageAsZombiesAtRandom 0,1000,1000) | 7.457 ms | 0.2323 ms | 0.6590 ms | 7.178 ms |
| **ResizeNew** | **(WithPercentageAsZombiesAtRandom 1,1000,1000)** | **3.802 ms** | **0.1106 ms** | **0.3172 ms** | **3.674 ms** |
| ResizeOld | (WithPercentageAsZombiesAtRandom 1,1000,1000) | 7.738 ms | 0.3241 ms | 0.9247 ms | 7.422 ms |
