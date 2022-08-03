using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace knumerics

{
    public class MyFraction
    {
        private BigInteger m_num = 0;
        private BigInteger m_den = 1;

        public MyFraction() : this(0, 1)
        {
        }

        public BigInteger getNum() { return m_num; }
        public BigInteger getDen() { return m_den; }

        public MyFraction(int a, int b)
        {
            m_num = a;
            m_den = b;
            if (m_den < 0 && m_num != 0)
            {
                m_num = -m_num;
                m_den = -m_den;
            }
            BigInteger g = gcd(a, b);
            if (g > 1)
            {
                m_num /= g;
                m_den /= g;
            }
        }

        public MyFraction(BigInteger a, BigInteger b)
        {
            m_num = a;
            m_den = b;
            if (m_den < 0 && m_num != 0)
            {
                m_num = -m_num;
                m_den = -m_den;
            }
            BigInteger g = gcd(a, b);
            if (g > 1)
            {
                m_num /= g;
                m_den /= g;
            }
        }


        public MyFraction(double x0)
        {
            if (Double.IsNaN(x0))
            {
                m_num = 0;
                m_den = 0;
                return;
            }
            else if (Double.IsPositiveInfinity(x0))
            {
                m_num = 1;
                m_den = 0;
                return;
            }
            else if (Double.IsNegativeInfinity(x0))
            {
                m_num = -1;
                m_den = 0;
                return;
            }

            double x = x0;
            int sign = 1;
            if (x < 0)
            {
                x = -x;
                sign = -1;
            }

            double x1 = x;
            int np = (int)Math.Log10(x);

            if (np != 0)
            {
                x = x * Math.Pow(10.0, -np);
            }

            double bound = Math.Pow(2.0, 53);

            if (x == 0.0)
            {
                if (x0 > 0)
                {
                    m_num = 0;
                    m_den = 1;
                    return;
                }
                else if (x0 < 0)
                {
                    m_num = 0;
                    m_den = -1;
                    return;
                }
            }
            else if (x >= bound)
            {
                if (x0 > 0)
                    if (x0 > 0)
                    {
                        m_num = 1;
                        m_den = 0;
                        return;
                    }
                    else if (x0 < 0)
                    {
                        m_num = -1;
                        m_den = 0;
                        return;
                    }

            }
            else if (1.0 / x >= bound)
            {
                if (x0 > 0)
                {
                    m_num = 0;
                    m_den = 1;
                    return;
                }
                else if (x0 < 0)
                {
                    m_num = 0;
                    m_den = -1;
                    return;
                }
            }

            double q, r;
            q = Math.Floor(x);
            r = x - q;

            ArrayList conv = new ArrayList();
            conv.Add((long)q);

            int max_count = 25;
            int counter = 0;

            while (r > 0 && counter++ < max_count)
            {
                x = 1.0 / r;

                q = Math.Floor(x);
                if (q == 0 || r/q < 1.0E-14)
                {
                    break;
                }
                r = x - q;
                conv.Add((long)q);
            }

            if (conv.Count > 1 && (long)conv[conv.Count - 1] == 1)
            {
                conv[conv.Count - 2] = (long)conv[conv.Count - 2] + 1;
                conv.RemoveAt(conv.Count - 1);
            }

            long old_h = 0, h = 1;
            long old_k = 1, k = 0;
            long tmp;
            long q1;

            int ncount = conv.Count;
            for (int i = 0; i < ncount; i++)
            {
                q1 = (long)conv[i];

                tmp = h;
                h = old_h + h * q1;
                old_h = tmp;

                tmp = k;
                k = old_k + k * q1;
                old_k = tmp;

            }

            m_num = h;
            m_den = k;

            if (np > 0)
            {
                BigInteger tens = BigInteger.Pow(new BigInteger(10), np);
                m_num = m_num * tens;
            }
            else if (np < 0)
            {
                BigInteger tens = BigInteger.Pow(new BigInteger(10), -np);
                m_den = m_den * tens;
            }


            if (sign < 0)
            {
                m_num = -m_num;
            }
        }

        public MyFraction abs()
        {
            BigInteger a = getNum();
            BigInteger b = getDen();

            if (a < 0)
            {
                a = -a;
            }
            if (b < 0)
            {
                b = -b;
            }

            return new MyFraction(a, b);
        }

        public MyFraction negate()
        {
            BigInteger a = getNum();
            BigInteger b = getDen();

            if (a == 0)
            {
                b = -b;
                return new MyFraction(a, b);
            }

            if (a < 0)
            {
                a = -a;
            }
            if (b < 0)
            {
                b = -b;
            }

            a = -a;
            return new MyFraction(a, b);
        }

        public static BigInteger gcd(BigInteger a, BigInteger b)
        {
            a = (a < 0) ? -a : a;
            b = (b < 0) ? -b : b;
            BigInteger tmp;
            while (b > 0)
            {
                tmp = b;
                b = a % b;
                a = tmp;
            }
            return a;
        }

        public static BigInteger lcm(BigInteger a0, BigInteger b0)
        {
            BigInteger a = a0, b = b0;
            if (a < 0)
                a = -a;
            if (b < 0)
                b = -b;
            BigInteger g = gcd(a, b);
            return a / g * b;
        }


        public static MyFraction convertFromDouble(double x0)
        {
            BigInteger num, den;

            double x = x0;
            int sign = 1;
            if (x < 0)
            {
                x = -x;
                sign = -1;
            }

            double x1 = x;

            int np = (int)Math.Log10(x);

            if (np != 0)
            {
                x = x * Math.Pow(10, -np);
            }

            if (x == 0.0)
            {
                if (x0 > 0)
                {
                    num = 0;
                    den = 1;
                    return new MyFraction(num, den);
                }
                else if (x0 < 0)
                {
                    num = 0;
                    den = -1;
                    return new MyFraction(num, den);
                }
            }

            double q, r;
            q = Math.Floor(x);
            r = x - q;

            ArrayList conv = new ArrayList();
            conv.Add(new BigInteger(q));

            int max_count = 25;
            int counter = 0;

            while (r > 0 && counter++ < max_count)
            {
                x = 1.0 / r;
                q = Math.Floor(x);

                if (q == 0)
                {
                    break;
                }

                if ((double)(new BigInteger(q)) > 1.0E14)
                {
                    break;
                }

                r = x - q;
                conv.Add(new BigInteger(q));
            }

            if (conv.Count > 1 && (BigInteger)conv[conv.Count - 1] == 1)
            {
                conv[conv.Count - 2] = (BigInteger)conv[conv.Count - 2] + 1;
                conv.RemoveAt(conv.Count - 1);
            }

            BigInteger old_h = 0, h = 1;
            BigInteger old_k = 1, k = 0;
            BigInteger tmp;
            BigInteger q1;

            int ncount = conv.Count;
            for (int i = 0; i < ncount; i++)
            {
                q1 = (BigInteger)conv[i];

                tmp = h;
                h = old_h + h * q1;
                old_h = tmp;

                tmp = k;
                k = old_k + k * q1;
                old_k = tmp;

            }

            num = h;
            den = k;

            if (np > 0)
            {
                BigInteger tens = BigInteger.Pow(new BigInteger(10), np);
                num = num * tens;
            }
            else if (np < 0)
            {
                BigInteger tens = BigInteger.Pow(new BigInteger(10), -np);
                den = den * tens;
            }

            if (sign < 0)
            {
                num = -num;
            }
            return new MyFraction(num, den);
        }


        public override int GetHashCode()
        {
            return (int)m_num ^ (int)m_den;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MyFraction))
                return false;

            return Equals((MyFraction)obj);
        }

        bool isNaN() { return m_num == 0 && m_den == 0; }
        bool isIndeterminated() { return m_num == 0 && m_den == 0; }
        bool isZero() { return m_num == 0 && m_den != 0; }
        bool isPositiveZero() { return m_num == 0 && m_den > 0; }
        bool isNegativeZero() { return m_num == 0 && m_den < 0; }
        bool isNonZero() { return m_num != 0 && m_den != 0; }
        bool isOne() { return m_num == m_den && m_den != 0; }
        bool isMinusOne() { return m_num == -m_den && m_den != 0; }
        bool isTwo() { return m_num == 2 * m_den && m_den != 0; }
        bool isThree() { return m_num == 3 * m_den && m_den != 0; }
        bool isFour() { return m_num == 4 * m_den && m_den != 0; }
        bool isEight() { return m_num == 8 * m_den && m_den != 0; }
        bool isTen() { return m_num == 10 * m_den && m_den != 0; }
        bool isFinite() { return m_den != 0; }
        bool isInfinite() { return m_num != 0 && m_den == 0; }
        bool isPositiveInfinite() { return m_num > 0 && m_den == 0; }
        bool isNegativeInfinite() { return m_num < 0 && m_den == 0; }
        bool isPositive() { return m_num > 0 && m_den != 0; }
        bool isNegative() { return m_num < 0 && m_den != 0; }
        bool isNonPositive() { return m_num <= 0 && m_den != 0; }
        bool isNonNegative() { return m_num <= 0 && m_den != 0; }


        public string ToMixedForm()
        {
            string s = "";
            if (this.m_den == 0)
            {
                if (this.m_num > 0)
                    return "Infinity";
                else if (this.m_num < 0)
                    return "-Infinity";
                else
                    return "0/0 (Indeterminated)";
            }
            else if (this.m_den == -1 && this.m_num == 0)
                return "-0";
            else if (this.m_den == -1)
                return String.Format("{0}", -this.m_num);
            else if (this.m_den == 1)
                return String.Format("{0}", this.m_num);
            else
            {
                BigInteger a = this.m_num;
                BigInteger b = this.m_den;
                if (b < 0)
                {
                    a = -a;
                    b = -b;
                }

                BigInteger q = a / b;
                BigInteger r = a - b * q;
                if (r < 0)
                {
                    q = q - 1;
                    r = r + b;
                }

                if (r != 0)
                {
                    if (q < 0)
                        s += String.Format("({0})", q);
                    else if (q > 0)
                        s += String.Format("{0}", q);
                    s += String.Format("_{0}/{1}", r, b);
                }
                else
                {
                    s += String.Format("{0}", q);
                }
            }

            return s;
        }


        public string ToDecimalString(int prec = 20)
        {
            string s = "";
            if (this.m_den == 0)
            {
                if (this.m_num > 0)
                    s = "Infinity";
                else if (this.m_num < 0)
                    s = "-Infinity";
                else
                    s = "NaN";
                return s;
            }
            else if (this.m_den == -1 && this.m_num == 0)
            {
                s = "-0";
                return s;
            }
            else if (this.m_den == -1)
            {
                s = String.Format("{0}", -this.m_num);
                return s;
            }
            else if (this.m_den == 1)
            {
                s = String.Format("{0}", this.m_num);
                return s;
            }

            BigInteger a = this.m_num;
            BigInteger b = this.m_den;
            if (b < 0)
            {
                a = -a;
                b = -b;
            }

            int sign = 1;
            if (a < 0)
            {
                a = -a;
                sign = -1;
            }

            BigInteger q = a / b;
            BigInteger r = a - b * q;

            if (sign < 0)
            {
                s += "-";
            }

            if (prec <= 0)
            {
                BigInteger r2 = new BigInteger(10);

                BigInteger y = (r2 + this.m_den / 2) / this.m_den;
                int ylen = (String.Format("{0}", y)).Length;

                s += new String('0', prec - ylen);
                s += String.Format("{0}", (r2 + this.m_den / 2) / this.m_den);
                return s;
            }

            if (r != 0)
            {
                BigInteger nval = q;

                BigInteger t = r + b;
                BigInteger q3 = 0;
                string s3 = "";
                for (int i = 0; i < prec + 1; i++)
                {
                    q3 = t / b;
                    s3 += String.Format("{0}", q3);
                    t = (t - b * q3) * 10;
                }

                int carry = 0;

                if (sign > 0 && t / 5 >= b)
                    carry = 1;

                else if (sign < 0 && t / 5 > b)
                    carry = 1;

                char[] digits = s3.ToCharArray();
                for (int j = digits.Length - 1; j >= 0; j--)
                {
                    if (carry == 0)
                        break;

                    if (digits[j] == '9')
                    {
                        digits[j] = '0';
                        carry = 1;
                    }
                    else
                    {
                        digits[j] = (char)(digits[j] + 1);
                        carry = 0;
                    }
                }

                if (digits[0] == '2')
                {
                    nval += 1;
                }

                string s4 = new String(digits);
                int j1 = digits.Length;
                for (int j = j1 - 1; j >= 0; j--)
                {
                    if (digits[j] != '0')
                    {
                        j1 = j + 1;
                        break;
                    }
                }

                if (j1 > 0)
                {
                    s4 = s4.Substring(1, j1 - 1);
                    if (s4.Length > 0)
                        s += String.Format("{0}.{1}", nval, s4);
                    else
                        s += String.Format("{0}", nval);
                }
                else
                {
                    s += String.Format("{0}", nval);
                }
            }
            else
            {
                s += String.Format("{0}", q);
            }
            return s;
        }

        public override string ToString()
        {
            string s = "";
            if (m_den == 0)
            {
                if (m_num > 0)
                    s = "Infinity";
                else if (m_num < 0)
                    s = "-Infinity";
                else
                    s = "Intertermined (0/0)";
            }
            else if (m_num == 0)
            {
                if (m_den > 0)
                    s = "0";
                else if (m_den < 0)
                    s = "-0";
            }
            else if (m_den == 1)
                s = String.Format("{0}", m_num);
            else
                s = String.Format("{0}/{1}", m_num, m_den);
            return s;
        }


        public MyFraction floor(int pos = 0)
        {
            if (isNaN())
            {
                return new MyFraction(0, 0);
            }

            if (isZero())
            {
                if (isPositiveZero())
                {
                    return new MyFraction(0, 1);
                }
                else if (isNegativeZero())
                {
                    return new MyFraction(0, -1);
                }
            }
            else if (isInfinite())
            {
                if (isPositiveInfinite())
                {
                    return new MyFraction(1, 0);
                }
                else if (isNegativeInfinite())
                {
                    return new MyFraction(-1, 0);
                }
            }

            BigInteger c = m_num;
            BigInteger d = m_den;
            if (d < 0)
            {
                c = -c;
                d = -d;
            }
            BigInteger q = c / d;
            BigInteger r = c - q * d;

            MyFraction z;
            if (pos > 0)
            {

                BigInteger tens = BigInteger.Pow(new BigInteger(10), pos);
                c = c * tens;
                q = c / d;
                r = c - q * d;

                if (r < 0)
                {
                    q -= 1;
                    r += d;
                }

                z = new MyFraction(q, tens);
                return z;
            }
            else if (pos < 0)
            {
                BigInteger tens = BigInteger.Pow(new BigInteger(10), -pos);

                d = d * tens;
                q = c / d;
                r = c - q * d;

                if (r < 0)
                {
                    q -= 1;
                    r += d;
                }

                z = new MyFraction(q * tens, 1);
                return z;
            }

            z = new MyFraction(q, 1);
            return z;
        }

        public MyFraction ceil(int pos = 0)
        {
            if (isNaN())
            {
                return new MyFraction(0, 0);
            }

            if (isZero())
            {
                if (isPositiveZero())
                {
                    return new MyFraction(0, 1);
                }
                else if (isNegativeZero())
                {
                    return new MyFraction(0, -1);
                }
            }
            else if (isInfinite())
            {
                if (isPositiveInfinite())
                {
                    return new MyFraction(1, 0);
                }
                else if (isNegativeInfinite())
                {
                    return new MyFraction(-1, 0);
                }
            }

            BigInteger c = m_num;
            BigInteger d = m_den;
            if (d < 0)
            {
                c = -c;
                d = -d;
            }
            BigInteger q = c / d;
            BigInteger r = c - q * d;

            MyFraction z;
            if (pos >= 0)
            {
                BigInteger tens = BigInteger.Pow(new BigInteger(10), pos);

                c = c * tens;
                q = c / d;
                r = c - q * d;

                if (r > 0)
                {
                    q += 1;
                    r -= d;
                }

                z = new MyFraction(q, tens);
                return z;
            }
            else if (pos < 0)
            {
                BigInteger tens = BigInteger.Pow(new BigInteger(10), -pos);

                d = d * tens;
                q = c / d;
                r = c - q * d;

                if (r > 0)
                {
                    q += 1;
                    r -= d;
                }

                z = new MyFraction(q * tens, 1);
                return z;
            }

            z = new MyFraction(q, 1);
            return z;

        }

        public MyFraction round(int pos = 0)
        {
            if (isNaN())
            {
                return new MyFraction(0, 0);
            }

            if (isZero())
            {
                if (isPositiveZero())
                {
                    return new MyFraction(0, 1);
                }
                else if (isNegativeZero())
                {
                    return new MyFraction(0, -1);
                }
            }
            else if (isInfinite())
            {
                if (isPositiveInfinite())
                {
                    return new MyFraction(1, 0);
                }
                else if (isNegativeInfinite())
                {
                    return new MyFraction(-1, 0);
                }
            }

            BigInteger c = m_num;
            BigInteger d = m_den;
            if (d < 0)
            {
                c = -c;
                d = -d;
            }
            BigInteger q = c / d;
            BigInteger r = c - q * d;
            if (r < 0)
            {
                q -= 1;
                r += d;
            }


            MyFraction z;
            if (pos > 0)
            {
                BigInteger tens = BigInteger.Pow(new BigInteger(10), pos);

                c = c * tens;
                q = c / d;
                r = c - q * d;

                if (2 * r >= d && q >= 0)
                {
                    q += 1;
                }
                else if (2 * r < -d && q < 0)
                {
                    q -= 1;
                }

                z = new MyFraction(q, tens);
                return z;
            }
            else if (pos < 0)
            {
                BigInteger tens = BigInteger.Pow(new BigInteger(10), -pos);

                d = d * tens;
                q = c / d;
                r = c - q * d;

                if (2 * r >= d && r > 0)
                {
                    q += 1;
                }
                else if (2 * r < -d && q < 0)
                {
                    q -= 1;
                }

                z = new MyFraction(q * tens, 1);
                return z;
            }

            if (r * 2 >= d)
            {
                q += 1;
            }

            z = new MyFraction(q, 1);
            return z;
        }

        public MyFraction trunc(int pos = 0)
        {
            if (isNaN())
            {
                return new MyFraction(0, 0);
            }

            if (isZero())
            {
                if (isPositiveZero())
                {
                    return new MyFraction(0, 1);
                }
                else if (isNegativeZero())
                {
                    return new MyFraction(0, -1);
                }
            }
            else if (isInfinite())
            {
                if (isPositiveInfinite())
                {
                    return new MyFraction(1, 0);
                }
                else if (isNegativeInfinite())
                {
                    return new MyFraction(-1, 0);
                }
            }

            BigInteger c = m_num;
            BigInteger d = m_den;
            if (d < 0)
            {
                c = -c;
                d = -d;
            }
            BigInteger q = c / d;

            MyFraction z;
            if (pos > 0)
            {
                BigInteger tens = BigInteger.Pow(new BigInteger(10), pos);

                c = c * tens;
                q = c / d;


                z = new MyFraction(q, tens);
                return z;
            }
            else if (pos < 0)
            {
                BigInteger tens = BigInteger.Pow(new BigInteger(10), -pos);

                d = d * tens;
                q = c / d; ;

                z = new MyFraction(q * tens, 1);
                return z;
            }

            z = new MyFraction(q, 1);
            return z;
        }

        public static int compare(MyFraction left, MyFraction right)
        {
            if (left.isNaN() || right.isNaN())
            {
                return -65536;
            }

            if (left.isZero())
            {
                if (right.isPositive())
                    return -1;
                else if (right.isNegative())
                    return 1;

                return 0;
            }
            else if (left.isPositiveInfinite())
            {
                if (right.isPositiveInfinite())
                    return 0;
                else
                    return 1;
            }
            else if (left.isNegativeInfinite())
            {
                if (right.isNegativeInfinite())
                    return 0;
                else
                    return -1;
            }
            else if (right.isPositiveInfinite())
            {
                if (left.isPositiveInfinite())
                    return 0;
                else
                    return -1;
            }
            else if (right.isNegativeInfinite())
            {
                if (left.isNegativeInfinite())
                    return 0;
                else
                    return 1;
            }

            BigInteger c = left.getNum() * right.getDen();
            BigInteger d = left.getDen() * right.getNum();
            BigInteger r = c - d;
            
            if (r < 0)
            {
                return -1;
            }
            else if (r > 0)
            {
                return 1;
            }

            return 0;
        }

        public static bool operator ==(MyFraction left, MyFraction right)
        {
            int result = compare(left, right);
            return result == 0;
        }

        public static bool operator !=(MyFraction left, MyFraction right)
        {
            int result = compare(left, right);
            return result != 0;
        }

        public static bool operator <(MyFraction left, MyFraction right)
        {
            int result = compare(left, right);
            return result < 0;
        }

        public static bool operator <=(MyFraction left, MyFraction right)
        {
            int result = compare(left, right);
            return result <= 0;
        }

        public static bool operator >(MyFraction left, MyFraction right)
        {
            int result = compare(left, right);
            return result > 0;
        }

        public static bool operator >=(MyFraction left, MyFraction right)
        {
            int result = compare(left, right);
            return result >= 0;
        }

        public static MyFraction abs(MyFraction right)
        {
            if (right.isNaN())
            {
                return new MyFraction(0, 0);
            }

            if (right.isZero())
            {
                return new MyFraction(0, 1);

            }

            if (right.isInfinite())
            {
                return new MyFraction(1, 0);
            }

            BigInteger c = right.getNum();
            BigInteger d = right.getDen();
            if (c < 0)
            {
                c = -c;
            }
            MyFraction z = new MyFraction(c, d);
            return z;
        }

        public MyFraction intPart()
        {
            if (isNaN())
            {
                return new MyFraction(0, 0);
            }

            if (isZero())
            {
                if (isPositiveZero())
                {
                    return new MyFraction(0, 1);
                }
                else if (isNegativeZero())
                {
                    return new MyFraction(0, -1);
                }
            }
            else if (isInfinite())
            {
                if (isPositiveInfinite())
                {
                    return new MyFraction(1, 0);
                }
                else if (isNegativeInfinite())
                {
                    return new MyFraction(-1, 0);
                }
            }

            BigInteger c = m_num;
            BigInteger d = m_den;
            if (d < 0)
            {
                c = -c;
                d = -d;
            }
            BigInteger q = c / d;
            BigInteger r = c - q * d;
            if (r < 0)
            {
                q -= 1;
                r += d;
            }
            MyFraction z = new MyFraction(q, 1);
            return z;
        }

        public MyFraction fracPart()
        {
            if (isNaN())
            {
                return new MyFraction(0, 0);
            }

            if (isZero())
            {
                if (isPositiveZero())
                {
                    return new MyFraction(0, 1);
                }
                else if (isNegativeZero())
                {
                    return new MyFraction(0, -1);
                }
            }
            else if (isInfinite())
            {
                return new MyFraction(0, 0);
            }

            BigInteger c = m_num;
            BigInteger d = m_den;
            if (d < 0)
            {
                c = -c;
                d = -d;
            }
            BigInteger q = c / d;
            BigInteger r = c - q * d;
            if (r < 0)
            {
                q -= 1;
                r += d;
            }
            MyFraction z = new MyFraction(r, d);
            return z;
        }

        public static MyFraction invert(MyFraction right)
        {
            if (right.isNaN())
            {
                return new MyFraction(0, 0);
            }

            if (right.isZero())
            {
                if (right.isPositiveZero())
                {
                    return new MyFraction(1, 0);
                }
                else if (right.isNegativeZero())
                {
                    return new MyFraction(-1, 0);
                }
            }

            if (right.isInfinite())
            {
                if (right.isPositiveInfinite())
                {
                    return new MyFraction(0, 1);
                }
                else if (right.isNegativeInfinite())
                {
                    return new MyFraction(0, -1);
                }
            }

            BigInteger c = right.getNum();
            BigInteger d = right.getDen();
            if (c < 0)
            {
                c = -c;
                d = -d;
            }
            MyFraction z = new MyFraction(d, c);
            return z;
        }

        public static MyFraction operator -(MyFraction right)
        {
            if (right.isNaN())
            {
                return new MyFraction(0, 0);
            }

            if (right.isZero())
            {
                if (right.isPositiveZero())
                {
                    return new MyFraction(0, -1);
                }
                else if (right.isNegativeZero())
                {
                    return new MyFraction(0, 1);
                }
            }

            if (right.isInfinite())
            {
                if (right.isPositiveInfinite())
                {
                    return new MyFraction(-1, 0);
                }
                else if (right.isNegativeInfinite())
                {
                    return new MyFraction(1, 0);
                }
            }

            BigInteger c = right.getNum();
            BigInteger d = right.getDen();
            MyFraction z = new MyFraction(-c, d);
            return z;
        }

        public static MyFraction operator +(MyFraction left, MyFraction right)
        {
            if (left.isNaN() || right.isNaN())
            {
                return new MyFraction(0, 0);
            }

            if (left.isZero() && right.isZero())
            {
                if (left.isPositive() && right.isPositive())
                {
                    return new MyFraction(0, 1);
                }
                else if (left.isNegative() && right.isNegative())
                {
                    return new MyFraction(0, -1);
                }
                else if (left.isNegative() && right.isPositive())
                {
                    return new MyFraction(0, -1);
                }
                else if (left.isPositive() && right.isNegative())
                {
                    return new MyFraction(0, 1);
                }
            }

            if (left.isInfinite() || right.isInfinite())
            {
                if (left.isInfinite() && right.isInfinite())
                {
                    if (left.isPositiveInfinite() && right.isPositiveInfinite())
                    {
                        return new MyFraction(1, 0);
                    }
                    else if (left.isNegativeInfinite() && right.isNegativeInfinite())
                    {
                        return new MyFraction(-1, 0);
                    }
                    else
                    {
                        return new MyFraction(0, 0);
                    }
                }
                else if (left.isPositiveInfinite() || right.isPositiveInfinite())
                {
                    return new MyFraction(1, 0);
                }
                else if (left.isNegativeInfinite() || right.isNegativeInfinite())
                {
                    return new MyFraction(-1, 0);
                }
            }

            BigInteger lnum = left.getNum();
            BigInteger lden = left.getDen();
            BigInteger rnum = right.getNum();
            BigInteger rden = right.getDen();

            BigInteger d = lcm(lden, rden);
            BigInteger m1 = d / lden;
            BigInteger m2 = d / rden;

            BigInteger c = lnum * m1 + m2 * rnum;

            MyFraction z = new MyFraction(c, d);
            return z;
        }

        public static MyFraction operator -(MyFraction left, MyFraction right)
        {
            if (left.isNaN() || right.isNaN())
            {
                return new MyFraction(0, 0);
            }

            if (left.isZero() && right.isZero())
            {
                if (left.isPositive() && right.isPositive())
                {
                    return new MyFraction(0, 1);
                }
                else if (left.isNegative() && right.isNegative())
                {
                    return new MyFraction(0, -1);
                }
                else if (left.isNegative() && right.isPositive())
                {
                    return new MyFraction(0, -1);
                }
                else if (left.isPositive() && right.isNegative())
                {
                    return new MyFraction(0, 1);
                }
            }

            if (left.isInfinite() || right.isInfinite())
            {
                if (left.isInfinite() && right.isInfinite())
                {
                    if (left.isPositiveInfinite() && right.isNegativeInfinite())
                    {
                        return new MyFraction(1, 0);
                    }
                    else if (left.isNegativeInfinite() && right.isPositiveInfinite())
                    {
                        return new MyFraction(-1, 0);
                    }
                    else
                    {
                        return new MyFraction(0, 0);
                    }
                }
                else if (left.isPositiveInfinite() || right.isNegativeInfinite())
                {
                    return new MyFraction(1, 0);
                }
                else if (left.isNegativeInfinite() || right.isPositiveInfinite())
                {
                    return new MyFraction(-1, 0);
                }
            }

            BigInteger lnum = left.getNum();
            BigInteger lden = left.getDen();
            BigInteger rnum = right.getNum();
            BigInteger rden = right.getDen();

            BigInteger d = lcm(lden, rden);
            BigInteger m1 = d / lden;
            BigInteger m2 = d / rden;

            BigInteger c = lnum * m1 - m2 * rnum;

            MyFraction z = new MyFraction(c, d);
            return z;
        }

        public static MyFraction operator *(MyFraction left, MyFraction right)
        {
            if (left.isNaN() || right.isNaN())
            {
                return new MyFraction(0, 0);
            }

            if (left.isInfinite() && right.isZero())
            {
                return new MyFraction(0, 0);
            }
            else if (left.isZero() && right.isInfinite())
            {
                return new MyFraction(0, 0);
            }

            if (left.isInfinite() || right.isInfinite())
            {
                if (left.isPositiveInfinite())
                {
                    if (right.isPositive())
                        return new MyFraction(1, 0);
                    else if (right.isNegative())
                        return new MyFraction(-1, 0);
                }
                else if (left.isNegativeInfinite())
                {
                    if (right.isPositive())
                        return new MyFraction(-1, 0);
                    else if (right.isNegative())
                        return new MyFraction(1, 0);
                }
                else if (right.isPositiveInfinite())
                {
                    if (left.isPositive())
                        return new MyFraction(1, 0);
                    else if (left.isNegative())
                        return new MyFraction(-1, 0);
                }
                else if (right.isNegativeInfinite())
                {
                    if (left.isPositive())
                        return new MyFraction(-1, 0);
                    else if (left.isNegative())
                        return new MyFraction(1, 0);
                }
            }
            else if (left.isZero() || right.isZero())
            {
                if (left.isPositive() && right.isPositive())
                {
                    return new MyFraction(0, 1);
                }
                else if (left.isNegative() && right.isNegative())
                {
                    return new MyFraction(0, 1);
                }
                else if (left.isNegative() && right.isPositive())
                {
                    return new MyFraction(0, -1);
                }
                else if (left.isPositive() && right.isNegative())
                {
                    return new MyFraction(0, -1);
                }
            }


            BigInteger a = left.m_num;
            BigInteger b = left.m_den;
            BigInteger c = right.m_num;
            BigInteger d = right.m_den;

            BigInteger g = gcd(a, d);
            if (g > 1)
            {
                a = a / g;
                d = d / g;
            }
            g = gcd(c, b);
            if (g > 1)
            {
                c = c / g;
                b = b / g;
            }

            MyFraction z = new MyFraction(a * c, b * d);
            return z;
        }


        public static MyFraction operator /(MyFraction left, MyFraction right)
        {
            if (left.isNaN() || right.isNaN())
            {
                return new MyFraction(0, 0);
            }

            if (left.isInfinite() || right.isInfinite())
            {
                if (left.isInfinite() && right.isInfinite())
                {
                    return new MyFraction(0, 0);
                }
                else if (left.isPositiveInfinite())
                {
                    if (right.isPositiveZero())
                        return new MyFraction(1, 0);
                    else if (right.isNegativeZero())
                        return new MyFraction(-1, 0);
                    else if (right.isPositive())
                        return new MyFraction(1, 0);
                    else if (right.isNegative())
                        return new MyFraction(-1, 0);
                }
                else if (left.isNegativeInfinite())
                {
                    if (right.isPositiveZero())
                        return new MyFraction(-1, 0);
                    else if (right.isNegativeZero())
                        return new MyFraction(1, 0);
                    else if (right.isPositive())
                        return new MyFraction(-1, 0);
                    else if (right.isNegative())
                        return new MyFraction(-1, 0);
                }

                else if (left.isFinite() && right.isInfinite())
                {
                    if (left.isPositiveZero() && right.isPositiveInfinite())
                        return new MyFraction(0, 1);
                    else if (left.isNegativeZero() && right.isPositiveInfinite())
                        return new MyFraction(0, -1);
                    else if (left.isPositiveZero() && right.isNegativeInfinite())
                        return new MyFraction(0, -1);
                    else if (left.isNegativeZero() && right.isNegativeInfinite())
                        return new MyFraction(0, 1);
                    else if (left.isPositive() && right.isPositiveInfinite())
                        return new MyFraction(0, 1);
                    else if (left.isNegative() && right.isPositiveInfinite())
                        return new MyFraction(0, -1);
                    else if (left.isPositive() && right.isNegativeInfinite())
                        return new MyFraction(0, -1);
                    else if (left.isNegative() && right.isNegativeInfinite())
                        return new MyFraction(0, 1);
                }
            }

            BigInteger a = left.m_num;
            BigInteger b = left.m_den;
            BigInteger c = right.m_num;
            BigInteger d = right.m_den;

            BigInteger g = gcd(a, c);
            if (g > 1)
            {
                a = a / g;
                c = c / g;
            }
            g = gcd(b, d);
            if (g > 1)
            {
                b = b / g;
                d = d / g;
            }

            MyFraction z = new MyFraction(a * d, b * c);
            return z;
        }

        public static MyFraction intDiv(MyFraction left, MyFraction right)
        {
            if (left.isNaN() || right.isNaN())
            {
                return new MyFraction(0, 0);
            }

            if (left.isInfinite() || right.isInfinite())
            {
                if (left.isInfinite() && right.isInfinite())
                {
                    return new MyFraction(0, 0);
                }
                else if (left.isPositiveInfinite())
                {
                    if (right.isPositiveZero())
                        return new MyFraction(1, 0);
                    else if (right.isNegativeZero())
                        return new MyFraction(-1, 0);
                    else if (right.isPositive())
                        return new MyFraction(1, 0);
                    else if (right.isNegative())
                        return new MyFraction(-1, 0);
                }
                else if (left.isNegativeInfinite())
                {
                    if (right.isPositiveZero())
                        return new MyFraction(-1, 0);
                    else if (right.isNegativeZero())
                        return new MyFraction(1, 0);
                    else if (right.isPositive())
                        return new MyFraction(-1, 0);
                    else if (right.isNegative())
                        return new MyFraction(-1, 0);
                }

                else if (left.isFinite() && right.isInfinite())
                {
                    if (left.isPositiveZero() && right.isPositiveInfinite())
                        return new MyFraction(0, 1);
                    else if (left.isNegativeZero() && right.isPositiveInfinite())
                        return new MyFraction(0, -1);
                    else if (left.isPositiveZero() && right.isNegativeInfinite())
                        return new MyFraction(0, -1);
                    else if (left.isNegativeZero() && right.isNegativeInfinite())
                        return new MyFraction(0, 1);
                    else if (left.isPositive() && right.isPositiveInfinite())
                        return new MyFraction(0, 1);
                    else if (left.isNegative() && right.isPositiveInfinite())
                        return new MyFraction(0, -1);
                    else if (left.isPositive() && right.isNegativeInfinite())
                        return new MyFraction(0, -1);
                    else if (left.isNegative() && right.isNegativeInfinite())
                        return new MyFraction(0, 1);
                }
            }

            BigInteger a = left.m_num;
            BigInteger b = left.m_den;
            BigInteger c = right.m_num;
            BigInteger d = right.m_den;

            BigInteger g = gcd(a, c);
            if (g > 1)
            {
                a = a / g;
                c = c / g;
            }
            g = gcd(b, d);
            if (g > 1)
            {
                b = b / g;
                d = d / g;
            }

            BigInteger q = (a * d) / (b * c);

            MyFraction z = new MyFraction(q, 1);
            return z;
        }

        public static MyFraction divRem(ref MyFraction rem, MyFraction left, MyFraction right)
        {
            if (left.isNaN() || right.isNaN())
            {
                /// return (new MyFraction(0, 0), new MyFraction(0, 0));
                rem = new MyFraction(0, 0);
                return new MyFraction(0, 0);
            }

            if (left.isInfinite() || right.isInfinite())
            {
                if (left.isInfinite() && right.isInfinite())
                {
                    /// return (new MyFraction(0, 0), new MyFraction(0, 0));
                    rem = new MyFraction(0, 0);
                    return new MyFraction(0, 0);
                }
                else if (left.isInfinite())
                {
                    /// return (new MyFraction(0, 0), new MyFraction(0, 0));
                    rem = new MyFraction(0, 0);
                    return new MyFraction(0, 0);
                }
                else if (right.isInfinite())
                {
                    /// return (new MyFraction(0, 1), new MyFraction(0, 0));
                    rem = new MyFraction(0, 1);
                    return new MyFraction(0, 0);
                }
            }

            BigInteger a = left.m_num;
            BigInteger b = left.m_den;
            BigInteger c = right.m_num;
            BigInteger d = right.m_den;

            if (c < 0)
            {
                c = -c;
                d = -d;
            }

            BigInteger g = gcd(a, c);
            if (g > 1)
            {
                a = a / g;
                c = c / g;
            }
            g = gcd(b, d);
            if (g > 1)
            {
                b = b / g;
                d = d / g;
            }

            BigInteger q = (a * d) / (b * c);
            BigInteger r = (a * d) - (b * c) * q;

            /*
            if (r < 0)
            {
                   q = q - 1;
                   r = r + (b * c);
            }
            */

            MyFraction z = new MyFraction(a * d, b*c);
            MyFraction z2 = z.floor();

            /// MyFraction w = new MyFraction(r, b * c);
            /// action w = left - right * new MyFraction(q, 1);
            MyFraction w = left - right*z2;


            /*
            if (w.isNegative())
            {
                z = z - new MyFraction(1, 1);
                w = left + abs(right);
            }
            else if (w >= abs(right))
            {
                z = z + new MyFraction(1, 1);
                w = left - abs(right);
            }
            */

            rem = w;
            return z2;
        }


        public static MyFraction operator %(MyFraction left, MyFraction right)
        {
            if (left.isNaN() || right.isNaN())
            {
                return new MyFraction(0, 0);
            }

            if (left.isZero())
            {
                return new MyFraction(0, 1);
            }
            else if (left.isFinite() && right.isInfinite())
            {
                return new MyFraction(left.getNum(), left.getDen());
            }
            else if (left.isInfinite() && right.isFinite())
            {
                if (left.isPositive() || right.isPositive())
                    return new MyFraction(1, 0);
                else if (left.isNegative() || right.isNegative())
                    return new MyFraction(1, 0);
                else if (left.isPositive() || right.isNegative())
                    return new MyFraction(-1, 0);
                else if (left.isNegative() || right.isPositive())
                    return new MyFraction(-1, 0);
            }

            BigInteger a = left.m_num;
            BigInteger b = left.m_den;
            BigInteger c = right.m_num;
            BigInteger d = right.m_den;

            BigInteger g = gcd(a, c);
            if (g > 1)
            {
                a = a / g;
                c = c / g;
            }
            g = gcd(b, d);
            if (g > 1)
            {
                b = b / g;
                d = d / g;
            }

            BigInteger q = (a * d) / (b * c);
            MyFraction rem = left - new MyFraction(q * right.getNum(), right.getDen());

            return rem;
        }
    }
}
