using System;

namespace MapArrayScoredB {
    class Program {
        static void Main(string[] args) {
            MapCreator map = new MapCreator();
            map.ResetMap();
            map.PrMainMap();
            map.UserZoom();
            map.PrFiSmMap();
        }
    }

    class MapCreator {
        /*  NOTES:
                - int[column(vertical), row(horizontal)]
                - Pr means print; Fi means find; Tr means target; Sm means small; s is list */
        Random rnd = new Random();
        CC tClr = new CC();
        static int mapCols = 16, mapRows = 16;
        int[,] mainMap = new int[mapCols, mapRows];
        int userTarget; int[] sTrLocate = new int[0]; int[] fnlTr = new int[2];
        int[,] smMap = new int[7, 7];

        public void ResetMap() {
            //Resets the main map with random values
            for (int col = 0; col < mainMap.GetLength(0); col++) {
                for (int row = 0; row < mainMap.GetLength(1); row++) {
                    mainMap[col, row] = rnd.Next(0, 100);
                }
            }
        }
        public void PrMainMap() {
            Console.WriteLine(". . . . . . . . . ");
            for (int col = 0; col < mainMap.GetLength(0); col++) {
                for (int row = 0; row < mainMap.GetLength(1); row++) {
                    if (mainMap[col, row] < 10 && mainMap[col, row] >= 0) { Console.Write("0{0} ", mainMap[col, row]); }
                    else { Console.Write("{0} ", mainMap[col, row]); }
                }
                Console.WriteLine();
            }
            Console.WriteLine(". . . . . . . . . ");
        }
        public void UserZoom() {
            Console.WriteLine("---------\nEnter the number which you would like the camera to zoom in on. (Ex: 56)");
            tClr.ChangeColor("r");
            userTarget = Convert.ToInt32(Console.ReadLine());
            tClr.ChangeColor("default");
            PrFiMapTargets();
        }
        private void PrFiMapTargets() {
            Console.Clear();
            Console.WriteLine(". . . . . . . . . ");
            for (int col = 0; col < mainMap.GetLength(0); col++) {
                for (int row = 0; row < mainMap.GetLength(1); row++) {
                    if (mainMap[col, row] == userTarget) {
                        if (mainMap[col, row] < 10) {
                            tClr.ChangeColor("r");
                            Console.Write("0{0} ", mainMap[col, row]);
                            tClr.ChangeColor("default");
                        }
                        else {
                            tClr.ChangeColor("r");
                            Console.Write("{0} ", mainMap[col, row]);
                            tClr.ChangeColor("default");
                        }
                        //Add target coords to the target array
                        sTrLocate = ArrAdd(sTrLocate, col);
                        sTrLocate = ArrAdd(sTrLocate, row);
                    }
                    else if (mainMap[col, row] != userTarget) {
                        if (mainMap[col, row] < 10) {
                            Console.Write("0{0} ", mainMap[col, row]);
                        }
                        else {
                            Console.Write("{0} ", mainMap[col, row]);
                        }
                    }
                }
                Console.WriteLine();
            }
            FiTrDupes();
        }
        private void FiTrDupes() {
            //If neccessary, ask which target value
            if (sTrLocate.Length <= 0) { tClr.ChangeColorTxt("r", "This number could not be found :/"); }
            else if (sTrLocate.Length == 2) { /*Continue on; one value found*/ }
            else {     /*If there were multiple targets in the array (dupes)*/
                Console.WriteLine(". . . . . . . . . \nMultiple values were found. Enter the position of the desired value.\n(Ex: You would " +
                    "enter 5 if it's the fifth time the number appears on the map)");
                tClr.ChangeColor("r");
                int userMultAns = Convert.ToInt32(Console.ReadLine());
                tClr.ChangeColor("w");
                fnlTr[0] = sTrLocate[(userMultAns * 2) - 2]; fnlTr[1] = sTrLocate[(userMultAns * 2) - 1];
            }
        }
        private int[] ArrAdd(int[] arr, int value) {
            int[] userNew = new int[arr.Length + 1];
            for (int e = 0; e < arr.Length; e++) {
                userNew[e] = arr[e];
            }
            userNew[arr.Length] = value;
            return userNew;
        }
        public void PrFiSmMap() {
            Console.Clear();
            /*  Create new smaller map out of bounds
                    col: fnlTr[0] - 3 to fnlTr[0] + 3
                    row: fnlTr[1] - 3 to fnlTr[1] + 3  */
            int cTemp = -3;
            for (int col = 0; col < smMap.GetLength(0); col++) {
                int rTemp = -3;
                for (int row = 0; row < smMap.GetLength(1); row++) {
                    if (fnlTr[0] + cTemp >= 0 && fnlTr[1] + rTemp >= 0) {
                        smMap[col, row] = mainMap[fnlTr[0] + cTemp, fnlTr[1] + rTemp];
                    }
                    else {
                        smMap[col, row] = -1;
                    }
                    rTemp++;
                }
                cTemp++;
            }
            /*Print out the small map*/
            for (int col = 0; col < smMap.GetLength(0); col++) {
                for (int row = 0; row < smMap.GetLength(1); row++) {
                    if (smMap[col, row] == -1) {
                        tClr.ChangeColorTxt("y", " . ");
                    }
                    if (smMap[col, row] == userTarget) {
                        if(userTarget > 9) {
                            tClr.ChangeColorTxt("y", Convert.ToString(userTarget) + " ");
                        }
                        else {
                            tClr.ChangeColorTxt("y", "0" + Convert.ToString(userTarget) + " ");
                        }
                    }
                    else {
                        if (smMap[col, row] < 10 && smMap[col, row] >= 0) { Console.Write("0{0} ", smMap[col, row]); }
                        else { Console.Write("{0} ", smMap[col, row]); }
                    }
                }
                Console.WriteLine();
            }
        }

    }
    class CC {
        public void ChangeColor(string color) {
            switch (color) {
                case ("y"):
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case ("r"):
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case ("w"):
                case ("default"):
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }
        public void ChangeColorTxt(string color, string text) {
            switch (color) {
                case ("yellow"):
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(text);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case ("red"):
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(text);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case ("default"):
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(text);
                    break;
            }
        }
    }

}