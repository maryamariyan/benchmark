``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 1 [1607, Anniversary Update] (10.0.14393.1198)
Processor=Intel Core i7-3517U CPU 1.90GHz (Ivy Bridge), ProcessorCount=4
Frequency=2338438 Hz, Resolution=427.6359 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT


```
|    Method |                                                                   Field |        Mean |     Error |    StdDev |      Median |
|---------- |------------------------------------------------------------------------ |------------:|----------:|----------:|------------:|
| **ResizeNew** |     **(ZombiesAtEnd startCapacity:0 originalSize:100, methodArgument:197)** |    **820.1 us** |  **16.00 us** |  **26.28 us** |    **817.8 us** |
| ResizeOld |     (ZombiesAtEnd startCapacity:0 originalSize:100, methodArgument:197) |    805.7 us |  21.66 us |  36.19 us |    787.7 us |
| **ResizeNew** |   **(ZombiesAtEnd startCapacity:0 originalSize:1000, methodArgument:1931)** |  **7,510.7 us** |  **67.95 us** |  **63.56 us** |  **7,507.7 us** |
| ResizeOld |   (ZombiesAtEnd startCapacity:0 originalSize:1000, methodArgument:1931) |  7,455.6 us |  51.31 us |  48.00 us |  7,455.1 us |
| **ResizeNew** | **(ZombiesAtEnd startCapacity:0 originalSize:10000, methodArgument:17519)** | **75,475.9 us** | **459.51 us** | **383.71 us** | **75,437.1 us** |
| ResizeOld | (ZombiesAtEnd startCapacity:0 originalSize:10000, methodArgument:17519) | 75,202.8 us | 452.86 us | 423.60 us | 75,201.5 us |
