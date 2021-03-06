``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.192)
Processor=Intel Core i7-6650U CPU 2.20GHz (Skylake), ProcessorCount=4
Frequency=2156248 Hz, Resolution=463.7685 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT


```
|    Method |                                                                             Field |        Mean |        Error |       StdDev |      Median |
|---------- |---------------------------------------------------------------------------------- |------------:|-------------:|-------------:|------------:|
| **ResizeNew** |         **(ZombiesAreScattered startCapacity:0 originalSize:100, methodArgument:10)** |    **102.5 us** |     **2.035 us** |     **5.741 us** |    **102.0 us** |
| ResizeOld |         (ZombiesAreScattered startCapacity:0 originalSize:100, methodArgument:10) |    125.7 us |     3.324 us |     9.644 us |    125.1 us |
| **ResizeNew** |         **(ZombiesAreScattered startCapacity:0 originalSize:100, methodArgument:50)** |    **464.2 us** |    **22.443 us** |    **64.753 us** |    **444.5 us** |
| ResizeOld |         (ZombiesAreScattered startCapacity:0 originalSize:100, methodArgument:50) |    516.3 us |    24.766 us |    73.024 us |    512.4 us |
| **ResizeNew** |         **(ZombiesAreScattered startCapacity:0 originalSize:100, methodArgument:90)** |    **795.2 us** |    **32.909 us** |    **93.892 us** |    **779.7 us** |
| ResizeOld |         (ZombiesAreScattered startCapacity:0 originalSize:100, methodArgument:90) |    810.5 us |    27.386 us |    79.885 us |    805.0 us |
| **ResizeNew** |       **(ZombiesAreScattered startCapacity:0 originalSize:1000, methodArgument:100)** |    **949.2 us** |    **42.687 us** |   **122.476 us** |    **929.4 us** |
| ResizeOld |       (ZombiesAreScattered startCapacity:0 originalSize:1000, methodArgument:100) |    931.2 us |    48.893 us |   142.623 us |    899.9 us |
| **ResizeNew** |       **(ZombiesAreScattered startCapacity:0 originalSize:1000, methodArgument:500)** |  **4,429.2 us** |   **218.320 us** |   **626.401 us** |  **4,281.4 us** |
| ResizeOld |       (ZombiesAreScattered startCapacity:0 originalSize:1000, methodArgument:500) |  4,231.0 us |   131.879 us |   378.385 us |  4,154.0 us |
| **ResizeNew** |       **(ZombiesAreScattered startCapacity:0 originalSize:1000, methodArgument:900)** |  **8,519.5 us** |   **496.266 us** | **1,455.463 us** |  **8,134.2 us** |
| ResizeOld |       (ZombiesAreScattered startCapacity:0 originalSize:1000, methodArgument:900) |  6,750.6 us |   229.296 us |   612.039 us |  6,564.6 us |
| **ResizeNew** |     **(ZombiesAreScattered startCapacity:0 originalSize:10000, methodArgument:1000)** |  **7,610.8 us** |   **188.101 us** |   **530.541 us** |  **7,517.1 us** |
| ResizeOld |     (ZombiesAreScattered startCapacity:0 originalSize:10000, methodArgument:1000) |  7,907.7 us |   267.788 us |   772.630 us |  7,707.8 us |
| **ResizeNew** |     **(ZombiesAreScattered startCapacity:0 originalSize:10000, methodArgument:5000)** | **37,912.4 us** |   **949.287 us** | **2,738.911 us** | **37,215.7 us** |
| ResizeOld |     (ZombiesAreScattered startCapacity:0 originalSize:10000, methodArgument:5000) | 40,598.1 us | 1,348.136 us | 3,932.577 us | 39,648.2 us |
| **ResizeNew** |     **(ZombiesAreScattered startCapacity:0 originalSize:10000, methodArgument:9000)** | **74,290.9 us** | **1,480.218 us** | **4,174.982 us** | **73,346.7 us** |
| ResizeOld |     (ZombiesAreScattered startCapacity:0 originalSize:10000, methodArgument:9000) | 73,918.9 us | 1,581.056 us | 4,636.965 us | 73,397.4 us |
| **ResizeNew** |       **(ZombiesAreScattered startCapacity:100 originalSize:100, methodArgument:10)** |    **114.3 us** |     **3.469 us** |    **10.118 us** |    **111.3 us** |
| ResizeOld |       (ZombiesAreScattered startCapacity:100 originalSize:100, methodArgument:10) |    116.1 us |     3.903 us |    11.198 us |    113.4 us |
| **ResizeNew** |       **(ZombiesAreScattered startCapacity:100 originalSize:100, methodArgument:50)** |    **410.0 us** |     **8.935 us** |    **25.780 us** |    **407.3 us** |
| ResizeOld |       (ZombiesAreScattered startCapacity:100 originalSize:100, methodArgument:50) |    440.2 us |    19.153 us |    55.260 us |    419.0 us |
| **ResizeNew** |       **(ZombiesAreScattered startCapacity:100 originalSize:100, methodArgument:90)** |    **767.7 us** |    **40.195 us** |   **117.886 us** |    **718.0 us** |
| ResizeOld |       (ZombiesAreScattered startCapacity:100 originalSize:100, methodArgument:90) |    719.2 us |    20.498 us |    58.149 us |    700.3 us |
| **ResizeNew** |    **(ZombiesAreScattered startCapacity:1000 originalSize:1000, methodArgument:100)** |    **808.3 us** |    **21.899 us** |    **62.832 us** |    **791.6 us** |
| ResizeOld |    (ZombiesAreScattered startCapacity:1000 originalSize:1000, methodArgument:100) |    756.1 us |    14.869 us |    25.249 us |    753.4 us |
| **ResizeNew** |    **(ZombiesAreScattered startCapacity:1000 originalSize:1000, methodArgument:500)** |  **3,653.1 us** |    **72.427 us** |   **144.644 us** |  **3,674.3 us** |
| ResizeOld |    (ZombiesAreScattered startCapacity:1000 originalSize:1000, methodArgument:500) |  3,643.5 us |    72.572 us |   168.198 us |  3,617.6 us |
| **ResizeNew** |    **(ZombiesAreScattered startCapacity:1000 originalSize:1000, methodArgument:900)** |  **7,120.8 us** |   **236.600 us** |   **682.646 us** |  **6,980.0 us** |
| ResizeOld |    (ZombiesAreScattered startCapacity:1000 originalSize:1000, methodArgument:900) |  6,729.0 us |   188.777 us |   544.665 us |  6,587.8 us |
| **ResizeNew** | **(ZombiesAreScattered startCapacity:10000 originalSize:10000, methodArgument:1000)** |  **7,844.1 us** |   **231.273 us** |   **681.913 us** |  **7,628.7 us** |
| ResizeOld | (ZombiesAreScattered startCapacity:10000 originalSize:10000, methodArgument:1000) |  7,478.6 us |   162.966 us |   467.580 us |  7,359.0 us |
| **ResizeNew** | **(ZombiesAreScattered startCapacity:10000 originalSize:10000, methodArgument:5000)** | **39,311.7 us** | **1,004.019 us** | **2,896.826 us** | **38,944.2 us** |
| ResizeOld | (ZombiesAreScattered startCapacity:10000 originalSize:10000, methodArgument:5000) | 37,756.3 us |   899.858 us | 2,552.748 us | 37,026.5 us |
| **ResizeNew** | **(ZombiesAreScattered startCapacity:10000 originalSize:10000, methodArgument:9000)** | **69,920.4 us** | **1,366.099 us** | **1,959.219 us** | **69,850.7 us** |
| ResizeOld | (ZombiesAreScattered startCapacity:10000 originalSize:10000, methodArgument:9000) | 69,242.1 us | 1,360.870 us | 2,036.886 us | 69,822.1 us |
| **ResizeNew** |       **(ZombiesAreScattered startCapacity:200 originalSize:100, methodArgument:10)** |    **112.9 us** |     **3.416 us** |     **9.965 us** |    **109.4 us** |
| ResizeOld |       (ZombiesAreScattered startCapacity:200 originalSize:100, methodArgument:10) |    126.4 us |     7.003 us |    20.538 us |    117.2 us |
| **ResizeNew** |       **(ZombiesAreScattered startCapacity:200 originalSize:100, methodArgument:50)** |    **419.3 us** |    **13.773 us** |    **39.517 us** |    **406.2 us** |
| ResizeOld |       (ZombiesAreScattered startCapacity:200 originalSize:100, methodArgument:50) |    407.7 us |    11.734 us |    34.413 us |    397.0 us |
| **ResizeNew** |       **(ZombiesAreScattered startCapacity:200 originalSize:100, methodArgument:90)** |    **757.3 us** |    **35.312 us** |   **103.564 us** |    **728.6 us** |
| ResizeOld |       (ZombiesAreScattered startCapacity:200 originalSize:100, methodArgument:90) |    727.4 us |    24.838 us |    71.664 us |    705.1 us |
| **ResizeNew** |    **(ZombiesAreScattered startCapacity:2000 originalSize:1000, methodArgument:100)** |    **811.0 us** |    **28.756 us** |    **82.043 us** |    **787.2 us** |
| ResizeOld |    (ZombiesAreScattered startCapacity:2000 originalSize:1000, methodArgument:100) |    787.3 us |    24.006 us |    69.263 us |    764.5 us |
| **ResizeNew** |    **(ZombiesAreScattered startCapacity:2000 originalSize:1000, methodArgument:500)** |  **3,661.6 us** |    **72.689 us** |   **198.985 us** |  **3,609.6 us** |
| ResizeOld |    (ZombiesAreScattered startCapacity:2000 originalSize:1000, methodArgument:500) |  3,766.0 us |   131.213 us |   380.672 us |  3,592.0 us |
| **ResizeNew** |    **(ZombiesAreScattered startCapacity:2000 originalSize:1000, methodArgument:900)** |  **6,762.7 us** |   **215.860 us** |   **629.672 us** |  **6,534.0 us** |
| ResizeOld |    (ZombiesAreScattered startCapacity:2000 originalSize:1000, methodArgument:900) |  6,650.4 us |   183.883 us |   518.645 us |  6,448.2 us |
| **ResizeNew** | **(ZombiesAreScattered startCapacity:20000 originalSize:10000, methodArgument:1000)** |  **7,600.9 us** |   **255.771 us** |   **742.039 us** |  **7,337.7 us** |
| ResizeOld | (ZombiesAreScattered startCapacity:20000 originalSize:10000, methodArgument:1000) |  7,520.4 us |   227.313 us |   666.670 us |  7,314.6 us |
| **ResizeNew** | **(ZombiesAreScattered startCapacity:20000 originalSize:10000, methodArgument:5000)** | **38,073.2 us** | **1,075.954 us** | **3,138.608 us** | **37,638.5 us** |
| ResizeOld | (ZombiesAreScattered startCapacity:20000 originalSize:10000, methodArgument:5000) | 40,623.8 us | 1,686.265 us | 4,700.634 us | 39,400.1 us |
| **ResizeNew** | **(ZombiesAreScattered startCapacity:20000 originalSize:10000, methodArgument:9000)** | **71,400.2 us** | **1,422.459 us** | **3,122.332 us** | **71,120.5 us** |
| ResizeOld | (ZombiesAreScattered startCapacity:20000 originalSize:10000, methodArgument:9000) | 72,067.0 us | 1,432.864 us | 3,292.244 us | 71,207.9 us |
| **ResizeNew** |                **(ZombiesAtEnd startCapacity:0 originalSize:100, methodArgument:80)** |    **637.9 us** |    **14.998 us** |    **43.032 us** |    **626.5 us** |
| ResizeOld |                (ZombiesAtEnd startCapacity:0 originalSize:100, methodArgument:80) |    628.9 us |    12.968 us |    32.773 us |    625.1 us |
| **ResizeNew** |              **(ZombiesAtEnd startCapacity:0 originalSize:1000, methodArgument:800)** |  **6,053.8 us** |   **118.294 us** |   **296.776 us** |  **6,039.5 us** |
| ResizeOld |              (ZombiesAtEnd startCapacity:0 originalSize:1000, methodArgument:800) |  6,045.7 us |   120.036 us |   253.197 us |  6,027.5 us |
| **ResizeNew** |            **(ZombiesAtEnd startCapacity:0 originalSize:10000, methodArgument:8000)** | **60,486.4 us** | **1,188.385 us** | **1,985.525 us** | **60,227.9 us** |
| ResizeOld |            (ZombiesAtEnd startCapacity:0 originalSize:10000, methodArgument:8000) | 60,117.8 us | 1,194.021 us | 1,673.851 us | 60,108.3 us |
| **ResizeNew** |              **(ZombiesAtEnd startCapacity:100 originalSize:100, methodArgument:80)** |    **645.1 us** |    **15.918 us** |    **46.180 us** |    **636.5 us** |
| ResizeOld |              (ZombiesAtEnd startCapacity:100 originalSize:100, methodArgument:80) |    627.1 us |    13.762 us |    38.815 us |    619.1 us |
| **ResizeNew** |           **(ZombiesAtEnd startCapacity:1000 originalSize:1000, methodArgument:800)** |  **6,114.9 us** |   **160.216 us** |   **443.956 us** |  **5,999.5 us** |
| ResizeOld |           (ZombiesAtEnd startCapacity:1000 originalSize:1000, methodArgument:800) |  6,029.7 us |   120.307 us |   329.339 us |  5,949.9 us |
| **ResizeNew** |        **(ZombiesAtEnd startCapacity:10000 originalSize:10000, methodArgument:8000)** | **60,079.7 us** | **1,246.648 us** | **2,986.888 us** | **59,588.7 us** |
| ResizeOld |        (ZombiesAtEnd startCapacity:10000 originalSize:10000, methodArgument:8000) | 62,447.3 us | 1,877.450 us | 5,264.571 us | 61,223.1 us |
| **ResizeNew** |              **(ZombiesAtEnd startCapacity:200 originalSize:100, methodArgument:80)** |    **641.4 us** |    **13.956 us** |    **38.672 us** |    **632.0 us** |
| ResizeOld |              (ZombiesAtEnd startCapacity:200 originalSize:100, methodArgument:80) |    653.5 us |    18.961 us |    54.404 us |    633.8 us |
| **ResizeNew** |           **(ZombiesAtEnd startCapacity:2000 originalSize:1000, methodArgument:800)** | **10,053.7 us** |   **472.149 us** | **1,392.142 us** | **10,103.6 us** |
| ResizeOld |           (ZombiesAtEnd startCapacity:2000 originalSize:1000, methodArgument:800) | 10,570.4 us |   604.229 us | 1,781.584 us | 10,269.5 us |
| **ResizeNew** |        **(ZombiesAtEnd startCapacity:20000 originalSize:10000, methodArgument:8000)** | **61,225.7 us** | **1,816.456 us** | **1,516.822 us** | **60,974.9 us** |
| ResizeOld |        (ZombiesAtEnd startCapacity:20000 originalSize:10000, methodArgument:8000) | 60,770.7 us | 1,212.392 us | 3,063.870 us | 60,143.9 us |
| **ResizeNew** |              **(ZombiesAtFront startCapacity:0 originalSize:100, methodArgument:80)** |    **649.4 us** |    **18.276 us** |    **52.141 us** |    **632.9 us** |
| ResizeOld |              (ZombiesAtFront startCapacity:0 originalSize:100, methodArgument:80) |    644.0 us |    17.494 us |    50.475 us |    629.5 us |
| **ResizeNew** |            **(ZombiesAtFront startCapacity:0 originalSize:1000, methodArgument:800)** |  **5,982.7 us** |   **151.418 us** |   **424.593 us** |  **5,844.1 us** |
| ResizeOld |            (ZombiesAtFront startCapacity:0 originalSize:1000, methodArgument:800) |  6,154.8 us |   180.641 us |   518.292 us |  6,013.9 us |
| **ResizeNew** |          **(ZombiesAtFront startCapacity:0 originalSize:10000, methodArgument:8000)** | **60,095.1 us** | **1,197.316 us** | **2,128.229 us** | **59,808.4 us** |
| ResizeOld |          (ZombiesAtFront startCapacity:0 originalSize:10000, methodArgument:8000) | 60,414.9 us | 1,397.292 us | 2,048.133 us | 60,403.5 us |
| **ResizeNew** |            **(ZombiesAtFront startCapacity:100 originalSize:100, methodArgument:80)** |    **648.7 us** |    **15.944 us** |    **45.490 us** |    **633.8 us** |
| ResizeOld |            (ZombiesAtFront startCapacity:100 originalSize:100, methodArgument:80) |    621.5 us |    15.594 us |    41.353 us |    613.6 us |
| **ResizeNew** |         **(ZombiesAtFront startCapacity:1000 originalSize:1000, methodArgument:800)** |  **6,348.9 us** |   **200.994 us** |   **576.689 us** |  **6,186.5 us** |
| ResizeOld |         (ZombiesAtFront startCapacity:1000 originalSize:1000, methodArgument:800) |  6,186.6 us |   135.416 us |   386.351 us |  6,119.8 us |
| **ResizeNew** |      **(ZombiesAtFront startCapacity:10000 originalSize:10000, methodArgument:8000)** | **61,666.1 us** | **1,222.307 us** | **1,938.708 us** | **62,204.7 us** |
| ResizeOld |      (ZombiesAtFront startCapacity:10000 originalSize:10000, methodArgument:8000) | 60,456.6 us | 1,173.109 us | 1,719.528 us | 60,236.7 us |
| **ResizeNew** |            **(ZombiesAtFront startCapacity:200 originalSize:100, methodArgument:80)** |    **640.1 us** |    **15.393 us** |    **43.667 us** |    **627.5 us** |
| ResizeOld |            (ZombiesAtFront startCapacity:200 originalSize:100, methodArgument:80) |    658.2 us |    14.913 us |    42.548 us |    648.8 us |
| **ResizeNew** |         **(ZombiesAtFront startCapacity:2000 originalSize:1000, methodArgument:800)** |  **6,134.1 us** |   **146.622 us** |   **423.039 us** |  **6,034.5 us** |
| ResizeOld |         (ZombiesAtFront startCapacity:2000 originalSize:1000, methodArgument:800) |  5,807.3 us |   116.991 us |   333.781 us |  5,779.1 us |
| **ResizeNew** |      **(ZombiesAtFront startCapacity:20000 originalSize:10000, methodArgument:8000)** | **60,860.0 us** | **1,248.826 us** | **2,187.218 us** | **60,718.4 us** |
| ResizeOld |      (ZombiesAtFront startCapacity:20000 originalSize:10000, methodArgument:8000) | 59,657.3 us | 1,174.992 us | 1,722.289 us | 60,031.3 us |
