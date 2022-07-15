//////////////////////////////////////////////////////
// Filename: UsingMyFraction-001.cs
//
//      Get  FractionLib.dll at:  https:://github.com/Gauss-KK/FractiobLib
//
//       Require: NET Framework 4.8
//
//
// Compile: csc UsingMyFraction-001.cs /r:System.Numerics.dll /r:FractionLib.dll
// Execute: UsingMyFraction-001
// Output:
//     Let
//        a1 = 17/2
//        a2 = 6/5
//
//     Then we get
//        a1 + a2 = 97/10
//        a1 - a2 = 73/10
//        a1 * a2 = 51/5
//        a1 / a2 = 85/12
//        a1 % a2 = 1/10
//        -a1 = -17/2
// 
//    Press any key...
//
//   Date: 2022.07.13
//////////////////////////////////////////////////////
	

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using knumerics;

namespace UseMyFraction
{
    class Program
    {
        static void Main(string[] args)
        {
            MyFraction a1 = new MyFraction(17, 2);
            MyFraction a2 = new MyFraction(120, 100);
            // Console.WriteLine("a1 = {0}", a1);
            
            Console.WriteLine("Let");
            Console.WriteLine($"  a1 = {a1}");
            Console.WriteLine($"  a2 = {a2}");
            Console.WriteLine();

           Console.WriteLine("Then we get");
            Console.WriteLine($"  a1 + a2 = {a1 + a2}");
            Console.WriteLine($"  a1 - a2 = {a1 - a2}");
            Console.WriteLine($"  a1 * a2 = {a1 * a2}");
            Console.WriteLine($"  a1 / a2 = {a1 / a2}");
            Console.WriteLine($"  a1 % a2 = {a1 % a2}");
            Console.WriteLine($"  -a1 = {-a1}");
            Console.WriteLine();

            Console.Write("Press any key...");
            Console.ReadKey();
        }

    }
}
