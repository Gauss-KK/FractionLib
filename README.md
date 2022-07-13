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



#### Copare To a usual coding:
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
