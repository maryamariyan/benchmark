``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.192)
Processor=Intel Core i7-6650U CPU 2.20GHz (Skylake), ProcessorCount=4
Frequency=2156248 Hz, Resolution=463.7685 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT


```
|          Method |                                                        Field |        Mean |       Error |     StdDev |      Median |
|---------------- |------------------------------------------------------------- |------------:|------------:|-----------:|------------:|
|       **ResizeNew** |   **(WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,1)** |    **42.33 us** |   **1.2927 us** |   **3.582 us** |    **41.52 us** |
|       ResizeOld |   (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,1) |    39.74 us |   1.0473 us |   1.029 us |    39.50 us |
|          AddNew |   (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,1) |    42.71 us |   0.9175 us |   2.512 us |    42.09 us |
|          AddOld |   (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,1) |    41.54 us |   0.8149 us |   1.220 us |    41.42 us |
| AddAndRemoveNew |   (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,1) |    42.45 us |   0.8780 us |   1.649 us |    41.92 us |
| AddAndRemoveOld |   (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,1) |    42.29 us |   0.8433 us |   1.869 us |    41.87 us |
|       **ResizeNew** |  **(WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,10)** |   **117.61 us** |   **7.2937 us** |  **21.391 us** |   **107.54 us** |
|       ResizeOld |  (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,10) |   108.44 us |   3.0395 us |   8.672 us |   105.84 us |
|          AddNew |  (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,10) |   109.55 us |   2.5702 us |   7.291 us |   107.20 us |
|          AddOld |  (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,10) |   112.94 us |   2.9563 us |   8.290 us |   111.38 us |
| AddAndRemoveNew |  (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,10) |   112.94 us |   3.7457 us |  10.986 us |   109.22 us |
| AddAndRemoveOld |  (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,10) |   109.47 us |   3.0911 us |   8.819 us |   105.74 us |
|       **ResizeNew** | **(WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,100)** |   **774.39 us** |  **22.9745 us** |  **65.918 us** |   **764.03 us** |
|       ResizeOld | (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,100) |   732.28 us |  14.1341 us |  15.123 us |   734.45 us |
|          AddNew | (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,100) |   787.53 us |  24.9217 us |  71.905 us |   755.93 us |
|          AddOld | (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,100) |   806.91 us |  35.1989 us | 100.425 us |   785.70 us |
| AddAndRemoveNew | (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,100) |   782.35 us |  29.5485 us |  85.254 us |   750.28 us |
| AddAndRemoveOld | (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,100) |   735.05 us |  19.7039 us |  54.927 us |   716.54 us |
|       **ResizeNew** | **(WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,200)** | **1,460.15 us** |  **47.8529 us** | **134.970 us** | **1,409.01 us** |
|       ResizeOld | (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,200) | 1,442.83 us |  38.2648 us | 108.551 us | 1,401.47 us |
|          AddNew | (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,200) | 1,437.62 us |  29.7531 us |  85.367 us | 1,411.75 us |
|          AddOld | (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,200) | 1,387.92 us |  27.2985 us |  40.014 us | 1,386.42 us |
| AddAndRemoveNew | (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,200) | 1,605.62 us | 103.4136 us | 301.662 us | 1,473.16 us |
| AddAndRemoveOld | (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,200) | 1,449.25 us |  39.1373 us | 112.920 us | 1,421.82 us |
|       **ResizeNew** |   **(WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,3)** |    **56.29 us** |   **1.3308 us** |   **3.775 us** |    **55.49 us** |
|       ResizeOld |   (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,3) |    64.38 us |   4.0239 us |  11.802 us |    58.57 us |
|          AddNew |   (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,3) |    54.78 us |   1.0685 us |   1.599 us |    54.79 us |
|          AddOld |   (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,3) |    55.03 us |   1.0998 us |   2.739 us |    54.42 us |
| AddAndRemoveNew |   (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,3) |    55.40 us |   1.1050 us |   2.307 us |    54.47 us |
| AddAndRemoveOld |   (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,3) |    55.46 us |   1.0911 us |   1.939 us |    55.21 us |
|       **ResizeNew** |  **(WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,50)** |   **371.64 us** |   **8.1011 us** |  **23.113 us** |   **365.64 us** |
|       ResizeOld |  (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,50) |   434.62 us |   8.4245 us |  11.246 us |   433.33 us |
|          AddNew |  (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,50) |   411.02 us |  14.0634 us |  40.124 us |   402.24 us |
|          AddOld |  (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,50) |   396.38 us |  11.8438 us |  33.599 us |   381.29 us |
| AddAndRemoveNew |  (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,50) |   410.28 us |  12.2017 us |  33.402 us |   402.68 us |
| AddAndRemoveOld |  (WithDictionaryAllEntriesRemovedAddAgainCurrent 0,10000,50) |   400.22 us |  14.0577 us |  41.007 us |   384.05 us |
|       **ResizeNew** |                      **(WithDictionaryFullCurrent 0,10000,100)** |   **774.20 us** |  **29.1463 us** |  **81.248 us** |   **745.09 us** |
|       ResizeOld |                      (WithDictionaryFullCurrent 0,10000,100) |   761.02 us |  24.4491 us |  70.931 us |   741.10 us |
|          AddNew |                      (WithDictionaryFullCurrent 0,10000,100) |   784.07 us |  24.1476 us |  69.284 us |   756.92 us |
|          AddOld |                      (WithDictionaryFullCurrent 0,10000,100) |   852.53 us |  60.1675 us | 177.405 us |   758.11 us |
| AddAndRemoveNew |                      (WithDictionaryFullCurrent 0,10000,100) |   797.71 us |  25.3636 us |  72.364 us |   773.58 us |
| AddAndRemoveOld |                      (WithDictionaryFullCurrent 0,10000,100) |   783.93 us |  19.1073 us |  55.737 us |   770.19 us |
|       **ResizeNew** |            **(WithPercentageAsZombiesAtRandomCurrent 0,100,10)** |          **NA** |          **NA** |         **NA** |          **NA** |
|       ResizeOld |            (WithPercentageAsZombiesAtRandomCurrent 0,100,10) |   108.26 us |   2.9452 us |   8.591 us |   105.30 us |
|          AddNew |            (WithPercentageAsZombiesAtRandomCurrent 0,100,10) |   114.14 us |   2.6330 us |   7.512 us |   111.34 us |
|          AddOld |            (WithPercentageAsZombiesAtRandomCurrent 0,100,10) |   109.69 us |   2.9692 us |   8.614 us |   106.57 us |
| AddAndRemoveNew |            (WithPercentageAsZombiesAtRandomCurrent 0,100,10) |   121.03 us |   4.8557 us |  13.854 us |   115.79 us |
| AddAndRemoveOld |            (WithPercentageAsZombiesAtRandomCurrent 0,100,10) |   105.12 us |   2.2126 us |   5.829 us |   102.89 us |
|       **ResizeNew** |            **(WithPercentageAsZombiesAtRandomCurrent 0,100,39)** |          **NA** |          **NA** |         **NA** |          **NA** |
|       ResizeOld |            (WithPercentageAsZombiesAtRandomCurrent 0,100,39) |   298.19 us |   5.9351 us |   8.512 us |   296.38 us |
|          AddNew |            (WithPercentageAsZombiesAtRandomCurrent 0,100,39) |   415.46 us |   8.2369 us |  19.415 us |   412.13 us |
|          AddOld |            (WithPercentageAsZombiesAtRandomCurrent 0,100,39) |   330.61 us |  11.0710 us |  30.307 us |   325.84 us |
| AddAndRemoveNew |            (WithPercentageAsZombiesAtRandomCurrent 0,100,39) |   449.05 us |  14.6489 us |  42.732 us |   432.15 us |
| AddAndRemoveOld |            (WithPercentageAsZombiesAtRandomCurrent 0,100,39) |   304.37 us |   6.5437 us |  14.364 us |   299.17 us |
|       **ResizeNew** |            **(WithPercentageAsZombiesAtRandomCurrent 0,100,84)** |          **NA** |          **NA** |         **NA** |          **NA** |
|       ResizeOld |            (WithPercentageAsZombiesAtRandomCurrent 0,100,84) |   596.91 us |  11.8115 us |  19.407 us |   590.72 us |
|          AddNew |            (WithPercentageAsZombiesAtRandomCurrent 0,100,84) |   647.67 us |  14.2658 us |  40.931 us |   643.84 us |
|          AddOld |            (WithPercentageAsZombiesAtRandomCurrent 0,100,84) |   639.12 us |  24.3587 us |  67.498 us |   612.25 us |
| AddAndRemoveNew |            (WithPercentageAsZombiesAtRandomCurrent 0,100,84) |   632.73 us |  13.3985 us |  26.133 us |   623.71 us |
| AddAndRemoveOld |            (WithPercentageAsZombiesAtRandomCurrent 0,100,84) |   608.91 us |  12.0275 us |  25.632 us |   601.88 us |
|       **ResizeNew** |                          **(WithZombiesAtEndCurrent 0,100,100)** |   **722.99 us** |  **18.0457 us** |  **51.485 us** |   **709.62 us** |
|       ResizeOld |                          (WithZombiesAtEndCurrent 0,100,100) |   702.47 us |  21.5197 us |  23.026 us |   696.70 us |
|          AddNew |                          (WithZombiesAtEndCurrent 0,100,100) |   768.36 us |  18.2836 us |  51.569 us |   753.40 us |
|          AddOld |                          (WithZombiesAtEndCurrent 0,100,100) |   739.05 us |  15.9351 us |  45.205 us |   724.31 us |
| AddAndRemoveNew |                          (WithZombiesAtEndCurrent 0,100,100) |   791.48 us |  22.9345 us |  67.263 us |   770.83 us |
| AddAndRemoveOld |                          (WithZombiesAtEndCurrent 0,100,100) |   796.31 us |  26.5296 us |  76.118 us |   766.31 us |
|       **ResizeNew** |                        **(WithZombiesAtFrontCurrent 0,100,100)** |   **737.11 us** |  **14.7345 us** |  **40.583 us** |   **724.52 us** |
|       ResizeOld |                        (WithZombiesAtFrontCurrent 0,100,100) |   761.08 us |  18.4064 us |  52.216 us |   746.20 us |
|          AddNew |                        (WithZombiesAtFrontCurrent 0,100,100) |   747.52 us |  14.8271 us |  36.649 us |   742.47 us |
|          AddOld |                        (WithZombiesAtFrontCurrent 0,100,100) |   767.17 us |  15.2452 us |  42.244 us |   754.63 us |
| AddAndRemoveNew |                        (WithZombiesAtFrontCurrent 0,100,100) |   761.05 us |  15.1642 us |  34.228 us |   751.03 us |
| AddAndRemoveOld |                        (WithZombiesAtFrontCurrent 0,100,100) |   769.36 us |  15.7790 us |  41.568 us |   755.67 us |

Benchmarks with issues:
  IntroIParam.ResizeNew: DefaultJob [Field=(WithPercentageAsZombiesAtRandomCurrent 0,100,10)]
  IntroIParam.ResizeNew: DefaultJob [Field=(WithPercentageAsZombiesAtRandomCurrent 0,100,39)]
  IntroIParam.ResizeNew: DefaultJob [Field=(WithPercentageAsZombiesAtRandomCurrent 0,100,84)]
