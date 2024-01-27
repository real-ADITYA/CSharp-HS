using System;

namespace ClassPractice2Points {
    class Program {
        static void Main(string[] args) {
            /*Point point01 = new Point();
            Point point02 = new Point(0f, 3f);
            Point point03 = new Point();*/
            AskForPnt();
        }
        static void AskForPnt() {
            Console.WriteLine("Create a circle:\n---------\n[0] With the default values\n[1] Enter custom values\n[2] Enter custom values + radius");
            switch(Convert.ToInt32(Console.ReadLine())){
                case (0):
                    Point point01 = new Point();
                    point01.PrintPointSi();
                    break;
                case (1):
                    Console.Clear();
                    Console.Write("---------\nEnter the x value: ");
                    float userXVal = float.Parse(Console.ReadLine());
                    Console.Write("\nEnter the y value: ");
                    float userYVal = float.Parse(Console.ReadLine());
                    Point point02 = new Point(userXVal, userYVal);
                    point02.PrintPointSi();
                    break;
                case (2):
                    Console.Clear();
                    Console.Write("---------\nEnter the x value: ");
                    float userXVal2 = float.Parse(Console.ReadLine());
                    Console.Write("\nEnter the y value: ");
                    float userYVal2 = float.Parse(Console.ReadLine());
                    Console.Write("\nEnter the radius: ");
                    float userRadius = float.Parse(Console.ReadLine());
                    Point point03 = new Point(userXVal2, userYVal2, userRadius);
                    point03.PrintPointWRad();
                    break;
                default:
                    break;
            }
        }

    }

    class Point {
        float pointX;
        float pointY;
        float radius;

        public float GetX() {
            return pointX;
        }
        public float GetY() {
            return pointY;
        }
        public void SetX(float Xin) {
            pointX = Xin;
        }
        public void SetY(float Yin) {
            pointY = Yin;
        }

        public Point() {
            pointX = 0f;
            pointY = 0f;
        }
        public Point(float Xin, float Yin) {
            pointX = Xin;
            pointY = Yin;
        }
        public Point(float Xin, float Yin, float URadius) {
            pointX = PointInCircleX(Xin, URadius);
            pointY = PointInCircleY(Yin, URadius);
            radius = URadius;
        }
        public float PointInCircleX(float x, float rad) {
            Random randomizer = new Random();
            return randomizer.Next(Convert.ToInt32(x - rad), Convert.ToInt32(x + rad));
        }
        public float PointInCircleY(float y, float rad) {
            Random randomizer = new Random();
            return randomizer.Next(Convert.ToInt32(y - rad), Convert.ToInt32(y + rad));
        }
        public void PrintPointWRad() {
            Console.Clear();
            Console.WriteLine("---------\nThe circle contains point ({0}, {1}) with a radius of {2}.", pointX, pointY, radius);
            if (AskPlanHS() == true) {
                HighlySpicy(pointX, pointY, radius);
            }
            else { }
        }
        public void PrintPointSi() {
            Console.Clear();
            Console.WriteLine("---------\nThe circle has an origin of ({0}, {1}).", pointX, pointY);
            if (AskPlanHS() == true) {
                HighlySpicy(pointX, pointY, radius);
            }
            else { }
    }

        public void HighlySpicy(float Xin, float Yin, float Radius) {
            Point newerPoint = new Point(Xin, Yin, Radius);
            newerPoint.PrintPointWRadSPI();
        }

        public bool AskPlanHS() {
            while (true) {
                Console.WriteLine("---------\nWould you like to continue with the existing point or exit?\n---------\n[0] YES COMMENCE CONSTRUCTOR HIGHLY SPICY\n[1] Nah");
                if (Convert.ToInt32(Console.ReadLine()) == 0 || Convert.ToInt32(Console.ReadLine()) == 1) {
                    if (Convert.ToInt32(Console.ReadLine()) == 0) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else { }
            }
        }

    }




}