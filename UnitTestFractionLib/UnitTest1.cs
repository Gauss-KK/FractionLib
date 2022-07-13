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

            MyFraction q, r;
            (q, r) = MyFraction.divRem(f, g); ;

            Assert.AreEqual(q.ToString(), "1");
            Assert.AreEqual(r.ToString(), "-85/3");

            MyFraction q2, r2;
            (q2, r2) = MyFraction.divRem(g, f); ;

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
        public void TestSqrt()
        {
            MyFraction g2 = new MyFraction(Math.Sqrt(2));

            Assert.AreEqual(g2.ToString(), "2486151499/1757974584");
            Assert.AreEqual(g2.toDecimalString(), "1.41421356237309515050");
            Assert.AreEqual(Math.Sqrt(2).ToString(), "1.4142135623731");
        }

    }
}