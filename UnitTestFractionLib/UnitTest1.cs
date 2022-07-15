using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using knumerics;

namespace UnitTestFractionLib
{
    [TestClass]
    public class UnitTestMyFraction
    {
        [TestMethod]
        public void TestCreator()
        {
            MyFraction f = new MyFraction(20, 12);
            MyFraction g = new MyFraction(new BigInteger(120), new BigInteger(4));

            Assert.AreEqual(f.ToString(), "5/3");
            Assert.AreEqual(g.ToString(), "30");
            Assert.AreEqual((f + g).ToString(), "95/3");
            Assert.AreEqual((f - g).ToString(), "-85/3");
            Assert.AreEqual((f * g).ToString(), "50");
            Assert.AreEqual((f / g).ToString(), "1/18");
            Assert.AreEqual((f % g).ToString(), "5/3");

            Assert.AreEqual(MyFraction.intDiv(f, g).ToString(), "0");
            Assert.AreEqual((MyFraction.intDiv(f, g) * g).ToString(), "0");
            Assert.AreEqual((MyFraction.intDiv(f, g) * g + (f % g)).ToString(), "5/3");

            Assert.AreEqual((g % f).ToString(), "0");
            Assert.AreEqual(MyFraction.intDiv(g, f).ToString(), "18");
            Assert.AreEqual((MyFraction.intDiv(g, f) * f).ToString(), "30");
            Assert.AreEqual((MyFraction.intDiv(g, f) * f + (g % f)).ToString(), "30");
        }

        [TestMethod]
        public void TestDivRem()
        {
            MyFraction f = new MyFraction(20, 12);
            MyFraction g = new MyFraction(new BigInteger(120), new BigInteger(4));


            Assert.AreEqual(f.ToString(), "5/3");
            Assert.AreEqual(g.ToString(), "30");

            MyFraction q, r = new MyFraction(0, 1);
            q = MyFraction.divRem(ref r, f, g); ;

            Assert.AreEqual(q.ToString(), "0");
            Assert.AreEqual(r.ToString(), "5/3");

            MyFraction q2, r2 = new MyFraction(0, 1);
            q2 = MyFraction.divRem(ref r2, g, f); ;

            Assert.AreEqual(q2.ToString(), "18");
            Assert.AreEqual(r2.ToString(), "0");
        }

