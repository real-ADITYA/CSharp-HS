using System;

namespace EditingArraysScoredPractice {
    class Program {
        static void Main(string[] args) {
            int[] userArrUpdateMain = MainTasksForUserRstrt();
            int daREALChoice = AskUserCont(userArrUpdateMain);
            while (true) {
                if (daREALChoice == 1) {
                    userArrUpdateMain = MainTasksForUserNew(userArrUpdateMain);
                    userArrUpdateMain = ChangeValueArr(PromptUserAddR(userArrUpdateMain), userArrUpdateMain);
                    daREALChoice = AskUserCont(userArrUpdateMain);
                }
                else if (daREALChoice == 2) {
                    userArrUpdateMain = MainTasksForUserRstrt();
                    userArrUpdateMain = ChangeValueArr(PromptUserAddR(userArrUpdateMain), userArrUpdateMain);
                    daREALChoice = AskUserCont(userArrUpdateMain);
                }
                else { }
            }
        }
        static int PromptUserAddR(int[] userArr) {
            SimplePrintArray(userArr);
            TheLine();
            Console.Write("[0]Add a value to the array\n[1]Remove a value from the array\n[2]Do nothing\n");
            return Convert.ToInt32(Console.ReadLine());
        }
        static int[] ChangeValueArr(int userChoiceN, int[] userArr) {
            if (userChoiceN == 0) {
                Console.WriteLine("Please type a value to add to your array: ");
                int newVal = Convert.ToInt32(Console.ReadLine());
                int[] newUserArr = new int[userArr.Length + 1];
                for (int a = 0; a < userArr.Length; a++) {
                    newUserArr[a] = userArr[a];
                    newUserArr[^1] = newVal;
                }
                return newUserArr;
            }
            else if (userChoiceN == 1) {
                TheLine();
                Console.Clear();
                PrintArrayI(userArr, "Current Array!");
                Console.WriteLine("\nPlease enter the value you would like to delete:");
                PrntArrayAndPlcs(userArr);
                int newValDel = Convert.ToInt32(Console.ReadLine());
                TheLine();
                userArr[newValDel - 1] = -2147483647;
                int[] newUserArr = new int[userArr.Length - 1];
                userArr = sortVal(userArr);
                for (int z = 0; z < newUserArr.Length; z++) {
                    newUserArr[z] = userArr[z];
                }
                return newUserArr;
            }
            else if (userChoiceN == 2) {
                return userArr;
            }
            else {
                ChangeValueArr(PromptUserAddR(userArr), userArr);
                return userArr;
            }
        }
        static void PrntArrayAndPlcs(int[] usAr) {
            for (int x = 0; x < usAr.Length; x++) {
                Console.WriteLine("[" + (x + 1) + "] " + usAr[x]);
            }
        }

        static void SimplePrintArray(int[] userArray) {
            Console.Write("{");
            for (int r = 0; r < userArray.Length - 1; r++) {
                Console.Write(userArray[r] + ", ");
            }
            Console.Write(userArray[^1] + "}\n---------");
        }

        static int[] MainTasksForUserRstrt() {
            int numElements = AskNumElements();
            int[] userValues = AskNumValues(numElements);
            Console.Clear();
            SimplePrintArray(userValues);
            Console.WriteLine("---------\n[0]Sort low to high\n[1]Sort high to low\n[2]Symmetrical sort (Complex)");
            int[] userUpdateArr = EditingArrays(numElements, userValues, Convert.ToInt32(Console.ReadLine()));
            Console.Clear();
            return userUpdateArr;
        }

        static int[] MainTasksForUserNew(int[] currentArrayNum) {
            Console.Clear();
            SimplePrintArray(currentArrayNum);
            Console.WriteLine("---------\n[0]Sort low to high\n[1]Sort high to low\n[2]Symmetrical sort (Complex)");
            int[] temp2 = EditingArrays(currentArrayNum.Length, currentArrayNum, Convert.ToInt32(Console.ReadLine()));
            Console.Clear();
            return temp2;
        }

        static int AskUserCont(int[] userArr) {
            Console.Clear();
            SimplePrintArray(userArr);
            Console.WriteLine("---------\n[1]Sort the array again\n[2]Start over");
            int userContChce = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            if (userContChce == 1) {
                return 1;
            }
            else if (userContChce == 2) {
                return 2;
            }
            else {
                Console.WriteLine("ERROR: Did not enter 1 or 2");
                return -1;
            }

        }

        static int[] EditingArrays(int numElements, int[] userValues, int choiceNum) {
            switch (choiceNum) {
                case (0):
                    int[] updatingTheArrayAgainSmh = sortValLH(userValues);
                    PrintArrayI(updatingTheArrayAgainSmh, "Low to High");
                    return updatingTheArrayAgainSmh;
                case (1):
                    int[] updatingTheArrayAgainSmh1 = sortVal(userValues);
                    PrintArrayI(sortVal(userValues), "High to Low");
                    return updatingTheArrayAgainSmh1;
                case (2):
                    int[] updatingTheArrayAgainSmh2 = sortValComplictd(userValues);
                    PrintArrayI(sortValComplictd(userValues), "Symmetrical Sort");
                    return updatingTheArrayAgainSmh2;
                default:
                    int[] updatingTheArrayAgainSmh3 = userValues;
                    PrintArrayI(userValues, "Not Sorted");
                    return updatingTheArrayAgainSmh3;
            }
        }

        static void PrintArrayI(int[] userArray, string addOnStr) {
            Console.Write("The array that you have created: [" + addOnStr + "]\n{");
            for (int r = 0; r < userArray.Length - 1; r++) {
                Console.Write(userArray[r] + ", ");
            }
            Console.Write(userArray[^1] + "}\n---------");
        }

        static int[] sortValComplictd(int[] userLst) {
            for (int i2 = 1; i2 < userLst.Length; i2++) {
                for (int i = 1; i < userLst.Length; i++) {
                    while (userLst[i] > userLst[i - 1]) {
                        int temp = userLst[i - 1];
                        userLst[i - 1] = userLst[i];
                        userLst[i] = temp;
                    }
                }
            }
            return userLst;
        }
        static int[] sortVal(int[] userLst) {
            for (int i2 = 1; i2 < userLst.Length; i2++) {
                for (int i = 1; i < userLst.Length; i++) {
                    while (userLst[i] > userLst[i - 1]) {
                        int temp = userLst[i - 1];
                        userLst[i - 1] = userLst[i];
                        userLst[i] = temp;
                    }
                }
            }
            return userLst;
        }
        static int[] sortValLH(int[] userLst) {
            for (int i2 = 1; i2 < userLst.Length; i2++) {
                for (int i = 1; i < userLst.Length; i++) {
                    while (userLst[i] < userLst[i - 1]) {
                        int temp = userLst[i - 1];
                        userLst[i - 1] = userLst[i];
                        userLst[i] = temp;
                    }
                }
            }
            return userLst;
        }

        static int AskNumElements() {
            Console.WriteLine("Enter the amount of elements: ");
            int userNumVal = Convert.ToInt32(Console.ReadLine());
            return userNumVal;
        }
        static int[] AskNumValues(int userNumVal) {
            Console.WriteLine("Enter the desired numbers (Seperate each using ', ' [comma space]:");
            string userNumbers = Console.ReadLine();
            string[] userArrayStr = userNumbers.Split(", ");
            int[] userArray = new int[userNumVal];
            for (int w = 0; w < userArray.Length; w++) {
                userArray[w] = Convert.ToInt32(userArrayStr[w]);
            }
            return userArray;
        }

        static void TheLine() {
            Console.WriteLine("---------");
        }
    }
}

