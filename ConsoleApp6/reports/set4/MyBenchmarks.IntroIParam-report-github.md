``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.192)
Processor=Intel Core i7-6650U CPU 2.20GHz (Skylake), ProcessorCount=4
Frequency=2156248 Hz, Resolution=463.7685 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT


```
|    Method |                             Field |        Mean |       Error |       StdDev |      Median |
|---------- |---------------------------------- |------------:|------------:|-------------:|------------:|
| **ResizeNew** |       **(WithZombiesAtEnd 0,100,80)** |    **531.4 us** |    **10.41 us** |     **19.81 us** |    **528.3 us** |
| ResizeOld |       (WithZombiesAtEnd 0,100,80) |    536.1 us |    10.56 us |     19.03 us |    533.0 us |
| **ResizeNew** |     **(WithZombiesAtEnd 0,1000,800)** |  **5,663.3 us** |   **149.44 us** |    **416.57 us** |  **5,525.5 us** |
| ResizeOld |     (WithZombiesAtEnd 0,1000,800) |  5,692.5 us |   165.96 us |    468.09 us |  5,533.8 us |
| **ResizeNew** |   **(WithZombiesAtEnd 0,10000,8000)** | **60,826.4 us** | **1,384.15 us** |  **3,881.31 us** | **60,722.4 us** |
| ResizeOld |   (WithZombiesAtEnd 0,10000,8000) | 59,873.8 us | 1,264.42 us |  3,708.31 us | 60,346.2 us |
| **ResizeNew** |       **(WithZombiesAtEnd 2,100,80)** |    **633.4 us** |    **18.23 us** |     **53.18 us** |    **625.9 us** |
| ResizeOld |       (WithZombiesAtEnd 2,100,80) |    675.8 us |    32.24 us |     91.99 us |    648.6 us |
| **ResizeNew** |     **(WithZombiesAtEnd 2,1000,800)** |  **5,890.6 us** |   **162.94 us** |    **464.89 us** |  **5,740.3 us** |
| ResizeOld |     (WithZombiesAtEnd 2,1000,800) |  5,864.2 us |   181.36 us |    520.36 us |  5,712.7 us |
| **ResizeNew** |   **(WithZombiesAtEnd 2,10000,8000)** | **60,800.8 us** | **2,133.07 us** |  **5,946.15 us** | **59,686.3 us** |
| ResizeOld |   (WithZombiesAtEnd 2,10000,8000) | 64,431.3 us | 2,230.08 us |  6,326.36 us | 63,581.1 us |
| **ResizeNew** |     **(WithZombiesAtFront 0,100,80)** |    **692.3 us** |    **42.41 us** |    **123.72 us** |    **643.7 us** |
| ResizeOld |     (WithZombiesAtFront 0,100,80) |    755.7 us |    51.26 us |    150.33 us |    769.9 us |
| **ResizeNew** |   **(WithZombiesAtFront 0,1000,800)** |  **6,976.1 us** |   **442.30 us** |  **1,304.13 us** |  **6,534.5 us** |
| ResizeOld |   (WithZombiesAtFront 0,1000,800) |  6,637.2 us |   403.86 us |  1,184.44 us |  6,036.1 us |
| **ResizeNew** | **(WithZombiesAtFront 0,10000,8000)** | **60,384.2 us** | **1,729.81 us** |  **4,822.01 us** | **59,101.6 us** |
| ResizeOld | (WithZombiesAtFront 0,10000,8000) | 74,742.7 us | 4,476.81 us | 13,199.98 us | 80,197.3 us |
| **ResizeNew** |     **(WithZombiesAtFront 2,100,80)** |    **627.9 us** |    **15.93 us** |     **45.20 us** |    **615.9 us** |
| ResizeOld |     (WithZombiesAtFront 2,100,80) |    629.4 us |    17.11 us |     48.55 us |    612.4 us |
| **ResizeNew** |   **(WithZombiesAtFront 2,1000,800)** |  **6,131.3 us** |   **202.94 us** |    **569.05 us** |  **5,942.8 us** |
| ResizeOld |   (WithZombiesAtFront 2,1000,800) |  6,683.1 us |   422.03 us |  1,244.36 us |  6,059.4 us |
| **ResizeNew** | **(WithZombiesAtFront 2,10000,8000)** | **59,800.3 us** | **1,357.59 us** |  **3,895.19 us** | **58,871.3 us** |
| ResizeOld | (WithZombiesAtFront 2,10000,8000) | 58,342.4 us | 1,278.11 us |  3,625.79 us | 57,309.5 us |
