using System;

namespace EvaluatingArraysA {
    class Program {
        static void Main(string[] args) {
            //Prerequisites
            InfoTable userTable = new InfoTable();
            userTable.CreateArr();
            userTable.PrintArr();

            bool isRunning = true;
            while (isRunning == true) {
                switch (AskTask()) {
                    case (1):
                        Console.Clear();
                        userTable.PrintArr();
                        userTable.LongestRepeatSequence();
                        break;
                    case (2):
                        Console.Clear();
                        userTable.PrintArr();
                        userTable.LargestDifferenceBetweenAdjacent();
                        break;
                    case (3):
                        Console.Clear();
                        userTable.PrintArr();
                        userTable.ColWithLargestVal();
                        break;
                    case (4):
                        Console.Clear();
                        userTable.PrintArr();
                        userTable.ColumnFewestUnique();
                        break;
                    case (5):
                        Console.Clear();
                        userTable.PrintArr();
                        userTable.AverageAllValues();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Invalid entry...\nTerminating program...");
                        Console.ForegroundColor = ConsoleColor.White;
                        isRunning = false;
                        break;
                }
            }
        }
        static int AskTask() {
            Console.WriteLine("---------");
            Console.WriteLine("(1) Find the largest sequence of repeating values\n(2) Find the largest difference between two adjacent values" +
                "\n(3) Find the row with the largest value\n(4) Find the column with the fewest unique values\n(5) Find the average of all the " +
                "values");
            Console.ForegroundColor = ConsoleColor.Yellow;
            int uAns = Convert.ToInt32(Console.ReadLine());
            Console.ForegroundColor = ConsoleColor.White;
            return uAns;
        }

        class InfoTable {
            int numCol, numRow, maxNum, minNum;
            int[,] userTable;

            int maxCharacters, tempNC;
            int maxDiff, maxDR, maxDC, minDR, minDC;
            int maxRep, maxRN;
            int maxVal, maxVR;
            int minUC;
            int avgVal;