        [TestMethod]
        public void TestDouble()
        {
            MyFraction c3 = new MyFraction(3E200);
            MyFraction c2 = new MyFraction(2E200);
            MyFraction c1 = new MyFraction(1E200);

            Assert.AreEqual(c3.ToString(), "300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            Assert.AreEqual(c2.ToString(), "200000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            Assert.AreEqual(c1.ToString(), "100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            Assert.AreEqual((c3 - c2 - c1).ToString(), "0");


            MyFraction d3 = MyFraction.convertFromDouble(0.3E-100);
            MyFraction d2 = MyFraction.convertFromDouble(0.2E-100);
            MyFraction d1 = MyFraction.convertFromDouble(0.1E-100);

            Assert.AreEqual(d3.ToString(), "3/100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            Assert.AreEqual(d2.ToString(), "1/50000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            Assert.AreEqual(d1.ToString(), "1/100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            Assert.AreEqual((d3 - d2 - d1).ToString(), "0");
        }

        [TestMethod]
        public void TestRound()
        {
            MyFraction f = new MyFraction(17, 200);

            Assert.AreEqual(f.ToString(), "17/200");

            Assert.AreEqual(f.floor().ToString(), "0");
            Assert.AreEqual(f.floor(2).ToString(), "2/25");
            Assert.AreEqual(f.ceil().ToString(), "1");
            Assert.AreEqual(f.ceil(2).ToString(), "9/100");
            Assert.AreEqual(f.round().ToString(), "0");
            Assert.AreEqual(f.round(2).ToString(), "9/100");

            Assert.AreEqual(f.round(2).toDecimalString(40).ToString(), "0.0900000000000000000000000000000000000000");
            Assert.AreEqual((f * new MyFraction(1000000, 1)).round(-4).toDecimalString(10), "90000");
            Assert.AreEqual((f * new MyFraction(1000000, 1)).floor(-4).toDecimalString(10), "80000");
            Assert.AreEqual((f * new MyFraction(1000000, 1)).ceil(-4).toDecimalString(10), "90000");
            Assert.AreEqual((f * new MyFraction(1000000, 1)).trunc(-4).toDecimalString(10), "80000");

            Assert.AreEqual(f.trunc().ToString(), "0");
            Assert.AreEqual(f.trunc(2).ToString(), "2/25");

            Assert.AreEqual(f.intPart().ToString(), "0");
            Assert.AreEqual(f.fracPart().ToString(), "17/200");

            Assert.AreEqual(f.toDecimalString().ToString(), "0.08500000000000000000");
            Assert.AreEqual(f.toDecimalString(40).ToString(), "0.0850000000000000000000000000000000000000");
        }


        [TestMethod]
        public void TestNegate()
        {
            MyFraction f = new MyFraction(17, 200);

            Assert.AreEqual(f.ToString(), "17/200");
            Assert.AreEqual((-f).ToString(), "-17/200");

            Assert.AreEqual((-f).floor().ToString(), "0");
            Assert.AreEqual((-f).floor(2).ToString(), "-9/100");
            Assert.AreEqual((-f).ceil().ToString(), "0");
            Assert.AreEqual((-f).ceil(2).ToString(), "-2/25");
            Assert.AreEqual((-f).round().ToString(), "0");
            Assert.AreEqual((-f).round(2).ToString(), "-2/25");

            Assert.AreEqual((-f).round(2).toDecimalString(40).ToString(), "-0.0800000000000000000000000000000000000000");
            Assert.AreEqual(((-f) * new MyFraction(1000000, 1)).round(-4).toDecimalString(10), "-80000");
            Assert.AreEqual(((-f) * new MyFraction(1000000, 1)).floor(-4).toDecimalString(10), "-90000");
            Assert.AreEqual(((-f) * new MyFraction(1000000, 1)).ceil(-4).toDecimalString(10), "-80000");
            Assert.AreEqual(((-f) * new MyFraction(1000000, 1)).trunc(-4).toDecimalString(10), "-80000");

            Assert.AreEqual((-f).trunc().ToString(), "0");
            Assert.AreEqual((-f).trunc(2).ToString(), "-2/25");

            Assert.AreEqual((-f).intPart().ToString(), "-1");
            Assert.AreEqual((-f).fracPart().ToString(), "183/200");

            Assert.AreEqual((-f).toDecimalString(50).ToString(), "-0.08500000000000000000000000000000000000000000000000");
            Assert.AreEqual((-f).toDecimalString().ToString(), "-0.08500000000000000000");
        }


        [TestMethod]
        public void TestNaN()
        {
            MyFraction f1 = new MyFraction(double.NaN);
            MyFraction f2 = new MyFraction(0, 0);
            MyFraction f3 = new MyFraction(0, 1) / new MyFraction(0, 1);
            MyFraction f4 = new MyFraction(0, 1) * new MyFraction(-1, 0);
            MyFraction f5 = new MyFraction(1, 0) - new MyFraction(1, 0);
            MyFraction f6 = new MyFraction(-1, 0) - new MyFraction(-1, 0);
            MyFraction f7 = new MyFraction(1, 0) + new MyFraction(-1, 0);

            Assert.AreEqual(f1.ToString(), "Intertermined (0/0)");
            Assert.AreEqual(f2.ToString(), "Intertermined (0/0)");
            Assert.AreEqual(f3.ToString(), "Intertermined (0/0)");
            Assert.AreEqual(f4.ToString(), "Intertermined (0/0)");
            Assert.AreEqual(f5.ToString(), "Intertermined (0/0)");
            Assert.AreEqual(f6.ToString(), "Intertermined (0/0)");
            Assert.AreEqual(f7.ToString(), "Intertermined (0/0)");

            Assert.AreEqual(f1.toDecimalString(), "NaN");
            Assert.AreEqual(f2.toDecimalString(), "NaN");
            Assert.AreEqual(f3.toDecimalString(), "NaN");
            Assert.AreEqual(f4.toDecimalString(), "NaN");
            Assert.AreEqual(f5.toDecimalString(), "NaN");
            Assert.AreEqual(f6.toDecimalString(), "NaN");
            Assert.AreEqual(f7.toDecimalString(), "NaN");
        }

        [TestMethod]
        public void TestInfinity()
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

            Assert.AreEqual(f1.ToString(), "Infinity");
            Assert.AreEqual(f2.ToString(), "-Infinity");
            Assert.AreEqual(f3.ToString(), "Infinity");
            Assert.AreEqual(f4.ToString(), "-Infinity");
            Assert.AreEqual(f5.ToString(), "0");
            Assert.AreEqual(f6.ToString(), "-0");
            Assert.AreEqual(f7.ToString(), "Infinity");
            Assert.AreEqual(f8.ToString(), "-Infinity");
            Assert.AreEqual(f9.ToString(), "Infinity");
            Assert.AreEqual(f10.ToString(), "-Infinity");
            Assert.AreEqual(f11.ToString(), "-Infinity");
            Assert.AreEqual(f12.ToString(), "Intertermined (0/0)"); ;
            Assert.AreEqual(f13.ToString(), "-Infinity");

            Assert.AreEqual(f1.toDecimalString(), "Infinity");
            Assert.AreEqual(f2.toDecimalString(), "-Infinity");
            Assert.AreEqual(f3.toDecimalString(), "Infinity");
            Assert.AreEqual(f4.toDecimalString(), "-Infinity");
            Assert.AreEqual(f5.toDecimalString(), "0");
            Assert.AreEqual(f6.toDecimalString(), "-0");
            Assert.AreEqual(f7.toDecimalString(), "Infinity");
            Assert.AreEqual(f8.toDecimalString(), "-Infinity");
            Assert.AreEqual(f9.toDecimalString(), "Infinity");
            Assert.AreEqual(f10.toDecimalString(), "-Infinity");
            Assert.AreEqual(f11.toDecimalString(), "-Infinity");
            Assert.AreEqual(f12.toDecimalString(), "NaN"); ;
            Assert.AreEqual(f13.toDecimalString(), "-Infinity");
        }

        [TestMethod]
        public void TestSqrt()
        {
            MyFraction g2 = new MyFraction(Math.Sqrt(2));

            Assert.AreEqual(g2.ToString(), "2486151499/1757974584");
            Assert.AreEqual(g2.toDecimalString(), "1.41421356237309515050");
            Assert.AreEqual(Math.Sqrt(2).ToString(), "1.4142135623731");
        }

    }
}
