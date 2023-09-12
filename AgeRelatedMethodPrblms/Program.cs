/* Author: Aditya Eswaran
   Date: Nov. 2018
*/

using System;
using System.Runtime.InteropServices.ComTypes;

namespace AgeRelatedMethodPrblms {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Enter your age by the end of 2020.");
            int userAge = Convert.ToInt32(Console.ReadLine());
            YearOfThe(userAge);
            NextMilestone(userAge);
            TimeRemaining(userAge);
        }

        static void TimeRemaining(int daAgeV3) {
            int dieYearSrry = 73 - daAgeV3;
            Console.WriteLine("Based on the average life expectancy, you have approximately " + dieYearSrry + " years to live.");
        }

        static void NextMilestone(int daAgeV2) {
            //Wierd, I know :D
            if (daAgeV2 >= 21 && daAgeV2 < 70) {
                int temp1 = daAgeV2 / 10;
                int temp2 = temp1 + 1;
                daAgeV2 = 10 * temp2;
            }
            else if (daAgeV2 > 71 && daAgeV2 < 75) {
                daAgeV2 = 75;
            }
            else if(daAgeV2 >= 75) {
                int temp3 = daAgeV2 / 10;
                int temp4 = temp3 + 1;
                daAgeV2 = 10 * temp4;
            }
            else {
                bool ageTen = daAgeV2 < 10;
                bool ageThirtn = daAgeV2 > 10 && daAgeV2 < 13;
                bool ageSixtn = daAgeV2 >= 13 && daAgeV2 < 16;
                bool ageEightn = daAgeV2 >= 16 && daAgeV2 < 18;
                bool ageTwntyOne = daAgeV2 >= 18 && daAgeV2 < 21;
                if (ageTen == true) {
                    daAgeV2 = 10;
                }
                else if (ageThirtn == true) {
                    daAgeV2 = 13;
                }
                else if (ageSixtn == true) {
                    daAgeV2 = 16;
                }
                else if (ageEightn == true) {
                    daAgeV2 = 18;
                }
                else if (ageTwntyOne == true) {
                    daAgeV2 = 21;
                }
            }
            Console.WriteLine("Your next milestone age is " + daAgeV2);
        }

        static void YearOfThe(int daAge) {
            string daZodiac;
            int yearBorn = 2020 - daAge;
            int numYearZodiac = (yearBorn - 1924) % 12;
            switch (numYearZodiac) {
                case (0):
                    daZodiac = "Rat";
                    break;
                case (1):
                    daZodiac = "Ox";
                    break;
                case (2):
                    daZodiac = "Tiger";
                    break;
                case (3):
                    daZodiac = "Rabbit";
                    break;
                case (4):
                    daZodiac = "Dragon";
                    break;
                case (5):
                    daZodiac = "Snake";
                    break;
                case (6):
                    daZodiac = "Horse";
                    break;
                case (7):
                    daZodiac = "Sheep";
                    break;
                case (8):
                    daZodiac = "Monkey";
                    break;
                case (9):
                    daZodiac = "Rooster";
                    break;
                case (10):
                    daZodiac = "Dog";
                    break;
                case (11):
                    daZodiac = "Pig";
                    break;
                default:
                    daZodiac = " ";
                    break;

            }
            Console.WriteLine("You are " + daAge + " and you were born in the year of the " + daZodiac);
        }
    }
}
