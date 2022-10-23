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
        static void Test_Infinity()
        {
            MyFraction f1 = new MyFraction(1.0 / 0.0);
            MyFraction f2 = new MyFraction(-1.0 / 0.0);
            MyFraction f3 = new MyFraction(1, 0);
            MyFraction f4 = new MyFraction(-1, 0);
            MyFraction f5 = new MyFraction(2, 3) / new MyFraction(1, 0);
            MyFraction f6 = new MyFraction(-9, 10) / new MyFraction(1, 0);
            MyFraction f7 = new MyFraction(1, 4) * new MyFraction(1, 0);
            MyFraction f8 = new MyFraction(100, 1) * new MyFraction(-1, 0);
            MyFraction f9 = new MyFraction(1, 0) + new MyFraction(1, 0);
            MyFraction f10 = new MyFraction(3, 0) * new MyFraction(-5, 0);
            MyFraction f11 = new MyFraction(30, 0) * new MyFraction(-500, 0);
            MyFraction f12 = new MyFraction(0, -2) * new MyFraction(-5, 0);
            MyFraction f13 = new MyFraction(0, -2) + new MyFraction(-5, 0);

            Console.WriteLine("f1 = {0}", f1);
            Console.WriteLine("f2 = {0}", f2);
            Console.WriteLine("f3 = {0}", f3);
            Console.WriteLine("f4 = {0}", f4);
            Console.WriteLine("f5 = {0}", f5);
            Console.WriteLine("f6 = {0}", f6);
            Console.WriteLine("f7 = {0}", f7);
            Console.WriteLine("f8 = {0}", f8);
            Console.WriteLine("f9 = {0}", f9);
            Console.WriteLine("f10 = {0}", f10);
            Console.WriteLine("f11 = {0}", f11);
            Console.WriteLine("f12 = {0}", f12);
            Console.WriteLine("f13 = {0}", f13);
            Console.WriteLine();

            Console.WriteLine("As a dcimal expression, f1 = {0}", f1.ToDecimalString());
            Console.WriteLine("As a dcimal expression, f2 = {0}", f2.ToDecimalString());
            Console.WriteLine("As a dcimal expression, f3 = {0}", f3.ToDecimalString());
            Console.WriteLine("As a dcimal expression, f4 = {0}", f4.ToDecimalString());
            Console.WriteLine("As a dcimal expression, f5 = {0}", f5.ToDecimalString());
            Console.WriteLine("As a dcimal expression, f6 = {0}", f6.ToDecimalString());
            Console.WriteLine("As a dcimal expression, f7 = {0}", f7.ToDecimalString());
            Console.WriteLine("As a dcimal expression, f8 = {0}", f8.ToDecimalString());
            Console.WriteLine("As a dcimal expression, f8 = {0}", f9.ToDecimalString());
            Console.WriteLine("As a dcimal expression, f10 = {0}", f10.ToDecimalString());
            Console.WriteLine("As a dcimal expression, f11 = {0}", f11.ToDecimalString());
            Console.WriteLine("As a dcimal expression, f12 = {0}", f12.ToDecimalString());
            Console.WriteLine("As a dcimal expression, f13 = {0}", f13.ToDecimalString());
            Console.WriteLine();
        }

        static void Test_ToDecimalString()
        {
            MyFraction f = new MyFraction(1.9999);
            Console.WriteLine("f = {0} = {1} = {2}", f, f.ToMixedForm(), f.ToDecimalString(7));
            Console.WriteLine("-f = {0} = {1} = {2}", -f, (-f).ToMixedForm(), (-f).ToDecimalString(7));
            Console.WriteLine("f = {0} = {1} = {2}", f, f.ToMixedForm(), f.ToDecimalString(3));
            Console.WriteLine("-f = {0} = {1} = {2}", -f, (-f).ToMixedForm(), (-f).ToDecimalString(3));
            Console.WriteLine();

            MyFraction g = new MyFraction(1.9995);
            Console.WriteLine("g = {0} = {1} = {2}", g, g.ToMixedForm(), g.ToDecimalString(7));
            Console.WriteLine("-g = {0} = {1} = {2}", -g, (-g).ToMixedForm(), (-g).ToDecimalString(7));
            Console.WriteLine("g = {0} = {1} = {2}", g, g.ToMixedForm(), g.ToDecimalString(3));
            Console.WriteLine("-g = {0} = {1} = {2}", -g, (-g).ToMixedForm(), (-g).ToDecimalString(3));
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            MyFraction f = new MyFraction(20, 12);
            MyFraction g = new MyFraction(new BigInteger(120), new BigInteger(4));
            Console.WriteLine("Let");
            Console.WriteLine("  f = {0}", f);
            Console.WriteLine("  g = {0}", g);
            Console.WriteLine();
            Console.WriteLine("Thn we have");
            Console.WriteLine("  f + g = {0}", f + g);
            Console.WriteLine("  f - g = {0}", f - g);
            Console.WriteLine("  f * g = {0}", f * g);
            Console.WriteLine("  f / g = {0}", f / g);
            Console.WriteLine("  f % g = {0}", f % g);
            Console.WriteLine("  MyFraction.intDiv(f, g) = {0}", MyFraction.intDiv(f, g));
            Console.WriteLine("  MyFraction.intDiv(f, g)*g = {0}", MyFraction.intDiv(f, g) * g);
            Console.WriteLine("  MyFraction.intDiv(f, g)*g + (f % g) = {0}", MyFraction.intDiv(f, g) * g + (f % g));
            Console.WriteLine("and");
            Console.WriteLine("  g + f = {0}", g + f);
            Console.WriteLine("  g - f = {0}", g - f);
            Console.WriteLine("  g * f = {0}", g * f);
            Console.WriteLine("  g / f = {0}", g / f);
            Console.WriteLine("  g % f = {0}", g % f);
            Console.WriteLine("  MyFraction.intDiv(g, f) = {0}", MyFraction.intDiv(g, f));
            Console.WriteLine("  MyFraction.intDiv(g, f)*f = {0}", MyFraction.intDiv(g, f) * f);
            Console.WriteLine("  MyFraction.intDiv(g, f)*f + (g % f) = {0}", MyFraction.intDiv(g, f) * f + (g % f));
            Console.WriteLine();

            g = new MyFraction(0, 1) - g;

            Console.WriteLine("Let");
            Console.WriteLine("  f = {0}", f);
            Console.WriteLine("  g = {0}", g);
            Console.WriteLine("Then we get");
            Console.WriteLine("    f / g = {0}", f / g);
            MyFraction q, r = new MyFraction(0, 1);
            q = MyFraction.divRem(ref r, f, g); ;
            Console.WriteLine("And, after trying q = divRem(ref r, f, g), we get");
            Console.WriteLine("     Quptient: q = {0}", q);
            Console.WriteLine("    Remainder: r = {0}", r);
            Console.WriteLine("Check Euclidean algorithm for divison: f / g");
            Console.WriteLine("Checking: g*q + r = {0}, f = {1}", g * q + r, f);
            Console.WriteLine("Checking: r >= 0 ? {0}", r >= new MyFraction(0, 1));
            Console.WriteLine("Checking: r < g,abs() ? {0}", r < g.abs());
            Console.WriteLine("Checking: g,abs() = {0}", g.abs());
            Console.WriteLine();

            Console.WriteLine("f = {0}", f);
            Console.WriteLine("g = {0}", g);
            Console.WriteLine("g / f = {0}", g / f);
            MyFraction q2, r2 = new MyFraction(0, 1); ;
            Console.WriteLine("And, after trying 2 = divRem(ref r2, g, f), we get");
            q2 = MyFraction.divRem(ref r2, g, f); ;
            Console.WriteLine("     Quptient: q2 = {0}", q2);
            Console.WriteLine("    Remainder: r2 = {0}", r2);
            Console.WriteLine("Check Euclidean algorithm for divison: g / f");
            Console.WriteLine("Checking: f*q2 + r2 = {0}, g = {1}", f * q2 + r2, g);
            Console.WriteLine("Checking: r2 >= 0 ? {0}", r2 >= new MyFraction(0, 1));
            Console.WriteLine("Checking: r2 < f,abs() ? {0}", r2 < f.abs());
            Console.WriteLine("Checking: f,abs() = {0}", f.abs());
            Console.WriteLine();


            MyFraction c3 = new MyFraction(3E200d);
            // MyFraction c3 = new MyFraction((double) 3);
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
            Console.WriteLine("  f.round(2).toDecimalString(40)) = {0}", f.round(2).ToDecimalString(40));
            Console.WriteLine("  (f * new MyFraction(1000000, 1)).round(-4).toDecimalString(10)) = {0}", (f * new MyFraction(1000000, 1)).round(-4).ToDecimalString(10));
            Console.WriteLine("  (f * new MyFraction(1000000, 1)).floor(-4).toDecimalString(10)) = {0}", (f * new MyFraction(1000000, 1)).floor(-4).ToDecimalString(10));
            Console.WriteLine("  (f * new MyFraction(1000000, 1)).ceil(-4).toDecimalString(10)) = {0}", (f * new MyFraction(1000000, 1)).ceil(-4).ToDecimalString(10));
            Console.WriteLine("  (f * new MyFraction(1000000, 1)).trunc(-4).toDecimalString(10)) = {0}", (f * new MyFraction(1000000, 1)).trunc(-4).ToDecimalString(10));
            Console.WriteLine("  f.trunc()) = {0}", f.trunc());
            Console.WriteLine("  f.trunc(2)) = {0}", f.trunc(2));
            Console.WriteLine("  f.intPart()) = {0}", f.intPart());
            Console.WriteLine("  f.fracPart()) = {0}", f.fracPart());
            Console.WriteLine("  f.toDecimalString(40) = {0}", f.ToDecimalString(40));
            Console.WriteLine("  f.toDecimalString() = {0}", f.ToDecimalString());
            Console.WriteLine();

            Console.WriteLine("-f = {0}", -f);
            Console.WriteLine("  (-f).floor() = {0}", (-f).floor());
            Console.WriteLine("  (-f).floor(2) = {0}", (-f).floor(2));
            Console.WriteLine("  (-f).ceil()) = {0}", (-f).ceil());
            Console.WriteLine("  (-f).ceil(2)) = {0}", (-f).ceil(2));
            Console.WriteLine("  (-f).round()) = {0}", (-f).round());
            Console.WriteLine("  (-f).round(2)) = {0}", (-f).round(2));
            Console.WriteLine("  (-f).round(2).toDecimalString(50)) = {0}", (-f).round(2).ToDecimalString(50));
            Console.WriteLine("  ((-f) * new MyFraction(1000000, 1)).round(-4).toDecimalString(10)) = {0}", ((-f) * new MyFraction(1000000, 1)).round(-4).ToDecimalString(10));
            Console.WriteLine("  ((-f) * new MyFraction(1000000, 1)).floor(-4).toDecimalString(10)) = {0}", ((-f) * new MyFraction(1000000, 1)).floor(-4).ToDecimalString(10));
            Console.WriteLine("  ((-f) * new MyFraction(1000000, 1)).ceil(-4).toDecimalString(10)) = {0}", ((-f) * new MyFraction(1000000, 1)).ceil(-4).ToDecimalString(10));
            Console.WriteLine("  ((-f) * new MyFraction(1000000, 1)).trunc(-4).toDecimalString(10)) = {0}", ((-f) * new MyFraction(1000000, 1)).trunc(-4).ToDecimalString(10));
            Console.WriteLine("  (-f).trunc()) = {0}", (-f).trunc());
            Console.WriteLine("  (-f).trunc(2)) = {0}", (-f).trunc(2));
            Console.WriteLine("  (-f).intPart()) = {0}", (-f).intPart());
            Console.WriteLine("  (-f).fracPart()) = {0}", (-f).fracPart());
            Console.WriteLine("  (-f).toDecimalString(50) = {0}", (-f).ToDecimalString(50));
            Console.WriteLine("  (-f).toDecimalString() = {0}", (-f).ToDecimalString());
            Console.WriteLine();

            MyFraction g2 = new MyFraction(Math.Sqrt(2));
            Console.WriteLine("g2 = {0}", g2);
            Console.WriteLine("g2.toDecimalString() = {0}", g2.ToDecimalString());
            Console.WriteLine("Math.Sqrt(2) = {0}", Math.Sqrt(2));
            Console.WriteLine();

            MyFraction f1 = new MyFraction(double.NaN);
            MyFraction f2 = new MyFraction(0, 0);
            MyFraction f3 = new MyFraction(0, 1) / new MyFraction(0, 1);
            MyFraction f4 = new MyFraction(0, 1) * new MyFraction(-1, 0);
            MyFraction f5 = new MyFraction(1, 0) - new MyFraction(1, 0);
            MyFraction f6 = new MyFraction(-1, 0) - new MyFraction(-1, 0);
            MyFraction f7 = new MyFraction(1, 0) + new MyFraction(-1, 0);

            Console.WriteLine("f1 = {0}", f1);
            Console.WriteLine("f2 = {0}", f2);
            Console.WriteLine("f3 = {0}", f3);
            Console.WriteLine("f4 = {0}", f4);
            Console.WriteLine("f5 = {0}", f5);
            Console.WriteLine("f6 = {0}", f6);
            Console.WriteLine("f7 = {0}", f7);
            Console.WriteLine();

            Console.WriteLine("As a decimal expression, f1 = {0}", f1.ToDecimalString());
            Console.WriteLine("As a decimal expression, f2 = {0}", f2.ToDecimalString());
            Console.WriteLine("As a decimal expression, f3 = {0}", f3.ToDecimalString());
            Console.WriteLine("As a decimal expression, f4 = {0}", f4.ToDecimalString());
            Console.WriteLine("As a decimal expression, f5 = {0}", f5.ToDecimalString());
            Console.WriteLine("As a decimal expression, f6 = {0}", f6.ToDecimalString());
            Console.WriteLine("As a decimal expression, f7 = {0}", f7.ToDecimalString());
            Console.WriteLine();

            Test_Infinity();
            Console.WriteLine("0.3 - 0.2 - 0.1 = {0}", 0.3 - 0.2 - 0.1);   // Output is-2.77555756156289E-17
            Console.WriteLine();


            Test_ToDecimalString();


            MyFraction f14 = new MyFraction(-2.340001E+5);
            Console.Write("f14 = {0} = {1} = {2}\n", f14, f14.ToDecimalString(), f14.ToDecimalString(40));
            Console.WriteLine("");

            MyFraction g14 = new MyFraction(-2.3400010059056789E-5);
            Console.Write("g14 = {0} = {1}\n", g14, g14.ToDecimalString(40));
            Console.Write("g14 = {0} = {1} = {2}\n", g14, g14.ToDecimalString(), g14.ToDecimalString(40));
            Console.WriteLine("");

            MyFraction g15 = new MyFraction("-23400015", "1000000000000");
            Console.Write("g15 = {0} = {1}\n", g15, g15.ToDecimalString(40));
            Console.Write("g15 = {0} = {1} = {2}\n", g15, g15.ToDecimalString(), g15.ToDecimalString(40));
            Console.WriteLine("");

            MyFraction f15 = new MyFraction(1.0 / 7 * 1E+30);
            Console.Write("f15 = {0} = {1}\n", f15, f15.ToDecimalString(40));
            Console.Write("f15 = {0} = {1} = {2}\n", f15, f15.ToDecimalString(), f15.ToDecimalString(40));
            Console.WriteLine("");


            MyFraction f16 = new MyFraction(1.2345E+100);
            Console.Write("f16 = {0:e30}\n", f16);
            Console.Write("-f16 = {0:e30}\n", -f16);
            Console.Write("f16 = {0} = {1}\n", f16, f16.ToDecimalString(40));
            Console.Write("f16 = {0} = {1} = {2}\n", f16, f16.ToDecimalString(), f16.ToDecimalString(40));
            Console.Write("(double) {0} - {1:F0} = {2}\n", f16, 1.2345E+100, (double) f16 - 1.2345E+100);
            Console.Write("(double) {0} == {1} ? {2}\n", (double) f16, 1.2345E+100, (double)f16 == 1.2345E+100);
            Console.WriteLine("");

            f16 = new MyFraction(0.1 + 0.2);
            Console.Write("f16 = 0.1 + 0.2 = {0} = {1}\n", f16, f16.ToDecimalString(30));
            Console.Write("f16 = 0.1 + 0.2 = {0} = {1} = {2}\n", f16, f16.ToDecimalString(), f16.ToDecimalString(30));
            Console.Write("(double) f16 - {0:G} = {1}\n", 0.3, (double)f16 - 0.3);
            Console.Write("(double) f16 == {0} ? {1}\n", 0.3, (double)f16 == 0.3);
            Console.Write("(double) f16 == 0.1 + 0.2 ? {0}\n", (double)f16 == 0.1 + 0.2);
            Console.Write("0.3 == 0.1 + 0.2 ? {0}\n", 0.3 == 0.1 + 0.2);
            Console.Write("0.3 - 0.1 - 0.2 == 0.0 ? {0}\n", 0.3 - 0.1 - 0.2 == 0.0);
            Console.Write("(double) f16 - 0.1 - 0.2 == 0.0 ? {0}\n", (double) f16 - 0.1 - 0.2 == 0.0);
            Console.Write("0.1 + 0.2 == {0:E20}\n", 0.1 + 0.2);
            Console.Write("0.3 == {0:E20}\n", 0.3);
            Console.Write("f16 == {0:E}\n", f16.ToDecimalString(20));
            Console.Write("f16 == {0}\n", f16);
            Console.Write("   f16 == {0:E}\n", f16);
            Console.Write("       f16 == {0:E30}\n", f16);
            Console.Write("       f16 == {0:e50}\n", f16);
            Console.WriteLine("");


            MyFraction f17 = new MyFraction(1.2345E-100);
            Console.Write("f17 = {0:e30}\n", f17);
            Console.Write($"-f17 = {-f17:e30}\n");
            f17 = new MyFraction(-1.2345E-100);
            Console.Write("f17 = {0:e30}\n", f17);
            Console.Write("f17.ToDecimalString() = {0}\n", f17.ToDecimalString());
            Console.Write("f17.ToString() = {0}\n", f17.ToString());
            Console.Write("f17 = {0:e30}\n", f17);
            Console.Write("f17/100 = {0:e30}\n", f17/new MyFraction(100));

            Console.Write("f17/1000 = {0:e30}\n", f17 / new MyFraction(1000));

            Console.Write("f17/10 = {0:e30}\n", f17 / new MyFraction(10));

            Console.Write("f17/1.0 = {0:e30}\n", f17 / new MyFraction(1.0));
            Console.Write("f17/0.1 = {0:e30}\n", f17 / new MyFraction(0.1));
            Console.Write("f17/0.01 = {0:e30}\n", f17 / new MyFraction(0.01));
            Console.Write("f17*100 = {0:e30}\n", f17 * new MyFraction(100));
            Console.Write("f17*10000000000 = {0:e30}\n", f17 * new MyFraction(10000000000));
            Console.Write("f17*10E96 = {0:e30}\n", f17 * new MyFraction(10E96));
            Console.Write("f17*10E97 = {0:e30}\n", f17 * new MyFraction(10E97));
            Console.Write("f17*10E98 = {0:e30}\n", f17 * new MyFraction(10E98));
            Console.Write("f17*10E99 = {0:e30}\n", f17 * new MyFraction(10E99));
            Console.Write("f17*10E100 = {0:e30}\n", f17 * new MyFraction(10E100));
            Console.Write("f17*1E99 = {0:e30}\n", f17 * new MyFraction(1E99));
            Console.Write("f17*1E100 = {0:e30}\n", f17 * new MyFraction(1E100));
            Console.Write("f17*-1E100 = {0:e30}\n", f17 * new MyFraction(-1E100));
            Console.Write("f17*-1E105 = {0:f20}\n", f17 * new MyFraction(-1E105));
            Console.Write("f17*-1E105 = {0:f20}\n", f17 * new MyFraction("-1E105"));
            Console.Write("f17*-1E105 = {0:f20}\n", (f17 * new MyFraction("-1E105")).ToDecimalString());
            Console.Write("f17*-1E105 = {0:e20}\n", f17 * new MyFraction("-1E105"));
            Console.Write("f17*-1E105 = {0:g20}\n", f17 * new MyFraction("-1E105"));
            Console.WriteLine();


            MyFraction f23 = new MyFraction("-23400015", "1000000000000");
            Console.WriteLine("f23.ToString() = {0}", f23.ToString());

            Console.Write("Press any key...");
            Console.ReadKey();
        }
    }
}