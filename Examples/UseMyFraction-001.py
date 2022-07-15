"""
// ////////////////////////////////////////////////////
// Filename: UseMyFraction-001.py
//
//       Get  FractionLib.dll at:  https:://github.com/Gauss-KK/FractiobLib
//
//       Require: NET Framework 4.8
//
//
// Compile: ipy UseMyFraction-001.py -r:FractionLib.dll
// Execute: UseMyFraction-001
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
// ////////////////////////////////////////////////////
"""

import clr

# clr.AddReferenceToFile("System.Numerics.dll")
clr.AddReferenceToFile("FractionLib.dll")

from System import *
# import System.Numerics
# from System.Numerics import *

from knumerics import *




if __name__ == "__main__":
    # Console.WriteLine("sign 5: {0}", (sign 5))
    a1 = MyFraction(17, 2)
    a2 = MyFraction(120, 100)

    print("Let")
    print("  a1 = %s" % a1.ToString())
    print("  a2 = %s"  % a2.ToString())
    print("")

    print("Then we get")
    print("  a1 + a2 = %s"  % (a1 + a2).ToString())
    print("  a1 - a2 = %s"  % (a1 - a2).ToString())
    print("  a1 * a2 = %s" % (a1 % a2).ToString())
    print("  a1 / a2 = %s" % (a1 / a2).ToString())
    print("  a1 %% a2 = %s" % (a1 % a2).ToString())
    print("  -a1 = %s" % (-a1).ToString())
    print("")

    print("  a1.getNum() = %s" % a1.getNum().ToString())
    print("  a1.getDec() = %s" % a1.getDen().ToString())
    print("")
    
    x = a1.getNum()
    y = a1.getDen()
    print("  x = %d and x.GetType() = %s" % (x, x.GetType()))
    print("  y = %d and y.GetType() = %s" % (y, y.GetType()))
    print("  %d + %d = %d" % (x, y, x + y))
    print("  %s + %s = %s" % (x, y, x + y))
    print("  %s * %s = %s" % (x + 1, y + 1, (x + 1)*(y + 1)))
    z = 12 + x
    print("  z = %d and z.GetType() = %s" % (z, z.GetType()))
    u  = 12
    print("  u = %d and u.GetType() = %s" % (u, z.GetType()))
    print("")
    
    c3 = MyFraction(3E-30)
    c2 = MyFraction(2E-30)
    c1 = MyFraction(1E-30)
    c0 = c3 - c2 - c1
    print("  c3 = %s"  % c3)
    print("  c2 = %s"  % c2)
    print("  c1 = %s" % c1)
    print("  c0 = c3  - c2 - c1 = %s" % c0)
    print("")

    print("  c3 = %s"  % c3.toDecimalString(50))
    print("  c2 = %s"  % c2.toDecimalString(50))
    print("  c1 = %s" % c1.toDecimalString(50))
    print("  c0 = c3  - c2 - c1 = %s" % c0.toDecimalString(40))
    print("")

    print("  c3 = c3.getNum() / c3.getDen() =  %d / %d"  % (c3.getNum(), c3.getDen()))
    print("  c2 = c2.getNum() / c2.getDen() =  %d / %d"  % (c2.getNum(), c2.getDen()))
    print("  c1 = c1.getNum() / c1.getDen() =  %d / %d"  % (c1.getNum(), c3.getDen()))
    print("  c0 = c0.getNum() / c0.getDen() =  %d / %d"  % (c0.getNum(), c0.getDen()))
    print("")

    Console.Write("Press any key...")
    Console.ReadKey()
