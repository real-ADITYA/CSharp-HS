using System;

namespace MathRelatedPrblms {
    class Program {
        static void Main(string[] args) {
            DiffOfExp(5, 3);
            DiffOfExp(2, 7);
            DiffOfExp(12, 6);

            FactorialMthd(6);
            FactorialMthd(3);
            FactorialMthd(16);
            Console.WriteLine("----------");

            VolPyramid(3, 4, 5);
            VolPyramid(5, 45, 2);
            VolPyramid(16, 25, 93);

        }

        static void DiffOfExp(double a, double b) {
            double result1 = Math.Pow(a, b);
            double result2 = Math.Pow(b, a);
            double diffResult = result1 - result2;
            Console.WriteLine("(" + a + "^" + b + ") = " + result1 + "\n(" + b + "^" + a + ") = " + result2 +"\nThe difference is " + diffResult + "\n----------");
        }

        static int FactorialMthd(int num) {
            int factoriNum = 1;
            if (num == 0) {
                int factZero = 1;
                return factZero;
            }
            else if (num > 0) {
                for (int i = 1; i <= num; i++) {
                    factoriNum = factoriNum * i;
                }
                Console.WriteLine("!" + num + " = " + factoriNum);
                return factoriNum;
            }
            return num = 0;
        }

        static float VolPyramid(int length, int width, int height) {
            float volume = (length * width * height) / 6f;
            Console.WriteLine("L = " + length + " in." + "\nW = " + width + " in." + "\nH = " + height + "in." + "\nV = " + volume + "in. cubed");
            Console.WriteLine("----------");
            return volume;
        }
    }
}
