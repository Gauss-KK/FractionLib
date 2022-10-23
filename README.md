# FractionLib

# knumerics 

## MyFraction

### Examples


``` C#

usimg knumerics;

.............
.............

    MyFraction c3 = new MyFraction(3E200);
    MyFraction c2 = new MyFraction(2E200);
    MyFraction c1 = new MyFraction(1E200);
    Console.WriteLine("c3 = {0}", c3);
    Console.WriteLine("c2 = {0}", c2);
    Console.WriteLine("c1 = {0}", c1);
    Console.WriteLine("c3 - c2 - c1 = {0}", c3 - c2 - c1);

    MyFraction d3 = new MyFraction(0.3E-100);
    MyFraction d2 = new MyFraction(0.2E-100);
    MyFraction d1 = new MyFraction(0.1E-100);
    Console.WriteLine("d3 = {0}", d3);
    Console.WriteLine("d2 = {0}", d2);
    Console.WriteLine("d1 = {0}", d1);
    Console.WriteLine("d3 - d2 - d1 = {0}", d3 - d2 - d1);
```



#### Compare to a usual csharp coding:
``` C#
    double c3 = 3E200;
    double c2 = 2E200;
    double c1 = 1E200;
    Console.WriteLine("c3 = {0}", c3);
    Console.WriteLine("c2 = {0}", c2);
    Console.WriteLine("c1 = {0}", c1);
    Console.WriteLine("c3 - c2 - c1 = {0}", c3 - c2 - c1);

    double d3 = 0.3E-100;
    double d2 = 0.2E-100;
    double d1 = 0.1E-100;
    Console.WriteLine("d3 = {0}", d3);
    Console.WriteLine("d2 = {0}", d2);
    Console.WriteLine("d1 = {0}", d1);
    Console.WriteLine("d3 - d2 - d1 = {0}", d3 - d2 - d1);
```




#### Example using string interpolation of MyFraction:
``` C#
    MyFraction f = new MyFraction(1.2345E-100);
    Console.Write("f = {0:e30}\n", f);
    Console.Write($"-f = {-f:e30}\n");

    MyFraction g = new MyFraction("123456.78E+095", "200");    // Create fractio from string
    Console.Write("g = {0:E40}\n", g);
    Console.Write("g = {0:f10}\n", g);
    Console.Write("g = {0:g20}\n", g);
    Console.Write("g = {0:M}\n", g);   .. fraction of mixed form
    Console.Write("g = {0}\n", g);     // Usual fraction
```
