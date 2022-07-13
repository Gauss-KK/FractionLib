using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using knumerics;

namespace TestConsoleApp
{

    class Program
    {

        static void Main(string[] args)
        {
            MyFraction f = new MyFraction(20, 12);
            MyFraction g = new MyFraction(new BigInteger(120), new BigInteger(4));
            Console.WriteLine("f = {0}", f);
            Console.WriteLine("g = {0}", g);
            Console.WriteLine("  f + g = {0}", f + g);
            Console.WriteLine("  f - g = {0}", f - g);
            Console.WriteLine("  f * g = {0}", f * g);
            Console.WriteLine("  f / g = {0}", f / g);
            Console.WriteLine("  f % g = {0}", f % g);
            Console.WriteLine("  MyFraction.intDiv(f, g) = {0}", MyFraction.intDiv(f, g));
            Console.WriteLine("  MyFraction.intDiv(f, g)*g = {0}", MyFraction.intDiv(f, g) * g);
            Console.WriteLine("  MyFraction.intDiv(f, g)*g + (f % g)= {0}", MyFraction.intDiv(f, g) * g + (f % g));

            Console.WriteLine("  g % f = {0}", g % f);
            Console.WriteLine("  MyFraction.intDiv(g, f) = {0}", MyFraction.intDiv(g, f));
            Console.WriteLine("  MyFraction.intDiv(g, f)*f = {0}", MyFraction.intDiv(g, f) * f);
            Console.WriteLine("  MyFraction.intDiv(g, f)*f + (g % f) = {0}", MyFraction.intDiv(g, f) * f + (g % f));

            Console.WriteLine("f = {0}", f);
            Console.WriteLine("g = {0}", g);
            MyFraction q, r;
            (q, r) = MyFraction.divRem(f, g); ;
            Console.WriteLine("  q = {0}", q);
            Console.WriteLine("  r = {0}", r);

            Console.WriteLine("f = {0}", f);
            Console.WriteLine("g = {0}", g);
            MyFraction q2, r2;
            (q2, r2) = MyFraction.divRem(g, f); ;
            Console.WriteLine("  q2 = {0}", q2);
            Console.WriteLine("  r2 = {0}", r2);

            MyFraction c3 = new MyFraction(3E200);
            Console.WriteLine("c3 = {0}", c3);
            MyFraction c2 = new MyFraction(2E200);
            Console.WriteLine("c2 = {0}", c2);
            MyFraction c1 = new MyFraction(1E200);
            Console.WriteLine("c1 = {0}", c1);
            Console.WriteLine("c3 - c2 - c1 = {0}", c3 - c2 - c1);

            MyFraction d3 = MyFraction.convertFromDouble(0.3E-100);
            Console.WriteLine("d3 = {0}", d3);
            MyFraction d2 = MyFraction.convertFromDouble(0.2E-100);
            Console.WriteLine("d2 = {0}", d2);
            MyFraction d1 = MyFraction.convertFromDouble(0.1E-100);
            Console.WriteLine("d1 = {0}", d1);
            Console.WriteLine("d3 - d2 - d1 = {0}", d3 - d2 - d1);
            Console.WriteLine();

            f = new MyFraction(17, 200);
            Console.WriteLine("f = {0}", f);
            Console.WriteLine("  f.floor() = {0}", f.floor());
            Console.WriteLine("  f.floor(2) = {0}", f.floor(2));
            Console.WriteLine("  f.ceil()) = {0}", f.ceil());
            Console.WriteLine("  f.ceil(2)) = {0}", f.ceil(2));
            Console.WriteLine("  f.round()) = {0}", f.round());
            Console.WriteLine("  f.round(2)) = {0}", f.round(2));
            Console.WriteLine("  f.round(2).toDecimalString(40)) = {0}", f.round(2).toDecimalString(40));
            Console.WriteLine("  (f * new MyFraction(1000000, 1)).round(-4).toDecimalString(10)) = {0}", (f * new MyFraction(1000000, 1)).round(-4).toDecimalString(10));
            Console.WriteLine("  (f * new MyFraction(1000000, 1)).floor(-4).toDecimalString(10)) = {0}", (f * new MyFraction(1000000, 1)).floor(-4).toDecimalString(10));
            Console.WriteLine("  (f * new MyFraction(1000000, 1)).ceil(-4).toDecimalString(10)) = {0}", (f * new MyFraction(1000000, 1)).ceil(-4).toDecimalString(10));
            Console.WriteLine("  (f * new MyFraction(1000000, 1)).trunc(-4).toDecimalString(10)) = {0}", (f * new MyFraction(1000000, 1)).trunc(-4).toDecimalString(10));
            Console.WriteLine("  f.trunc()) = {0}", f.trunc());
            Console.WriteLine("  f.trunc(2)) = {0}", f.trunc(2));
            Console.WriteLine("  f.intPart()) = {0}", f.intPart());
            Console.WriteLine("  f.fracPart()) = {0}", f.fracPart());
            Console.WriteLine("  f.toDecimalString(40) = {0}", f.toDecimalString(40));
            Console.WriteLine("  f.toDecimalString() = {0}", f.toDecimalString());
            Console.WriteLine();

            Console.WriteLine("-f = {0}", -f);
            Console.WriteLine("  (-f).floor() = {0}", (-f).floor());
            Console.WriteLine("  (-f).floor(2) = {0}", (-f).floor(2));
            Console.WriteLine("  (-f).ceil()) = {0}", (-f).ceil());
            Console.WriteLine("  (-f).ceil(2)) = {0}", (-f).ceil(2));
            Console.WriteLine("  (-f).round()) = {0}", (-f).round());
            Console.WriteLine("  (-f).round(2)) = {0}", (-f).round(2));
            Console.WriteLine("  (-f).round(2).toDecimalString(50)) = {0}", (-f).round(2).toDecimalString(50));
            Console.WriteLine("  ((-f) * new MyFraction(1000000, 1)).round(-4).toDecimalString(10)) = {0}", ((-f) * new MyFraction(1000000, 1)).round(-4).toDecimalString(10));
            Console.WriteLine("  ((-f) * new MyFraction(1000000, 1)).floor(-4).toDecimalString(10)) = {0}", ((-f) * new MyFraction(1000000, 1)).floor(-4).toDecimalString(10));
            Console.WriteLine("  ((-f) * new MyFraction(1000000, 1)).ceil(-4).toDecimalString(10)) = {0}", ((-f) * new MyFraction(1000000, 1)).ceil(-4).toDecimalString(10));
            Console.WriteLine("  ((-f) * new MyFraction(1000000, 1)).trunc(-4).toDecimalString(10)) = {0}", ((-f) * new MyFraction(1000000, 1)).trunc(-4).toDecimalString(10));
            Console.WriteLine("  (-f).trunc()) = {0}", (-f).trunc());
            Console.WriteLine("  (-f).trunc(2)) = {0}", (-f).trunc(2));
            Console.WriteLine("  (-f).intPart()) = {0}", (-f).intPart());
            Console.WriteLine("  (-f).fracPart()) = {0}", (-f).fracPart());
            Console.WriteLine("  (-f).toDecimalString(50) = {0}", (-f).toDecimalString(50));
            Console.WriteLine("  (-f).toDecimalString() = {0}", (-f).toDecimalString());
            Console.WriteLine();

            MyFraction g2 = new MyFraction(Math.Sqrt(2));
            Console.WriteLine("g2 = {0}", g2);
            Console.WriteLine("g2.toDecimalString() = {0}", g2.toDecimalString());
            Console.WriteLine("Math.Sqrt(2) = {0}", Math.Sqrt(2));
            Console.WriteLine();

            Console.Write("Press any key...");
            Console.ReadKey();
        }
    }
}