            public void CreateArr() {
                EnterUserInfo();
                FillUserArray();
            }
            public void AverageAllValues() {
                for (int columnCount = 0; columnCount < numCol; columnCount++) {
                    for (int rowCount = 0; rowCount < numRow; rowCount++) {
                        avgVal += userTable[columnCount, rowCount];
                    }
                }
                avgVal /= (numCol * numRow);
                PrintAvg();
            }
            private void PrintAvg() {
                Console.Write("The average of all of the values is ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(avgVal);
                Console.ForegroundColor = ConsoleColor.White;
            }
            public void ColumnFewestUnique() {
                int tempUnq = 2000000;
                int tempUnqC = 0;
                for (int columnCount = 0; columnCount < numCol; columnCount++) {
                    for (int rowCount = 0; rowCount < numRow; rowCount++) {
                        if (rowCount != 0) {
                            if (userTable[columnCount, rowCount] == userTable[columnCount, rowCount - 1]) {
                                tempUnqC++;
                                if (tempUnqC < tempUnq) {
                                    minUC = columnCount;
                                }
                                else { }
                            }
                        }
                        else if (rowCount == 0) {
                            if (userTable[columnCount, numRow - 1] == userTable[columnCount, 0]) {
                                tempUnqC++;
                                if (tempUnqC < tempUnq) {
                                    minUC = columnCount;
                                }
                                else { }
                            }
                            else { }
                        }
                        else { }
                    }
                }
                PrintColFewUnq();
            }
            private void PrintColFewUnq() {
                Console.Write("Column ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(minUC + 1);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" had the least unique values.");
            }
            public void ColWithLargestVal() {
                int tempVal = 0;
                for (int columnCount = 0; columnCount < numCol; columnCount++) {
                    for (int rowCount = 0; rowCount < numRow; rowCount++) {
                        if (rowCount == 0 && columnCount != 0) {
                            if (userTable[columnCount, rowCount] > userTable[columnCount - 1, numRow - 1]) {
                                tempVal = userTable[columnCount, rowCount];
                                if (tempVal > maxVal) {
                                    maxVal = tempVal;
                                    maxVR = rowCount;
                                }
                                else { }
                            }
                            else { }
                        }
                        else if (rowCount == 0 && columnCount == 0) {
                            if (userTable[columnCount, rowCount] > userTable[numCol - 1, numRow - 1]) {
                                tempVal = userTable[columnCount, rowCount];
                                if (tempVal > maxVal) {
                                    maxVal = tempVal;
                                    maxVR = rowCount;
                                }
                                else { }
                            }
                            else { }
                        }
                        else {
                            if (userTable[columnCount, rowCount] > userTable[columnCount, rowCount - 1]) {
                                tempVal = userTable[columnCount, rowCount];
                                if (tempVal > maxVal) {
                                    maxVal = tempVal;
                                    maxVR = rowCount;
                                }
                                else { }
                            }
                            else { }
                        }
                    }
                }
                PrintColMethod();
            }
            private void PrintColMethod() {
                Console.Write("Column ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(maxVR + 1);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" contained the maximmum value of ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(maxVal);
                Console.ForegroundColor = ConsoleColor.White;
            }
            public void LongestRepeatSequence() {
                int tempRep = 0;
                for (int columnCount = 0; columnCount < numCol; columnCount++) {
                    for (int rowCount = 0; rowCount < numRow; rowCount++) {
                        if (rowCount == 0 && columnCount != 0) {
                            if (userTable[columnCount, rowCount] == userTable[columnCount - 1, numRow - 1]) {
                                //if the row count is 0 and its not on the first column
                                tempRep++;
                                if (tempRep > maxRep) {
                                    maxRep = tempRep;
                                    maxRN = userTable[columnCount, rowCount];
                                }
                                else { }
                            }
                            else { }
                        }
                        else if (rowCount == 0 && columnCount == 0) {
                            if (userTable[columnCount, rowCount] == userTable[numCol - 1, numRow - 1]) {
                                //if the counter has just begun indexing at 0, 0 then compare with last
                                tempRep++;
                                if (tempRep > maxRep) {
                                    maxRep = tempRep;
                                    maxRN = userTable[columnCount, rowCount];
                                }
                                else { }
                            }
                            else { }
                        }
                        else {
                            if (userTable[columnCount, rowCount] == userTable[columnCount, rowCount - 1]) {
                                //if its a regular check to see if the number to the left is equal
                                tempRep++;
                                if (tempRep > maxRep) {
                                    maxRep = tempRep;
                                    maxRN = userTable[columnCount, rowCount];
                                }
                                else { }
                            }
                            else { }
                        }
                    }
                }
                PrintSequenceMethod();
            }
            private void PrintSequenceMethod() {
                Console.Write("The largest sequence in the table is as shown: ");
                if (maxRep > 1) {
                    for (int i = 0; i < maxRep - 1; i++) {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(maxRN);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(", ");
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine
                        (maxRN);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("N/A");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            public void LargestDifferenceBetweenAdjacent() {
                int tempDiff = 0;
                for (int columnCount = 0; columnCount < numCol; columnCount++) {
                    for (int rowCount = 0; rowCount < numRow; rowCount++) {
                        if (rowCount == 0 && columnCount != 0) {
                            tempDiff = userTable[columnCount, rowCount] - userTable[columnCount - 1, numRow - 1];
                            if (tempDiff > maxDiff) {
                                maxDiff = tempDiff;
                                maxDC = columnCount;
                                maxDR = rowCount;
                                minDC = columnCount - 1;
                                minDR = numRow - 1;
                            }
                            else { }
                        }
                        else if (rowCount == 0 && columnCount == 0) {
                            tempDiff = userTable[columnCount, rowCount] - userTable[numCol - 1, numRow - 1];
                            if (tempDiff > maxDiff) {
                                maxDiff = tempDiff;
                                maxDC = columnCount;
                                maxDR = rowCount;
                                minDC = numCol - 1;
                                minDR = numRow - 1;
                            }
                            else { }
                        }
                        else {
                            tempDiff = userTable[columnCount, rowCount] - userTable[columnCount, rowCount - 1];
                            if (tempDiff > maxDiff) {
                                maxDiff = tempDiff;
                                maxDC = columnCount;
                                maxDR = rowCount;
                                minDC = columnCount;
                                minDR = rowCount - 1;
                            }
                            else { }
                        }
                    }
                }
                PrintDiffMethod();
            }
            private void PrintDiffMethod() {
                Console.Write("The largest difference between two adjacent values in the table was ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(maxDiff);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(", between ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(userTable[maxDC, maxDR]);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" and ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(userTable[minDC, minDR]);
                Console.ForegroundColor = ConsoleColor.White;
            }
            public void PrintArr() {
                Console.WriteLine("---------");
                maxCharacters = maxNum.ToString().Length;
                for (int columnCount = 0; columnCount < numCol; columnCount++) {
                    for (int rowCount = 0; rowCount < numRow; rowCount++) {
                        tempNC = userTable[columnCount, rowCount].ToString().Length;
                        Console.Write("{0}{1} ", CreateZerosTempNC(), userTable[columnCount, rowCount]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("---------");
            }
            private string CreateZerosTempNC() {
                string temp = "";
                for (int i = 0; i < maxCharacters - tempNC; i++) {
                    temp += "0";
                }
                return temp;
            }
            private void FillUserArray() {
                userTable = new int[numCol, numRow];
                Random rn = new Random();
                for (int columnCount = 0; columnCount < numCol; columnCount++) {
                    for (int rowCount = 0; rowCount < numRow; rowCount++) {
                        userTable[columnCount, rowCount] = rn.Next(minNum, maxNum + 1);
                    }
                }
            }
            private void EnterUserInfo() {
                Console.Write("Enter the credentials of the table.\n---------\nAmout of columns: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                numCol = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Amout of rows: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                numRow = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Minimum value (Inclusive): ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                minNum = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Maximum value (Inclusive): ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                maxNum = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.White;
            }

        }
    }
}