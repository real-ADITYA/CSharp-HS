using System;

namespace MultidimensionalArraysB {
    class Program {
        static void Main(string[] args) {
            bool isRunning = true;
            Console.Title = "The MAP Zoomer 3000";
            int[,] userMap = CreateLarArray();
            while (isRunning == true) {
                PrintMap(userMap);
                int[] targetVals = PointCameraAsk(userMap);
                ZoomInConsole(userMap, targetVals);
                isRunning = AskUserCont();
                Console.Clear();
            }
        }
        static void ChangeColorYellow(string text) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
        static bool AskUserCont() {
            while (true) {
                Console.Write("---------\nPlease choose one of the following:\n Type '");
                ChangeColorYellow("quit");
                Console.Write("' to end the program.\n Type '");
                ChangeColorYellow("again");
                Console.Write("' to zoom in to another portion of the map\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
                string userAnswer = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                if (userAnswer == "quit" || userAnswer == "Quit" || userAnswer == "QUIT") {
                    return false;
                }
                else if (userAnswer == "again" || userAnswer == "Again" || userAnswer == "AGAIN") {
                    return true;
                }
                else {
                    Console.WriteLine("That response was invalid; please try again.");
                }
            }
        }
        static void OutOfBounds(int[] targetVals, int[,] userArr) {
            int[,] smallMap = new int[7, 7];
            int cnt1 = 0;
            int cnt2 = 0;
            for (int y = targetVals[0] - 3; y <= targetVals[0] + 3; y++) {
                for (int x = targetVals[1] - 3; x <= targetVals[1] + 3; x++) {
                    if (y < 0 || x < 0 || y > 13 || x > 13) {
                        smallMap[cnt1, cnt2] = 200;
                        if (cnt2 < 6) { cnt2++; }
                        else if (cnt2 >= 6) { }
                    }
                    else {
                        smallMap[cnt1, cnt2] = userArr[y, x];
                        if (cnt2 < 6) { cnt2++; }
                        else if (cnt2 >= 6) { }
                    }
                }
                cnt2 = 0;
                if (cnt1 < 6) { cnt1++; }
                else if (cnt1 >= 6) { }
            }
            //Print map
            for (int i = 0; i < smallMap.GetLength(0); i++) {
                for (int p = 0; p < smallMap.GetLength(1); p++) {
                    if (smallMap[i, p] < 10 && smallMap[i, p] >= 0) {
                        Console.Write("0{0} ", smallMap[i, p]);
                    }
                    else if (smallMap[i, p] == 200) {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" . ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else {
                        Console.Write("{0} ", smallMap[i, p]);
                    }
                }
                Console.WriteLine();
            }

        }
        static void InBounds(int[] targetVals, int[,] userArr) {
            int[,] smallMap = new int[7, 7];
            int cnt1 = 0;
            int cnt2 = 0;
            for (int y = targetVals[0] - 3; y <= targetVals[0] + 3; y++) {
                for (int x = targetVals[1] - 3; x <= targetVals[1] + 3; x++) {
                    smallMap[cnt1, cnt2] = userArr[y, x];
                    if (cnt2 < 6) { cnt2++; }
                    else if (cnt2 >= 6) { }
                }
                cnt2 = 0;
                if (cnt1 < 6) { cnt1++; }
                else if (cnt1 >= 6) { }
            }
            //Print map
            for (int i = 0; i < smallMap.GetLength(0); i++) {
                for (int p = 0; p < smallMap.GetLength(1); p++) {
                    if (smallMap[i, p] < 10 && smallMap[i, p] >= 0) {
                        Console.Write("0{0} ", smallMap[i, p]);
                    }
                    else {
                        Console.Write("{0} ", smallMap[i, p]);
                    }
                }
                Console.WriteLine();
            }


        }
        static void ZoomInConsole(int[,] userArr, int[] targetVals) {
            Console.Clear();
            if (targetVals[0] > 3 && targetVals[0] < 14 && targetVals[1] > 3 && targetVals[1] < 14) {
                InBounds(targetVals, userArr);
            }
            else {
                OutOfBounds(targetVals, userArr);
            }
        }
        static void PrintMap(int[,] arr) {
            for (int i = 0; i < arr.GetLength(0); i++) {
                for (int p = 0; p < arr.GetLength(1); p++) {
                    if (arr[i, p] < 10 && arr[i, p] >= 0) {
                        Console.Write("0{0} ", arr[i, p]);
                    }
                    else {
                        Console.Write("{0} ", arr[i, p]);
                    }
                }
                Console.WriteLine();
            }
        }
        static int[,] CreateLarArray() {
            int[,] hugeArray = new int[16, 16];
            Random ran = new Random();
            for (int e = 0; e < hugeArray.GetLength(0); e++) {
                for (int q = 0; q < hugeArray.GetLength(1); q++) {
                    hugeArray[e, q] = ran.Next(0, 100);
                }
            }
            return hugeArray;
        }
        static int[] PointCameraAsk(int[,] userArr) {
            //Prompt user
            Console.WriteLine("---------\nEnter the number which you would like the camera to zoom in on. (Ex: 5)");
            Console.ForegroundColor = ConsoleColor.Red;
            int userAns = Convert.ToInt32(Console.ReadLine());
            Console.ForegroundColor = ConsoleColor.White;
            //Label targets
            int[] userTargets = LabelTargetsConsole(userArr, userAns);
            //Returns the values of the selected target
            return CheckNSaveTarget(userArr, userTargets);
        }
        static int[] AddValueArr(int[] userArr, int value) {
            int[] userNew = new int[userArr.Length + 1];
            for (int e = 0; e < userArr.Length; e++) {
                userNew[e] = userArr[e];
            }
            userNew[userArr.Length] = value;
            return userNew;
        }
        static int[] CheckNSaveTarget(int[,] userArr, int[] userTargets) {
            bool askin = true;
            while (askin == true) {
                //If there are no repeaters, then just continue; only continue if the number is valid and/or exists
                if (userTargets.Length != 1) {
                    Console.WriteLine("---------\nMultiple values were found. Enter the position of the desired value.\n(Ex: You would enter 5 if it's the fifth time the number appears on the map)");
                    Console.ForegroundColor = ConsoleColor.Red;
                    int userMultAns = Convert.ToInt32(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.White;
                    //Save the x and y values of the target
                    int temp = 0;
                    for (int e = 0; e < userArr.GetLength(0); e++) {
                        for (int q = 0; q < userArr.GetLength(1); q++) {
                            if (userArr[e, q] == userTargets[0] && temp == userMultAns - 1) {
                                int[] valZoom = new int[2];
                                valZoom[0] = e;
                                valZoom[1] = q;
                                return valZoom;
                            }
                            else if (userArr[e, q] == userTargets[0] && temp != userMultAns - 1) {
                                temp++;
                            }
                            else { }
                        }
                    }
                    askin = false;
                }
                else if (userTargets.Length == 1) {
                    //Save the x and y values of the target
                    for (int e = 0; e < userArr.GetLength(0); e++) {
                        for (int q = 0; q < userArr.GetLength(1); q++) {
                            if (userArr[e, q] == userTargets[0]) {
                                int[] valZoom = new int[2];
                                valZoom[0] = e;
                                valZoom[1] = q;
                                return valZoom;
                            }
                            else { }
                        }
                    }
                    askin = false;
                }
                else {
                    //This will retry till the boolean is able to break out of the while loop
                    Console.WriteLine("This number does not exist. Try again.");
                }
            }
            return null;
        }
        static int[] LabelTargetsConsole(int[,] userArr, int userZoomNum) {
            //Array of targets
            int[] userTargets = new int[0];
            Console.Clear();
            //Labeling
            for (int e = 0; e < userArr.GetLength(0); e++) {
                for (int q = 0; q < userArr.GetLength(1); q++) {
                    if (userArr[e, q] == userZoomNum && userArr[e, q] < 10) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("0{0} ", userArr[e, q]);
                        userTargets = AddValueArr(userTargets, userArr[e, q]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (userArr[e, q] == userZoomNum && userArr[e, q] >= 10) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("{0} ", userArr[e, q]);
                        userTargets = AddValueArr(userTargets, userArr[e, q]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (userArr[e, q] != userZoomNum && userArr[e, q] < 10) {
                        Console.Write("0{0} ", userArr[e, q]);
                    }
                    else {
                        Console.Write("{0} ", userArr[e, q]);
                    }
                }
                Console.WriteLine();
            }
            return userTargets;
        }
    }
}