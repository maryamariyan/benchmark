``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.192)
Processor=Intel Core i7-6650U CPU 2.20GHz (Skylake), ProcessorCount=4
Frequency=2156248 Hz, Resolution=463.7685 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT


```
| Method |                                        Field |     Mean |     Error |   StdDev |   Median |
|------- |--------------------------------------------- |---------:|----------:|---------:|---------:|
| **AddNew** |   **(WithPercentageAsZombiesAtRandom 0,100,53)** | **403.9 us** | **12.181 us** | **35.53 us** | **397.8 us** |
| AddOld |   (WithPercentageAsZombiesAtRandom 0,100,53) | 442.3 us | 13.329 us | 38.88 us | 429.2 us |
| **AddNew** | **(WithPercentageAsZombiesAtRandom 0.5,100,52)** | **423.9 us** | **13.482 us** | **37.80 us** | **416.1 us** |
| AddOld | (WithPercentageAsZombiesAtRandom 0.5,100,52) | 420.1 us |  9.720 us | 27.57 us | 413.7 us |
| **AddNew** |   **(WithPercentageAsZombiesAtRandom 1,100,39)** | **422.6 us** | **13.859 us** | **39.99 us** | **406.9 us** |
| AddOld |   (WithPercentageAsZombiesAtRandom 1,100,39) | 336.0 us | 11.921 us | 34.77 us | 321.1 us |
| **AddNew** |   **(WithPercentageAsZombiesAtRandom 2,100,40)** | **428.3 us** | **16.185 us** | **46.70 us** | **414.6 us** |
| AddOld |   (WithPercentageAsZombiesAtRandom 2,100,40) | 336.6 us |  8.424 us | 24.17 us | 330.3 us |
