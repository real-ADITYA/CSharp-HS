using System;

namespace ScoredPracticeSetB {
    class Program {
        static void Main(string[] args) {
            LeapYear(2021);
            Factorize(54);
            MultiplyWithoutMultiplication(3, 6);
            CompoundInterest(1000, 5, 12);
        }

        static void LeapYear(int year) {
            if(year % 4 == 0) {
                Console.WriteLine(year + " is a leap year!");
            }
            else {
                Console.WriteLine(year + " is not a leap year...");
            }
            Console.WriteLine();
        }

        static void Factorize(int integerPr) {
            for(int counter = 1; counter < integerPr; counter++) {
                int primeFinder = integerPr % counter;
                    if(primeFinder == 0) {
                    Console.WriteLine(counter + " is a factor of " + integerPr);
                }
                else { }
            }
            Console.WriteLine();
        }

        static void MultiplyWithoutMultiplication(int firstNum, int secNum) {
            int fNumSTAY = firstNum;
            int fNumCHANGE = firstNum;
            for(int counter2 = 1; counter2 < secNum; counter2++) {
                int solTemp = fNumSTAY + fNumCHANGE;
                fNumCHANGE = solTemp;
            }
            Console.WriteLine(firstNum + " x " + secNum + " = " + fNumCHANGE + "\n");
        }

        static void CompoundInterest(double money, int intRate, int years) {
            Console.WriteLine("0 $" + String.Format("{0:0.00}", money));
            double ActualRate = (Convert.ToDouble(intRate) / 100) + 1;
            for(int counter3 = 1; counter3 <= years; counter3++) {
                double tempMon = money * ActualRate;
                money = tempMon;
                Console.WriteLine(counter3 + " $" + String.Format("{0:0.00}", tempMon));
            }
        }

    }
}
