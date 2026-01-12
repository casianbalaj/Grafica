using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafica_4
{
    public static class Utility
    {
        private static Dictionary<int, int> factorialMemoCache;
        //public static int Factorial(int n) {
        //    if (n == 0) return 1;
        //    int result = 1;
        //    int i = n;
        //    while (i >= 1) {
        //        if (factorialMemoCache.ContainsKey(i)) {
        //            result = factorialMemoCache[i];
        //            break;
        //        }
        //        i--;
        //    }
        //    for (int j = i; j <= n; j++) {
        //        factorialMemoCache[j] = result;
        //        result *= j;
        //    }
        //    factorialMemoCache[n] = result;
        //    return result;
        //}

        public static int Factorial(int n) {
            if (factorialMemoCache.ContainsKey(n)) {
                return factorialMemoCache[n];
            }
            else {
                int result = n * Factorial(n - 1);
                factorialMemoCache[n] = result;
                return result;
            }
        }

        public static int BinomialCoefficient(int n, int k) {
            return Factorial(n) / (Factorial(k) * Factorial(n - k));
        }

        private static Dictionary<(double, int), double> powerMemoCache;
        static Utility() {
            factorialMemoCache = new Dictionary<int, int>
            {
                [0] = 1,
                [1] = 1
            };
            powerMemoCache = [];
        }

        public static double Pow(double n, int exponent) {
            if (n == 1)
            {
                return 1;
            }
            else if (exponent == 0)
            {
                return n;
            }
            else if (powerMemoCache.ContainsKey((n, exponent)))
            {
                return powerMemoCache[(n, exponent)];
            }
            else if (exponent % 2 == 0)
            {
                double res = Pow(n, exponent / 2);
                res *= res;
                powerMemoCache[(n, exponent)] = res;
                return res;
            }
            else /*(exponent%2==1)*/ {
                double res = Pow(n, exponent - 1);
                res *= n;
                powerMemoCache[(n, exponent)] = res;
                return res;
            }
        }

    }

    public class BernsteinPolynomial(int i, int n)
    {
        private int coeff = Utility.BinomialCoefficient(n, i);

        public double Eval(double u) {
            return coeff * Utility.Pow(u, i) * Utility.Pow(1 - u, n - i);
        }
    }
   
}
