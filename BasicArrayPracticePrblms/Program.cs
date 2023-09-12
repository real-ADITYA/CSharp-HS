using System;

namespace BasicArrayPracticePrblms {
    class Program {
        static void Main(string[] args) {
            int[] numSet1 = { 0, 1, 2, 3 };
            int[] numSet2 = { 10, 9, 7, 3, 2, 1, 6, 5, 4, 8 };
            int[] numSet3 = { 1, 1, 1, 1, 0 };

            prntArray(); //--Q1
            highArray(numSet1); //--Q2
            highArray(numSet2);
            highArray(numSet3);
            highLowArray(numSet1); //--Q3
            highLowArray(numSet2);
            highLowArray(numSet3);
            sortVal(numSet1);  //--Q4
            sortVal(numSet2);
            sortVal(numSet3);

        }

        static void sortVal(int[] userLst) {
            for(int i2 = 1; i2<userLst.Length; i2++) {
            for (int i = 1; i < userLst.Length; i++) {
                    while (userLst[i] > userLst[i - 1]) {
                        int temp = userLst[i - 1];
                        userLst[i - 1] = userLst[i];
                        userLst[i] = temp;
                    }
            }
          }
            foreach(int number in userLst) {
                Console.Write(number + ", ");
            }
            Console.WriteLine();
        }

        static void highLowArray(int[] userLs) {
            int greatNum = userLs[0];
            int leastNum = userLs[0];

            for(int i = 0; i < userLs.Length; i++) {
                if(userLs[i] > greatNum) {
                    greatNum = userLs[i];
                }
                if(userLs[i] < leastNum) {
                    leastNum = userLs[i];
                }
            }
            int diffNum = greatNum - leastNum;
            Console.WriteLine("The greatest number is: " + greatNum + "\nThe lowest  number is: " + leastNum
                + "\nThe range of the data set: " + diffNum + "\n---------");
        }

        static void highArray(int[] userlist) {
            int greatestNum = userlist[0];

            for(int temp = 0; temp < userlist.Length; temp++) {
                if (userlist[temp] > greatestNum) {
                    greatestNum = userlist[temp];
                }
                else { }
            }

            Console.WriteLine("The largest number in the given array is: " + greatestNum);
            Console.WriteLine("---------");
        }

        static void prntArray() {
            int[] simpleNums = { 0, 1, 2, 3 };
            int[] complexNums = { 10, 9, 7, 3, 2, 1, 6, 5, 4, 8 };
            string[] listNames = { "Susan", "Rowan", "Bob" };
            foreach (int number in simpleNums) {
                Console.Write(number + ", ");
            }
            Console.WriteLine("\n---------");
            foreach (int num in complexNums) {
                Console.Write(num + ", ");
            }
            Console.WriteLine("\n---------");
            foreach (string pName in listNames) {
                Console.Write(pName + ", ");
            }
            Console.WriteLine("\n---------");
        }

    }
}
