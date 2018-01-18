``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 1 [1607, Anniversary Update] (10.0.14393.1198)
Processor=Intel Core i7-3517U CPU 1.90GHz (Ivy Bridge), ProcessorCount=4
Frequency=2338438 Hz, Resolution=427.6359 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT


```
| Method |                                        Field |     Mean |     Error |    StdDev |
|------- |--------------------------------------------- |---------:|----------:|----------:|
| **AddNew** |   **(WithPercentageAsZombiesAtRandom 0,100,53)** | **794.9 us** | **12.019 us** | **11.242 us** |
| AddOld |   (WithPercentageAsZombiesAtRandom 0,100,53) | 834.1 us |  4.927 us |  4.367 us |
| **AddNew** | **(WithPercentageAsZombiesAtRandom 0.5,100,52)** | **805.8 us** | **14.470 us** | **13.535 us** |
| AddOld | (WithPercentageAsZombiesAtRandom 0.5,100,52) | 824.0 us |  5.473 us |  4.852 us |
| **AddNew** |   **(WithPercentageAsZombiesAtRandom 1,100,39)** | **799.3 us** |  **4.821 us** |  **4.026 us** |
| AddOld |   (WithPercentageAsZombiesAtRandom 1,100,39) | 638.0 us | 11.130 us | 10.411 us |
| **AddNew** |   **(WithPercentageAsZombiesAtRandom 2,100,40)** | **798.7 us** |  **6.888 us** |  **6.443 us** |
| AddOld |   (WithPercentageAsZombiesAtRandom 2,100,40) | 662.9 us | 11.408 us | 10.671 us |
