using System;

namespace ScoredPracticeSetA {
    class Program {
        static void Main(string[] args) {
            TriangleInfo(43.4, 23.6, 23.6);
            DrawARectangle(7, 5, 0);
        }
        static void TriangleInfo(double s1, double s2, double s3) {
            bool isEquilateral = s1 == s2 && s1 == s3;

            bool isIsosceles = s1 == s2 || s1 == s3 || s2 == s3 || s3 == s1;

            bool isRightAngle1 = (s1 * s1) + (s2 * s2) == (s3 * s3);
            bool isRightAngle2 = (s2 * s2) + (s3 * s3) == (s1 * s1);
            bool isRightAngle3 = (s3 * s3) + (s1 * s1) == (s2 * s2);

            bool isScalene = s1 != s2 && s1 != s3 && s2 != s3;

            bool isObtuse = (s1 > s2 && s1 > s3) || (s2 > s1 && s2 > s3) || (s3 > s2 && s3 > s1);

            if (isEquilateral == true) {
                Console.WriteLine("This is an equilateral triangle");
            }
            else if (isIsosceles == true) {
                Console.WriteLine("This is an isosceles triangle");
            }
            else if (isRightAngle1 == true || isRightAngle2 == true || isRightAngle3 == true) {
                Console.WriteLine("This is a right angle triangle");
            }
            else if (isScalene == true) {
                Console.WriteLine("This is a scalene triangle");
            }
            else if (isObtuse == true) {
                Console.WriteLine("This is an obtuse triangle");
            }
            Console.WriteLine("The perimeter is: " + Perimeter(s1, s2, s3));
            Console.WriteLine("The area is: " + HeronFormula(s1, s2, s3) + "\nOR APPROX: " +
            SimpForm(HeronFormula(s1, s2, s3)));

            static double HeronFormula(double a, double b, double c) {
                double s = (a + b + c) / 2;
                double solution = Math.Sqrt(s * (s - a) * (s - b) * (s - c));
                return solution;
            }
            static int SimpForm(double a) {
                return Convert.ToInt32(a);
            }
            static double Perimeter(double s1, double s2, double s3) {
                return s1 + s2 + s3;
            }
        }


        static void DrawARectangle(int x, int y, int type) {
            int xH = x / 2;
            int yH = y / 2;
            if (type == 0) {
                for (int count3 = 0; count3 < y; count3++) {
                    if (count3 < yH) {
                        for (int count4 = 0; count4 < x; count4++) {
                            Console.Write(y);
                        }
                        Console.WriteLine();
                    }
                    else if (count3 == yH) {
                        for (int count4 = 0; count4 < x; count4++) {
                            if (count4 < xH) {
                                Console.Write(y);
                            }
                            else if (count4 == xH) {
                                Console.Write(0);
                            }
                            else if (count4 > xH) {
                                Console.Write(y);
                            }
                        }
                        Console.WriteLine();
                    }
                    else if (count3 > yH) {
                        for (int count4 = 0; count4 < x; count4++) {
                            Console.Write(y);
                        }
                        Console.WriteLine();
                    }
                }

            }
            else if (type > 0 && type < 10) {
                for (int count2 = 0; count2 < y; count2++) {
                    for (int count = 0; count < x; count++) {
                        Console.Write(y);
                    }
                    Console.WriteLine();
                }
            }
            else if (type >= 10) {
            }
            else {
                Console.WriteLine("'Type' cannot be negative...");
            }
        }

    }
}