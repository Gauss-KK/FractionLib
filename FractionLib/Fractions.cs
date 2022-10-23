using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace knumerics

{
    public class MyFraction : IFormattable
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


        public MyFraction(string a0, string b0 = "1")
        {
            MyFraction f1 = ParseString(a0);
            MyFraction f2 = ParseString(b0);
            MyFraction f3 = f1 / f2;
            m_num = f3.m_num;
            m_den = f3.m_den;
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



            while (x >= 10.0)
            {
                x = x / 10.0;
                np = np + 1;
            }
            while (x < 1.0)
            {
                x = x * 10.0;
                np = np - 1;
            }

            double y1 = x;
            string sy1 = y1.ToString("F14");

            char c = sy1[sy1.Length - 1];

            if (c >= '5' && c <= '9')
            {
                sy1 = AddCarryAtTail(sy1.Substring(0,sy1.Length - 1), 1);
            }
            else
            {
                sy1 = sy1.Substring(0, sy1.Length - 1);
            }

            int j = sy1.Length - 1;
            while (j >= 4 && sy1[j] == '0')
            {
                j--;
            }
            if (sy1[j] == '.' && j < sy1.Length)
            {
                j = j + 1;
            }

            sy1 = sy1.Substring(0, j + 1);
            if (sy1.Length <= 13 && sy1[1] == '.')
            {
                sy1 = sy1.Substring(0, 1) + sy1.Substring(2);
                BigInteger num2 = BigInteger.Parse(sy1);
                BigInteger den2 = MyFraction.mPow(10, sy1.Length - 1);

                BigInteger g2 = gcd(num2, den2);
                if (g2 > 1)
                {
                    num2 = num2 / g2;
                    den2 = den2 / g2;
                }

                BigInteger tens = 10;
                if (np > 0)
                {
                    tens = MyFraction.mPow(tens, np);
                    num2 = num2 * tens;
                }
                else if (np < 0)
                {
                    tens = MyFraction.mPow(10, -np);
                    den2 = den2 * tens;
                }

                g2 = gcd(num2, den2);
                if (g2 > 1)
                {
                    num2 = num2 / g2;
                    den2 = den2 / g2;
                }

                if (sign < 0)
                {
                    num2 = -num2;
                }

                this.m_num = num2;
                this.m_den = den2;

                return;
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

            long q;
            double r;
            q = (long) Math.Floor(x);
            r = x - Math.Floor(x);

            ArrayList conv = new ArrayList();
            conv.Add(q);

            long old_h2 = 1;
            long h2 = q;
            long old_k2 = 0;
            long k2 = 1;

            long t2 = 0;

            double x2 = (double)q;

            int max_count = 50;
            int counter = 0;

            while (r > 0 && counter++ < max_count)
            {
                x = 1.0 / r;

                q = (long) Math.Floor(x);
                r = x - Math.Floor(x);
                conv.Add(q);

                t2 = h2;
                h2 = old_h2 + h2 * q;
                old_h2 = t2;

                t2 = k2;
                k2 = old_k2 + k2 * q;
                old_k2 = t2;

                if (k2 < old_k2)
                {
                    throw new ArithmeticException("Exception:\n");
                }

                x2 = (double)h2 / (double)k2;
                if (counter > 1 && Math.Abs(x2 - y1) < 1.0E-15)
                {
                    break;
                }
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


        public static MyFraction ParseString(string src)
        {
            string s1 = "";
            string s2 = "";
            string s3 = "";

            int epos = -1;
            int dpos = -1;
            int sign = 1;

            epos = src.IndexOf("E");
            if (epos < 0)
            {
                epos = src.IndexOf("E");
            }

            if (epos > 0 && epos < src.Length)
            {
                s1 = src.Substring(0, epos);
                s3 = src.Substring(epos + 1);
            }
            else
            {
                s1 = src;
                s3 = "";
            }

            dpos = s1.IndexOf(".");
            if (dpos > 0 && dpos < s1.Length)
            {
                s2 = s1.Substring(dpos + 1);
                s1 = s1.Substring(0, dpos);
            }
            else
            {
                s2 = "";
            }

            if (s1.StartsWith("-"))
            {
                sign = -1;
                s1 = s1.Substring(1);
            }
            else if (s1.StartsWith("+"))
            {
                s1 = s1.Substring(1);
            }

            s1 = s1.TrimStart('0');
            s2 = s2.TrimEnd('0');

            int k = s2.Length;
            s1 = s1 + s2;

            BigInteger a = BigInteger.Parse(s1);

            BigInteger b = new BigInteger(1);
            BigInteger c = new BigInteger(1);

            int k1 = 0;
            int k2 = 0;
            if (s3.StartsWith("-"))
            {
                k2 = Int32.Parse(s3.Substring(1));
            }
            else if (s3.StartsWith("+"))
            {
                k1 = Int32.Parse(s3.Substring(1));
            }

            k = k + k1 - k2;

            BigInteger ten = new BigInteger(10);
            if (k > 0)
            {
                b = BigInteger.Pow(ten, k);
            }
            else if (k < 0)
            {
                c = BigInteger.Pow(ten, -k);
            }

            BigInteger num = a * b;
            BigInteger den = c;

            if (sign < 0)
            {
                num = -num;
            }

            return new MyFraction(num, den);
        }


        static string ReverseString(string s0)
        {
            char[] charArr = s0.ToCharArray();
            Array.Reverse(charArr);
            string dest = new string(charArr);
            return dest;
        }

        static string AddCarryAtTail(string s0, int amount)
        {
            int carry = amount;
            char a;
            string t = "";
            int i = s0.Length - 1;
            for (; i >= 0; i--)
            {
                a = s0[i];
                if (a == '.')
                    continue;
                else if (a == '-' || a == '+')
                    break;
                t += (char)((((int)(a - '0') + carry) % 10) + '0');
                carry = ((int)(a - '0') + carry) / 10;

                if (carry <= 0)
                {
                    break;
                }
            }

            if (carry > 0)
            {
                t += (char) ((carry % 10) + '0');
                carry = carry / 10;
            }

            for (int j = i - 1; j >= 0; j--)
            {
                a = s0[j];
                t += a;
            }

            string dest = ReverseString(t);
            return dest;
        }


        static BigInteger mPow(long a1, long n0)
        {
            BigInteger a0 = BigInteger.Parse(String.Format("{0}", a1));

            if (n0 < 0)
                return new BigInteger(0);    // BigInteger.ZERO;
            else if (n0 == 0)
                return new BigInteger(1);      // BigInteger.ONE;
            else if (n0 == 1)
                return a0;
            else if (n0 == 2)
                return a0 * a0;

            BigInteger t = a0;
            BigInteger y = 1;

            long n = n0;

            while (n > 0)
            {
                if (n % 2 == 1)
                    y = y * t; 
                t = t * t; 
                n >>= 1;
            }

            return y;
        }


        static BigInteger mPow(BigInteger a0, long n0)
        {
            if (n0 < 0)
                return new BigInteger(0);
            else if (n0 == 0)
                return new BigInteger(1);
            else if (n0 == 1)
                return a0;
            else if (n0 == 2)
                return a0 * a0; 

            BigInteger t = a0;
            BigInteger y = 1;

            long n = n0;

            while (n > 0)
            {
                if (n % 2 == 1)
                {
                    y = y * t;
                }

                t = t * t;
                n >>= 1;
            }

            return y;
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


         public static explicit operator double (MyFraction right)
         {
         	 int sign = 1;
              BigInteger b = right.getNum();
              if (b < 0)
              {
              	  sign = -1;
              	  b = - b;
              }
              BigInteger c = right.getDen();

         	 if (c == 0)
         	 {
            	       if (b > 0)
         	 	    return double.PositiveInfinity;
            	       else if (b < 0)
         	 	    return double.NegativeInfinity;
         	 	return double.NaN;
         	 }

              BigInteger a = b / c;
              b = b - c*a;
              double y = 0.0;
              
              if (a > new BigInteger(double.MaxValue))
              {
                    y = double.PositiveInfinity;
              }
              else if (a >= BigInteger.Pow(10, 20))
              {
                    y =  (double) a;
              }
              else
              {
                    BigInteger r = b - c*a;
                    y = (double) a;
                    y += (double) r / (double) c;
              }
              
              if (sign < 0)
              {
              	  y = -y;
              }
              return y;
      }


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
                   s += String.Format("({0})_", q);
                else if (q > 0)
                    s += String.Format("{0}_", q);
                s += String.Format("{0}/{1}", r, b);
            }
            else
            {
                 s += String.Format("{0}", q);
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

            if (prec <= 0)
            {
                BigInteger r2 = new BigInteger(10);

                BigInteger y = (r2 + this.m_den / 2) / this.m_den;
                int ylen = (String.Format("{0}", y)).Length;

                s += new String('0', prec - ylen);
                s += String.Format("{0}", (r2 + this.m_den / 2) / this.m_den);
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
                s += String.Format("{0}", q);
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
 
        public string ToExponentialForm_old(string format)
        {
            int prec = 15;

            if ((format.StartsWith("E") || format.StartsWith("e")) && format.Length > 1)
            {
                prec = Int32.Parse(format.Substring(1));
            }

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
                string s3 = String.Format("{0}", -this.m_num);
                string strSign2 = "";
                if (s3.Substring(0, 1) == "-")
                {
                    strSign2 = "-";
                    s3 = s3.Substring(1);
                }

                int nk = s3.Length - 1;
                if (prec + 1 < s3.Length)
                {
                    s3 = s3.Substring(0, prec + 2);
                }
                s3 = s3.Substring(1, 2) + "." + s3.Substring(1);
                s = strSign2 + s3 + format.Substring(0, 1) + string.Format("+{0:D03}", nk);
                return s;
            }
            else if (this.m_den == 1)
            {
                string s3 = String.Format("{0}", this.m_num);
                int nk = s3.Length - 1;
                if (prec < s3.Length)
                {
                    s3 = s3.Substring(0, prec + 1);
                }
                s3 = s3.Substring(0, 1) + "." + s3.Substring(1);
                s = s3 + format.Substring(0, 1) + string.Format("+{0:D03}", nk);
                return s;
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

            string strSign = "";

            if (sign < 0)
            {
                strSign = "-";
            }

            if (prec <= String.Format("{0}", q).Length)
            {

                string s3 = String.Format("{0}", q);
                int nk = s3.Length - 1;
                if (prec < s3.Length)
                {
                    s3 = s3.Substring(0, prec + 1);
                }
                s3 = s3.Substring(0, 1) + "." + s3.Substring(1);
                s = strSign + s3 + format.Substring(0, 1) + string.Format("+{0:D03}", nk);
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

            string s1 = s;
            string s2 = "";

            int kexp = 0;
            string sexp = "";

            if (s.Length > 0 && s.IndexOf(".") < 0)
            {
                kexp = s.Length - 1;
                sexp = $"{kexp,+03}";

                if (s.Length > prec + 1)
                {
                    s1 = s.Substring(0, 1) + "." + s.Substring(1, prec);
                }
            }
            else if (s.Length > 1 && s.StartsWith("0."))
            {
                s1 = s.Substring(2);
                s1 = s1.TrimStart('0');
                char sig = '+';

                kexp = s1.Length - s.Length + 1;
                if (kexp < 0)
                {
                    sig = '-';
                    kexp = -kexp;
                }
                s1 = s1.Substring(0, 1) + "." + s1.Substring(1);

                sexp = string.Format("{0}{1:D03}", sig, kexp);

                if (s1.Length < prec + 2)
                {
                    s1 = s1.PadRight(prec + 2 - s1.Length, '0');
                }
            }
            else if (s.Length > 1 && s.IndexOf(".") > 0 && s.Substring(0, 1) != "0")
            {
                kexp = s.IndexOf(".") - 1;
                sexp = $"{kexp,+03}";

                if (s.Length > prec + 1)
                {
                    s1 = s.Substring(0, prec + 1);
                }
                else
                {
                    s1 = s.PadRight(prec + 1 - s.Length, '0');
                }

                s1 = s1.Substring(0, s1.IndexOf(".")) + s1.Substring(s1.IndexOf(".") + 1);
                s1 = s1.Substring(0, 1) + "." + s1.Substring(1);
            }

            s2 = strSign + s1 + format.Substring(0, 1) + sexp;

            return s2;
        }


        public string ToExponentialString(string format)
        {
            int prec = 15;

            if ((format.StartsWith("E") || format.StartsWith("e")) && format.Length > 1)
            {
                prec = Int32.Parse(format.Substring(1));
            }

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

            if (prec <= 0)
            {
                prec = 0;
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

            string strSign = "";

            if (sign < 0)
            {
                strSign = "-";
            }

            BigInteger mpow = BigInteger.Pow(new BigInteger(10), prec + 2);
            int ndiff10 = (int)Math.Floor(BigInteger.Log10(a) - BigInteger.Log10(b));
            string strExp = "E" + string.Format("{0:+00#}", ndiff10); ;

            if (b > a)
            {
                mpow = BigInteger.Pow(new BigInteger(10), -ndiff10 + prec + 2);
                strExp = format.Substring(0, 1) + string.Format("{0:D3}", ndiff10);
            }

            a = a * mpow;
            BigInteger q = a / b;
            BigInteger r = a - b * q;

            string qstr = string.Format("{0}", q);
            string s1 = qstr;

            s1 = s1.Substring(0, 1) + "." + s1.Substring(1);
            if (s1.Length > prec + 2)
            {
                s1 = s1.Substring(0, prec + 2);
            }
            else if (s1.Length < prec + 2)
            {
                s1 = s1.PadLeft(prec + 2 - s1.Length, '0');
            }

            s1 = strSign + s1 + strExp;
            return s1;
        }


        public string ToFixedDotString(string format)
        {
            int prec = 15;

            if ((format.StartsWith("F") || format.StartsWith("f")) && format.Length > 1)
            {
                prec = Int32.Parse(format.Substring(1));
            }

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

            if (prec <= 0)
            {
                prec = 0;
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

            string strSign = "";

            if (sign < 0)
            {
                strSign = "-";
            }


            BigInteger mpow = BigInteger.Pow(new BigInteger(10), prec + 2);
            int ndiff10 = (int) Math.Floor(BigInteger.Log10(a) - BigInteger.Log10(b));

            string strExp = "E" + string.Format("{0:+00#}", ndiff10); ;
            BigInteger q = a / b;
            BigInteger r = a - b * q;

            string qstr = string.Format("{0}", q);
            string s1 = qstr;

            if (prec > 0)
            {
                mpow = BigInteger.Pow(new BigInteger(10), prec);
                BigInteger q2 = (r * mpow) / b;
                string qstr2 = string.Format("{0}", q2);
                if (qstr2.Length < prec)
                {
                    qstr2 = qstr2.PadLeft(prec - qstr2.Length, '0');
                }
                s1 = s1 + "." + qstr2;
            }

            if (s1.Length < prec + 2)
            {
                s1 = s1.PadRight(prec + 2 - s1.Length, '0');
            }
            s1 = strSign + s1;
            return s1;
        }


        public string ToGeneralString(string format)
        {
            int prec = 15;

            if ((format.StartsWith("G") || format.StartsWith("g")) && format.Length > 1)
            {
                prec = Int32.Parse(format.Substring(1));
            }

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

            if (prec <= 0)
            {
                prec = 0;
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

            BigInteger ubound = BigInteger.Pow(new BigInteger(10), 14);
            if (a / b > ubound || b / a > ubound)
            {
                if (format.Substring(0, 1) == "g")
                {
                    return ToExponentialString("e" + format.Substring(1));
                }
                return ToExponentialString("E" + format.Substring(1));
            }

            string strSign = "";
            if (sign < 0)
            {
                strSign = "-";
            }

            BigInteger q = a / b;
            BigInteger r = a - b * q;

            string qstr = string.Format("{0}", q);
            return qstr;
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


        public string ToStringWithFormat(string format0 = "G")
        {
            if (format0 != null)
            {
                string format = format0.Trim();
                if (format == "")
                {
                    return ToString();
                }
                else if (format == "M")
                {
                    return ToMixedForm();
                }
                else if (format.StartsWith("E") || format.StartsWith("e"))
                {
                    return ToExponentialString(format);
                }
                else if (format.StartsWith("F") || format.StartsWith("f"))
                {
                    return ToFixedDotString(format);
                }
                else if (format.StartsWith("G") || format.StartsWith("g"))
                {
                    return ToGeneralString(format);
                }
            }
            return ToString();
        }


        public string ToString(string format, IFormatProvider provider)
        {
            return ToStringWithFormat(format);
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
                rem = new MyFraction(0, 0);
                return new MyFraction(0, 0);
            }

            if (left.isInfinite() || right.isInfinite())
            {
                if (left.isInfinite() && right.isInfinite())
                {
                    rem = new MyFraction(0, 0);
                    return new MyFraction(0, 0);
                }
                else if (left.isInfinite())
                {
                    rem = new MyFraction(0, 0);
                    return new MyFraction(0, 0);
                }
                else if (right.isInfinite())
                {
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

            MyFraction z = new MyFraction(a * d, b * c);
            MyFraction z2 = z.floor();

            MyFraction w = left - right * z2;

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